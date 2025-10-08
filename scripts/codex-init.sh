#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR=$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)
UNITY_VERSION=${UNITY_VERSION:-"6000.0.23f1"}
UNITY_PATH=${UNITY_PATH:-"/mnt/c/Program Files/Unity/Hub/Editor/${UNITY_VERSION}/Editor/Unity.exe"}
PROJECT_PATH=${PROJECT_PATH:-"$ROOT_DIR/unity"}

echo "[codex-init] Step 1: Generate starter content"
bash "$ROOT_DIR/scripts/gen-content.sh" --seed 42

echo "[codex-init] Step 2: Import data into ScriptableObjects"
bash "$ROOT_DIR/scripts/codex-import-data.sh"

echo "[codex-init] Step 3: Scaffold core scenes (Boot/MainMenu/Gameplay) and add to Build Settings"
"$UNITY_PATH" -batchmode -nographics -projectPath "$PROJECT_PATH" \
  -executeMethod ExecutiveDisorder.EditorTools.SceneScaffolder.SetupAll -quit -logFile -

echo "[codex-init] Step 4: (Optional) Ensure Addressables package & settings"
"$UNITY_PATH" -batchmode -nographics -projectPath "$PROJECT_PATH" \
  -executeMethod ExecutiveDisorder.EditorTools.CodexAddressablesSetup.EnsureSettings -quit -logFile - || true

echo "[codex-init] Complete. You can now run the WebGL build script."
