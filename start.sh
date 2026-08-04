#!/usr/bin/env bash
set -e

# 取得腳本所在目錄（兼容 WSL / Linux）
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

# Activate venv
if [ ! -f ".venv/bin/activate" ]; then
    echo "Error: .venv not found. Run ./setup.sh first."
    exit 1
fi
source .venv/bin/activate

CHAR="${1:-paimon}"

echo ""
echo "=========================================="
echo "  Digital Life Server - Starting"
echo "=========================================="
echo "  Character : $CHAR"
echo "  Listen    : 0.0.0.0:38438"
echo "=========================================="
echo ""

# Check LM Studio connectivity (non-blocking)
if ! curl -s --max-time 2 http://192.168.18.3:1234/v1/models >/dev/null 2>&1; then
    echo "[!] Warning: Cannot connect to LM Studio at http://192.168.18.3:1234"
    echo "    Please ensure:"
    echo "      - LM Studio is running with Local Server started"
    echo "      - 'Allow connections from local network' is enabled"
    echo ""
fi

# Start server
python SocketServer.py \
    --character "$CHAR" \
    --stream true
