$ErrorActionPreference = 'Stop'

function Get-DropboxRoot {
  if($env:Dropbox){ return $env:Dropbox }
  $candidate = Join-Path $env:USERPROFILE 'Dropbox'
  if(Test-Path $candidate){ return $candidate }
  throw 'Dropbox folder not found. Set $env:Dropbox or edit this script.'
}

function Ensure-Dir($p){ if(!(Test-Path $p)){ New-Item -ItemType Directory -Path $p | Out-Null } }

$root = Get-DropboxRoot
$base = Join-Path $root 'ExecutiveDisorder/Art'
$paths = @(
  'Portraits/Executives',
  'Portraits/Staff',
  'Portraits/Stakeholders',
  'Portraits/Crisis',
  'Scenes/Backgrounds',
  'Scenes/Crisis',
  'UI/Icons',
  'UI/FramesAndCards',
  'Cards/Thumbnails',
  'Brand/Logos',
  'Fonts',
  'Audio/Music',
  'Audio/SFX',
  'Video/Openings',
  'Prefabs/UI',
  '3D/Models',
  '3D/Props'
)

foreach($rel in $paths){ Ensure-Dir (Join-Path $base $rel) }
Write-Host "Created/verified Dropbox structure under: $base"

