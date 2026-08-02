Status:
Experimental / Personal maintained revival

This project is under active development.

# Digital Life Server (Refactored)

Fork from https://github.com/zixiiu/Digital_Life_Server

A refactored, modernized backend for the "Digital Life" voice AI character server.  
Designed to run with local LLMs via LM Studio (OpenAI-compatible API), and connect to the original UE client (T.exe).

## Features

- OpenAI-compatible LLM integration (LM Studio / any compatible endpoint)
- Automatic CPU/CUDA device selection for TTS
- Clean startup scripts for Linux/WSL
- Docker support for portable deployment
- Configurable via `config.yaml` — no hardcoded IPs or models
- Compatible with existing UE client (T.exe) via 127.0.0.1:38438

## Quick Start (Linux / WSL)

### Prerequisites

- Python 3.10+ (recommended 3.11)
- Git
- C++ compiler (build-essential python3-dev)
- LM Studio running on the same machine with Local Server enabled

### First-time setup

```bash
cd ~
git clone https://github.com/Xavier05273/Digital_Life_Server_Refactored.git
cd ./Digital_Life_Server_Refactored
chmod +x setup.sh start.sh
./setup.sh
```

This will:
- Create a Python virtual environment (.venv)
- Install all dependencies (PyTorch CPU, OpenAI SDK, ASR/TTS/Sentiment libs)
- Initialize TTS submodule
- Compile monotonic_align

### Configure your environment

Edit `config.yaml`:

- Set `llm.base_url` to your LLM endpoint:
  - For local LM Studio on the same machine: `http://127.0.0.1:1234/v1`
  - For WSL connecting to Windows LM Studio: use your host IP (e.g., `http://192.168.x.x:1234/v1`)
- Set `llm.model` to the model name you are using (must match what your LLM server provides)
- Optionally set `server.host` and `server.port` if needed

### Add your models

You can download models from [Google Drive](https://drive.google.com/file/d/1_jp826uLmK8qT6BCJ_iyaU6WB9RSljv0/view?usp=sharing) or [百度网盘](https://pan.baidu.com/s/1EnHDPADNdhDl71x_DHeElg?pwd=75gr)

Place model files in the corresponding directories:

```
Digital_Life_Server_Package/
├── ASR/resources/models/          # ASR model files
├── SentimentEngine/models/        # Sentiment analysis model
└── TTS/models/                    # TTS character models (paimon/yunfei/catmaid)
```

(Models are not included in this repository due to size.)

### Start LM Studio(Or other LLM provider)

1. Open LM Studio → Local Server
2. Click "Start server"
3. Enable "Allow connections from local network" if needed
4. Load a model (e.g., gemma-4-e4b-abliterated)

### One-click start

```bash
./start.sh paimon      # Paimon character
./start.sh yunfei      # Yunfei character (Untested)
./start.sh catmaid     # Catmaid character (Untested)
```

The server listens on the address configured in `config.yaml` (default: 0.0.0.0:38438) and waits for the UE client (T.exe) to connect.

### Connect with UE Client

Download [T.exe](https://drive.google.com/drive/folders/1FWgK3M2Mh2gyF9gVj6v2vmvTcCn27UM9?usp=sharing)

- Configure T.exe to connect to: 127.0.0.1:38438
- Or use the container/host IP + 38438 if running via Docker

## Docker Mode (Optional) (Untested)

If you have Docker installed, you can run everything in a container:

```bash
docker compose up --build
```

Edit `docker-compose.yml` to change character or LLM model.  
For custom endpoints, override environment variables:

- OPENAI_BASE_URL
- LLM_MODEL

## Architecture

- ASR: Speech-to-text using Paraformer-based pipeline
- GPT/LLM: OpenAI-compatible API client (for LM Studio or similar)
- TTS: VITS-based voice synthesis with per-character models
- SentimentEngine: ONNX-based sentiment inference for expressive responses
- SocketServer.py: Core server handling UE client communication

## Notes

- This is a refactored version of the original Digital_Life_Server.
- Legacy ChatGPT/revChatGPT dependencies have been removed.
- Designed to be portable and easy to deploy on any machine with Docker or Python 3.10+.