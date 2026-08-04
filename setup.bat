@echo off
setlocal enabledelayedexpansion

cd /d "%~dp0"
set "ROOT=%CD%"
set "PYTHON=!ROOT!\.venv\Scripts\python.exe"

echo ==========================================
echo   Digital Life Server - First Time Setup
echo ==========================================
echo.

REM 1. Ask CPU or CUDA
set "PYTORCH_CHOICE="
:ask_pytorch
set /p "PYTORCH_CHOICE=Install PyTorch version? [cpu/cuda] (default: cpu): "
if "!PYTORCH_CHOICE!"=="" set "PYTORCH_CHOICE=cpu"

if /i not "!PYTORCH_CHOICE!"=="cpu" if /i not "!PYTORCH_CHOICE!"=="cuda" (
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

REM 3. Install PyTorch + requirements (with retry and longer timeout)
echo [2/4] Installing Python packages (this may take a while)...

set "PYTORCH_OK=0"
for %%i in (1 2) do (
    if "!PYTORCH_OK!"=="1" goto pytorch_done

    echo Attempt %%i of 2: installing PyTorch...

    if /i "!PYTORCH_CHOICE!"=="cpu" (
        !PYTHON! -m pip install --timeout 180 --quiet torch torchaudio --index-url https://download.pytorch.org/whl/cpu && set "PYTORCH_OK=1"
    ) else if /i "!PYTORCH_CHOICE!"=="cuda" (
        !PYTHON! -m pip install --timeout 180 --quiet torch torchaudio --index-url https://download.pytorch.org/whl/cu124 && set "PYTORCH_OK=1"
    )

    if "!PYTORCH_OK!"=="0" (
        echo PyTorch installation failed or timed out. Retrying once...
    )
)

:pytorch_done
if "!PYTORCH_OK!"=="0" (
    echo Error: Failed to install PyTorch after 2 attempts. Please check your internet connection and try again.
    pause
    exit /b 1
)

!PYTHON! -m pip install --timeout 180 --quiet -r requirements.txt

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

REM 5. Compile monotonic_align (use venv python directly to avoid PATH issues)
echo [4/4] Compiling monotonic_align...

cd /d "!ROOT!\TTS\vits\monotonic_align"

if exist "build" rmdir /s /q build >nul 2>&1 || true
for %%f in (*.so *.pyd) do if exist "%%f" del "%%f" >nul 2>&1 || true

!PYTHON! setup.py build_ext --inplace

REM Ensure .so/.pyd is in the correct location (same dir as __init__.py)
set "FOUND=0"
for %%f in (core.*.pyd core.*.so) do if exist "%%f" set "FOUND=1"

if "!FOUND!"=="0" (
    for /r %%f in (core.*.pyd core.*.so) do (
        move "%%f" . >nul 2>&1 || true
    )
)

cd /d "!ROOT!"

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
