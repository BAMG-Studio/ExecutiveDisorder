param(
  [string]$AssetsRoot,
  [string]$OutDir
)

$ErrorActionPreference = 'Stop'

function Ensure-Dir($p){ if(!(Test-Path $p)){ New-Item -ItemType Directory -Path $p | Out-Null } }
if(-not $AssetsRoot){ $AssetsRoot = 'unity/Assets' }
if(-not $OutDir){ $OutDir = 'docs/assets' }
Ensure-Dir $OutDir

function Get-Rel($path){
  $root = (Resolve-Path '.').Path
  return (Resolve-Path $path).Path.Replace($root+'\','')
}

function Get-ImageSize($file){
  try {
    Add-Type -AssemblyName System.Drawing -ErrorAction SilentlyContinue | Out-Null
    $img = [System.Drawing.Image]::FromFile($file)
    $w = $img.Width; $h = $img.Height; $img.Dispose()
    return @($w,$h)
  } catch { return @($null,$null) }
}

function Get-Category($f){
  $p = $f.ToLower()
  if($p -match '/art/ui/icons' -or $p -match 'icon' -or $p -match 'arrow' -or $p -match 'warning'){ return 'UI.Icons' }
  if($p -match '/art/ui/' -or $p -match 'ui/frames' -or $p -match 'card' -and $p -match 'frame'){ return 'UI.FramesCards' }
  if($p -match 'logo'){ return 'Brand.Logos' }
  if($p -match 'portrait' -or $p -match 'fullbody' -or $p -match 'technocrat' -or $p -match 'madamcash' -or $p -match 'presi'){
    if($p -match 'advisor|assistant|staff'){ return 'Portraits.Staff' }
    if($p -match 'board|media|public'){ return 'Portraits.Stakeholders' }
    if($p -match 'protest|whistle|crowd'){ return 'Portraits.Crisis' }
    return 'Portraits.Executives'
  }
  if($p -match 'mainbg|background|scene|crisisnews|newscrisis|press|office'){ return 'Scenes.Backgrounds' }
  if($p -match 'video|opening' -and ($p -like '*.mp4' -or $p -like '*.webm')){ return 'Video.Cinematics' }
  if($p -like '*.ttf' -or $p -like '*.otf'){ return 'Fonts' }
  if($p -like '*.wav' -or $p -like '*.mp3' -or $p -like '*.ogg'){
    if($p -match 'music|bgm|theme'){ return 'Audio.Music' } else { return 'Audio.SFX' }
  }
  if($p -like '*.prefab'){ return 'Prefabs' }
  if($p -like '*.anim' -or $p -like '*.controller'){ return 'Animations' }
  if($p -like '*.fbx' -or $p -like '*.obj'){ return 'Models3D' }
  if($p -match 'card' -or $p -match 'event'){ return 'Cards.Events' }
  return 'Uncategorized'
}

$patterns = '*.png','*.jpg','*.jpeg','*.psd','*.svg','*.ttf','*.otf','*.wav','*.mp3','*.ogg','*.mp4','*.webm','*.fbx','*.obj','*.prefab','*.anim','*.controller'
$files = Get-ChildItem $AssetsRoot -Recurse -File -Include $patterns -ErrorAction SilentlyContinue

$rows = @()
foreach($f in $files){
  $cat = Get-Category ($f.FullName.Replace('\\','/'))
  $w=$null; $h=$null
  if($f.Extension -match 'png|jpg|jpeg'){
    $s = Get-ImageSize $f.FullName
    $w = $s[0]; $h = $s[1]
  }
  $rows += [PSCustomObject]@{
    Category = $cat
    RelativePath = (Get-Rel $f.FullName).Replace('\\','/')
    Extension = $f.Extension.ToLower()
    SizeKB = [math]::Round($f.Length/1KB,2)
    Width = $w
    Height = $h
  }
}

$inv = Join-Path $OutDir 'assets_inventory.csv'
$rows | Sort-Object Category,RelativePath | Export-Csv -Path $inv -NoTypeInformation -Encoding UTF8

# Missing plan
$expected = @(
  @{ Category='Portraits.Executives'; Required=24; Notes='6 execs x 4 emotions (neutral, happy, angry, stressed)'; Format='png'; Size='1024x1024'; },
  @{ Category='Portraits.Staff'; Required=12; Notes='6 staff x 2 emotions'; Format='png'; Size='512x512'; },
  @{ Category='Portraits.Stakeholders'; Required=12; Notes='board, media, public (12 total)'; Format='png'; Size='512x512'; },
  @{ Category='Portraits.Crisis'; Required=16; Notes='protesters, whistleblowers, rivals'; Format='png'; Size='512x512'; },
  @{ Category='Scenes.Backgrounds'; Required=9; Notes='office day/evening/night, press room, war room, street protest, tv studio, crisis variants'; Format='png'; Size='1920x1080'; },
  @{ Category='UI.Icons'; Required=40; Notes='resources, statuses, actions, severity'; Format='png'; Size='128x128'; },
  @{ Category='UI.FramesCards'; Required=12; Notes='card frames by rarity + event banners'; Format='png'; Size='1024x1536 (cards)'; },
  @{ Category='Brand.Logos'; Required=2; Notes='studio + game'; Format='png/svg'; Size='1024x1024'; },
  @{ Category='Cards.Events'; Required=50; Notes='card thumbnails'; Format='png'; Size='512x512'; },
  @{ Category='Fonts'; Required=4; Notes='heading/body/numeric/fallback'; Format='ttf/otf'; Size='TMP assets'; },
  @{ Category='Audio.Music'; Required=3; Notes='menu, gameplay, crisis'; Format='ogg'; Size='128-160kbps'; },
  @{ Category='Audio.SFX'; Required=25; Notes='ui, cards, stingers'; Format='wav/ogg'; Size='short'; },
  @{ Category='Video.Cinematics'; Required=10; Notes='opening (1), stingers (3), meme overlays (6)'; Format='webm'; Size='720p max for WebGL'; }
)

$presentByCat = $rows | Group-Object Category | ForEach-Object { @{ Category=$_.Name; Present=$_.Count } }
$presentMap = @{}
foreach($p in $presentByCat){ $presentMap[$p.Category] = $p.Present }

$missingRows = foreach($e in $expected){
  $p = 0; if($presentMap.ContainsKey($e.Category)) { $p = [int]$presentMap[$e.Category] }
  [PSCustomObject]@{
    Category = $e.Category
    Required = $e.Required
    Present = $p
    Missing = [Math]::Max(0, [int]$e.Required - $p)
    RecommendedFormat = $e.Format
    RecommendedSize = $e.Size
    Notes = $e.Notes
  }
}

$miss = Join-Path $OutDir 'missing_assets.csv'
$missingRows | Export-Csv -Path $miss -NoTypeInformation -Encoding UTF8

Write-Host "Wrote:`n $inv`n $miss"
