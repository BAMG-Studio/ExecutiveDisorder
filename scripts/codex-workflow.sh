#!/bin/bash

# Codex CLI - Automated Build and Deploy Workflow
# This orchestrates the entire build, commit, and push process

set -e

# Colors
GREEN='\033[0;32m'
BLUE='\033[0;34m'
YELLOW='\033[1;33m'
NC='\033[0m'

echo -e "${BLUE}🤖 Codex CLI - Automated Build Workflow${NC}"
echo "========================================="
echo ""

# Step 1: Verify we're in the right repo
CURRENT_REPO=$(git remote get-url origin 2>/dev/null || echo "NOT_SET")

if [[ "$CURRENT_REPO" != *"BAMG-Studio/ExecutiveDisorder"* ]]; then
    echo -e "${YELLOW}⚠️  BAMG-Studio remote not configured${NC}"
    echo "Run: bash scripts/setup-codex-remote.sh"
    exit 1
fi

echo -e "${GREEN}✅ BAMG-Studio remote configured${NC}"
echo ""

# Step 0: Content Generation Pipeline
echo -e "${BLUE}📝 Step 0: Content Generation Pipeline...${NC}"

# Check if Python is available
if command -v python3 &> /dev/null; then
    echo -e "${GREEN}  ✓ Python3 found${NC}"
    
    # Generate content data (YAML)
    echo -e "${BLUE}  → Generating content data...${NC}"
    python3 tools/gen_content.py theme.json 2>/dev/null || echo "  ⚠️  Content generation skipped"
    
    # Generate images
    echo -e "${BLUE}  → Generating images...${NC}"
    python3 tools/gen_images.py 2>/dev/null || echo "  ⚠️  Image generation skipped"
    
    # Generate audio
    echo -e "${BLUE}  → Generating audio...${NC}"
    python3 tools/gen_audio.py 2>/dev/null || echo "  ⚠️  Audio generation skipped"
    
    echo -e "${GREEN}  ✓ Content generation complete${NC}"
else
    echo -e "${YELLOW}  ⚠️  Python3 not found, skipping content generation${NC}"
fi

echo ""

# Step 1: Unity Build
echo -e "${BLUE}📦 Step 1: Building WebGL...${NC}"
bash scripts/codex-build-webgl.sh

if [ $? -ne 0 ]; then
    echo -e "${RED}❌ Build failed!${NC}"
    exit 1
fi

echo ""

# Step 3: Stage build files
echo -e "${BLUE}📝 Step 2: Staging build files...${NC}"
git add unity/Builds/
git add scripts/
git add docs/

# Step 4: Create commit
BUILD_VERSION=$(date +%Y%m%d_%H%M%S)
COMMIT_MSG="🤖 Codex CLI: WebGL Build $BUILD_VERSION

- Platform: WebGL
- Unity Version: 6.0
- Build Type: Automated
- Content: Auto-generated
- Timestamp: $(date)

[automated-build]"

git commit -m "$COMMIT_MSG" || echo "No changes to commit"

echo -e "${GREEN}✅ Build committed${NC}"
echo ""

# Step 5: Push to BAMG-Studio repo
echo -e "${BLUE}🚀 Step 3: Pushing to BAMG-Studio repo...${NC}"
git push origin codex-cli:main

echo ""
echo -e "${GREEN}✨ Codex CLI workflow complete!${NC}"
echo ""
echo -e "${BLUE}Build available at:${NC}"
echo "  https://github.com/BAMG-Studio/ExecutiveDisorder"
echo ""
