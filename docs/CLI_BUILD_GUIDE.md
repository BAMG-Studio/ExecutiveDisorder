# Executive Disorder - Command Line Build Guide

## 🎯 Build Unity Game WITHOUT Opening Unity Editor

This guide shows you how to build **Executive Disorder** entirely from the command line using Unity's batch mode.

---

## ✅ **What You CAN Do (Command Line Only)**

- ✅ Build WebGL version
- ✅ Build Windows standalone
- ✅ Build Linux standalone
- ✅ Build macOS standalone
- ✅ Run automated builds
- ✅ Integrate with CI/CD pipelines
- ✅ Script the entire build process

---

## ⚙️ **Prerequisites**

### **1. Unity Must Be Installed**
You need Unity installed, but you **don't need to open the Editor UI**.

**Check Unity installation:**
```bash
# Windows (PowerShell)
Test-Path "C:\Program Files\Unity\Hub\Editor\*\Editor\Unity.exe"

# WSL/Linux (adjust path)
ls "/mnt/c/Program Files/Unity/Hub/Editor/"
```

### **2. Find Your Unity Version Path**
```bash
# List installed Unity versions
ls "/mnt/c/Program Files/Unity/Hub/Editor/"

# Example output:
# 2022.3.50f1/
# 6000.0.23f1/  (Unity 6)
```

**Update the path in build scripts:**
```bash
UNITY_PATH="/mnt/c/Program Files/Unity/Hub/Editor/6000.0.23f1/Editor/Unity.exe"
```

---

## 🚀 **Method 1: Use the Automated Build Script**

### **Step 1: Update Unity Path**

Edit `scripts/build-webgl.sh`:
```bash
# Find your Unity version first
ls "/mnt/c/Program Files/Unity/Hub/Editor/"

# Then update UNITY_PATH in the script
UNITY_PATH="/mnt/c/Program Files/Unity/Hub/Editor/YOUR_VERSION/Editor/Unity.exe"
```

### **Step 2: Make Script Executable**
```bash
cd /mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit
chmod +x scripts/build-webgl.sh
```

### **Step 3: Run Build**
```bash
./scripts/build-webgl.sh
```

**What happens:**
1. ✅ Checks Unity installation
2. ✅ Verifies project exists
3. ✅ Builds WebGL version (10-30 minutes)
4. ✅ Creates timestamped output folder
5. ✅ Generates build log

**Output location:**
```
Builds/WebGL/20251007_143022/
├── index.html
├── Build/
└── TemplateData/
```

---

## 🔧 **Method 2: Manual Command Line Build**

### **WebGL Build:**
```bash
# From WSL
"/mnt/c/Program Files/Unity/Hub/Editor/6000.0.23f1/Editor/Unity.exe" \
  -quit \
  -batchmode \
  -nographics \
  -projectPath "/mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/unity" \
  -buildTarget WebGL \
  -executeMethod BuildScript.BuildWebGL \
  -logFile "/mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/Builds/Logs/webgl_build.log"
```

### **Windows Standalone Build:**
```bash
"/mnt/c/Program Files/Unity/Hub/Editor/6000.0.23f1/Editor/Unity.exe" \
  -quit \
  -batchmode \
  -nographics \
  -projectPath "/mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/unity" \
  -buildTarget StandaloneWindows64 \
  -executeMethod BuildScript.BuildWindows \
  -logFile "/mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/Builds/Logs/windows_build.log"
```

### **Linux Build:**
```bash
"/mnt/c/Program Files/Unity/Hub/Editor/6000.0.23f1/Editor/Unity.exe" \
  -quit \
  -batchmode \
  -nographics \
  -projectPath "/mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/unity" \
  -buildTarget StandaloneLinux64 \
  -executeMethod BuildScript.BuildLinux \
  -logFile "/mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/Builds/Logs/linux_build.log"
```

---

## 📊 **Command Line Arguments Explained**

| Argument | Purpose |
|----------|---------|
| `-quit` | Exit Unity after build completes |
| `-batchmode` | Run without GUI (headless) |
| `-nographics` | Disable graphics device (faster) |
| `-projectPath` | Path to Unity project |
| `-buildTarget` | Platform to build for |
| `-executeMethod` | C# method to run |
| `-logFile` | Where to save build log |

---

## 🎨 **Advanced: Configure Build Settings via CLI**

### **Add Scenes to Build:**
```bash
"/mnt/c/Program Files/Unity/Hub/Editor/6000.0.23f1/Editor/Unity.exe" \
  -quit \
  -batchmode \
  -nographics \
  -projectPath "/mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/unity" \
  -executeMethod BuildScript.ConfigureBuildSettings \
  -scenes "Assets/Scenes/MainMenu.unity,Assets/Scenes/GamePlay.unity,Assets/Scenes/LoadingScreen.unity,Assets/Scenes/EndingCinematic.unity"
```

---

## 🤖 **CI/CD Integration (GitHub Actions)**

Create `.github/workflows/build.yml`:

```yaml
name: Build Unity Game

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - uses: game-ci/unity-builder@v4
      with:
        projectPath: unity
        targetPlatform: WebGL
        buildName: ExecutiveDisorder
        
    - uses: actions/upload-artifact@v3
      with:
        name: WebGL-Build
        path: build/WebGL
```

---

## 🧪 **Test Build Locally**

### **After WebGL Build Completes:**

```bash
# Navigate to build folder
cd Builds/WebGL/[latest-timestamp]

# Start local server
python3 -m http.server 8000

# Or use Node.js
npx http-server -p 8000
```

**Open in browser:** `http://localhost:8000`

---

## 📝 **Build Script (PowerShell Alternative)**

Create `scripts/build-webgl.ps1`:

```powershell
# Executive Disorder - WebGL Build (PowerShell)

$UnityPath = "C:\Program Files\Unity\Hub\Editor\6000.0.23f1\Editor\Unity.exe"
$ProjectPath = "C:\Users\POK28\source\repos\ExecutiveDisorderReplit\unity"
$BuildPath = "C:\Users\POK28\source\repos\ExecutiveDisorderReplit\Builds\WebGL\$(Get-Date -Format 'yyyyMMdd_HHmmss')"
$LogPath = "C:\Users\POK28\source\repos\ExecutiveDisorderReplit\Builds\Logs\build_$(Get-Date -Format 'yyyyMMdd_HHmmss').log"

Write-Host "🚀 Starting WebGL Build..." -ForegroundColor Green

& $UnityPath `
  -quit `
  -batchmode `
  -nographics `
  -projectPath $ProjectPath `
  -buildTarget WebGL `
  -executeMethod BuildScript.BuildWebGL `
  -logFile $LogPath

if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ BUILD SUCCESSFUL!" -ForegroundColor Green
    Write-Host "Output: $BuildPath"
} else {
    Write-Host "❌ BUILD FAILED!" -ForegroundColor Red
    Write-Host "Check log: $LogPath"
    exit 1
}
```

**Run in PowerShell:**
```powershell
.\scripts\build-webgl.ps1
```

---

## 🔍 **Monitor Build Progress**

### **Watch Log File (Live):**
```bash
# In WSL
tail -f /mnt/c/Users/POK28/source/repos/ExecutiveDisorderReplit/Builds/Logs/[latest-log].log
```

### **Check Build Status:**
```bash
# Check if Unity process is running
ps aux | grep Unity

# Check build output size
du -sh Builds/WebGL/*
```

---

## 🚫 **What You CANNOT Avoid**

Even with command-line builds:

1. **Unity Engine Required** - Must be installed (but don't need to open UI)
2. **Initial Setup** - Need Editor to configure scenes once
3. **Asset Processing** - Unity must process sprites, audio, etc.
4. **Platform Support** - Unity compiles platform-specific code

---

## ❌ **Non-Unity Alternatives (Complete Rewrites)**

If you want to **completely avoid Unity**, you'd need to:

### **Option A: Godot Engine**
- Open-source game engine
- GDScript or C#
- CLI build support
- Would require **complete game rewrite**

### **Option B: Web-Native (React/Phaser.js)**
- Pure JavaScript/TypeScript
- No game engine needed
- Would require **complete game rewrite**

### **Option C: Bevy (Rust)**
- Modern game framework
- Full CLI support
- Would require **complete game rewrite** in Rust

**Reality:** These would take **weeks/months** to rebuild what you have in Unity.

---

## 💡 **Recommended Approach**

### **Best of Both Worlds:**

1. **Initial Setup (One-Time, Unity Editor):**
   - Configure scenes
   - Set up build settings
   - Assign assets

2. **All Future Builds (Command Line):**
   ```bash
   ./scripts/build-webgl.sh
   ```

3. **Automate Everything:**
   - CI/CD pipeline
   - Scheduled builds
   - Version tagging

---

## 🎯 **Quick Start - Command Line Build**

```bash
# 1. Find Unity version
ls "/mnt/c/Program Files/Unity/Hub/Editor/"

# 2. Update build script
nano scripts/build-webgl.sh
# Change UNITY_PATH to your version

# 3. Make executable
chmod +x scripts/build-webgl.sh

# 4. Build!
./scripts/build-webgl.sh

# 5. Test
cd Builds/WebGL/[latest]/
python3 -m http.server 8000
# Open http://localhost:8000
```

---

## 📚 **Resources**

- [Unity Command Line Arguments](https://docs.unity3d.com/Manual/CommandLineArguments.html)
- [Unity Batch Mode](https://docs.unity3d.com/Manual/BatchMode.html)
- [Unity Build Pipeline](https://docs.unity3d.com/Manual/BuildPlayerPipeline.html)

---

## ✅ **Summary**

**Can you avoid Unity Editor UI?** ✅ YES
**Can you avoid Unity Engine completely?** ❌ NO

**You can:**
- Build entirely from terminal
- Automate builds with scripts
- Never open Unity Editor UI again (after initial setup)

**You cannot:**
- Build Unity game without Unity installed
- Use generic CLI tools (codex, etc.) alone
- Avoid Unity completely without rewriting the game

---

**Your project uses Unity, so embrace the command-line Unity workflow!** 🚀
