#!/bin/bash

# Executive Disorder - WebGL Build Script (Command Line Only)
# Run this from WSL terminal - NO Unity Editor UI needed!

# Color codes for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

echo -e "${GREEN}🚀 Executive Disorder - Automated WebGL Build${NC}"
echo "================================================"

# Configuration
UNITY_PATH="/mnt/c/Program Files/Unity/Hub/Editor/6000.0.40f1/Editor/Unity.exe"
PROJECT_PATH="/mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/unity"
BUILD_PATH="/mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/Builds/WebGL/$(date +%Y%m%d_%H%M%S)"
LOG_PATH="/mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/Builds/Logs/build_$(date +%Y%m%d_%H%M%S).log"

# Check Unity installation
echo -e "${YELLOW}Checking Unity installation...${NC}"
if [ ! -f "$UNITY_PATH" ]; then
    echo -e "${RED}❌ Unity not found at: $UNITY_PATH${NC}"
    echo "Please update UNITY_PATH in this script"
    exit 1
fi
echo -e "${GREEN}✅ Unity found${NC}"

# Check project exists
echo -e "${YELLOW}Checking project...${NC}"
if [ ! -d "$PROJECT_PATH" ]; then
    echo -e "${RED}❌ Project not found at: $PROJECT_PATH${NC}"
    exit 1
fi
echo -e "${GREEN}✅ Project found${NC}"

# Create build directories
mkdir -p "$BUILD_PATH"
mkdir -p "$(dirname "$LOG_PATH")"

# Build command
echo -e "${YELLOW}Starting build (this will take 10-30 minutes)...${NC}"
echo "Build output: $BUILD_PATH"
echo "Log file: $LOG_PATH"
echo ""

"$UNITY_PATH" \
    -quit \
    -batchmode \
    -nographics \
    -silent-crashes \
    -projectPath "$PROJECT_PATH" \
    -buildTarget WebGL \
    -executeMethod BuildScript.BuildWebGL \
    -logFile "$LOG_PATH"

# Check build result
if [ $? -eq 0 ]; then
    echo ""
    echo -e "${GREEN}✅ BUILD SUCCESSFUL!${NC}"
    echo "Output: $BUILD_PATH"
    echo ""
    echo "To test locally, run:"
    echo "  cd '$BUILD_PATH'"
    echo "  python3 -m http.server 8000"
    echo "  # Then open: http://localhost:8000"
else
    echo ""
    echo -e "${RED}❌ BUILD FAILED!${NC}"
    echo "Check log: $LOG_PATH"
    exit 1
fi
