#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR=$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)

PYTHON_BIN=${PYTHON_BIN:-python3}

echo "[gen-content] Generating game data..."
"$PYTHON_BIN" "$ROOT_DIR/tools/gen_content.py" "$@"
echo "[gen-content] Done."

