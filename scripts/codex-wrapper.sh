#!/usr/bin/env bash
# WSL wrapper for Windows Codex CLI
# This allows calling the Windows-installed codex from WSL

# Set the correct base directory for Windows npm installation
CODEX_BASE="/mnt/c/Users/POK28/AppData/Roaming/npm"
CODEX_SCRIPT="$CODEX_BASE/node_modules/@openai/codex/bin/codex.js"

# Use Node.js to run the codex script directly
if [ -f "$CODEX_SCRIPT" ]; then
    node "$CODEX_SCRIPT" "$@"
else
    echo "❌ Codex CLI not found at: $CODEX_SCRIPT"
    echo "Please ensure Codex is installed via: npm install -g @openai/codex"
    exit 1
fi
