#!/usr/bin/env bash
set -e

cd "$(dirname "$0")"

echo "=========================================="
echo "  Digital Life Server - First Time Setup"
echo "=========================================="

# 1. Create venv if not exists
if [ ! -d ".venv/bin/activate" ]; then
    echo "[1/4] Creating Python virtual environment..."
    python3 -m venv .venv
fi
source .venv/bin/activate

# 2. Install dependencies (PyTorch CPU + requirements)
echo "[2/4] Installing Python packages (this may take a while)..."
pip install --quiet \
    torch torchaudio --index-url https://download.pytorch.org/whl/cpu && \
pip install --quiet -r requirements.txt

# 3. Init git submodule (vits TTS)
echo "[3/4] Initializing TTS submodule..."
if [ ! -d "TTS/vits" ]; then
    git init >/dev/null 2>&1 || true
    git config user.email "setup@digital-life"
    git config user.name "Setup"
    git add -A >/dev/null 2>&1 && git commit -m "init for packaging" >/dev/null 2>&1 || true
    git submodule update --init --recursive
fi

# 4. Compile monotonic_align (clean old builds first)
echo "[4/4] Compiling monotonic_align..."
cd TTS/vits/monotonic_align
rm -rf build *.so monotonic_align/__init__.py >/dev/null 2>&1 || true
python setup.py build_ext --inplace >/dev/null 2>&1
# Ensure .so is in the correct location (same dir as __init__.py)
if [ ! -f "core.cpython-*.so" ]; then
    # If setuptools put it in a subfolder, move it up
    find . -name "core.cpython-*.so" -exec mv {} ./ \; 2>/dev/null || true
fi
cd ../../../..

echo ""
echo "=========================================="
echo "  Setup complete!"
echo "=========================================="
echo ""
echo "Next steps:"
echo "  1. Place your models in the correct folders (see README)"
echo "  2. Start LM Studio with a model loaded"
echo "  3. Run: ./start.sh paimon"
echo ""
