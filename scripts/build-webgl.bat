@echo off
REM Executive Disorder - WebGL Build Script (Windows Batch)
REM Run from Command Prompt or PowerShell

echo ========================================
echo Executive Disorder - WebGL Build
echo ========================================
echo.

REM Configuration - UPDATE THESE PATHS
set UNITY_PATH=C:\Program Files\Unity\Hub\Editor\6000.0.40f1\Editor\Unity.exe
set PROJECT_PATH=C:\Users\POK28\source\repos\ExecutiveDisorderReplit\unity
set BUILD_PATH=C:\Users\POK28\source\repos\ExecutiveDisorderReplit\Builds\WebGL\%date:~-4,4%%date:~-10,2%%date:~-7,2%_%time:~0,2%%time:~3,2%%time:~6,2%
set LOG_PATH=C:\Users\POK28\source\repos\ExecutiveDisorderReplit\Builds\Logs\build_%date:~-4,4%%date:~-10,2%%date:~-7,2%_%time:~0,2%%time:~3,2%%time:~6,2%.log

REM Check Unity exists
echo Checking Unity installation...
if not exist "%UNITY_PATH%" (
    echo ERROR: Unity not found at: %UNITY_PATH%
    echo Please update UNITY_PATH in this script
    pause
    exit /b 1
)
echo Unity found: OK

REM Check project exists
echo Checking project...
if not exist "%PROJECT_PATH%" (
    echo ERROR: Project not found at: %PROJECT_PATH%
    pause
    exit /b 1
)
echo Project found: OK

REM Create directories
if not exist "%BUILD_PATH%" mkdir "%BUILD_PATH%"
if not exist "C:\Users\POK28\source\repos\ExecutiveDisorderReplit\Builds\Logs" mkdir "C:\Users\POK28\source\repos\ExecutiveDisorderReplit\Builds\Logs"

REM Build
echo.
echo Starting build (this may take 10-30 minutes)...
echo Build output: %BUILD_PATH%
echo Log file: %LOG_PATH%
echo.

"%UNITY_PATH%" -quit -batchmode -nographics -projectPath "%PROJECT_PATH%" -buildTarget WebGL -executeMethod BuildScript.BuildWebGL -logFile "%LOG_PATH%"

REM Check result
if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo BUILD SUCCESSFUL!
    echo ========================================
    echo Output: %BUILD_PATH%
    echo.
    echo To test locally, run:
    echo   cd "%BUILD_PATH%"
    echo   python -m http.server 8000
    echo   Then open: http://localhost:8000
    echo.
) else (
    echo.
    echo ========================================
    echo BUILD FAILED!
    echo ========================================
    echo Check log: %LOG_PATH%
    pause
    exit /b 1
)

pause
