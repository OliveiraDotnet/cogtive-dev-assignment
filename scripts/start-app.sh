#!/bin/bash

# Colors for better readability
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
NC='\033[0m' # No Color

# Function to display usage information
show_usage() {
  echo -e "${YELLOW}Cogtive DevAssignment Application Start Script${NC}"
  echo
  echo "Usage: ./start-app.sh [options]"
  echo
  echo "Options:"
  echo "  --reset    - Reset the environment before starting (calls reset-environment.sh)"
  echo "  --docker   - Start using Docker Compose"
  echo "  --backend  - Start only the backend"
  echo "  --web      - Start only the web frontend"
  echo
  echo "If no options are specified, both backend and web frontend will be started."
}

# Function to start backend
start_backend() {
  echo -e "${YELLOW}Starting Backend API...${NC}"
  (cd backend && dotnet run) &
  BACKEND_PID=$!
  echo -e "${GREEN}✓ Backend API started${NC}"
}

# Function to start frontend
start_frontend() {
  echo -e "${YELLOW}Starting Web Frontend...${NC}"
  (cd web && npm start) &
  FRONTEND_PID=$!
  echo -e "${GREEN}✓ Web Frontend started${NC}"
}

# Function to start docker-compose environment
start_docker() {
  if command -v docker-compose >/dev/null 2>&1; then
    echo -e "${YELLOW}Starting Docker environment...${NC}"
    docker-compose up --build -d
    echo -e "${GREEN}✓ Docker environment started${NC}"
    
    echo
    echo -e "${YELLOW}Services are running:${NC}"
    echo "  - Backend API: http://localhost:5000"
    echo "  - Web Frontend: http://localhost:3000"
  else
    echo -e "${RED}❌ Docker Compose not installed. Cannot start Docker environment.${NC}"
  fi
}

# Parse command-line arguments
RESET=false
USE_DOCKER=false
START_BACKEND=false
START_WEB=false

# If no arguments, start both backend and web in development mode
if [ $# -eq 0 ]; then
  START_BACKEND=true
  START_WEB=true
else
  for arg in "$@"; do
    case "$arg" in
      --reset)
        RESET=true
        ;;
      --docker)
        USE_DOCKER=true
        ;;
      --backend)
        START_BACKEND=true
        ;;
      --web)
        START_WEB=true
        ;;
      --help|-h)
        show_usage
        exit 0
        ;;
      *)
        echo -e "${RED}❌ Unknown option: $arg${NC}"
        show_usage
        exit 1
        ;;
    esac
  done
fi

# Reset environment if requested
if [ "$RESET" = true ]; then
  echo -e "${YELLOW}Resetting environment before starting...${NC}"
  ./scripts/reset-environment.sh
  echo
fi

# Start services
if [ "$USE_DOCKER" = true ]; then
  start_docker
else
  # Start local services
  if [ "$START_BACKEND" = true ]; then
    start_backend
  fi
  
  if [ "$START_WEB" = true ]; then
    start_frontend
  fi
  
  if [ "$START_BACKEND" = true ] || [ "$START_WEB" = true ]; then
    echo
    echo -e "${YELLOW}Services started in development mode:${NC}"
    [ "$START_BACKEND" = true ] && echo "  - Backend API: http://localhost:5211 (check console for actual URL)"
    [ "$START_WEB" = true ] && echo "  - Web Frontend: http://localhost:3000"
    echo
    echo -e "${YELLOW}Press Ctrl+C to stop all services${NC}"
    
    # Wait for Ctrl+C
    trap "echo -e '\n${RED}Stopping all services...${NC}'" INT
    wait
  fi
fi
