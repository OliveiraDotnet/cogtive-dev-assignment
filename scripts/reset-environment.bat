@echo off
setlocal enabledelayedexpansion

:: Colors for Windows console
set GREEN=[92m
set YELLOW=[93m
set RED=[91m
set NC=[0m

:: Function to display usage information
:show_usage
echo %YELLOW%Cogtive DevAssignment Environment Reset Script%NC%
echo.
echo Usage: reset-environment.bat [component]
echo.
echo Components:
echo   backend   - Reset only the backend (.NET) environment
echo   web       - Reset only the frontend (React) environment
echo   mobile    - Reset only the mobile (Xamarin) environment
echo   docker    - Reset Docker containers (if using Docker)
echo.
echo If no component is specified, all components will be reset.
exit /b 0

:: Function to reset backend
:reset_backend
echo %YELLOW%Cleaning Backend...%NC%
cd backend
if exist bin rmdir /s /q bin
if exist obj rmdir /s /q obj
if exist *.db del *.db
if exist *.db-shm del *.db-shm
if exist *.db-wal del *.db-wal
echo %GREEN%^✓ Backend files cleaned%NC%
  
echo %YELLOW%Restoring Backend packages...%NC%
dotnet restore
echo %GREEN%^✓ Backend packages restored%NC%
cd ..
exit /b 0

:: Function to reset frontend
:reset_frontend
echo %YELLOW%Cleaning Frontend...%NC%
cd web
if exist node_modules rmdir /s /q node_modules
if exist package-lock.json del package-lock.json
echo %GREEN%^✓ Frontend files cleaned%NC%
  
echo %YELLOW%Reinstalling Frontend dependencies...%NC%
call npm install
echo %GREEN%^✓ Frontend dependencies reinstalled%NC%
cd ..
exit /b 0

:: Function to reset mobile
:reset_mobile
echo %YELLOW%Cleaning Mobile...%NC%
cd mobile
if exist bin rmdir /s /q bin
if exist obj rmdir /s /q obj
echo %GREEN%^✓ Mobile files cleaned%NC%
  
echo %YELLOW%Restoring Mobile packages...%NC%
dotnet restore
echo %GREEN%^✓ Mobile packages restored%NC%
cd ..
exit /b 0

:: Function to reset Docker containers
:reset_docker
where docker-compose >nul 2>&1
if %ERRORLEVEL% equ 0 (
  echo %YELLOW%Cleaning Docker environment...%NC%
  docker-compose down -v
  echo %GREEN%^✓ Docker environment cleaned%NC%
) else (
  echo %RED%^❌ Docker Compose not installed. Skipping Docker reset.%NC%
)
exit /b 0

:: Main logic
if "%1"=="--help" goto show_usage
if "%1"=="-h" goto show_usage

:: Check if a specific component was specified
if "%1"=="" (
  echo %YELLOW%Resetting all components...%NC%
  call :reset_backend
  call :reset_frontend
  call :reset_mobile
  echo %GREEN%^✓ All components have been reset successfully!%NC%
) else (
  if "%1"=="backend" (
    call :reset_backend
  ) else if "%1"=="web" (
    call :reset_frontend
  ) else if "%1"=="mobile" (
    call :reset_mobile
  ) else if "%1"=="docker" (
    call :reset_docker
  ) else (
    echo %RED%^❌ Unknown component: %1%NC%
    goto show_usage
    exit /b 1
  )
  echo %GREEN%^✓ Component '%1' has been reset successfully!%NC%
)

echo.
echo %YELLOW%To start the application, use:%NC% start-app.bat
exit /b 0
