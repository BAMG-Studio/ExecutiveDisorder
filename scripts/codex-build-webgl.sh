#!/bin/bash

# Codex CLI Build Script - WebGL
# This script is designed to be run by Codex CLI for automated builds

set -e  # Exit on error

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Configuration
UNITY_VERSION="6000.0.23f1"
UNITY_PATH="/mnt/c/Program Files/Unity/Hub/Editor/${UNITY_VERSION}/Editor/Unity.exe"
PROJECT_PATH="/mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/unity"
BUILD_METHOD="CodexBuildScript.BuildWebGL"
LOG_FILE="build-$(date +%Y%m%d_%H%M%S).log"

echo -e "${BLUE}🚀 Codex CLI - Unity WebGL Build${NC}"
echo "=================================="
echo ""

# Check if Unity is installed
if [ ! -f "$UNITY_PATH" ]; then
    echo -e "${RED}❌ Unity not found at: $UNITY_PATH${NC}"
    echo -e "${YELLOW}Please update UNITY_VERSION in this script${NC}"
    exit 1
fi

echo -e "${GREEN}✅ Unity found: $UNITY_VERSION${NC}"

# Check if project exists
if [ ! -d "$PROJECT_PATH" ]; then
    echo -e "${RED}❌ Project not found at: $PROJECT_PATH${NC}"
    exit 1
fi

echo -e "${GREEN}✅ Project found${NC}"
echo ""

# Display build info
echo -e "${BLUE}Build Configuration:${NC}"
echo "  Unity Version: $UNITY_VERSION"
echo "  Project Path:  $PROJECT_PATH"
echo "  Build Method:  $BUILD_METHOD"
echo "  Log File:      $LOG_FILE"
echo ""

# Start build
echo -e "${YELLOW}⏳ Starting Unity build (this may take 10-30 minutes)...${NC}"
echo ""

# Execute Unity build
"$UNITY_PATH" \
    -batchmode \
    -nographics \
    -projectPath "$PROJECT_PATH" \
    -executeMethod "$BUILD_METHOD" \
    -quit \
    -logFile "$LOG_FILE"

# Check build result
BUILD_EXIT_CODE=$?

echo ""
if [ $BUILD_EXIT_CODE -eq 0 ]; then
    echo -e "${GREEN}✅ Build completed successfully!${NC}"
    
    # Display build info if available
    BUILD_INFO="$PROJECT_PATH/Builds/WebGL/*/build-info.json"
    if ls $BUILD_INFO 1> /dev/null 2>&1; then
        echo ""
        echo -e "${BLUE}Build Information:${NC}"
        cat $(ls -t $BUILD_INFO | head -1) | grep -E "platform|version|totalSizeFormatted|buildTime"
    fi
    
    echo ""
    echo -e "${GREEN}📦 Build output: $PROJECT_PATH/Builds/WebGL/${NC}"
    
    # Commit to git (Codex CLI will handle this)
    echo ""
    echo -e "${BLUE}Ready for Codex CLI to commit and push${NC}"
    
else
    echo -e "${RED}❌ Build failed with exit code: $BUILD_EXIT_CODE${NC}"
    echo -e "${YELLOW}Check log file: $LOG_FILE${NC}"
    exit $BUILD_EXIT_CODE
fi

echo ""
echo -e "${GREEN}✨ Codex CLI build process complete!${NC}"
