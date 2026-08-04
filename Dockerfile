FROM python:3.11-slim AS base

ENV DEBIAN_FRONTEND=noninteractive \
    PYTHONDONTWRITEBYTECODE=1 \
    PYTHONUNBUFFERED=1

RUN apt-get update && apt-get install -y --no-install-recommends \
        build-essential git curl ffmpeg libsndfile1 \
        && rm -rf /var/lib/apt/lists/*

WORKDIR /app

# 複製整個專案（不含 .venv）
COPY requirements.txt ./requirements.txt
COPY GPT/ ./GPT/
COPY ASR/ ./ASR/
COPY TTS/ ./TTS/
COPY SentimentEngine/ ./SentimentEngine/
COPY utils/ ./utils/
COPY SocketServer.py run-lmstudio.sh start.sh ./.gitmodules* ./

# 安裝 Python 依賴 (PyTorch CPU)
RUN pip install --no-cache-dir \
        torch torchaudio --index-url https://download.pytorch.org/whl/cpu && \
    pip install --no-cache-dir -r requirements.txt

# 編譯 monotonic_align
WORKDIR /app/TTS/vits/monotonic_align
RUN python setup.py build_ext --inplace
WORKDIR /app

EXPOSE 38438

ENTRYPOINT ["python", "SocketServer.py"]
CMD ["--character", "paimon", "--stream", "true"]
