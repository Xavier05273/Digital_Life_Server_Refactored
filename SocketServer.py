import argparse
import os
import socket
import time
import logging
import traceback
from logging.handlers import TimedRotatingFileHandler

import librosa
import requests
import soundfile

import GPT.tune
from utils.FlushingFileHandler import FlushingFileHandler
from utils.config import load_config, get_server_config, get_character_config
from ASR import ASRService
from GPT import GPTService
from TTS import TTService
from SentimentEngine import SentimentEngine


import sys

# Force UTF-8 for stdout/stderr on Windows to avoid UnicodeEncodeError with Chinese text
if sys.platform == 'win32':
    if hasattr(sys.stdout, 'reconfigure'):
        sys.stdout.reconfigure(encoding='utf-8')
    if hasattr(sys.stderr, 'reconfigure'):
        sys.stderr.reconfigure(encoding='utf-8')

console_logger = logging.getLogger()
console_logger.setLevel(logging.INFO)
FORMAT = '%(asctime)s %(levelname)s %(message)s'

# Remove default handler and create new ones with UTF-8 support
for h in console_logger.handlers[:]:
    console_logger.removeHandler(h)

file_handler = FlushingFileHandler("log.log", formatter=logging.Formatter(FORMAT))
file_handler.setFormatter(logging.Formatter(FORMAT))
file_handler.setLevel(logging.INFO)
console_logger.addHandler(file_handler)

# Console handler with UTF-8 encoding (critical for Windows)
try:
    console_handler = logging.StreamHandler(sys.stdout)
except Exception:
    console_handler = logging.StreamHandler()
console_handler.setFormatter(logging.Formatter(FORMAT))
console_handler.setLevel(logging.INFO)
console_logger.addHandler(console_handler)


def str2bool(v):
    if isinstance(v, bool):
        return v
    if v.lower() in ('yes', 'true', 't', 'y', '1'):
        return True
    elif v.lower() in ('no', 'false', 'f', 'n', '0'):
        return False
    else:
        raise argparse.ArgumentTypeError('Unsupported value encountered.')


def parse_args():
    parser = argparse.ArgumentParser(
        description="Digital Life Server (refactored for OpenAI-compatible API)"
    )

    # Config file path
    parser.add_argument("--config", type=str, required=False, default="config.yaml",
                        help="Path to config.yaml (default: ./config.yaml)")

    # LLM / Chat settings
    parser.add_argument("--APIKey", type=str, required=False,
                        help="OpenAI-compatible API key (overrides config/env)")
    parser.add_argument("--base_url", type=str, required=False,
                        help="LLM API base URL (overrides config/env)")
    parser.add_argument("--model", type=str, required=False,
                        help="Model name to use (e.g. gpt-4o-mini). Overrides config/env.")

    # Behavior
    parser.add_argument("--stream", type=str2bool, nargs='?', const=True, default=None,
                        help="Enable streaming responses for TTS chunks.")
    parser.add_argument("--character", type=str, required=False,
                        help="Character preset: paimon / yunfei / catmaid (or from config)")
    parser.add_argument("--brainwash", type=str2bool, nargs='?', const=False, default=None,
                        help="(Reserved) Periodically reinforce system prompt.")

    # Network
    parser.add_argument("--ip", type=str, required=False,
                        help="Bind IP (overrides config/env)")
    parser.add_argument("--port", type=int, required=False,
                        help="Listen port (overrides config/env)")

    # Proxy (optional)
    parser.add_argument("--proxy", type=str, required=False,
                        help="HTTP/HTTPS proxy for LLM requests.")

    return parser.parse_args()


class Server():
    def __init__(self, args):
        # Load configuration from file
        cfg = load_config(args.config)

        # SERVER STUFF
        self.addr = None
        self.conn = None
        logging.info('Initializing Server...')

        # Resolve server bind config: CLI > env > config.yaml > default
        srv_cfg = get_server_config(cfg, args)
        bind_ip = (args.ip or srv_cfg["host"])
        port = int(args.port if args.port is not None else srv_cfg["port"])

        self.s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        self.s.setsockopt(socket.SOL_SOCKET, socket.SO_SNDBUF, 10240000)
        self.s.bind((bind_ip, port))

        self.tmp_recv_file = 'tmp/server_received.wav'
        self.tmp_proc_file = 'tmp/server_processed.wav'
        os.makedirs('tmp', exist_ok=True)

        # Character selection: CLI > config default (first enabled)
        char_name_input = (args.character or "paimon").lower()

        # Build character map from config.yaml + fallback defaults
        self.char_name = {
            'paimon': ['TTS/models/paimon6k.json', 'TTS/models/paimon6k_390k.pth', 'character_paimon', 1],
            'yunfei': ['TTS/models/yunfeimix2.json', 'TTS/models/yunfeimix2_53k.pth', 'character_yunfei', 1.1],
            'catmaid': ['TTS/models/catmix.json', 'TTS/models/catmix_107k.pth', 'character_catmaid', 1.2]
        }

        # Override with config.yaml character settings if present
        char_cfg = get_character_config(cfg, char_name_input)
        if char_cfg.get("tts_model_path"):
            self.char_name[char_name_input][1] = char_cfg["tts_model_path"]

        if char_name_input not in self.char_name:
            raise ValueError(f"Unknown character: {char_name_input}. Choose from {list(self.char_name.keys())}")

        # Remember which character we're using (for char_tag to client)
        self.current_char_key = char_name_input

        # PARAFORMER ASR
        self.paraformer = ASRService.ASRService('./ASR/resources/config.yaml')

        # LLM (OpenAI-compatible / LM Studio) - GPTService reads config internally
        self.chat_gpt = GPTService.GPTService(args)

        # TTS
        logging.info(f'Initializing TTS Service for character_{char_name_input}...')
        self.tts = TTService.TTService(*self.char_name[char_name_input])

        # Sentiment Engine (default model; can be extended via config later)
        self.sentiment = SentimentEngine.SentimentEngine('SentimentEngine/models/paimon_sentiment.onnx')

    def listen(self):
        # MAIN SERVER LOOP
        while True:
            self.s.listen()
            logging.info(f"Server is listening on {self.s.getsockname()}...")
            self.conn, self.addr = self.s.accept()
            logging.info(f"Connected by {self.addr}")

            char_tag = self.char_name[self.current_char_key][2]
            self.conn.sendall(b'%s' % char_tag.encode())

            while True:
                try:
                    file_data = self.__receive_file()
                    with open(self.tmp_recv_file, 'wb') as f:
                        f.write(file_data)
                    logging.info('WAV file received and saved.')

                    ask_text = self.process_voice()

                    if args.stream is not None:
                        use_stream = args.stream
                    else:
                        # Default to True unless explicitly disabled via env/config later.
                        use_stream = True

                    if use_stream:
                        for sentence in self.chat_gpt.ask_stream(ask_text):
                            self.send_voice(sentence)
                        self.notice_stream_end()
                        logging.info('Stream finished.')
                    else:
                        resp_text = self.chat_gpt.ask(ask_text)
                        self.send_voice(resp_text)
                        self.notice_stream_end()

                except requests.exceptions.RequestException as e:
                    logging.error(e.__str__())
                    reply = GPT.tune.error_reply or "網路有點問題，稍後再試。"
                    logging.info(f'Network error, sending: {reply}')
                    self.send_voice(reply)
                    self.notice_stream_end()

                except Exception as e:
                    logging.error(e.__str__())
                    logging.error(traceback.format_exc())
                    # On unexpected errors, break inner loop and restart connection.
                    break

    def notice_stream_end(self):
        time.sleep(0.5)
        try:
            self.conn.sendall(b'stream_finished')
        except Exception as e:
            logging.error(f'Error sending stream_finished: {e}')

    def send_voice(self, resp_text, senti_or=None):
        if not resp_text or not resp_text.strip():
            return
        self.tts.read_save(resp_text, self.tmp_proc_file, self.tts.hps.data.sampling_rate)
        with open(self.tmp_proc_file, 'rb') as f:
            senddata = f.read()

        if senti_or is not None:
            senti = senti_or
        else:
            try:
                senti = self.sentiment.infer(resp_text)
            except Exception as e:
                logging.warning(f'Sentiment inference failed, using default 0: {e}')
                senti = 0

        senddata += b'?!'
        senddata += b'%i' % int(senti)
        self.conn.sendall(senddata)
        time.sleep(0.5)
        logging.info('WAV SENT, size %i' % len(senddata))

    def __receive_file(self):
        file_data = b''
        while True:
            data = self.conn.recv(1024)
            self.conn.send(b'sb')
            if not data:
                continue
            if data[-2:] == b'?!':
                file_data += data[0:-2]
                break
            file_data += data
        return file_data

    def fill_size_wav(self):
        with open(self.tmp_recv_file, "r+b") as f:
            size = os.path.getsize(self.tmp_recv_file) - 8
            f.seek(4)
            f.write(size.to_bytes(4, byteorder='little'))
            f.seek(40)
            f.write((size - 28).to_bytes(4, byteorder='little'))
            f.flush()

    def process_voice(self):
        # stereo to mono + resample
        self.fill_size_wav()
        y, sr = librosa.load(self.tmp_recv_file, sr=None, mono=False)
        y_mono = librosa.to_mono(y)
        y_mono = librosa.resample(y_mono, orig_sr=sr, target_sr=16000)
        soundfile.write(self.tmp_recv_file, y_mono, 16000)
        text = self.paraformer.infer(self.tmp_recv_file)
        return text


if __name__ == '__main__':
    try:
        args = parse_args()

        # If character not provided via CLI, default to 'paimon' (can be overridden in config later).
        if not args.character:
            args.character = "paimon"

        s = Server(args)
        s.listen()
    except Exception as e:
        logging.error(e.__str__())
        logging.error(traceback.format_exc())
        raise e
