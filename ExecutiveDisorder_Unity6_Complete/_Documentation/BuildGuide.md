# Executive Disorder - Build & Deployment Guide

## 🚀 Building the Game

### Prerequisites

- Unity 6 (2023.x or later)
- Visual Studio 2022 or VS Code
- Git (for version control)

---

## 🖥️ Desktop Build (Windows/Mac/Linux)

### Windows Build

1. **Open Unity Project**
   ```
   Open Unity Hub → Projects → Add → Select project folder
   ```

2. **Configure Build Settings**
   ```
   File → Build Settings
   Platform: PC, Mac & Linux Standalone
   Target Platform: Windows
   Architecture: x86_64
   ```

3. **Player Settings**
   ```
   Company Name: [Your Company]
   Product Name: Executive Disorder
   Icon: Set custom icon from Art/UI/
   Fullscreen Mode: Fullscreen Window
   Default Screen Width: 1920
   Default Screen Height: 1080
   ```

4. **Quality Settings**
   ```
   Edit → Project Settings → Quality
   Set Quality Level: High
   ```

5. **Build**
   ```
   Build Settings → Build
   Choose output folder
   Wait for build to complete
   ```

### Mac Build

Follow same steps as Windows, but:
- Target Platform: macOS
- Architecture: Apple Silicon or Intel (or Universal)

### Linux Build

Follow same steps as Windows, but:
- Target Platform: Linux
- Architecture: x86_64

---

## 🌐 WebGL Build

### WebGL Build Steps

1. **Switch Platform**
   ```
   File → Build Settings
   Platform: WebGL
   Click "Switch Platform"
   ```

2. **Player Settings for WebGL**
   ```
   Resolution and Presentation:
     - Default Canvas Width: 960
     - Default Canvas Height: 600
     - Run in Background: true
   
   Publishing Settings:
     - Compression Format: Gzip
     - Data Caching: false (for development)
     - Debugging: false (for production)
   
   Memory Settings:
     - Memory Size: 256 MB
   ```

3. **Optimization Settings**
   ```
   Other Settings:
     - Strip Engine Code: true
     - Managed Stripping Level: Medium
     - Enable Exceptions: None
   ```

4. **Build**
   ```
   Build Settings → Build
   Choose output folder (e.g., "WebGL_Build")
   Wait for build (may take 10-30 minutes)
   ```

### WebGL Build Output

Your build folder will contain:
```
WebGL_Build/
├── Build/
│   ├── [ProjectName].data
│   ├── [ProjectName].framework.js
│   ├── [ProjectName].loader.js
│   └── [ProjectName].wasm
├── TemplateData/
│   ├── favicon.ico
│   ├── fullscreen-button.png
│   ├── progress-bar-empty-dark.png
│   ├── progress-bar-full-dark.png
│   ├── unity-logo-dark.png
│   └── webgl-logo.png
└── index.html
```

### Testing WebGL Build Locally

**Option 1: Python Server**
```bash
cd WebGL_Build
python -m http.server 8000
# Open browser to http://localhost:8000
```

**Option 2: Node.js Server**
```bash
cd WebGL_Build
npx http-server -p 8000
# Open browser to http://localhost:8000
```

**Option 3: Unity Build & Run**
```
Build Settings → Build And Run
Unity will automatically start a local server
```

---

## 📦 Deployment Options

### 1. Itch.io Deployment

**Upload as HTML5 Game:**

1. Create itch.io project
2. Upload WebGL build folder as ZIP
3. Set "This file will be played in the browser" for index.html
4. Recommended viewport: 960x600 or 1280x720
5. Enable "Embed in page"
6. Enable "Mobile friendly" if applicable

**Itch.io Butler (CLI Upload):**
```bash
# Install butler
https://itch.io/docs/butler/

# Push build
butler push WebGL_Build username/executive-disorder:webgl
```

### 2. GitHub Pages Deployment

**Setup:**

1. Create repository on GitHub
2. Build WebGL to `docs/` folder
3. Push to GitHub
4. Enable GitHub Pages in repository settings
5. Set source to `main` branch, `/docs` folder

**Commands:**
```bash
# Build to docs folder in project
# Then:
git add docs/
git commit -m "WebGL build"
git push origin main
```

Your game will be live at:
```
https://[username].github.io/[repository-name]/
```

### 3. Custom Web Hosting

**Requirements:**
- Web server (Apache, Nginx, etc.)
- Support for serving .wasm files with correct MIME type

**Nginx Configuration:**
```nginx
location / {
    add_header 'Access-Control-Allow-Origin' '*';
    types {
        application/wasm wasm;
        application/javascript js;
        application/octet-stream data;
    }
}
```

**Apache Configuration (.htaccess):**
```apache
AddType application/wasm .wasm
AddType application/javascript .js
AddType application/octet-stream .data
```

### 4. Steam Deployment

**For Desktop Builds:**

1. Register as Steamworks Partner
2. Create app in Steamworks
3. Use Steamworks SDK integration
4. Build for Windows/Mac/Linux
5. Upload using SteamPipe
6. Set up store page

**Steamworks Integration:**
- Install Steamworks.NET package
- Add achievements, cloud saves
- Test with Steam SDK

---

## 🎯 Build Optimization

### Reduce Build Size

**Asset Compression:**
```
Textures:
  - Use ETC2/ASTC compression for mobile
  - Max texture size: 2048x2048
  - Enable "Generate Mip Maps"

Audio:
  - Compress music as Vorbis
  - Compress SFX as PCM/ADPCM
  - Use streaming for music

Models:
  - Keep poly count under 5000
  - Optimize meshes
  - Atlas textures where possible
```

**Code Stripping:**
```
Player Settings → Other Settings:
  - Managed Stripping Level: Medium or High
  - Strip Engine Code: true
  - IL2CPP (for faster performance)
```

**WebGL Specific:**
```
Player Settings → Publishing Settings:
  - Compression: Gzip or Brotli
  - Data Caching: true (production)
  - Memory Size: Reduce if possible
```

### Performance Optimization

**Build Settings:**
```
Project Settings → Quality:
  - Reduce shadow quality for WebGL
  - Disable anti-aliasing for mobile
  - Reduce pixel light count

Project Settings → Graphics:
  - Use URP (Universal Render Pipeline)
  - Bake lighting where possible
```

**Script Optimization:**
```csharp
// Use object pooling
// Avoid Update() where possible
// Cache component references
// Use events instead of polling
```

---

## ✅ Pre-Release Checklist

### Testing

- [ ] Test all 10 endings
- [ ] Test all character interactions
- [ ] Test save/load functionality
- [ ] Test all decision cards
- [ ] Test resource management edge cases
- [ ] Test on target platforms
- [ ] Test different screen resolutions
- [ ] Performance profiling
- [ ] Memory leak checks

### Build Validation

- [ ] No console errors
- [ ] No missing assets
- [ ] All scenes included in build
- [ ] Audio works correctly
- [ ] UI scales properly
- [ ] Input works (keyboard, mouse, controller)
- [ ] Settings save and load
- [ ] Game can be completed start to finish

### Marketing Assets

- [ ] Game screenshots
- [ ] Gameplay trailer
- [ ] Store description
- [ ] Press kit
- [ ] Social media graphics

---

## 🐛 Troubleshooting

### Common Issues

**WebGL Build Errors:**
```
Error: "Not enough memory"
Solution: Increase memory size in Player Settings

Error: "Build failed - IL2CPP"
Solution: Install IL2CPP module in Unity Hub

Error: "WASM streaming compilation failed"
Solution: Ensure server MIME types are correct
```

**Performance Issues:**
```
Low FPS in WebGL:
  - Reduce texture quality
  - Disable shadows
  - Use simpler shaders
  - Optimize draw calls

Large Build Size:
  - Compress textures
  - Compress audio
  - Remove unused assets
  - Enable code stripping
```

**Platform-Specific:**
```
Windows: Missing Visual C++ Runtime
  - Include redistributables
  
Mac: "App is damaged and can't be opened"
  - Sign the build or bypass Gatekeeper
  
Linux: Missing dependencies
  - Include required libraries
```

---

## 📊 Analytics & Telemetry

### Unity Analytics

1. Enable Unity Analytics in Services
2. Track custom events:
   - Game starts
   - Endings reached
   - Decisions made
   - Play time
   - Resource levels

### Custom Analytics

```csharp
// Example event tracking
Analytics.CustomEvent("EndingReached", new Dictionary<string, object>
{
    { "EndingType", endingId },
    { "DaysSurvived", currentDay },
    { "FinalPopularity", popularity }
});
```

---

## 🔄 Continuous Integration

### GitHub Actions Example

```yaml
name: Build Unity Project

on:
  push:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v2
    - uses: game-ci/unity-builder@v2
      with:
        targetPlatform: WebGL
    - uses: actions/upload-artifact@v2
      with:
        name: WebGL Build
        path: build/WebGL
```

---

## 📝 Version Control

### Git LFS for Large Files

```bash
# Install Git LFS
git lfs install

# Track large file types
git lfs track "*.psd"
git lfs track "*.wav"
git lfs track "*.mp3"
git lfs track "*.fbx"

# Commit
git add .gitattributes
git commit -m "Configure Git LFS"
```

---

## 🎉 Release Process

1. **Version Bump:** Update version in Player Settings
2. **Build:** Create builds for all target platforms
3. **Test:** QA test all builds
4. **Package:** Create distribution packages
5. **Upload:** Upload to distribution platforms
6. **Announce:** Post on social media, forums
7. **Monitor:** Watch for bugs, player feedback
8. **Patch:** Fix critical issues quickly

---

**Last Updated:** October 2025  
**Build Version:** 1.0.0
