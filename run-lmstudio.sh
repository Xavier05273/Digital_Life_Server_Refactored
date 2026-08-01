#!/usr/bin/env bash
# run-lmstudio.sh
# 用法: ./run-lmstudio.sh [character]
# character: paimon / yunfei / catmaid (預設 paimon)

set -e

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
cd "$SCRIPT_DIR"

CHAR="${1:-paimon}"

# 啟動虛擬環境
if [ ! -d ".venv/bin" ]; then
    echo "找不到 .venv，先建立..."
    python3 -m venv .venv
fi
source .venv/bin/activate

# LM Studio 預設端點 (WSL -> Windows host)
export OPENAI_BASE_URL="${OPENAI_BASE_URL:-http://192.168.18.3:1234/v1}"
export LLM_MODEL="${LLM_MODEL:-gemma-4-e4b-abliterated}"

echo "=== Digital Life Server (LM Studio mode) ==="
echo "Character : $CHAR"
echo "Base URL  : $OPENAI_BASE_URL"
echo "Model     : $LLM_MODEL"
echo "============================================="

python SocketServer.py \
    --character "$CHAR" \
    --stream true
