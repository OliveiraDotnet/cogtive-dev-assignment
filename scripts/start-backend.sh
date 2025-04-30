#!/bin/bash
# Start the backend server after a reset if needed
# Usage: ./scripts/start-backend.sh [--reset]

# Set text colors
GREEN='\033[0;32m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Get the project root directory
PROJECT_ROOT="$( cd "$( dirname "${BASH_SOURCE[0]}" )/.." && pwd )"

cd "$PROJECT_ROOT/backend" || exit

# Check if reset flag is provided
if [ "$1" == "--reset" ]; then
  echo -e "${BLUE}Performing backend reset before starting...${NC}"
  
  # Delete database files
  rm -f *.db *.db-shm *.db-wal
  
  echo -e "${GREEN}Reset complete. Starting backend...${NC}"
fi

# Run the backend
echo -e "${BLUE}Starting backend server...${NC}"
dotnet run

exit 0
