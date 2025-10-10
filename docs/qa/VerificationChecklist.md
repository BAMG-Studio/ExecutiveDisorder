# Verification Checklist

Editor
- Open `unity` project; fix TMP resources if prompted
- Play `unity/Assets/Scenes/MainMenu.unity`
- Cards display; resources update; endings reachable

Assets
- Sprite atlases built (`Assets/Art/Atlases/*.spriteatlas`)
- Portrait sizes correct (512/1024); icons 128x128; backgrounds 1920x1080
- Inventory refreshed: `docs/assets/assets_inventory.csv`
- Missing list reviewed: `docs/assets/missing_assets.csv`

Build
- WebGL build succeeds; no console errors on load
- All assets load; audio plays; fonts render
- Budget check: frame ≤ 5 ms (editor proxy), download ≤ 25 MB

Security
- No secrets in repo; `.gitignore` excludes keys/certs
- Input validation present where needed

Release
- Itch.io: zip WebGL folder; set “play in browser”
- GitHub Pages: CI deploys on main
