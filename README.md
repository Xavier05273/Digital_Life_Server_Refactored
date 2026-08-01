# Digital Life Server (Refactored)

A refactored, modernized backend for the "Digital Life" voice AI character server.  
Designed to run with local LLMs via LM Studio (OpenAI-compatible API), and connect to the original UE client (T.exe).

## Features

- OpenAI-compatible LLM integration (LM Studio / any compatible endpoint)
- Automatic CPU/CUDA device selection for TTS
- Clean startup scripts for Linux/WSL
- Docker support for portable deployment
- Compatible with existing UE client (T.exe) via 127.0.0.1:38438

## Quick Start (Linux / WSL)

### Prerequisites

- Python 3.10+ (recommended 3.11)
- Git
- LM Studio running on the same machine with Local Server enabled

### First-time setup

```bash
cd Digital_Life_Server_Package
chmod +x setup.sh start.sh
./setup.sh
```

This will:
- Create a Python virtual environment (.venv)
- Install all dependencies (PyTorch CPU, OpenAI SDK, ASR/TTS/Sentiment libs)
- Initialize TTS submodule
- Compile monotonic_align

### Add your models

Place model files in the corresponding directories:

```
Digital_Life_Server_Package/
├── ASR/resources/models/          # ASR model files
├── SentimentEngine/models/        # Sentiment analysis model
└── TTS/models/                    # TTS character models (paimon/yunfei/catmaid)
```

(Models are not included in this repository due to size.)

### Start LM Studio

1. Open LM Studio → Local Server
2. Click "Start server"
3. Enable "Allow connections from local network"
4. Load a model (e.g., gemma-4-e4b-abliterated)

### One-click start

```bash
./start.sh paimon      # Paimon character
./start.sh yunfei      # Yunfei character
./start.sh catmaid     # Catmaid character
```

The server listens on 0.0.0.0:38438 and waits for the UE client (T.exe) to connect.

### Connect with UE Client

- Configure T.exe to connect to: 127.0.0.1:38438
- Or use the container/host IP + 38438 if running via Docker

## Docker Mode (Optional)

If you have Docker installed, you can run everything in a container:

```bash
docker compose up --build
```

Edit docker-compose.yml to change character or LLM model.

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
