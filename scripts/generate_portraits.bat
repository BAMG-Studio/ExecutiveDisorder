@echo off
echo ========================================
echo Executive Disorder Portrait Generator
echo ========================================
echo.

REM Check if Python is installed
python --version >nul 2>&1
if errorlevel 1 (
    echo ERROR: Python is not installed or not in PATH
    echo Please install Python 3.x from python.org
    pause
    exit /b 1
)

REM Install required packages
echo Installing required packages...
pip install requests >nul 2>&1

REM Run the generator
echo.
echo Starting portrait generation...
echo.
python generate_character_portraits.py

echo.
echo ========================================
echo Generation complete!
echo ========================================
pause
