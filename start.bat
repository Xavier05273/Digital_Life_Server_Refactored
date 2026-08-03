@echo off
setlocal enabledelayedexpansion

cd /d "%~dp0"

REM Activate venv if it exists
if not exist ".venv\Scripts\activate.bat" (
    echo Error: .venv not found. Run setup.bat first.
    pause
    exit /b 1
)

call .venv\Scripts\activate.bat

set "CHAR=%~1"
if "%CHAR%"=="" set "CHAR=paimon"

echo ==========================================
echo   Digital Life Server - Starting
echo ==========================================
echo   Character : %CHAR%
echo   Listen    : 0.0.0.0:38438
echo ==========================================
echo.

python SocketServer.py --character "%CHAR%" --stream true

pause
