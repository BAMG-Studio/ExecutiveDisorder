# Advanced Unity Packages - Quick Start
## Executive Disorder AI Integration

This document provides quick installation and usage instructions for the advanced Unity packages.

---

## 📦 Package Overview

| Package | Purpose | Status |
|---------|---------|--------|
| **ML-Agents** | Train AI agents with reinforcement learning | ✅ Configured |
| **Unity Sentis** | Run neural networks in Unity | ✅ Configured |
| **Addressables** | Dynamic asset loading & memory management | ✅ Configured |
| **AI Navigation** | Behavior trees and navigation | ✅ Configured |

---

## 🚀 Quick Installation

### Option 1: Automatic (Recommended)

1. **Copy the Unity Package folder** to your Unity project:
   ```
   Copy ExecutiveDisorder_Unity6_Complete/ to your project's Assets/ folder
   ```

2. **Replace manifest.json**:
   ```
   Copy Packages/manifest.json to YourProject/Packages/manifest.json
   ```

3. **Open Unity** - packages will install automatically

### Option 2: Manual Installation

Open Unity and install each package:

```
Window → Package Manager → + → Add package from git URL
```

Then add these URLs one by one:
- `com.unity.ml-agents` (ML-Agents)
- `com.unity.sentis` (Sentis)
- `com.unity.addressables` (Addressables)
- `com.unity.ai.navigation` (AI Navigation)

---

## 🐍 Python Setup (ML-Agents Only)

### Windows (PowerShell)
```powershell
cd ExecutiveDisorder_Unity6_Complete/ML-Agents
.\setup-mlagents.ps1
```

### Linux/Mac/WSL
```bash
cd ExecutiveDisorder_Unity6_Complete/ML-Agents
chmod +x setup-mlagents.sh
./setup-mlagents.sh
```

### Manual Setup
```bash
# Create virtual environment
python -m venv mlagents-env

# Activate (Windows)
.\mlagents-env\Scripts\activate

# Activate (Linux/Mac)
source mlagents-env/bin/activate

# Install packages
pip install mlagents==1.0.0
pip install torch torchvision
```

---

## 🎮 Usage Examples

### ML-Agents Training

1. **Attach PlayerSimulationAgent** to a GameObject in your scene

2. **Start Training**:
   ```bash
   # Activate Python environment
   source mlagents-env/bin/activate  # or .\mlagents-env\Scripts\activate on Windows
   
   # Start training
   mlagents-learn ML-Agents/ml-agents-config.yaml --run-id=ExecutiveDisorder_v1
   
   # Press Play in Unity when prompted
   ```

3. **Monitor Training**:
   ```bash
   tensorboard --logdir results
   # Open http://localhost:6006
   ```

### Sentis Inference

```csharp
// In your script
using ExecutiveDisorder.AI;

// Get predictions
float[] gameState = new float[] { /* current game state */ };
int prediction = SentisInferenceManager.Instance.PredictPlayerChoice(gameState, 3);

// Get probabilities
float[] probs = SentisInferenceManager.Instance.GetChoiceProbabilities(gameState, 3);
```

### Addressables Loading

```csharp
// In your script
using ExecutiveDisorder.Assets;

// Load a card
AddressableManager.Instance.LoadCard("Cards/Day1Card", (card) => {
    Debug.Log($"Loaded card: {card.cardTitle}");
});

// Load all characters
AddressableManager.Instance.LoadAllCharacters((characters) => {
    Debug.Log($"Loaded {characters.Count} characters");
});

// Load audio
AddressableManager.Instance.LoadAudio("Audio/BackgroundMusic", (clip) => {
    audioSource.clip = clip;
    audioSource.Play();
});
```

### AI Content Generation

```csharp
// In your script
using ExecutiveDisorder.AI;

// Generate a decision card
AIContentGenerator.Instance.GenerateCard(currentDay, (card) => {
    if (card != null) {
        // Use the generated card
        cardManager.PresentCard(card);
    }
});

// Generate dialogue
AIContentGenerator.Instance.GenerateCharacterDialogue(
    "Chief of Staff", 
    loyalty: 75, 
    context: "Budget crisis",
    (dialogue) => {
        Debug.Log($"Generated dialogue: {dialogue}");
    }
);
```

---

## 📁 File Structure

```
ExecutiveDisorder_Unity6_Complete/
├── Scripts/
│   ├── AI/
│   │   ├── AIAssetManager.cs          # AI API integrations
│   │   ├── AIContentGenerator.cs      # Procedural content
│   │   ├── PlayerSimulationAgent.cs   # ML-Agents integration
│   │   └── SentisInferenceManager.cs  # Neural network inference
│   ├── Assets/
│   │   └── AddressableManager.cs      # Asset loading
│   └── Core/
│       └── [Game managers...]
├── ML-Agents/
│   ├── ml-agents-config.yaml          # Training configuration
│   ├── setup-mlagents.ps1             # Windows setup
│   └── setup-mlagents.sh              # Linux/Mac setup
├── Packages/
│   └── manifest.json                  # Unity packages
└── _Documentation/
    └── PackageInstallationGuide.md    # Detailed guide
```

---

## ✅ Verification Checklist

After installation, verify everything works:

### Unity Editor
- [ ] Open Package Manager - all packages show "Installed"
- [ ] No compilation errors in Console
- [ ] Scripts folder contains AI/ and Assets/ directories

### ML-Agents (Optional)
- [ ] Python environment activates successfully
- [ ] `mlagents-learn --help` works
- [ ] TensorBoard accessible at localhost:6006

### Addressables
- [ ] Window → Asset Management → Addressables → Groups opens
- [ ] Addressables settings created

### Sentis
- [ ] Import a test ONNX model successfully
- [ ] No errors when SentisInferenceManager initializes

---

## 🔧 Troubleshooting

### "Package not found" error
- Ensure Unity version is 2023.2+ (Unity 6)
- Check internet connection
- Try adding packages manually via git URL

### ML-Agents Python errors
```bash
# Reinstall with specific versions
pip uninstall mlagents torch
pip install mlagents==1.0.0
pip install torch==2.0.1
```

### Addressables build fails
```
Window → Asset Management → Addressables → Groups
→ Build → New Build → Default Build Script
```

### Sentis model import fails
- Ensure ONNX model uses supported operations
- Check Sentis documentation for compatibility
- Try exporting model with opset version 9-15

---

## 📚 Next Steps

1. **Test ML-Agents**: Run a training session (see PackageInstallationGuide.md)
2. **Configure Addressables**: Set up asset groups for your content
3. **Train a model**: Use PlayerSimulationAgent to train an AI player
4. **Export to ONNX**: Convert trained model for Sentis inference

---

## 🆘 Support & Documentation

- **ML-Agents**: https://github.com/Unity-Technologies/ml-agents
- **Sentis**: https://docs.unity3d.com/Packages/com.unity.sentis@latest
- **Addressables**: https://docs.unity3d.com/Packages/com.unity.addressables@latest
- **Unity Forum**: https://forum.unity.com

---

**Last Updated**: October 6, 2025
**Compatible With**: Unity 6 (2023.2+)
