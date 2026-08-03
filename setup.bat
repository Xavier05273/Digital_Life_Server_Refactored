@echo off
setlocal enabledelayedexpansion

cd /d "%~dp0"

echo ==========================================
echo   Digital Life Server - First Time Setup
echo ==========================================
echo.

REM 1. Ask CPU or CUDA
set "PYTORCH_CHOICE="
:ask_pytorch
set /p "PYTORCH_CHOICE=Install PyTorch version? [cpu/cuda] (default: cpu): "
if "!PYTORCH_CHOICE!"=="" set "PYTORCH_CHOICE=cpu"

if /i not "!PYTORCH_CHOIVE!"=="cpu" if /i not "!PYTORCH_CHOICE!"=="cuda" (
    echo Invalid choice. Please enter 'cpu' or 'cuda'.
    goto ask_pytorch
)

echo Selected: !PYTORCH_CHOICE!
echo.

REM 2. Create venv if not exists
if not exist ".venv\Scripts\activate.bat" (
    echo [1/4] Creating Python virtual environment...
    python -m venv .venv
) else (
    echo [1/4] Virtual environment already exists.
)

call .venv\Scripts\activate.bat

REM 3. Install PyTorch + requirements
echo [2/4] Installing Python packages (this may take a while)...

if /i "!PYTORCH_CHOICE!"=="cpu" (
    pip install --quiet torch torchaudio --index-url https://download.pytorch.org/whl/cpu
) else if /i "!PYTORCH_CHOICE!"=="cuda" (
    pip install --quiet torch torchaudio --index-url https://download.pytorch.org/whl/cu124
)

pip install --quiet -r requirements.txt

REM 4. Init git submodule (vits TTS)
echo [3/4] Initializing TTS submodule...

if not exist "TTS\vits" (
    if not exist ".git" (
        git init >nul 2>&1 || (
            echo Error: Git is required but not found. Please install Git and try again.
            pause
            exit /b 1
        )
        git config user.email "setup@digital-life"
        git config user.name "Setup"
        git add -A >nul 2>&1 && git commit -m "init for packaging" >nul 2>&1 || true
    )
    git submodule update --init --recursive
) else (
    echo TTS/vits already exists.
)

REM 5. Compile monotonic_align
echo [4/4] Compiling monotonic_align...

cd TTS\vits\monotonic_align

if exist "build" rmdir /s /q build >nul 2>&1 || true
for %%f in (*.so *.pyd) do if exist "%%f" del "%%f" >nul 2>&1 || true

python setup.py build_ext --inplace >nul 2>&1

REM Ensure .so/.pyd is in the correct location (same dir as __init__.py)
set "FOUND=0"
for %%f in (core.*.pyd core.*.so) do if exist "%%f" set "FOUND=1"

if "!FOUND!"=="0" (
    for /r %%f in (core.*.pyd core.*.so) do (
        move "%%f" . >nul 2>&1 || true
    )
)

cd ..\..\..

echo.
echo ==========================================
echo   Setup complete!
echo ==========================================
echo.
echo Next steps:
echo   1. Place your models in the correct folders (see README)
echo   2. Start LM Studio with a model loaded
echo   3. Run: start.bat paimon
echo.

pause
