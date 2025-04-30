#!/bin/bash

# Colors for better readability
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
NC='\033[0m' # No Color

# Function to display usage information
show_usage() {
  echo -e "${YELLOW}Cogtive DevAssignment Environment Reset Script${NC}"
  echo
  echo "Usage: ./reset-environment.sh [component]"
  echo
  echo "Components:"
  echo "  backend   - Reset only the backend (.NET) environment"
  echo "  web       - Reset only the frontend (React) environment"
  echo "  mobile    - Reset only the mobile (Xamarin) environment"
  echo "  docker    - Reset Docker containers (if using Docker)"
  echo
  echo "If no component is specified, all components will be reset."
}

# Function to reset backend
reset_backend() {
  echo -e "${YELLOW}Cleaning Backend...${NC}"
  (cd backend && rm -rf bin/ obj/ *.db *.db-shm *.db-wal)
  echo -e "${GREEN}✓ Backend files cleaned${NC}"
  
  echo -e "${YELLOW}Restoring Backend packages...${NC}"
  (cd backend && dotnet restore)
  echo -e "${GREEN}✓ Backend packages restored${NC}"
}

# Function to reset frontend
reset_frontend() {
  echo -e "${YELLOW}Cleaning Frontend...${NC}"
  (cd web && rm -rf node_modules/ package-lock.json)
  echo -e "${GREEN}✓ Frontend files cleaned${NC}"
  
  echo -e "${YELLOW}Reinstalling Frontend dependencies...${NC}"
  (cd web && npm install)
  echo -e "${GREEN}✓ Frontend dependencies reinstalled${NC}"
}

# Function to reset mobile
reset_mobile() {
  echo -e "${YELLOW}Cleaning Mobile...${NC}"
  (cd mobile && rm -rf bin/ obj/)
  echo -e "${GREEN}✓ Mobile files cleaned${NC}"
  
  echo -e "${YELLOW}Restoring Mobile packages...${NC}"
  (cd mobile && dotnet restore)
  echo -e "${GREEN}✓ Mobile packages restored${NC}"
}

# Function to reset Docker containers
reset_docker() {
  if command -v docker-compose >/dev/null 2>&1; then
    echo -e "${YELLOW}Cleaning Docker environment...${NC}"
    docker-compose down -v
    echo -e "${GREEN}✓ Docker environment cleaned${NC}"
  else
    echo -e "${RED}❌ Docker Compose not installed. Skipping Docker reset.${NC}"
  fi
}

# Main logic
if [ "$1" = "--help" ] || [ "$1" = "-h" ]; then
  show_usage
  exit 0
fi

# Check if a specific component was specified
if [ $# -eq 0 ]; then
  echo -e "${YELLOW}Resetting all components...${NC}"
  reset_backend
  reset_frontend
  reset_mobile
  echo -e "${GREEN}✓ All components have been reset successfully!${NC}"
else
  case "$1" in
    backend)
      reset_backend
      ;;
    web)
      reset_frontend
      ;;
    mobile)
      reset_mobile
      ;;
    docker)
      reset_docker
      ;;
    *)
      echo -e "${RED}❌ Unknown component: $1${NC}"
      show_usage
      exit 1
      ;;
  esac
  echo -e "${GREEN}✓ Component '$1' has been reset successfully!${NC}"
fi

echo
echo -e "${YELLOW}To start the application, use:${NC} ./start-app.sh"
