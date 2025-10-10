# Asset Pipeline (Dropbox ↔ Unity)

Source of truth for raw art: Dropbox at `ExecutiveDisorder/Art`.

Create Dropbox structure
- CLI: `powershell -ExecutionPolicy Bypass -File tools/create_dropbox_structure.ps1`
- Unity: Tools → Assets → Create Dropbox Structure

Export existing Unity assets to Dropbox
- CLI: `powershell -ExecutionPolicy Bypass -File tools/export_assets_to_dropbox.ps1 -UnityAssetsPath unity/Assets`

Import from Dropbox into Unity and build atlases
- Unity: Tools → Assets → Import From Dropbox And Build Atlases
  - Copies portraits, icons, frames, backgrounds, fonts, audio, video
  - Rebuilds `Assets/Art/Atlases/*.spriteatlas`

Normalize current assets (optional)
- CLI: `powershell -ExecutionPolicy Bypass -File tools/normalize_assets.ps1`
- Unity: Tools → Assets → Normalize Existing Assets

Inventory and gaps
- Unity: Tools → Assets → Export Asset Inventory CSVs
- Files: `docs/assets/assets_inventory.csv`, `docs/assets/missing_assets.csv`

Naming rules
- Executives: `Exec_<Name>_<Emotion>.png`
- Staff/Stakeholders/Crisis: `Staff_<Role>_<Var>.png` / `Stake_<Type>_<Var>.png` / `Crisis_<Type>_<Var>.png`
- Backgrounds: `BG_<Location>_<Mood>.png`
- Icons: `Icon_<Concept>.png`
- Cards: `CardThumb_<EventId>.png`
