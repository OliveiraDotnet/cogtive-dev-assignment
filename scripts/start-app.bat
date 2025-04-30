@echo off
setlocal enabledelayedexpansion

:: Colors for Windows console
set GREEN=[92m
set YELLOW=[93m
set RED=[91m
set NC=[0m

:: Function to display usage information
:show_usage
echo %YELLOW%Cogtive DevAssignment Application Start Script%NC%
echo.
echo Usage: start-app.bat [options]
echo.
echo Options:
echo   --reset    - Reset the environment before starting (calls reset-environment.bat)
echo   --docker   - Start using Docker Compose
echo   --backend  - Start only the backend
echo   --web      - Start only the web frontend
echo.
echo If no options are specified, both backend and web frontend will be started.
exit /b 0

:: Function to start backend
:start_backend
echo %YELLOW%Starting Backend API...%NC%
start "Cogtive Backend" cmd /c "cd backend && dotnet run"
echo %GREEN%^✓ Backend API started%NC%
exit /b 0

:: Function to start frontend
:start_frontend
echo %YELLOW%Starting Web Frontend...%NC%
start "Cogtive Frontend" cmd /c "cd web && npm start"
echo %GREEN%^✓ Web Frontend started%NC%
exit /b 0

:: Function to start docker-compose environment
:start_docker
where docker-compose >nul 2>&1
if %ERRORLEVEL% equ 0 (
  echo %YELLOW%Starting Docker environment...%NC%
  docker-compose up --build -d
  echo %GREEN%^✓ Docker environment started%NC%
  
  echo.
  echo %YELLOW%Services are running:%NC%
  echo   - Backend API: http://localhost:5000
  echo   - Web Frontend: http://localhost:3000
) else (
  echo %RED%^❌ Docker Compose not installed. Cannot start Docker environment.%NC%
)
exit /b 0

:: Parse command-line arguments
set RESET=false
set USE_DOCKER=false
set START_BACKEND=false
set START_WEB=false

:: If no arguments, start both backend and web in development mode
if "%1"=="" (
  set START_BACKEND=true
  set START_WEB=true
) else (
  :parse_args
  if "%1"=="" goto continue_execution
  
  if "%1"=="--reset" (
    set RESET=true
  ) else if "%1"=="--docker" (
    set USE_DOCKER=true
  ) else if "%1"=="--backend" (
    set START_BACKEND=true
  ) else if "%1"=="--web" (
    set START_WEB=true
  ) else if "%1"=="--help" (
    goto show_usage
  ) else if "%1"=="-h" (
    goto show_usage
  ) else (
    echo %RED%^❌ Unknown option: %1%NC%
    goto show_usage
    exit /b 1
  )
  
  shift
  goto parse_args
)

:continue_execution
:: Reset environment if requested
if "%RESET%"=="true" (
  echo %YELLOW%Resetting environment before starting...%NC%
  call scripts\reset-environment.bat
  echo.
)

:: Start services
if "%USE_DOCKER%"=="true" (
  call :start_docker
) else (
  :: Start local services
  if "%START_BACKEND%"=="true" (
    call :start_backend
  )
  
  if "%START_WEB%"=="true" (
    call :start_frontend
  )
  
  if "%START_BACKEND%"=="true" if "%START_WEB%"=="true" (
    echo.
    echo %YELLOW%Services started in development mode:%NC%
    echo   - Backend API: http://localhost:5211 (check console for actual URL)
    echo   - Web Frontend: http://localhost:3000
    echo.
    echo %YELLOW%Close the terminal windows to stop the services%NC%
  )
)
exit /b 0
