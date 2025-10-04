# 🎮 Executive Disorder - Complete Workspace Digest

**Generated:** December 2024  
**Repository:** `git@github.com:papaert-cloud/ExecutiveDisorder.git`  
**Location:** `C:\Users\POK28\source\repos\ExecutiveDisorderReplit`

---

## 📊 Project Overview

**Executive Disorder™** is a satirical political decision-making game with **TWO COMPLETE IMPLEMENTATIONS**:

1. **Unity 6 WebGL Version** - Browser-based with Flask backend
2. **.NET 9 Console Version** - Desktop terminal game

Both versions share the same core game concept: manage 4 resources, interact with 8 political characters, make decisions from 101+ cards, and reach one of 10 unique endings.

---

## 🏗️ Repository Structure

```
ExecutiveDisorderReplit/
│
├── 🎮 UNITY 6 WEBGL GAME (Primary Implementation)
│   ├── Assets/                          # Unity game assets
│   │   ├── Art/                        # Sprites, UI graphics, character art
│   │   ├── Audio/                      # Sound effects, music, mixer
│   │   ├── Data/                       # ScriptableObjects for game data
│   │   │   ├── Characters/            # Character definitions
│   │   │   ├── DecisionCards/         # Card definitions
│   │   │   ├── Effects/               # Effect definitions
│   │   │   ├── Endings/               # Ending scenarios
│   │   │   └── MediaReactions/        # Media response data
│   │   ├── Materials/                  # Unity materials
│   │   ├── Plugins/                    # DOTween and other plugins
│   │   ├── Prefabs/                    # UI prefabs, game objects
│   │   ├── Resources/                  # Runtime loadable assets
│   │   ├── Scenes/                     # MainMenu.unity
│   │   ├── Scripts/                    # C# Unity scripts
│   │   │   ├── Cards/                 # Card UI and logic
│   │   │   ├── Characters/            # Character system
│   │   │   ├── Managers/              # Game managers
│   │   │   │   ├── AudioManager.cs
│   │   │   │   ├── CharacterManager.cs
│   │   │   │   ├── DecisionsManager.cs
│   │   │   │   ├── EffectManager.cs
│   │   │   │   ├── GameAssetsManager.cs
│   │   │   │   ├── HistoryManager.cs
│   │   │   │   ├── NewsHeadlineManager.cs
│   │   │   │   ├── NewsManager.cs
│   │   │   │   ├── ReactionsManager.cs
│   │   │   │   └── ResourcesManager.cs
│   │   │   ├── StateManager/          # State machine
│   │   │   │   ├── MainStates/       # Main game states
│   │   │   │   ├── Substates/        # Sub-states
│   │   │   │   ├── MainStateController.cs
│   │   │   │   ├── StateManager.cs
│   │   │   │   └── SubstateController.cs
│   │   │   ├── UI/                    # UI controllers
│   │   │   └── Utils/                 # Utility scripts
│   │   ├── Settings/                   # URP settings, build profiles
│   │   ├── Shaders/                    # Custom shaders
│   │   ├── TextMesh Pro/               # TMP assets
│   │   ├── cardsjson.json             # Card data (JSON)
│   │   ├── charactersjson.json        # Character data (JSON)
│   │   ├── endingjson.json            # Ending data (JSON)
│   │   └── download.json              # Config data
│   ├── Packages/                       # Unity packages
│   ├── ProjectSettings/                # Unity project configuration
│   └── ExecutiveDisord/                # WebGL build output
│       ├── Build/                     # Compiled WebGL files
│       │   ├── ExecutiveDisord.data
│       │   ├── ExecutiveDisord.framework.js
│       │   ├── ExecutiveDisord.loader.js
│       │   └── ExecutiveDisord.wasm
│       ├── TemplateData/              # WebGL template assets
│       └── index.html                 # WebGL entry point
│
├── 🖥️ .NET 9 CONSOLE GAME (Secondary Implementation)
│   ├── ExecutiveDisorder.Core/         # Core game logic library
│   │   ├── Cards/
│   │   │   └── DecisionCard.cs        # Card model with choices
│   │   ├── Characters/
│   │   │   └── Character.cs           # Character model with dialogue
│   │   ├── Data/
│   │   │   ├── CardDatabase.cs        # 101 decision cards
│   │   │   └── CharacterDatabase.cs   # 8 political characters
│   │   ├── Entity/
│   │   │   └── GameObject.cs          # Base game entity
│   │   ├── Models/
│   │   │   └── Resource.cs            # Resource model (4 types)
│   │   ├── State/
│   │   │   └── GameState.cs           # Game state management
│   │   ├── Systems/
│   │   │   ├── ConsequenceEngine.cs   # Decision processing
│   │   │   └── ResourceManager.cs     # Resource tracking
│   │   └── ExecutiveDisorder.Core.csproj
│   ├── ExecutiveDisorder.Engine/       # Game engine
│   │   ├── GameEngine.cs              # Main engine logic
│   │   └── ExecutiveDisorder.Engine.csproj
│   ├── ExecutiveDisorder.Game/         # Main executable
│   │   ├── Program.cs                 # Console UI (600+ lines)
│   │   └── ExecutiveDisorder.Game.csproj
│   ├── ExecutiveDisorder.Tests/        # Unit tests (placeholder)
│   └── ExecutiveDisorder.sln           # Visual Studio solution
│
├── 🐳 BACKEND & INFRASTRUCTURE
│   ├── app/                            # Flask backend (planned)
│   │   ├── Dockerfile                 # Python 3.11 container
│   │   └── requirements.txt           # Flask, gunicorn, cryptography
│   ├── docs/                           # Documentation
│   │   ├── github-secrets-setup.md
│   │   └── repo-sync-setup.md
│   ├── scripts/                        # Utility scripts
│   │   ├── bootstrap.sh
│   │   └── sync-repos.sh              # Git sync automation
│   └── .infracost/                     # Infrastructure cost analysis
│
├── 📄 DOCUMENTATION
│   ├── README.md                       # Main project readme
│   ├── IMPLEMENTATION_STATUS.md        # .NET console game status
│   ├── ORGANIZATION_SUMMARY.md         # Repository organization
│   ├── UNITY_PROJECT.md                # Unity project details
│   └── WORKSPACE_DIGEST.md             # This file
│
└── ⚙️ CONFIGURATION
    ├── .editorconfig                   # Code formatting
    ├── .gitignore                      # Git ignore patterns
    ├── .gitignore-unity                # Unity-specific ignores
    ├── .vsconfig                       # Visual Studio setup
    └── get-docker.sh                   # Docker installation
```

---

## 🎯 Implementation Comparison

### Unity 6 WebGL Version

**Status:** ✅ **LIVE & DEPLOYED**

**Technology Stack:**
- Unity 6 (2023.x)
- C# for Unity scripting
- Universal Render Pipeline (URP)
- TextMesh Pro for UI
- DOTween for animations
- WebGL build target

**Features:**
- ✅ Interactive browser-based gameplay
- ✅ Rich visual UI with animations
- ✅ Character portraits and artwork
- ✅ Sound effects and audio
- ✅ State machine architecture
- ✅ ScriptableObject-based data
- ✅ News ticker and media reactions
- ✅ Character relationship system
- ✅ Visual resource bars with effects
- ✅ Crisis warnings and visual feedback
- ✅ Multiple endings with cinematics

**Deployment:**
- WebGL build in `ExecutiveDisord/Build/`
- Hosted on Replit (mentioned in analysis)
- Flask backend for user accounts (planned)
- PostgreSQL database (planned)

**Key Scripts:**
- **Managers:** 10 manager classes for different systems
- **State Machine:** MainStateController + SubstateController
- **UI Controllers:** 20+ UI component scripts
- **Card System:** BaseDecisionCardUI, DecisionCardUI, CrisisCardUI
- **Character System:** CharacterManager, CharacterCardUI

---

### .NET 9 Console Version

**Status:** ✅ **COMPLETE & PLAYABLE**

**Technology Stack:**
- .NET 9.0
- C# 13
- Console UI with ANSI colors
- Event-driven architecture

**Features:**
- ✅ 101 decision cards
- ✅ 8 political characters with dialogue
- ✅ 4 resource management (Popularity, Stability, Media Trust, Economic Health)
- ✅ 10 unique endings
- ✅ Crisis escalation system
- ✅ Dynamic headline generation
- ✅ Cascade event system
- ✅ Animated title screen
- ✅ Typewriter text effects
- ✅ Color-coded resource bars
- ✅ Turn-based gameplay (100 days)
- ✅ Statistics and scoring

**Architecture:**
```
ExecutiveDisorder.Core (Library)
  ├── Models: Resource, Character, DecisionCard
  ├── Data: CardDatabase, CharacterDatabase
  ├── Systems: ResourceManager, ConsequenceEngine
  └── State: GameState

ExecutiveDisorder.Engine (Game Engine)
  └── GameEngine: Core game loop logic

ExecutiveDisorder.Game (Executable)
  └── Program: Console UI and main entry point
```

**How to Run:**
```bash
cd ExecutiveDisorderReplit
dotnet build
dotnet run --project ExecutiveDisorder.Game
```

---

## 🎮 Game Content (Shared Across Versions)

### 4 Core Resources (0-100%)
| Resource | Icon | Description |
|----------|------|-------------|
| **Popularity** | 👥 | Public approval rating |
| **Stability** | 🏛️ | Government/institutional stability |
| **Media Trust** | 📺 | Press and media relations |
| **Economic Health** | 💰 | Economic indicators |

### 8 Political Characters
1. **Rex Scaleston III** - The Iguana King (Conspiracy theorist)
2. **Donald J. Executive** - The 45th (Speaks in superlatives)
3. **POTUS-9000** - Mascot Bot (Sentient AI)
4. **Alexandria Sanders-Warren** - Progressive (Idealistic reformer)
5. **Richard M. Moneybags III** - Corporate Lobbyist (Business synergist)
6. **General James 'Ironside' Steel** - Military Hawk (Force advocate)
7. **Diana Newsworthy** - Media Mogul (Narrative controller)
8. **Johnny Q. Public** - Populist (Voice of "the people")

### 10 Unique Endings
1. **Democratic Victory** - Democracy survives
2. **Autocratic Empire** - Absolute power consolidated
3. **Economic Collapse** - Financial disaster
4. **Nuclear Winter** - Nuclear option exercised
5. **Alien Overlords** - Intergalactic Federation
6. **Impeachment** - Removed from office
7. **Military Coup** - Military takeover
8. **Media Revolution** - Media controls narrative
9. **Chaos Reigns** - Complete disorder
10. **Time Loop Paradox** - Secret ending

### Decision Card Categories
- **Normal** - Standard policy decisions
- **Crisis** - Urgent situations requiring immediate action
- **Scandal** - Political scandals and controversies
- **Absurd** - Satirical and ridiculous scenarios
- **Character** - Character-specific events
- **Follow-up** - Consequences of previous decisions

---

## 🔧 Development Setup

### Unity 6 Development

**Prerequisites:**
- Unity Hub
- Unity 6 (2023.x or later)
- Visual Studio 2022 (configured via `.vsconfig`)

**Opening the Project:**
```bash
unity-hub --projectPath "C:\Users\POK28\source\repos\ExecutiveDisorderReplit"
```

**Building WebGL:**
1. Open in Unity
2. File → Build Settings
3. Select WebGL platform
4. Build to `ExecutiveDisord/Build/`

---

### .NET 9 Development

**Prerequisites:**
- .NET 9 SDK
- Visual Studio 2022 or VS Code

**Building:**
```bash
dotnet restore
dotnet build
```

**Running:**
```bash
dotnet run --project ExecutiveDisorder.Game
```

**Testing:**
```bash
dotnet test
```

---

## 📦 Dependencies

### Unity Packages
- Universal RP (URP)
- TextMesh Pro
- DOTween (Pro)
- Input System
- 2D Sprite
- Audio

### .NET Packages
- BenchmarkDotNet 0.15.4 (for performance testing)

### Backend (Planned)
- Flask 3.0.0
- Gunicorn 21.2.0
- Cryptography 41.0.7
- PostgreSQL (database)

---

## 🚀 Deployment Status

### Unity WebGL
- ✅ Built and compiled
- ✅ WebGL files generated
- ✅ Ready for web hosting
- 🔄 Flask backend integration (in progress)
- 🔄 PostgreSQL database (planned)
- 🔄 User authentication (planned)

### .NET Console
- ✅ Fully functional
- ✅ Cross-platform (Windows, Linux, macOS)
- ⚠️ Desktop-only (no web deployment)

---

## 📊 Code Statistics

### Unity Project
- **Total Scripts:** 50+ C# files
- **Managers:** 10 manager classes
- **UI Components:** 20+ UI scripts
- **State Machine:** 15+ state/substate classes
- **ScriptableObjects:** 100+ data assets
- **Scenes:** 1 main scene
- **Prefabs:** 30+ UI prefabs

### .NET Console Project
- **Total Files:** 8 core systems
- **Lines of Code:** ~3,500+
- **Decision Cards:** 101 unique scenarios
- **Characters:** 8 fully implemented
- **Dialogue Lines:** 100+ character quotes
- **Headlines:** 20+ dynamic templates

---

## 🔄 Git Configuration

**Repository:** `git@github.com:papaert-cloud/ExecutiveDisorder.git`

**Remotes:**
- **origin:** `git@github.com:papaert-cloud/ExecutiveDisorder.git` (SSH)
- **upstream:** `git@github.com:papaert-cloud/ExecutiveDisorder.git` (SSH)

**User:**
- Name: papaert
- Email: beaconagilelogistics@gmail.com

**Sync Script:**
```bash
./scripts/sync-repos.sh
# or
git sync
```

---

## 🎯 Next Steps & Recommendations

### Immediate Actions
1. ✅ **Unity WebGL is deployed** - Continue maintaining
2. ✅ **.NET Console is complete** - Ready for distribution
3. 🔄 **Flask Backend** - Complete integration for user accounts
4. 🔄 **PostgreSQL** - Set up database for game saves
5. 📝 **Documentation** - Add API documentation for backend

### Enhancement Opportunities

#### Unity Version
- [ ] Add more decision cards
- [ ] Implement multiplayer features
- [ ] Add achievements system
- [ ] Create mobile build (iOS/Android)
- [ ] Add localization support
- [ ] Implement analytics tracking

#### .NET Console Version
- [ ] Add save/load system (JSON)
- [ ] Implement high score tracking
- [ ] Create GUI version (WPF/Avalonia)
- [ ] Port to Blazor WebAssembly
- [ ] Add modding support
- [ ] Create Steam release

#### Infrastructure
- [ ] Set up CI/CD pipeline (GitHub Actions)
- [ ] Implement automated testing
- [ ] Add Docker Compose for full stack
- [ ] Set up Kubernetes deployment
- [ ] Implement monitoring and logging
- [ ] Add infrastructure as code (Terraform)

---

## 🤝 Contributing

### Adding New Content

**Unity (ScriptableObjects):**
1. Navigate to `Assets/Data/DecisionCards/`
2. Right-click → Create → Decision Card
3. Fill in card properties
4. Reference in DecisionsManager

**.NET (Code):**
1. Open `ExecutiveDisorder.Core/Data/CardDatabase.cs`
2. Add new card to `InitializeCards()` method
3. Define choices and effects
4. Rebuild solution

### Code Style
- Follow `.editorconfig` settings
- Use C# naming conventions
- Document public APIs
- Write unit tests for new features

---

## 📞 Support & Resources

### Documentation
- `README.md` - Main project overview
- `IMPLEMENTATION_STATUS.md` - .NET console status
- `UNITY_PROJECT.md` - Unity project details
- `docs/` - Additional documentation

### Repository
- **GitHub:** https://github.com/papaert-cloud/ExecutiveDisorder
- **Issues:** Report bugs and feature requests
- **Pull Requests:** Contributions welcome

---

## 🏆 Project Status Summary

| Component | Status | Notes |
|-----------|--------|-------|
| Unity WebGL Game | ✅ Complete | Deployed and playable |
| .NET Console Game | ✅ Complete | Fully functional |
| Flask Backend | 🔄 In Progress | Basic structure exists |
| PostgreSQL Database | 📝 Planned | Schema design needed |
| User Authentication | 📝 Planned | Backend integration |
| CI/CD Pipeline | 📝 Planned | GitHub Actions |
| Documentation | ✅ Complete | Comprehensive docs |
| Testing | ⚠️ Partial | Unity tested, .NET needs tests |

**Legend:**
- ✅ Complete
- 🔄 In Progress
- 📝 Planned
- ⚠️ Needs Attention

---

## 🎉 Conclusion

**Executive Disorder™** is a **dual-implementation project** with both a polished Unity WebGL game and a complete .NET console version. The Unity version provides a rich, interactive browser experience, while the .NET version offers a lightweight, cross-platform terminal game.

Both implementations share the same satirical political gameplay, featuring resource management, character interactions, and multiple endings based on player choices.

**Current State:**
- ✅ Unity WebGL: Deployed and playable
- ✅ .NET Console: Complete and functional
- 🔄 Backend: In development
- 📝 Infrastructure: Planned enhancements

**Key Strengths:**
- Two complete implementations
- Rich satirical content
- Clean, maintainable codebase
- Comprehensive documentation
- Ready for deployment and distribution

---

**Democracy: Optional. Chaos: Guaranteed.** 🎮

---

*Last Updated: December 2024*  
*Workspace Location: `C:\Users\POK28\source\repos\ExecutiveDisorderReplit`*  
*Repository: `git@github.com:papaert-cloud/ExecutiveDisorder.git`*
