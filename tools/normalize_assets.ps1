$ErrorActionPreference = 'Stop'

function Ensure-Dir($p){ if(!(Test-Path $p)){ New-Item -ItemType Directory -Path $p | Out-Null } }
function Move-WithMeta($src, $dst){
  Ensure-Dir (Split-Path $dst)
  Move-Item -LiteralPath $src -Destination $dst -Force
  $m = "$src.meta"; if(Test-Path $m){ Move-Item -LiteralPath $m -Destination "$dst.meta" -Force }
}

$root = 'unity/Assets'
$targets = @{
  'UI\Icons' = @('icon','arrow','warning');
  'Portraits\Executives' = @('portrait','fullbody','technocrat','madamcash','presi');
  'Scenes\Backgrounds' = @('mainbg','background','crisisnews','newscrisis');
}

foreach($k in $targets.Keys){ Ensure-Dir (Join-Path $root "Art\$k") }

$moved = 0
Get-ChildItem $root -Recurse -File -Include *.png,*.jpg,*.jpeg,*.psd | ForEach-Object {
  $name = $_.Name.ToLower(); $path = $_.FullName
  foreach($kv in $targets.GetEnumerator()){
    if($kv.Value | Where-Object { $name -like "*$_*" }){
      $dest = Join-Path $root ("Art\" + $kv.Key + "\" + $_.Name)
      if($path -ne (Resolve-Path $dest -ErrorAction SilentlyContinue)){
        Move-WithMeta $_.FullName $dest
        $moved++
      }
      break
    }
  }
}

Write-Host "Moved $moved assets into Art subfolders."

