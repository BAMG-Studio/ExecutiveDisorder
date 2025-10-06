# Unity Package Installation Guide
## Executive Disorder - AI and Advanced Features

This guide covers the installation of advanced Unity packages for AI integration, neural networks, behavior trees, and asset management.

---

## Required Packages

### 1. ML-Agents (Machine Learning Agents)
**Purpose:** Train intelligent agents using reinforcement learning

**Installation Methods:**

#### Method A: Package Manager (Recommended)
1. Open Unity Editor
2. Go to `Window → Package Manager`
3. Click the `+` button (top-left)
4. Select `Add package from git URL`
5. Enter: `com.unity.ml-agents`
6. Click `Add`

#### Method B: Manual manifest.json
Add to `Packages/manifest.json`:
```json
{
  "dependencies": {
    "com.unity.ml-agents": "3.0.0"
  }
}
```

**Post-Installation:**
- Install Python ML-Agents toolkit: `pip install mlagents`
- Verify installation: Check for `ML-Agents` in Package Manager

**Documentation:** https://github.com/Unity-Technologies/ml-agents

---

### 2. Unity Sentis
**Purpose:** Run neural network models directly in Unity (inference)

**Installation Methods:**

#### Package Manager
1. Open `Window → Package Manager`
2. Click `+` → `Add package from git URL`
3. Enter: `com.unity.sentis`
4. Click `Add`

#### Manual manifest.json
```json
{
  "dependencies": {
    "com.unity.sentis": "1.4.0"
  }
}
```

**Features:**
- ONNX model support
- GPU acceleration
- Real-time inference
- Neural network execution without Python

**Use Cases for Executive Disorder:**
- Player behavior prediction
- Dynamic difficulty adjustment
- Content recommendation
- Sentiment analysis for dialogue

**Documentation:** https://docs.unity3d.com/Packages/com.unity.sentis@latest

---

### 3. Unity Behavior (Behavior Trees / Muse Behavior)
**Purpose:** Visual behavior tree creation for AI decision-making

**Installation Methods:**

#### Package Manager
1. Open `Window → Package Manager`
2. Toggle to `Unity Registry`
3. Search for `Behavior` or `Muse Behavior`
4. Click `Install`

**Alternative:** If using AI Navigation/Behavior package:
```json
{
  "dependencies": {
    "com.unity.ai.navigation": "2.0.0"
  }
}
```

**Features:**
- Visual behavior tree editor
- Reusable behavior graphs
- Blackboard system for shared data
- Event-driven AI

**Use Cases for Executive Disorder:**
- NPC advisor behavior
- Character response systems
- Event trigger logic
- Dynamic narrative branching

**Documentation:** https://docs.unity3d.com/Manual/com.unity.muse.behavior.html

---

### 4. Addressables
**Purpose:** Advanced asset management and dynamic content loading

**Installation Methods:**

#### Package Manager (Recommended)
1. Open `Window → Package Manager`
2. Search for `Addressables`
3. Click `Install`

#### Manual manifest.json
```json
{
  "dependencies": {
    "com.unity.addressables": "1.21.19"
  }
}
```

**Post-Installation Setup:**
1. Go to `Window → Asset Management → Addressables → Groups`
2. Click `Create Addressables Settings`
3. Organize assets into groups

**Features:**
- Async asset loading
- Memory management
- Content updates without rebuilding
- Remote asset hosting
- Build-time optimization

**Use Cases for Executive Disorder:**
- Dynamic card loading
- Character portrait streaming
- Audio asset management
- Localization content
- DLC and expansion content

**Documentation:** https://docs.unity3d.com/Packages/com.unity.addressables@latest

---

## Complete Package Manifest

Add all packages at once by editing `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.unity.addressables": "1.21.19",
    "com.unity.ai.navigation": "2.0.0",
    "com.unity.collab-proxy": "2.0.5",
    "com.unity.feature.development": "1.0.1",
    "com.unity.inputsystem": "1.7.0",
    "com.unity.ml-agents": "3.0.0",
    "com.unity.render-pipelines.universal": "16.0.3",
    "com.unity.sentis": "1.4.0",
    "com.unity.textmeshpro": "3.0.6",
    "com.unity.timeline": "1.8.2",
    "com.unity.ugui": "2.0.0",
    "com.unity.visualscripting": "1.9.1",
    "com.unity.modules.ai": "1.0.0",
    "com.unity.modules.androidjni": "1.0.0",
    "com.unity.modules.animation": "1.0.0",
    "com.unity.modules.assetbundle": "1.0.0",
    "com.unity.modules.audio": "1.0.0",
    "com.unity.modules.cloth": "1.0.0",
    "com.unity.modules.director": "1.0.0",
    "com.unity.modules.imageconversion": "1.0.0",
    "com.unity.modules.imgui": "1.0.0",
    "com.unity.modules.jsonserialize": "1.0.0",
    "com.unity.modules.particlesystem": "1.0.0",
    "com.unity.modules.physics": "1.0.0",
    "com.unity.modules.physics2d": "1.0.0",
    "com.unity.modules.screencapture": "1.0.0",
    "com.unity.modules.terrain": "1.0.0",
    "com.unity.modules.terrainphysics": "1.0.0",
    "com.unity.modules.tilemap": "1.0.0",
    "com.unity.modules.ui": "1.0.0",
    "com.unity.modules.uielements": "1.0.0",
    "com.unity.modules.umbra": "1.0.0",
    "com.unity.modules.unityanalytics": "1.0.0",
    "com.unity.modules.unitywebrequest": "1.0.0",
    "com.unity.modules.unitywebrequestassetbundle": "1.0.0",
    "com.unity.modules.unitywebrequestaudio": "1.0.0",
    "com.unity.modules.unitywebrequesttexture": "1.0.0",
    "com.unity.modules.unitywebrequestwww": "1.0.0",
    "com.unity.modules.vehicles": "1.0.0",
    "com.unity.modules.video": "1.0.0",
    "com.unity.modules.vr": "1.0.0",
    "com.unity.modules.wind": "1.0.0",
    "com.unity.modules.xr": "1.0.0"
  }
}
```

---

## Python Setup for ML-Agents

### Prerequisites
- Python 3.9 - 3.10 (recommended)
- pip package manager

### Installation Commands

#### Windows (PowerShell/CMD)
```bash
# Create virtual environment
python -m venv mlagents-env

# Activate environment
.\mlagents-env\Scripts\activate

# Install ML-Agents
pip install mlagents==1.0.0
pip install torch torchvision

# Verify installation
mlagents-learn --help
```

#### Linux/Mac/WSL
```bash
# Create virtual environment
python3 -m venv mlagents-env

# Activate environment
source mlagents-env/bin/activate

# Install ML-Agents
pip install mlagents==1.0.0
pip install torch torchvision

# Verify installation
mlagents-learn --help
```

---

## Integration Steps

### 1. ML-Agents Integration
1. Attach `PlayerSimulationAgent.cs` to a GameObject
2. Create training configuration file (see `ml-agents-config.yaml`)
3. Run training: `mlagents-learn config.yaml --run-id=ExecutiveDisorder_v1`

### 2. Sentis Integration
1. Export your trained ML-Agents model to ONNX format
2. Import ONNX file to Unity
3. Use `ModelAsset` and `Worker` classes to run inference
4. See `SentisInferenceManager.cs` for implementation

### 3. Addressables Setup
1. Mark assets as "Addressable" in Inspector
2. Create Addressable Groups: `Window → Asset Management → Addressables → Groups`
3. Organize by content type:
   - `Cards` - Decision card ScriptableObjects
   - `Characters` - Character data and portraits
   - `Audio` - Music and sound effects
   - `UI` - UI sprites and prefabs

### 4. Behavior Trees Setup
1. Create behavior tree assets: `Right-click → Create → Behavior Graph`
2. Design AI advisor responses
3. Attach to character GameObjects
4. Use blackboard for shared game state

---

## Troubleshooting

### ML-Agents Issues
**Problem:** "No module named mlagents"
**Solution:** Activate Python virtual environment and reinstall

**Problem:** Training doesn't start
**Solution:** Check YAML config file syntax and Unity console for errors

### Sentis Issues
**Problem:** ONNX model import fails
**Solution:** Ensure model uses supported operations (check Sentis docs)

**Problem:** Slow inference
**Solution:** Use GPU backend: `worker = new Worker(model, BackendType.GPUCompute)`

### Addressables Issues
**Problem:** Assets not loading
**Solution:** Build Addressables: `Window → Asset Management → Addressables → Build → New Build → Default Build Script`

**Problem:** Build size too large
**Solution:** Split content into remote groups and host on CDN

### Behavior Trees Issues
**Problem:** Package not found
**Solution:** Ensure Unity version is 2022.2+ for Muse Behavior

---

## Performance Recommendations

1. **ML-Agents Training:**
   - Train on GPU if available
   - Use curriculum learning for complex behaviors
   - Start with simple reward functions

2. **Sentis Inference:**
   - Batch multiple predictions
   - Use quantized models for mobile
   - Cache frequently used predictions

3. **Addressables:**
   - Load assets asynchronously
   - Unload unused assets promptly
   - Use labels for batch loading

4. **Behavior Trees:**
   - Keep trees shallow (max 5-6 levels)
   - Use decorators for efficiency
   - Cache expensive evaluations

---

## Next Steps After Installation

1. ✅ Verify all packages installed successfully
2. ✅ Set up Python environment for ML-Agents
3. ✅ Configure Addressables groups
4. ✅ Test PlayerSimulationAgent in Play mode
5. ✅ Create behavior trees for character AI
6. ✅ Build and test Addressables content

---

## Additional Resources

- **ML-Agents:** https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Readme.md
- **Sentis:** https://docs.unity3d.com/Packages/com.unity.sentis@latest
- **Addressables:** https://learn.unity.com/tutorial/addressables-introduction
- **Behavior:** https://docs.unity3d.com/Manual/com.unity.muse.behavior.html

---

**Last Updated:** October 6, 2025
**Unity Version:** 2023.2+ (Unity 6)
**Package Versions:** Latest stable releases
