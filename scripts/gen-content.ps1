Param(
  [int]$Seed = 42,
  [string]$Theme
)

$Root = Resolve-Path "$PSScriptRoot/.."
$Py = $env:PYTHON_BIN
if (-not $Py) { $Py = "python" }

Write-Host "[gen-content] Generating game data..."
$argsList = @()
if (-not $Theme) {
  $defaultTheme = Join-Path $Root "theme.json"
  if (Test-Path $defaultTheme) {
    $Theme = $defaultTheme
  }
}

if ($Theme) {
  $argsList += $Theme
}
$argsList += "--seed"
$argsList += $Seed

& $Py "$Root/tools/gen_content.py" @argsList
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
Write-Host "[gen-content] Done."
