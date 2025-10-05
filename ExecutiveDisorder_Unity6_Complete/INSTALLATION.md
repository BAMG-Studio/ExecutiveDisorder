# Executive Disorder - Installation & Setup Guide

## 🎯 Quick Start (5 Minutes)

### For Unity Developers

1. **Copy the entire `ExecutiveDisorder_Unity6_Complete` folder into your Unity project's `Assets` folder**
2. **Open Unity** (Unity 6 / 2023.x or later)
3. **Open the Main Menu scene**: `Assets/ExecutiveDisorder_Unity6_Complete/Scenes/MainMenu.unity`
4. **Press Play** to test

That's it! The game is ready to play in the editor.

---

## 📋 Detailed Installation

### Step 1: Prerequisites

**Required:**
- Unity 6 (2023.x or later) - [Download Unity Hub](https://unity.com/download)
- 5 GB free disk space

**Recommended:**
- Visual Studio 2022 (for C# editing)
- Git (for version control)

**Unity Packages Required:**
The following packages should be installed automatically, but verify in Package Manager:
- Universal Render Pipeline (URP)
- TextMesh Pro
- Input System
- 2D Sprite

### Step 2: Import Package

**Option A: Direct Folder Copy**
```
1. Copy "ExecutiveDisorder_Unity6_Complete" folder
2. Paste into your Unity project's Assets folder
3. Unity will automatically import all files
```

**Option B: Unity Package**
```
1. Assets → Import Package → Custom Package
2. Select ExecutiveDisorder_Unity6_Complete.unitypackage
3. Click "Import"
```

**Option C: Git Clone**
```bash
cd YourUnityProject/Assets
git clone [repository-url] ExecutiveDisorder_Unity6_Complete
```

### Step 3: Install Dependencies

**DOTween (Animation Library)**

This package uses DOTween for smooth animations. You have two options:

**Option 1: DOTween (Free)**
1. Open Unity Package Manager
2. Window → Package Manager
3. Click "+" → Add package from git URL
4. Enter: `https://github.com/Demigiant/dotween.git`

**Option 2: DOTween Pro (Paid - Recommended)**
1. Purchase from Unity Asset Store
2. Download and import
3. More features and visual tools

**If you don't want to use DOTween:**
- Remove all `using DG.Tweening;` statements
- Replace `.DOScale()`, `.DOFade()` calls with manual coroutines
- Or use Unity's built-in `Animation` component

**TextMesh Pro**
1. Window → TextMeshPro → Import TMP Essential Resources
2. Window → TextMeshPro → Import TMP Examples & Extras (optional)

### Step 4: Configure Project Settings

**Quality Settings:**
```
Edit → Project Settings → Quality
- Set active quality level to "High" or "Ultra"
```

**Graphics Settings:**
```
Edit → Project Settings → Graphics
- Ensure URP is set as the render pipeline
- If not, assign the UniversalRenderPipelineAsset from Settings/URP/
```

**Player Settings:**
```
Edit → Project Settings → Player
- Company Name: [Your Company]
- Product Name: Executive Disorder
- Version: 1.0.0
```

**Input System:**
```
Edit → Project Settings → Player → Other Settings
- Active Input Handling: Both (or Input System Package)
```

### Step 5: Scene Setup

**Add Scenes to Build Settings:**
```
File → Build Settings

Add these scenes in order:
1. Assets/ExecutiveDisorder_Unity6_Complete/Scenes/MainMenu.unity
2. Assets/ExecutiveDisorder_Unity6_Complete/Scenes/GamePlay.unity
3. Assets/ExecutiveDisorder_Unity6_Complete/Scenes/EndingCinematic.unity
```

**Set Main Menu as Active Scene:**
```
Double-click: Assets/ExecutiveDisorder_Unity6_Complete/Scenes/MainMenu.unity
```

### Step 6: Verify Installation

**Check for Errors:**
1. Open Console: Window → General → Console
2. Should see no red errors
3. Yellow warnings are OK

**Test Play:**
1. Press Play button (▶️) in Unity Editor
2. Game should load to main menu
3. Click "Start New Game" to test gameplay

**Common Issues:**

❌ **"DOTween not found"**
- Install DOTween package (see Step 3)
- Or comment out DOTween code

❌ **"TextMeshPro resources not found"**
- Import TMP Essential Resources (see Step 3)

❌ **Missing sprites/assets**
- Ensure all files were imported
- Check folder structure matches README

❌ **Scripts won't compile**
- Check Unity version (needs 6+)
- Verify all files were copied
- Check for namespace conflicts

---

## 🎮 First Run

### Testing the Game

1. **Main Menu:**
   - Should see game title
   - "Start New Game" button
   - "Settings" button
   - "Quit" button

2. **Start New Game:**
   - Should transition to gameplay
   - See resource bars (Popularity, Stability, Media Trust, Economic Health)
   - First decision card appears
   - Day counter shows "Day 1"

3. **Make Decisions:**
   - Click on choice buttons
   - Resources should update
   - New card appears
   - Day advances

4. **Game Over:**
   - Trigger by depleting any resource to 0
   - Or reaching day 100
   - Should see ending screen
   - Can return to main menu or play again

---

## 🔧 Customization

### Changing Game Content

**Add New Cards:**
```
1. Right-click in ScriptableObjects/Cards/
2. Create → Executive Disorder → Decision Card
3. Fill in card details
4. Cards automatically load into game
```

**Add New Characters:**
```
1. Right-click in ScriptableObjects/Characters/
2. Create → Executive Disorder → Character
3. Configure character properties
4. Add portrait sprites in Art/Sprites/Characters/
```

**Modify Resources:**
```
1. Open: ScriptableObjects/Resources/ResourceDefinitions
2. Adjust starting values
3. Change colors/icons
```

**Change Settings:**
```
1. Open: ScriptableObjects/GameSettings
2. Modify:
   - Total game days
   - Resource thresholds
   - Difficulty settings
   - Auto-save interval
```

### Changing Visual Style

**Replace UI Assets:**
```
Art/Sprites/UI/
- Replace sprites with your own
- Keep same names or update references in prefabs
```

**Change Colors:**
```
Edit prefabs in Prefabs/UI/
- Modify Image components
- Update color schemes
```

**Custom Fonts:**
```
1. Import font files to Art/Fonts/
2. Create TextMesh Pro Font Asset
3. Update UI prefabs to use new font
```

---

## 📦 Building the Game

### Quick Build (WebGL)

```
1. File → Build Settings
2. Platform: WebGL
3. Click "Build"
4. Choose output folder
5. Wait for build (10-30 min)
```

### Desktop Build

```
1. File → Build Settings
2. Platform: PC, Mac & Linux Standalone
3. Click "Build"
4. Choose output folder
5. Wait for build (5-10 min)
```

**See `BuildGuide.md` for detailed build instructions**

---

## 🐛 Troubleshooting

### Installation Issues

**Problem: Can't find package in Unity**
```
Solution:
1. Ensure folder is in Assets/
2. Check Console for import errors
3. Reimport: Right-click folder → Reimport
```

**Problem: Scripts don't compile**
```
Solution:
1. Check Unity version (needs 6+)
2. Verify .NET version: Edit → Project Settings → Player → Other → API Compatibility Level (should be .NET Standard 2.1)
3. Delete Library folder and reopen project
```

**Problem: Missing references in scenes**
```
Solution:
1. Open problematic scene
2. Check Inspector for missing script components
3. Reassign scripts from Scripts/ folder
```

### Runtime Issues

**Problem: Game won't start**
```
Solution:
1. Check GameManager exists in scene
2. Verify all managers present (ResourceManager, CardManager, etc.)
3. Check Console for initialization errors
```

**Problem: Cards don't appear**
```
Solution:
1. Verify CardDatabase has cards assigned
2. Check Cards are in Resources/Cards/ folder
3. Ensure CardManager is initialized
```

**Problem: Resources don't update**
```
Solution:
1. Check ResourceManager exists
2. Verify ResourceBarUI components are assigned
3. Check event subscriptions in UIManager
```

---

## 💡 Tips for Success

1. **Save Often:** Unity can crash, save scenes regularly
2. **Use Version Control:** Git is your friend
3. **Test Frequently:** Play test after changes
4. **Read Documentation:** Check `_Documentation/` folder
5. **Customize Gradually:** Make small changes, test, repeat

---

## 📞 Getting Help

**Resources:**
- Game Design Document: `_Documentation/GameDesignDocument.md`
- Technical Architecture: `_Documentation/TechnicalArchitecture.md`
- Build Guide: `_Documentation/BuildGuide.md`
- Content Guide: `_Documentation/ContentGuide.md`

**Common Questions:**

**Q: Can I use this commercially?**
A: Check LICENSE file for usage terms

**Q: Can I modify the game?**
A: Yes! It's designed to be customizable

**Q: Do I need programming knowledge?**
A: Basic Unity knowledge helps, but ScriptableObjects allow content creation without coding

**Q: What if I find a bug?**
A: Check Console for errors, refer to troubleshooting section

**Q: Can I add multiplayer?**
A: Not included, but architecture supports extension

---

## ✅ Installation Checklist

- [ ] Unity 6 installed
- [ ] Project created or opened
- [ ] Package imported to Assets/
- [ ] DOTween installed (or code modified)
- [ ] TextMesh Pro resources imported
- [ ] Scenes added to Build Settings
- [ ] No errors in Console
- [ ] Test play successful
- [ ] Can start new game
- [ ] Cards appear and work
- [ ] Resources update correctly
- [ ] Can reach an ending

**If all checked, you're ready to go! 🎉**

---

## 🎓 Next Steps

1. **Play Through:** Complete one full playthrough (100 days)
2. **Explore Content:** Check out different cards and endings
3. **Customize:** Add your own cards and characters
4. **Build:** Create a WebGL or desktop build
5. **Share:** Deploy and share with others!

---

**Version:** 1.0  
**Last Updated:** October 2025  
**Installation Time:** ~15 minutes  
**Support:** See documentation folder for help
