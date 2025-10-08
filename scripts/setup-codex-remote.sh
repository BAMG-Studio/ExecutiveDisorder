#!/usr/bin/env bash
set -euo pipefail

# Configure remotes for Codex CLI workflows
# - upstream: SSH (canonical) -> git@github.com:BAMG-Studio/ExecutiveDisorder.git
# - origin:   HTTPS (push via token) -> https://github.com/BAMG-Studio/ExecutiveDisorder.git
# Optionally set $GITHUB_TOKEN to embed PAT for origin pushes.

REPO_SSH="${1:-git@github.com:BAMG-Studio/ExecutiveDisorder.git}"
REPO_HTTPS="${2:-https://github.com/BAMG-Studio/ExecutiveDisorder.git}"

if git remote | grep -qx upstream; then
  git remote set-url upstream "$REPO_SSH"
else
  git remote add upstream "$REPO_SSH"
fi

if git remote | grep -qx origin; then
  git remote set-url origin "$REPO_HTTPS"
else
  git remote add origin "$REPO_HTTPS"
fi

if [[ -n "${GITHUB_TOKEN:-}" ]]; then
  git remote set-url origin "https://x-access-token:${GITHUB_TOKEN}@github.com/BAMG-Studio/ExecutiveDisorder.git"
fi

git remote -v

