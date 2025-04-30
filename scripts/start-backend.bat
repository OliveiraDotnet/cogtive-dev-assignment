@echo off
:: Start the backend server after a reset if needed
:: Usage: scripts\start-backend.bat [--reset]

setlocal enabledelayedexpansion

:: Set text colors
set "GREEN=[92m"
set "BLUE=[94m"
set "NC=[0m"

:: Get the project root directory (parent directory of this script)
set "PROJECT_ROOT=%~dp0.."

cd "%PROJECT_ROOT%\backend" || exit /b

:: Check if reset flag is provided
if "%1"=="--reset" (
  echo %BLUE%Performing backend reset before starting...%NC%
  
  :: Delete database files
  del /f /q *.db *.db-shm *.db-wal 2>nul
  
  echo %GREEN%Reset complete. Starting backend...%NC%
)

:: Run the backend
echo %BLUE%Starting backend server...%NC%
dotnet run

exit /b 0
