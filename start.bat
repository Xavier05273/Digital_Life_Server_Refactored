@echo off
setlocal enabledelayedexpansion

cd /d "%~dp0"

REM Activate venv if it exists
if not exist ".venv\Scripts\activate.bat" (
    echo Error: .venv not found. Run setup.bat first. >> log_async.log 2>&1
    exit /b 1
)

call .venv\Scripts\activate.bat

set "CHAR=%~1"
if "%CHAR%"=="" set "CHAR=paimon"

echo ========================================== >> log_async.log 2>&1
echo   Digital Life Server - Starting >> log_async.log 2>&1
echo ========================================== >> log_async.log 2>&1
echo   Character : %CHAR% >> log_async.log 2>&1
echo   Listen    : 0.0.0.0:38438 >> log_async.log 2>&1
echo ========================================== >> log_async.log 2>&1
echo. >> log_async.log 2>&1

python SocketServer.py --character "%CHAR%" --stream true >> log_async.log 2>&1
