# Art Specs (Unity/WebGL)

- Portraits.Executives
  - Format: PNG + alpha
  - Size: 1024x1024
  - Naming: `Exec_<Name>_<Emotion>.png` (Neutral|Happy|Angry|Stressed)
- Portraits.Staff | Stakeholders | Crisis
  - Format: PNG + alpha
  - Size: 512x512
  - Naming: `Staff_<Role>_<Variant>.png`, `Stake_<Type>_<Variant>.png`, `Crisis_<Type>_<Variant>.png`
- Scenes.Backgrounds
  - Format: PNG
  - Size: 1920x1080 (keep ≤ 2048 max size)
  - Naming: `BG_<Location>_<Mood>.png`
- UI.Icons
  - Format: PNG
  - Size: 128x128 (power-of-two)
  - Atlas: included in `Assets/Art/Atlases/UI.spriteatlas`
- UI.FramesCards
  - Frames: PNG 1024x1536
  - Thumbs: PNG 512x512 (`Cards.Events`)
- Fonts
  - TTF/OTF → TMP Font Assets with fallbacks
- Audio
  - Music: OGG 128–160 kbps (loop-ready)
  - SFX: OGG or short WAV (< 3s)
- Video (Cinematics)
  - WebM (VP8/9), ≤ 720p, short loops/stingers

Importer defaults (auto-applied)
- `unity/Assets/Editor/ImportSettings/DefaultImportSettings.cs`
  - UI/Portraits → Sprite, HQ compression, max 1024–2048, no mipmaps
  - Backgrounds → Default, mipmaps on, max 2048
  - Audio → Vorbis; Music=Streaming, SFX=CompressedInMemory

Sprite Atlases
- Builder: `unity/Assets/Editor/Atlases/BuildSpriteAtlases.cs`
- Atlases: `Assets/Art/Atlases/UI.spriteatlas`, `Assets/Art/Atlases/Portraits.spriteatlas`

Examples present (samples)
- Executives: `unity/Assets/Art/MadamCashPortrait.png`, `unity/Assets/Art/TheTechnocratPortrait.png`
- Backgrounds: `unity/Assets/Art/MainBGMorning.png`, `unity/Assets/Art/MainBGAfternoon.png`
- Icons: `unity/Assets/Art/PopularityIcon.png`, `unity/Assets/Art/StabilityIcon.png`
- Logos: `unity/Assets/Art/zzExecutive Disorder Initial logo.png`
- Fonts: `unity/Assets/TextMesh Pro/Fonts/Anton-Regular.ttf`
- Audio: `unity/Assets/Audio/Sounds/newsreportmusic-6242.mp3`

Missing summary
- See `docs/assets/missing_assets.csv` for counts and gaps.
