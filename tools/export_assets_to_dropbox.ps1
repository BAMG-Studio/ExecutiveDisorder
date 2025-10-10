param(
  [string]$UnityAssetsPath = 'unity/Assets',
  [string]$DropboxRoot
)

$ErrorActionPreference = 'Stop'

function Get-DropboxRoot {
  if($DropboxRoot){ return $DropboxRoot }
  if($env:Dropbox){ return $env:Dropbox }
  $candidate = Join-Path $env:USERPROFILE 'Dropbox'
  if(Test-Path $candidate){ return $candidate }
  throw 'Dropbox folder not found. Provide -DropboxRoot or set $env:Dropbox.'
}

function Ensure-Dir($p){ if(!(Test-Path $p)){ New-Item -ItemType Directory -Path $p | Out-Null } }

$db = Get-DropboxRoot
$base = Join-Path $db 'ExecutiveDisorder/Art'

# Ensure base structure exists (mirrors create_dropbox_structure.ps1)
$folders = @(
  'Portraits/Executives','Portraits/Staff','Portraits/Stakeholders','Portraits/Crisis',
  'Characters/Executives','Characters/Staff','Characters/Stakeholders','Characters/Crisis',
  'Scenes/Backgrounds','Scenes/Crisis','UI/Icons','UI/FramesAndCards','Cards/Thumbnails',
  'Brand/Logos','Fonts','Audio/Music','Audio/SFX','Video/Openings','Prefabs/UI','3D/Models','3D/Props','Misc'
)
foreach($rel in $folders){ Ensure-Dir (Join-Path $base $rel) }

# Map a Unity asset path to a Dropbox category path
function Map-Category($relPath){
  $p = $relPath.ToLower().Replace('\','/')
  if($p -match '/art/portraits/exec' -or $p -match 'portrait' -or $p -match 'fullbody' -or $p -match 'technocrat' -or $p -match 'madamcash' -or $p -match 'presi') { return 'Portraits/Executives' }
  if($p -match '/art/ui/icons' -or $p -match 'icon' -or $p -match 'arrow' -or $p -match 'warning'){ return 'UI/Icons' }
  if($p -match '/art/ui/frames' -or ($p -match 'card' -and $p -match 'frame')){ return 'UI/FramesAndCards' }
  if($p -match 'logo'){ return 'Brand/Logos' }
  if($p -match 'mainbg|background|scene|crisisnews|newscrisis|press|office'){ return 'Scenes/Backgrounds' }
  if($p -match '/fonts/' -or $p -like '*.ttf' -or $p -like '*.otf'){ return 'Fonts' }
  if($p -like '*.wav' -or $p -like '*.mp3' -or $p -like '*.ogg'){
    if($p -match 'music|bgm|theme'){ return 'Audio/Music' } else { return 'Audio/SFX' }
  }
  if($p -like '*.webm' -or $p -like '*.mp4'){ return 'Video/Openings' }
  if($p -like '*.fbx' -or $p -like '*.obj'){ return '3D/Models' }
  if($p -like '*.prefab'){ return 'Prefabs/UI' }
  return 'Misc'
}

# Collect files (images, fonts, audio, video, models)
$patterns = '*.png','*.jpg','*.jpeg','*.psd','*.ttf','*.otf','*.wav','*.mp3','*.ogg','*.webm','*.mp4','*.fbx','*.obj','*.prefab'
$files = Get-ChildItem $UnityAssetsPath -Recurse -File -Include $patterns -ErrorAction SilentlyContinue

$copied = 0
foreach($f in $files){
  $rel = $f.FullName.Substring((Resolve-Path $UnityAssetsPath).Path.Length).TrimStart('\','/')
  $cat = Map-Category $rel
  $dest = Join-Path (Join-Path $base $cat) $f.Name
  Copy-Item -LiteralPath $f.FullName -Destination $dest -Force
  $copied++
}

Write-Host "Copied $copied files to Dropbox: $base"
