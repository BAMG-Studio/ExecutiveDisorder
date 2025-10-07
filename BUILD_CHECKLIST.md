# Executive Disorder - Build Checklist

## 🎯 Build Goal
Create a playable WebGL build of Executive Disorder game

## 📋 Pre-Build Verification

### ✅ Version Control Status
- [ ] Current changeset committed: `[Changeset ID: ___]`
- [ ] Working directory clean (no pending changes)
- [ ] Created branch: `build/webgl-v1.0`

### ✅ Unity Project Health
- [ ] No console errors
- [ ] No missing script references
- [ ] All scenes in Build Settings
- [ ] ScriptableObjects properly assigned

### ✅ Scene Verification
- [ ] **MainMenu.unity** - Loads without errors
- [ ] **GamePlay.unity** - Managers present
- [ ] **LoadingScreen.unity** - Configured
- [ ] **EndingCinematic.unity** - Ready

### ✅ Manager Setup
- [ ] GameManager exists in Hierarchy
- [ ] ResourceManager configured
- [ ] CardManager has card database
- [ ] CharacterManager has character data
- [ ] AudioManager has audio mixer
- [ ] UIManager connected to UI elements

### ✅ Asset Verification
- [ ] All character portraits present
- [ ] Card sprites loaded
- [ ] Background images assigned
- [ ] Audio files imported
- [ ] Fonts available

---

## 🔧 Build Configuration Steps

### Step 1: Build Settings
```
File → Build Settings
```
- [ ] Platform: WebGL selected
- [ ] Add Open Scenes: All 4 scenes added
- [ ] Scene order: MainMenu → GamePlay → LoadingScreen → EndingCinematic

### Step 2: Player Settings - Company Info
```
Edit → Project Settings → Player
```
- [ ] Company Name: `[Your Studio Name]`
- [ ] Product Name: `Executive Disorder`
- [ ] Version: `1.0.0`
- [ ] Default Icon: Set

### Step 3: Player Settings - WebGL
- [ ] **Resolution and Presentation**
  - [ ] Default Canvas Width: `1920`
  - [ ] Default Canvas Height: `1080`
  - [ ] Run in Background: `Enabled`

- [ ] **Publishing Settings**
  - [ ] Compression Format: `Gzip` (or Brotli for better compression)
  - [ ] Enable Exceptions: `None` (for size optimization)
  - [ ] Data Caching: `Enabled`

- [ ] **Optimization**
  - [ ] Managed Stripping Level: `Medium`
  - [ ] Code Optimization: `Speed`

### Step 4: Quality Settings
```
Edit → Project Settings → Quality
```
- [ ] Set WebGL quality level to: `Medium` or `Good`
- [ ] Anti-aliasing: `2x` or `4x`
- [ ] Texture Quality: `Full Res`

### Step 5: Graphics Settings
- [ ] Rendering Path: URP (Universal Render Pipeline)
- [ ] Color Space: `Linear` (if supported)

---

## 🎨 Asset Optimization

### Sprites & Textures
- [ ] Max size: `2048` for backgrounds
- [ ] Max size: `1024` for character portraits
- [ ] Max size: `512` for UI elements
- [ ] Compression: `Automatic` or `High Quality`
- [ ] Generate Mip Maps: `Disabled` for UI

### Audio
- [ ] Background music: `Compressed` (Vorbis)
- [ ] Quality: `70%` for music
- [ ] SFX: `Compressed` (Vorbis), `50%` quality
- [ ] Force to Mono: `Enabled` for SFX

---

## 🏗️ Build Process

### Pre-Build Actions
1. [ ] **Commit current work** to Version Control
   ```
   Comment: "Pre-build checkpoint - all assets configured"
   ```

2. [ ] **Create build branch**
   ```
   Branch name: build/webgl-v1.0
   ```

3. [ ] **Clear Console** (`Ctrl+Shift+C`)

4. [ ] **Test Play Mode** (Press Play, verify no errors)

### Build Execution
5. [ ] `File → Build Settings → WebGL → Build`

6. [ ] Choose output folder:
   ```
   c:\Users\POK28\source\repos\ExecutiveDisorderReplit\Builds\WebGL\v1.0
   ```

7. [ ] **Wait for build** (may take 10-30 minutes)

8. [ ] **Check build log** for warnings/errors

### Post-Build Verification
9. [ ] Build completed without errors

10. [ ] Output files present:
    - [ ] `index.html`
    - [ ] `Build/` folder
    - [ ] `TemplateData/` folder

11. [ ] Test locally:
    ```
    Use local web server (Python or Unity's built-in)
    ```

12. [ ] **Commit build** to Version Control
    ```
    Comment: "WebGL Build v1.0 - [Date]"
    ```

---

## 🧪 Testing Checklist

### Local Test
- [ ] Open `index.html` in browser
- [ ] Game loads without errors
- [ ] Main menu displays correctly
- [ ] Can start new game
- [ ] Cards display and can be selected
- [ ] Resources update correctly
- [ ] Audio plays
- [ ] Can reach an ending

### Browser Compatibility
- [ ] Chrome/Edge (Chromium)
- [ ] Firefox
- [ ] Safari (if available)

---

## 🐛 Common Issues & Fixes

### Build Fails
- **Error**: "No scenes in build settings"
  - **Fix**: Add all 4 scenes to Build Settings

- **Error**: "Missing script references"
  - **Fix**: Use Unity Assistant: "Find and fix all missing script references"

### Build Too Large
- **Issue**: WebGL build > 100MB
  - **Fix**: Compress textures, audio, reduce quality settings

### Game Doesn't Load
- **Issue**: Blank screen in browser
  - **Fix**: Check browser console for errors, verify CORS settings

---

## 📊 Build Metrics

- **Build Time**: `___ minutes`
- **Build Size**: `___ MB`
- **Scenes Included**: `4`
- **Assets Count**: `___`
- **Scripts**: `___`

---

## 🎉 Success Criteria

- ✅ Build completes without errors
- ✅ Game runs in browser
- ✅ All core mechanics work
- ✅ Can complete a full playthrough
- ✅ Build committed to Version Control

---

## 📝 Notes

**AI Tools Used:**
- [ ] Unity Assistant
- [ ] GitHub Copilot
- [ ] Unity DevOps Version Control

**Build Date**: `___________`
**Built By**: `___________`
**Version**: `1.0.0`
**Platform**: `WebGL`

---

## 🔄 Version Control Commands

### Before Build
```bash
# Check status
git status

# Commit current work
git add .
git commit -m "Pre-build: All assets and scripts ready"

# Create build branch
git checkout -b build/webgl-v1.0
```

### After Build
```bash
# Add build files (if tracking builds)
git add Builds/WebGL/v1.0/

# Commit
git commit -m "WebGL Build v1.0 completed"

# Tag release
git tag -a v1.0.0 -m "First playable WebGL build"

# Push to remote
git push origin build/webgl-v1.0
git push origin v1.0.0
```

---

**Last Updated**: October 6, 2025
