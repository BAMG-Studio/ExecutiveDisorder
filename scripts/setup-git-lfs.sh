#!/usr/bin/env bash
set -euo pipefail

# Git LFS Setup for Executive Disorder
# Tracks large Unity build artifacts

cd /mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit

echo "🔧 Setting up Git LFS"
echo "====================="
echo ""

# Install Git LFS
echo "📦 Installing Git LFS..."
git lfs install
echo "✅ Git LFS installed"
echo ""

# Track WebAssembly files
echo "📝 Tracking file patterns..."
git lfs track "*.wasm"
git lfs track "*.data"
git lfs track "*.unity3d"
git lfs track "*.bundle"
git lfs track "*.asset"

# Track large audio/video files
git lfs track "*.mp3"
git lfs track "*.wav"
git lfs track "*.ogg"
git lfs track "*.mp4"
git lfs track "*.mov"

# Track large textures
git lfs track "*.psd"
git lfs track "*.tga"
git lfs track "*.exr"

# Track Unity build artifacts
git lfs track "*.apk"
git lfs track "*.aab"
git lfs track "*.ipa"
git lfs track "*.app"

echo "✅ File patterns configured"
echo ""

# Add .gitattributes
echo "💾 Saving .gitattributes..."
git add .gitattributes
echo "✅ .gitattributes added"
echo ""

# Show tracked patterns
echo "📊 Currently tracking:"
git lfs track
echo ""

echo "✅ Git LFS setup complete!"
echo ""
echo "🎯 Large files will now be tracked efficiently"
