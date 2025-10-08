#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR=$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)

PYTHON_BIN=${PYTHON_BIN:-python3}

echo "[gen-content] Generating game data..."

args=("$@")
theme_supplied=false
for arg in "$@"; do
  if [[ "$arg" == *.json ]]; then
    theme_supplied=true
    break
  fi
done

if ! $theme_supplied && [ -f "$ROOT_DIR/theme.json" ]; then
  args=("$ROOT_DIR/theme.json" "${args[@]}")
fi

"$PYTHON_BIN" "$ROOT_DIR/tools/gen_content.py" "${args[@]}"
echo "[gen-content] Done."
