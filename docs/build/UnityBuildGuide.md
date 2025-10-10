# Unity Build Guide

WebGL (Editor)
- File → Build Settings → WebGL → Build → `Builds/WebGL/ExecutiveDisorder`

WebGL (CI)
- Workflow: `.github/workflows/deploy-pages.yml`
- Requires secrets: `UNITY_LICENSE`, `UNITY_EMAIL`, `UNITY_PASSWORD`
- Outputs artifact + (on main) deploys to GitHub Pages

Windows (Steam contingency)
- Switch platform to Windows → Build `Builds/Windows/ExecutiveDisorder`
- Use same scenes list; confirm Input System settings

Build size/perf tips
- Use atlases; compress textures; OGG for audio; strip unused shaders
- Target download ≤ 25 MB for first load; set COEP/COOP headers if serving cross-origin
