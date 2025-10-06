# Executive Disorder - Unity 6 Complete Game Package

## 🎮 Game Overview

**Executive Disorder™** is a satirical political decision-making game where you manage a nation through chaos, scandal, and absurd situations. Balance 4 critical resources, interact with 8 unique political characters, and navigate 100+ decision cards to reach one of 10 unique endings.

**Democracy: Optional. Chaos: Guaranteed.**

---

## 📦 Package Contents

This package contains a **complete, ready-to-play** Unity 6 game with:

- ✅ **Full Game Loop** - Start menu → Gameplay → Multiple endings
- ✅ **Resource Management System** - 4 core resources (Popularity, Stability, Media Trust, Economic Health)
- ✅ **Decision Card System** - 100+ unique cards with multiple choices
- ✅ **Character System** - 8 political archetypes with dialogue and loyalty
- ✅ **State Machine Architecture** - Clean, maintainable game states
- ✅ **UI System** - Complete menus, HUD, and feedback systems
- ✅ **Audio System** - Music, SFX, and audio mixing
- ✅ **Visual Effects** - Animations, particle effects, and shaders
- ✅ **Save/Load System** - Persistent game progress
- ✅ **Analytics & Events** - Game tracking and events
- ✅ **WebGL Ready** - Optimized for browser deployment

---

## 🚀 Quick Start

### Installation

1. **Open Unity Hub**
2. **Create New Project** (Unity 6.x, URP 3D Template)
3. **Import Package**:
   - Drag the entire `ExecutiveDisorder_Unity6_Complete` folder into your Unity project's Assets folder
   - OR use `Assets → Import Package → Custom Package` and select the .unitypackage file

### First Run

1. Open the scene: `Assets/ExecutiveDisorder_Unity6_Complete/Scenes/MainMenu.unity`
2. Press **Play** in Unity Editor
3. Click "Start New Game" to begin playing

### Building

**WebGL Build:**
```
File → Build Settings → WebGL → Build
```

**Desktop Build:**
```
File → Build Settings → PC, Mac & Linux Standalone → Build
```

---

## 📁 Folder Structure

```
ExecutiveDisorder_Unity6_Complete/
│
├── _Documentation/              # Game design docs, tutorials
│   ├── GameDesignDocument.md
│   ├── TechnicalArchitecture.md
│   ├── ContentGuide.md
│   └── BuildGuide.md
│
├── Scripts/                     # All C# game scripts
│   ├── Core/                   # Core game systems
│   │   ├── GameManager.cs
│   │   ├── GameStateManager.cs
│   │   ├── ResourceManager.cs
│   │   ├── EventManager.cs
│   │   └── SaveLoadManager.cs
│   ├── Cards/                  # Decision card system
│   │   ├── DecisionCard.cs
│   │   ├── CardManager.cs
│   │   ├── CardUI.cs
│   │   ├── CardDatabase.cs
│   │   └── CardEffectProcessor.cs
│   ├── Characters/             # Character system
│   │   ├── Character.cs
│   │   ├── CharacterManager.cs
│   │   ├── CharacterDatabase.cs
│   │   └── CharacterDialogue.cs
│   ├── UI/                     # User interface
│   │   ├── MainMenuController.cs
│   │   ├── GameHUDController.cs
│   │   ├── ResourceBarUI.cs
│   │   ├── CharacterPanelUI.cs
│   │   ├── NewsTickerUI.cs
│   │   └── EndingScreenUI.cs
│   ├── Audio/                  # Audio management
│   │   ├── AudioManager.cs
│   │   └── MusicController.cs
│   ├── VFX/                    # Visual effects
│   │   ├── ParticleController.cs
│   │   └── CameraShake.cs
│   └── Utilities/              # Helper scripts
│       ├── Singleton.cs
│       ├── TypewriterEffect.cs
│       ├── AnimationHelper.cs
│       └── Constants.cs
│
├── Prefabs/                     # Game object prefabs
│   ├── Managers/               # Manager prefabs
│   │   ├── GameManager.prefab
│   │   ├── UIManager.prefab
│   │   └── AudioManager.prefab
│   ├── UI/                     # UI prefabs
│   │   ├── MainMenu.prefab
│   │   ├── GameHUD.prefab
│   │   ├── DecisionCard.prefab
│   │   ├── CharacterCard.prefab
│   │   ├── ResourceBar.prefab
│   │   └── EndingScreen.prefab
│   ├── Characters/             # Character prefabs
│   │   ├── RexScalestonIII.prefab
│   │   ├── DonaldJExecutive.prefab
│   │   ├── POTUS9000.prefab
│   │   └── [... other characters]
│   └── VFX/                    # Effect prefabs
│       ├── ResourceGainEffect.prefab
│       ├── ResourceLossEffect.prefab
│       └── CrisisWarning.prefab
│
├── ScriptableObjects/           # Data assets
│   ├── Cards/                  # Card definitions
│   │   ├── Normal/             # Standard decision cards
│   │   ├── Crisis/             # Crisis cards
│   │   ├── Character/          # Character-specific cards
│   │   └── Absurd/             # Satirical cards
│   ├── Characters/             # Character data
│   │   └── [8 character assets]
│   ├── Endings/                # Ending scenarios
│   │   └── [10 ending assets]
│   ├── Resources/              # Resource definitions
│   └── GameSettings/           # Global settings
│
├── Scenes/                      # Unity scenes
│   ├── MainMenu.unity          # Main menu scene
│   ├── GamePlay.unity          # Main gameplay scene
│   ├── LoadingScreen.unity     # Loading screen
│   └── EndingCinematic.unity   # Ending sequence
│
├── Art/                         # Visual assets
│   ├── Sprites/                # 2D sprites
│   │   ├── Characters/         # Character portraits
│   │   ├── UI/                 # UI elements
│   │   ├── Icons/              # Resource/card icons
│   │   └── Backgrounds/        # Scene backgrounds
│   ├── Materials/              # Unity materials
│   ├── Textures/               # Textures
│   └── Fonts/                  # Custom fonts
│
├── Audio/                       # Sound assets
│   ├── Music/                  # Background music
│   │   ├── MainTheme.mp3
│   │   ├── GameplayLoop.mp3
│   │   └── EndingTheme.mp3
│   ├── SFX/                    # Sound effects
│   │   ├── CardFlip.wav
│   │   ├── ButtonClick.wav
│   │   ├── ResourceGain.wav
│   │   ├── ResourceLoss.wav
│   │   └── Crisis.wav
│   └── AudioMixer.mixer        # Audio mixing
│
├── Animations/                  # Animation files
│   ├── UI/                     # UI animations
│   └── Characters/             # Character animations
│
├── Resources/                   # Runtime resources
│   ├── Data/                   # JSON data files
│   │   ├── cards.json
│   │   ├── characters.json
│   │   └── endings.json
│   └── Localization/           # Language files
│
└── Settings/                    # Project settings
    ├── URP/                    # URP settings
    └── Input/                  # Input system
```

---

## 🎯 Core Game Systems

### 1. Resource Management
Four critical resources that determine game outcomes:
- **Popularity** (👥): Public approval rating
- **Stability** (🏛️): Government/institutional stability
- **Media Trust** (📺): Press relations
- **Economic Health** (💰): Economic indicators

All resources range from 0-100%. Game ends if any hits 0% or 100%.

### 2. Decision Cards
Over 100 unique cards across categories:
- **Normal**: Standard policy decisions
- **Crisis**: Urgent situations (timed)
- **Scandal**: Political controversies
- **Character**: Character-specific events
- **Absurd**: Satirical scenarios
- **Follow-up**: Consequence cards

### 3. Character System
8 Political Archetypes:
1. **Rex Scaleston III** - The Iguana King
2. **Donald J. Executive** - The 45th
3. **POTUS-9000** - Mascot Bot
4. **Alexandria Sanders-Warren** - Progressive
5. **Richard M. Moneybags III** - Corporate Lobbyist
6. **General James 'Ironside' Steel** - Military Hawk
7. **Diana Newsworthy** - Media Mogul
8. **Johnny Q. Public** - Populist

Each character has:
- Unique dialogue and personality
- Loyalty meter (affects gameplay)
- Special abilities
- Relationship dynamics

### 4. Endings System
10 Unique Endings based on resources and choices:
1. Democratic Victory
2. Autocratic Empire
3. Economic Collapse
4. Nuclear Winter
5. Alien Overlords
6. Impeachment
7. Military Coup
8. Media Revolution
9. Chaos Reigns
10. Time Loop Paradox (Secret)

---

## 🛠️ Technical Architecture

### State Machine
```
Main States:
├── MainMenuState
├── GameplayState
│   ├── CardSelectionSubstate
│   ├── DecisionMakingSubstate
│   ├── ConsequenceSubstate
│   └── CrisisSubstate
├── PauseState
└── EndingState
```

### Manager Pattern
All core systems use singleton managers:
- **GameManager**: Central game controller
- **ResourceManager**: Resource tracking
- **CardManager**: Card deck and dealing
- **CharacterManager**: Character interactions
- **UIManager**: UI coordination
- **AudioManager**: Sound control
- **SaveLoadManager**: Persistence
- **EventManager**: Event bus

### Data-Driven Design
All game content defined in ScriptableObjects:
- Easy to modify without code changes
- Designer-friendly
- Runtime efficient
- Version control friendly

---

## 🎨 Customization Guide

### Adding New Cards

1. Right-click in `ScriptableObjects/Cards/`
2. Create → Executive Disorder → Decision Card
3. Fill in:
   - Title and Description
   - Category and Rarity
   - Choices (2-4 options)
   - Resource effects for each choice
   - Requirements (optional)

### Adding Characters

1. Create character sprite in `Art/Sprites/Characters/`
2. Right-click in `ScriptableObjects/Characters/`
3. Create → Executive Disorder → Character
4. Configure:
   - Name and archetype
   - Dialogue lines
   - Stats and abilities
   - Sprite references

### Modifying Resources

Edit `ScriptableObjects/Resources/ResourceDefinitions.asset`:
- Change starting values
- Adjust thresholds
- Modify colors/icons
- Add new resource types

---

## 📊 Performance Optimization

### WebGL Optimization
- Compressed textures (ETC2/ASTC)
- Audio compression (Vorbis)
- Code stripping enabled
- Minimal asset bundle size
- Lazy loading for large assets

### Memory Management
- Object pooling for cards/UI
- Texture atlasing
- Audio streaming for music
- Async scene loading
- GC-friendly code patterns

---

## 🧪 Testing

### Play Mode Tests
Run in Unity Test Runner:
- Resource calculation tests
- Card effect tests
- Character loyalty tests
- Save/load tests
- State transition tests

### Manual Testing Checklist
- [ ] Start new game
- [ ] Make 10 decisions
- [ ] Test all character interactions
- [ ] Trigger at least one crisis
- [ ] Save and load game
- [ ] Reach an ending
- [ ] Test all UI screens
- [ ] Verify audio plays correctly

---

## 📝 Known Limitations & Future Enhancements

### Current Limitations
- Single player only
- English language only
- No cloud saves (local only)
- Limited accessibility features

### Planned Features
- [ ] Multiplayer mode
- [ ] Localization (ES, FR, DE, JP)
- [ ] Cloud save integration
- [ ] Achievement system
- [ ] Daily challenges
- [ ] Modding support
- [ ] Mobile build
- [ ] Accessibility improvements

---

## 🤝 Contributing

### Code Style
- Follow C# naming conventions
- Use XML documentation comments
- Keep methods under 20 lines
- Use regions for organization
- Comment complex logic

### Asset Guidelines
- Sprites: PNG, power-of-2 sizes
- Audio: 44.1kHz, compressed
- Textures: Max 2048x2048
- Models: Under 5000 triangles

---

## 📄 License

**Executive Disorder™** is licensed under MIT License.

See LICENSE file for details.

---

## 🙏 Credits

**Game Design & Development**: papaert-cloud
**Engine**: Unity 6
**Additional Tools**:
- TextMesh Pro (UI)
- DOTween (Animations)
- Universal Render Pipeline

---

## 📞 Support

**GitHub**: https://github.com/papaert-cloud/ExecutiveDisorder
**Issues**: Report bugs and feature requests on GitHub
**Email**: beaconagilelogistics@gmail.com

---

## 🎮 Enjoy the Game!

**Executive Disorder™**
*Democracy: Optional. Chaos: Guaranteed.*

---

**Version**: 1.0.0
**Unity Version**: Unity 6 (2023.x+)
**Last Updated**: October 2025

**Project Subdirectory**: unity
