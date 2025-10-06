# 🚀 Complete Installation Checklist
## Executive Disorder Unity 6 - AI Integration

Use this checklist to ensure all packages and features are properly installed.

---

## ✅ Pre-Installation

- [ ] Unity 2023.2 or later (Unity 6) installed
- [ ] Python 3.9 or 3.10 installed (for ML-Agents)
- [ ] Git installed (optional, for version control)
- [ ] At least 5GB free disk space
- [ ] Internet connection available

---

## 📦 Unity Package Installation

### Method 1: Automatic (Recommended)

- [ ] Locate `ExecutiveDisorder_Unity6_Complete/Packages/manifest.json`
- [ ] Back up your existing `YourProject/Packages/manifest.json`
- [ ] Replace with new `manifest.json` file
- [ ] Open Unity Editor
- [ ] Wait for package compilation (5-10 minutes)
- [ ] Verify no errors in Console

### Method 2: Manual Installation

**ML-Agents:**
- [ ] Open `Window → Package Manager`
- [ ] Click `+` → `Add package from git URL`
- [ ] Enter: `com.unity.ml-agents`
- [ ] Wait for installation

**Sentis:**
- [ ] Package Manager → `+` → `Add package from git URL`
- [ ] Enter: `com.unity.sentis`
- [ ] Verify installation

**Addressables:**
- [ ] Package Manager → Search "Addressables"
- [ ] Click Install
- [ ] Accept dependencies

**AI Navigation:**
- [ ] Package Manager → Search "AI Navigation"
- [ ] Click Install

### Verification

- [ ] Open `Window → Package Manager`
- [ ] All packages show status "Installed"
- [ ] No red errors in Console
- [ ] Package list includes:
  - [ ] ML-Agents (3.0.0+)
  - [ ] Sentis (1.4.0+)
  - [ ] Addressables (1.21.19+)
  - [ ] AI Navigation (2.0.0+)
  - [ ] Input System (1.7.0+)
  - [ ] Universal RP (16.0.3+)

---

## 📁 Script Installation

### Copy Scripts to Project

- [ ] Copy `Scripts/` folder to `Assets/ExecutiveDisorder/Scripts/`
- [ ] Verify folder structure:
  ```
  Assets/ExecutiveDisorder/
  ├── Scripts/
  │   ├── AI/
  │   │   ├── AIAssetManager.cs
  │   │   ├── AIContentGenerator.cs
  │   │   ├── PlayerSimulationAgent.cs
  │   │   └── SentisInferenceManager.cs
  │   ├── Assets/
  │   │   └── AddressableManager.cs
  │   ├── Audio/
  │   ├── Cards/
  │   ├── Characters/
  │   ├── Core/
  │   ├── UI/
  │   └── Utilities/
  ```

### Verify Compilation

- [ ] Check Unity Console - no compilation errors
- [ ] All scripts show green checkmark in Project window
- [ ] Namespaces properly recognized:
  - [ ] `ExecutiveDisorder.AI`
  - [ ] `ExecutiveDisorder.Assets`
  - [ ] `ExecutiveDisorder.Core`

---

## 🐍 Python Environment Setup (ML-Agents)

### Windows Installation

- [ ] Open PowerShell as Administrator
- [ ] Navigate to `ExecutiveDisorder_Unity6_Complete/ML-Agents/`
- [ ] Run: `.\setup-mlagents.ps1`
- [ ] Wait for installation (10-15 minutes)
- [ ] Verify success message appears
- [ ] Test command: `.\mlagents-env\Scripts\activate`
- [ ] Test command: `mlagents-learn --help`
- [ ] Should see ML-Agents help output

### Linux/Mac/WSL Installation

- [ ] Open Terminal
- [ ] Navigate to `ExecutiveDisorder_Unity6_Complete/ML-Agents/`
- [ ] Make executable: `chmod +x setup-mlagents.sh`
- [ ] Run: `./setup-mlagents.sh`
- [ ] Wait for installation (10-15 minutes)
- [ ] Verify success message appears
- [ ] Test command: `source mlagents-env/bin/activate`
- [ ] Test command: `mlagents-learn --help`
- [ ] Should see ML-Agents help output

### Manual Python Setup (if scripts fail)

- [ ] Create venv: `python -m venv mlagents-env`
- [ ] Activate (Windows): `.\mlagents-env\Scripts\activate`
- [ ] Activate (Linux/Mac): `source mlagents-env/bin/activate`
- [ ] Install: `pip install -r requirements.txt`
- [ ] Verify: `mlagents-learn --help`

---

## 🎮 Addressables Setup

### Initialize Addressables

- [ ] Go to `Window → Asset Management → Addressables → Groups`
- [ ] Click "Create Addressables Settings"
- [ ] Verify window opens without errors

### Create Asset Groups

- [ ] Right-click in Groups window → `Create New Group → Packed Assets`
- [ ] Create group: "Cards"
- [ ] Create group: "Characters"  
- [ ] Create group: "Audio"
- [ ] Create group: "UI"

### Mark Assets as Addressable

- [ ] Select a test asset (ScriptableObject or Sprite)
- [ ] Check "Addressable" in Inspector
- [ ] Assign to appropriate group
- [ ] Verify asset appears in Addressables Groups window

### Build Addressables

- [ ] In Addressables Groups window:
- [ ] Click `Build → New Build → Default Build Script`
- [ ] Wait for build completion
- [ ] Check for "Build completed successfully" message

---

## 🧠 ML-Agents Integration

### Scene Setup

- [ ] Create new scene or open existing
- [ ] Create empty GameObject: "ML-Agents"
- [ ] Add component: `PlayerSimulationAgent`
- [ ] Assign references in Inspector:
  - [ ] GameManager
  - [ ] ResourceManager
  - [ ] CharacterManager
  - [ ] CardManager

### Training Configuration

- [ ] Locate `ML-Agents/ml-agents-config.yaml`
- [ ] Review configuration settings
- [ ] Adjust hyperparameters if needed (optional)

### Test Training Session

- [ ] Activate Python environment
- [ ] Navigate to ML-Agents folder
- [ ] Run: `./train.sh` (or `.\train.ps1` on Windows)
- [ ] Wait for "Start training by pressing the Play button"
- [ ] Press Play in Unity Editor
- [ ] Verify training starts:
  - [ ] Step counter increments
  - [ ] No Python errors
  - [ ] Unity runs without crashes

### Monitor with TensorBoard

- [ ] Open new terminal
- [ ] Activate Python environment
- [ ] Run: `tensorboard --logdir results`
- [ ] Open browser: `http://localhost:6006`
- [ ] Verify graphs appear

---

## 🔮 Sentis Setup

### Verify Sentis Installation

- [ ] Create test scene
- [ ] Create empty GameObject: "SentisTest"
- [ ] Add component: `SentisInferenceManager`
- [ ] Check Console - no errors on Play

### Import Test Model (Optional)

- [ ] Download sample ONNX model
- [ ] Drag to Unity Project window
- [ ] Select model in Inspector
- [ ] Verify "Model Asset" appears
- [ ] Assign to SentisInferenceManager

---

## 🎨 AI Content Generator Setup

### Initialize Generator

- [ ] Create empty GameObject: "AI Systems"
- [ ] Add component: `AIContentGenerator`
- [ ] Add component: `AIAssetManager`

### Configure API Keys (Optional)

For AI-powered content generation:

- [ ] Set environment variable: `OPENAI_API_KEY`
- [ ] Set environment variable: `STABILITY_API_KEY`  
- [ ] Set environment variable: `ELEVENLABS_API_KEY`

Or configure in Unity:
- [ ] Select AIAssetManager GameObject
- [ ] Enter API keys in Inspector
- [ ] Click Apply

### Test Content Generation

- [ ] Enter Play mode
- [ ] Open Console
- [ ] Look for "AIContentGenerator initialized" message
- [ ] Test card generation (via script or menu)

---

## 🧪 Final Verification

### Core Systems Test

- [ ] Create new scene: "IntegrationTest"
- [ ] Add GameObjects with managers:
  - [ ] GameManager
  - [ ] ResourceManager
  - [ ] CardManager
  - [ ] CharacterManager
  - [ ] EventManager
  - [ ] UIManager
  - [ ] SaveLoadManager
  - [ ] AudioManager

### AI Systems Test

- [ ] Add AI GameObjects:
  - [ ] AIAssetManager
  - [ ] AIContentGenerator
  - [ ] PlayerSimulationAgent
  - [ ] SentisInferenceManager
  - [ ] AddressableManager

### Run Complete Test

- [ ] Press Play in Unity
- [ ] Wait 30 seconds
- [ ] Check Console for errors
- [ ] Verify all managers initialize
- [ ] Test basic functionality:
  - [ ] Resource changes work
  - [ ] Cards can be presented
  - [ ] Save/Load functions
  - [ ] Addressables load successfully

---

## 📊 Performance Check

### Frame Rate

- [ ] Enable Stats window: `Window → Analysis → Stats`
- [ ] Play mode FPS > 30
- [ ] No major frame drops
- [ ] Memory usage stable

### Loading Times

- [ ] Addressable assets load < 1 second
- [ ] Scene loads < 5 seconds
- [ ] No stuttering during gameplay

---

## 📚 Documentation Review

### Required Reading

- [ ] Read: `PACKAGES_README.md`
- [ ] Read: `AI_INSTALLATION_SUMMARY.md`
- [ ] Review: `_Documentation/PackageInstallationGuide.md`
- [ ] Bookmark: `FILE_INDEX.md`

### Optional Deep Dives

- [ ] `_Documentation/TechnicalArchitecture.md`
- [ ] `_Documentation/GameDesignDocument.md`
- [ ] `_Documentation/BuildGuide.md`

---

## 🚨 Troubleshooting

If you encounter issues:

### Package Installation Failed
- [ ] Check Unity version (must be 2023.2+)
- [ ] Clear package cache: `~/Library/Unity/Asset Store-5.x` (Mac) or `%APPDATA%\Unity\Asset Store-5.x` (Windows)
- [ ] Delete `Library/` folder and reopen project
- [ ] Try manual package installation

### Python Environment Issues
- [ ] Verify Python version: `python --version` (should be 3.9 or 3.10)
- [ ] Reinstall packages: `pip install --force-reinstall mlagents`
- [ ] Try different Python version
- [ ] Check antivirus isn't blocking installation

### Compilation Errors
- [ ] Check all namespaces are correct
- [ ] Verify all required packages installed
- [ ] Reimport scripts: Right-click `Scripts/` → `Reimport`
- [ ] Restart Unity Editor

### ML-Agents Training Won't Start
- [ ] Verify Python environment activated
- [ ] Check config file syntax: `ml-agents-config.yaml`
- [ ] Ensure behavior name matches in script and config
- [ ] Check firewall isn't blocking port 5005

---

## ✅ Installation Complete!

### When ALL checkboxes are checked:

**Congratulations! 🎉**

You now have:
- ✅ All Unity packages installed
- ✅ All scripts compiled
- ✅ Python environment configured
- ✅ ML-Agents ready for training
- ✅ Addressables system operational
- ✅ AI content generation available
- ✅ Neural network inference ready

### Next Steps:

1. **Start Creating Content**
   - Design decision cards
   - Create character data
   - Import art and audio assets

2. **Train Your First Agent**
   - Run training session
   - Monitor progress in TensorBoard
   - Export trained model

3. **Build and Deploy**
   - Follow `BuildGuide.md`
   - Test WebGL build
   - Deploy to itch.io or web hosting

---

**Installation Date:** _________________  
**Unity Version:** _________________  
**Installer Name:** _________________  

**Status:** □ In Progress  □ Complete  □ Issues Found

**Notes:**
_________________________________________________________________
_________________________________________________________________
_________________________________________________________________

---

*Save this checklist for future reference*
