param(
  [string]$UnityVersion = "6000.0.23f1"
)

$Root = Resolve-Path "$PSScriptRoot/.."
$UnityPath = ${env:UNITY_PATH}
if (-not $UnityPath) { $UnityPath = "/mnt/c/Program Files/Unity/Hub/Editor/$UnityVersion/Editor/Unity.exe" }
$ProjectPath = "$Root/unity"

Write-Host "[codex-init] Step 1: Generate starter content"
powershell -ExecutionPolicy Bypass -File "$Root/scripts/gen-content.ps1" -Seed 42
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host "[codex-init] Step 2: Import data into ScriptableObjects"
bash "$Root/scripts/codex-import-data.sh"
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host "[codex-init] Step 3: Scaffold core scenes (Boot/MainMenu/Gameplay) and add to Build Settings"
& "$UnityPath" -batchmode -nographics -projectPath "$ProjectPath" -executeMethod ExecutiveDisorder.EditorTools.SceneScaffolder.SetupAll -quit -logFile -
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host "[codex-init] Step 4: (Optional) Ensure Addressables settings"
& "$UnityPath" -batchmode -nographics -projectPath "$ProjectPath" -executeMethod ExecutiveDisorder.EditorTools.CodexAddressablesSetup.EnsureSettings -quit -logFile -

Write-Host "[codex-init] Complete."
