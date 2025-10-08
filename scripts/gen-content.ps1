Param(
  [int]$Seed = 42
)

$Root = Resolve-Path "$PSScriptRoot/.."
$Py = $env:PYTHON_BIN
if (-not $Py) { $Py = "python" }

Write-Host "[gen-content] Generating game data..."
& $Py "$Root/tools/gen_content.py" --seed $Seed
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
Write-Host "[gen-content] Done."

