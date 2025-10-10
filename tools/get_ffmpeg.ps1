param(
  [string]$OutDir = 'tools/bin'
)

$ErrorActionPreference = 'Stop'

function Ensure-Dir($p){ if(!(Test-Path $p)){ New-Item -ItemType Directory -Path $p | Out-Null } }
Ensure-Dir $OutDir

# Try PATH first
$ff = (Get-Command ffmpeg -ErrorAction SilentlyContinue)
if($ff){ Write-Host "ffmpeg found in PATH: $($ff.Source)"; exit 0 }

$ffLocal = Join-Path $OutDir 'ffmpeg.exe'
if(Test-Path $ffLocal){ Write-Host "ffmpeg found: $ffLocal"; exit 0 }

$zipUrl = 'https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-essentials.zip'
$zipPath = Join-Path $OutDir 'ffmpeg.zip'
Write-Host "Downloading ffmpeg from $zipUrl ..."
Invoke-WebRequest -Uri $zipUrl -OutFile $zipPath -UseBasicParsing

Write-Host "Expanding archive..."
Expand-Archive -Path $zipPath -DestinationPath $OutDir -Force

# Find ffmpeg.exe inside extracted folder
$exe = Get-ChildItem $OutDir -Recurse -Filter ffmpeg.exe | Select-Object -First 1 -ExpandProperty FullName
if(-not $exe){ throw 'ffmpeg.exe not found after extraction.' }
Copy-Item -LiteralPath $exe -Destination $ffLocal -Force
Remove-Item $zipPath -Force
Write-Host "ffmpeg installed at $ffLocal"

