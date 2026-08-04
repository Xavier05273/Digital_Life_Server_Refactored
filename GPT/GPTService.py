import logging
import os
import time

from openai import OpenAI, DefaultHttpxClient

import GPT.tune as tune
from utils.config import load_config, get_llm_config


class GPTService:
    def __init__(self, args):
        logging.info('Initializing LLM Service (OpenAI-compatible)...')

        cfg = load_config()
        llm_cfg = get_llm_config(cfg, args)

        self.tune = tune.get_tune(args.character, getattr(args, 'model', None))
        self.counter = 0
        self.stream_sentences = getattr(args, 'stream', True) in ('true', 'True', '1', True)

        api_key = llm_cfg['api_key']
        base_url = llm_cfg['base_url']
        model = llm_cfg['model']

        # Optional proxy via httpx if needed
        proxy = getattr(args, 'proxy', None) or os.getenv('HTTP_PROXY') or os.getenv('HTTPS_PROXY')
        if proxy:
            client_kwargs = {
                "api_key": api_key,
                "base_url": base_url,
                "http_client": DefaultHttpxClient(proxies=proxy),
            }
        else:
            client_kwargs = {
                "api_key": api_key,
                "base_url": base_url,
            }

        self.client = OpenAI(**client_kwargs)
        self.model = model

        logging.info(
            f'LLM Service initialized. '
            f'model={self.model}, base_url={base_url}'
        )

    def _build_messages(self, user_text: str, force_system: bool = False):
        # For now we always send system prompt; can be tuned later.
        messages = [
            {"role": "system", "content": self.tune}
        ]
        messages.append({"role": "user", "content": user_text})
        return messages

    def ask(self, text: str) -> str:
        stime = time.time()
        try:
            messages = self._build_messages(text)
            resp = self.client.chat.completions.create(
                model=self.model,
                messages=messages,
                stream=False,
            )
            content = (resp.choices[0].message.content or "").strip()
            logging.info(
                f'LLM Response: {content}, time used {time.time() - stime:.2f}'
            )
            return content
        except Exception as e:
            logging.error(f'LLM ask error: {e}')
            raise

    def ask_stream(self, text: str):
        """
        Yields sentences suitable for TTS.
        Splits on Chinese punctuation and newlines to stream audio chunks.
        """
        stime = time.time()
        complete_text = ""

        # Periodically reinforce system prompt via brainwash-style behavior if needed.
        force_system = (self.counter % 5 == 0)
        self.counter += 1

        try:
            messages = self._build_messages(text, force_system=force_system)
            stream = self.client.chat.completions.create(
                model=self.model,
                messages=messages,
                stream=True,
            )

            for chunk in stream:
                delta = (chunk.choices[0].delta.content or "") if chunk.choices else ""
                if not delta:
                    continue
                complete_text += delta

                # Flush as a TTS chunk on sentence boundaries.
                if any(p in delta for p in ["。", "！", "？", "\n"]) and len(complete_text) > 3:
                    logging.info(
                        f'LLM Stream Response: {complete_text.strip()}, @Time {time.time() - stime:.2f}'
                    )
                    yield complete_text.strip()
                    complete_text = ""

            # Flush remaining.
            if complete_text.strip():
                logging.info(
                    f'LLM Stream Response: {complete_text.strip()}, @Time {time.time() - stime:.2f}'
                )
                yield complete_text.strip()

        except Exception as e:
            logging.error(f'LLM ask_stream error: {e}')
            raise
