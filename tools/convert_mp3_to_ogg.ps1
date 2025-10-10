param(
  [string[]]$Roots,
  [int]$MinKB,
  [int]$Quality
)

$ErrorActionPreference = 'Stop'

function Ensure-FFmpeg {
  & powershell -NoProfile -ExecutionPolicy Bypass -File "tools/get_ffmpeg.ps1" | Write-Host
  $ff = (Get-Command ffmpeg -ErrorAction SilentlyContinue)
  if($ff){ return $ff.Source }
  $local = Join-Path 'tools/bin' 'ffmpeg.exe'
  if(Test-Path $local){ return $local }
  throw 'ffmpeg not available'
}

function Convert-One($ffmpeg, $inFile, $q){
  $dir = Split-Path $inFile -Parent
  $name = [System.IO.Path]::GetFileNameWithoutExtension($inFile)
  $out = Join-Path $dir ($name + '.ogg')
  if(Test-Path $out){ return @{ Status='SkipExists'; Out=$out } }
  $tmp = $out + '.tmp.ogg'
  & $ffmpeg -y -hide_banner -loglevel error -i "$inFile" -vn -c:a libvorbis -qscale:a $q "$tmp"
  if(-not (Test-Path $tmp)){ throw "Conversion failed: $inFile" }

  # Preserve GUID by renaming .meta
  $metaIn = "$inFile.meta"
  $metaOut = "$out.meta"
  if(Test-Path $metaIn){
    Move-Item -LiteralPath $metaIn -Destination $metaOut -Force
  }
  Remove-Item -LiteralPath $inFile -Force
  Move-Item -LiteralPath $tmp -Destination $out -Force
  return @{ Status='Converted'; Out=$out }
}

$ff = Ensure-FFmpeg

$Roots = if($Roots){ $Roots } else { @('unity/Assets') }
$MinKB = if($MinKB){ $MinKB } else { 4096 }
$Quality = if($Quality){ $Quality } else { 5 }

$mp3s = @()
foreach($r in $Roots){
  if(Test-Path $r){
    $mp3s += Get-ChildItem $r -Recurse -File -Include *.mp3 -ErrorAction SilentlyContinue |
             Where-Object { $_.Length -ge ($MinKB * 1KB) -and $_.FullName -notmatch '\\Library\\|\\PackageCache\\|\\Temp\\' }
  }
}

if(-not $mp3s){ Write-Host "No MP3 files >= $MinKB KB found."; exit 0 }

$total = 0; $savedKB = 0
foreach($f in $mp3s){
  $before = [math]::Round($f.Length/1KB,2)
  try {
    $res = Convert-One $ff $f.FullName $Quality
    if($res.Status -eq 'Converted'){
      $afterSize = (Get-Item $res.Out).Length
      $afterKB = [math]::Round($afterSize/1KB,2)
      $savedKB += [math]::Max(0, $before - $afterKB)
      $total++
      Write-Host "Converted: $($f.FullName) -> $($res.Out) | $before KB -> $afterKB KB"
    } else {
      Write-Host "Skip (exists): $($f.FullName)"
    }
  } catch {
    Write-Warning $_.Exception.Message
  }
}

Write-Host "Converted $total files. Approx space saved: $([math]::Round($savedKB/1024,2)) MB"
