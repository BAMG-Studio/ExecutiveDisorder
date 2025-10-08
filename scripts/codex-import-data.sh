#!/usr/bin/env bash
set -euo pipefail

UNITY_VERSION=${UNITY_VERSION:-"6000.0.23f1"}
UNITY_PATH=${UNITY_PATH:-"/mnt/c/Program Files/Unity/Hub/Editor/${UNITY_VERSION}/Editor/Unity.exe"}
PROJECT_PATH=${PROJECT_PATH:-"$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)/unity"}

if [ ! -f "$UNITY_PATH" ]; then
  echo "Unity not found at: $UNITY_PATH" >&2
  exit 1
fi

"$UNITY_PATH" \
  -batchmode -nographics \
  -projectPath "$PROJECT_PATH" \
  -executeMethod CodexDataImporter.ImportAll \
  -quit -logFile -

echo "Import completed."

