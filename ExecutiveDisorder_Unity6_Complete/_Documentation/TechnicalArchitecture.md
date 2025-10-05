# Executive Disorder - Technical Architecture

## 🏗️ System Architecture Overview

### High-Level Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                        Unity Engine                          │
│  ┌────────────────────────────────────────────────────────┐ │
│  │                     GameManager                         │ │
│  │  (Central Controller - Manages Game Loop & State)       │ │
│  └────────────────────────────────────────────────────────┘ │
│                            │                                 │
│         ┌──────────────────┼──────────────────┐             │
│         │                  │                  │             │
│    ┌────▼─────┐     ┌─────▼──────┐    ┌──────▼─────┐      │
│    │ Resource │     │    Card    │    │ Character  │      │
│    │ Manager  │     │  Manager   │    │  Manager   │      │
│    └──────────┘     └────────────┘    └────────────┘      │
│         │                  │                  │             │
│    ┌────▼─────┐     ┌─────▼──────┐    ┌──────▼─────┐      │
│    │   UI     │     │   Event    │    │ Save/Load  │      │
│    │ Manager  │     │  Manager   │    │  Manager   │      │
│    └──────────┘     └────────────┘    └────────────┘      │
│         │                  │                  │             │
│    ┌────▼─────────────────▼──────────────────▼─────┐      │
│    │          Audio Manager & Effects              │      │
│    └───────────────────────────────────────────────┘      │
└─────────────────────────────────────────────────────────────┘
```

---

## 🔄 Game Flow Diagram

### Main Game Loop

```
START
  │
  ▼
┌─────────────────┐
│   Main Menu     │
│  - New Game     │
│  - Load Game    │
│  - Settings     │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│  Game Initialize│
│  - Resources    │
│  - Characters   │
│  - Card Deck    │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│   Day Start     │
│   (Day Counter) │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│  Present Card   │
│  - Draw from    │
│    deck         │
│  - Check reqs   │
│  - Display UI   │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ Player Choice   │
│  - 2-4 options  │
│  - Timer (if    │
│    crisis)      │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ Apply Effects   │
│  - Resources    │
│  - Loyalty      │
│  - Events       │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ Show Results    │
│  - Resource     │
│    changes      │
│  - News ticker  │
│  - Character    │
│    reactions    │
└────────┬────────┘
         │
         ▼
┌─────────────────┐
│ Check Game Over?│
│  - Resource = 0 │
│  - Resource=100 │
│  - Day = 100    │
│  - Special      │
└────────┬────────┘
         │
    ┌────┴────┐
    │         │
   NO        YES
    │         │
    │         ▼
    │    ┌─────────────┐
    │    │   Ending    │
    │    │   Screen    │
    │    └─────────────┘
    │         │
    │         ▼
    │      [END]
    │
    ▼
┌─────────────────┐
│  Advance Day    │
│  - Day++        │
│  - Auto-save    │
│  - Random event │
└────────┬────────┘
         │
         └──────┐
                │
       (Loop back to Day Start)
```

---

## 💾 Data Flow Architecture

### Manager Communication

```
┌──────────────────────────────────────────────────────────┐
│                    Event Manager (Event Bus)              │
│                                                           │
│  ┌─────────┐  ┌─────────┐  ┌─────────┐  ┌─────────┐    │
│  │Resource │  │  Card   │  │Character│  │   UI    │    │
│  │ Events  │  │ Events  │  │ Events  │  │ Events  │    │
│  └────┬────┘  └────┬────┘  └────┬────┘  └────┬────┘    │
│       │            │             │            │          │
└───────┼────────────┼─────────────┼────────────┼──────────┘
        │            │             │            │
        ▼            ▼             ▼            ▼
  [Subscribers listen to events they care about]
        │            │             │            │
        │            │             │            │
   ┌────▼────┐  ┌───▼────┐   ┌────▼────┐  ┌───▼────┐
   │Resource │  │  Card  │   │Character│  │   UI   │
   │ Manager │  │Manager │   │ Manager │  │Manager │
   └─────────┘  └────────┘   └─────────┘  └────────┘
```

### Data Layer (ScriptableObjects)

```
┌────────────────────────────────────────────────────────────┐
│               ScriptableObject Data Layer                  │
├────────────────────────────────────────────────────────────┤
│                                                            │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐   │
│  │   Card       │  │  Character   │  │   Ending     │   │
│  │  Database    │  │   Database   │  │   Database   │   │
│  └──────┬───────┘  └──────┬───────┘  └──────┬───────┘   │
│         │                  │                  │            │
│    ┌────▼──────┐      ┌───▼─────┐      ┌────▼──────┐    │
│    │ Decision  │      │Character│      │  Ending   │    │
│    │ CardData  │      │  Data   │      │   Data    │    │
│    │ (100+)    │      │  (8)    │      │  (10)     │    │
│    └───────────┘      └─────────┘      └───────────┘    │
│                                                            │
│  ┌──────────────┐  ┌──────────────┐                      │
│  │   Resource   │  │     Game     │                      │
│  │  Definition  │  │   Settings   │                      │
│  └──────────────┘  └──────────────┘                      │
└────────────────────────────────────────────────────────────┘
         │                      │
         └──────────┬───────────┘
                    │
              [Used at Runtime]
                    │
         ┌──────────▼──────────┐
         │   Game Managers     │
         │  (Read/Process)     │
         └─────────────────────┘
```

---

## 🎯 Manager Responsibilities

### GameManager
**Role:** Central controller, orchestrates all systems
```
Responsibilities:
├── Game state (active, paused, ended)
├── Day/turn management
├── Game loop execution
├── Game over condition checking
├── Scene transitions
└── Coordination of all other managers

Events Published:
├── OnGameStart
├── OnGameEnd
├── OnDayChanged
├── OnGamePaused
└── OnGameResumed
```

### ResourceManager
**Role:** Manages the 4 core resources
```
Responsibilities:
├── Resource value tracking (0-100%)
├── Resource modification
├── Threshold checking (critical low/high)
├── Cascading effects between resources
└── Resource-based game over conditions

Events Published:
├── OnResourceChanged
├── OnResourceCriticalLow
├── OnResourceCriticalHigh
├── OnResourceDepleted
└── OnResourceMaxed

Resources Managed:
├── Popularity (👥)
├── Stability (🏛️)
├── Media Trust (📺)
└── Economic Health (💰)
```

### CardManager
**Role:** Manages decision card deck and flow
```
Responsibilities:
├── Deck initialization and shuffling
├── Card drawing and presentation
├── Card requirement checking
├── Player choice processing
├── Effect application
└── Follow-up card queuing

Events Published:
├── OnCardPresented
├── OnCardChoiceMade
├── OnCardResolved
├── OnDeckShuffled
└── OnDeckEmpty

Card Categories:
├── Normal (60%)
├── Crisis (15%)
├── Scandal (10%)
├── Character (10%)
└── Absurd (5%)
```

### CharacterManager
**Role:** Manages all political characters
```
Responsibilities:
├── Character initialization
├── Loyalty tracking (0-100)
├── Relationship management
├── Dialogue system
└── Special ending conditions

Events Published:
├── OnLoyaltyChanged
├── OnCharacterBecameLoyal
├── OnCharacterBecameHostile
├── OnCharacterLeft
└── OnCharacterJoined

Characters:
├── Rex Scaleston III (Iguana King)
├── Donald J. Executive (The 45th)
├── POTUS-9000 (Mascot Bot)
├── Alexandria Sanders-Warren (Progressive)
├── Richard M. Moneybags III (Corporate)
├── General James Steel (Military)
├── Diana Newsworthy (Media Mogul)
└── Johnny Q. Public (Populist)
```

### UIManager
**Role:** Coordinates all UI screens and components
```
Responsibilities:
├── Screen management (menu, gameplay, ending)
├── UI component coordination
├── Resource bar updates
├── Notification display
└── Animation triggers

Screens Managed:
├── Main Menu
├── Gameplay (HUD)
├── Pause Menu
├── Settings
└── Ending Screen
```

### EventManager
**Role:** Global event bus for loose coupling
```
Responsibilities:
├── Event subscription
├── Event publishing
├── Event handling
└── Consequence processing

Event Types:
├── Resource events
├── Character events
├── Card events
├── UI events
└── Custom game events
```

### SaveLoadManager
**Role:** Game persistence
```
Responsibilities:
├── Save game state to file
├── Load game state from file
├── Auto-save functionality
├── Save slot management
└── Data serialization

Save Data Includes:
├── Current day
├── Resource values
├── Character states (loyalty, active)
├── Card deck state
└── Player preferences
```

### AudioManager
**Role:** Sound and music control
```
Responsibilities:
├── Music playback and crossfading
├── Sound effect playback
├── Volume control
├── Audio mixing
└── Settings persistence

Audio Categories:
├── Music (background, crisis, ending)
├── SFX (card flip, resource change)
├── UI (button clicks)
└── Voice (character audio)
```

---

## 🔌 Design Patterns Used

### Singleton Pattern
```csharp
All managers use Singleton<T> for global access:
- GameManager.Instance
- ResourceManager.Instance
- CardManager.Instance
- etc.

Benefits:
✅ Single source of truth
✅ Easy access from anywhere
✅ Thread-safe implementation
✅ Automatic DontDestroyOnLoad
```

### Observer Pattern (Events)
```csharp
C# events for loose coupling:
- OnResourceChanged
- OnCardPresented
- OnLoyaltyChanged

Benefits:
✅ Decoupled systems
✅ Easy to add new listeners
✅ Maintainable code
✅ Clear data flow
```

### ScriptableObject Pattern (Data-Driven)
```csharp
All content as ScriptableObjects:
- DecisionCardData
- CharacterData
- EndingData
- GameSettings

Benefits:
✅ Designer-friendly
✅ No code changes for content
✅ Runtime efficient
✅ Version control friendly
✅ Easy to test
```

### State Machine Pattern
```csharp
Game states:
- MainMenuState
- GameplayState
- PauseState
- EndingState

Benefits:
✅ Clear game flow
✅ Easy transitions
✅ Maintainable
✅ Testable
```

---

## 📦 Dependency Graph

```
GameManager
  ├─► ResourceManager
  ├─► CardManager
  │     └─► CardDatabase
  ├─► CharacterManager
  │     └─► CharacterDatabase
  ├─► SaveLoadManager
  ├─► EventManager
  └─► UIManager
        ├─► AudioManager
        ├─► CardUIController
        ├─► GameHUDController
        ├─► ResourceBarUI
        ├─► EndingScreenController
        └─► NewsTickerController
```

---

## 🎮 Runtime Object Hierarchy

```
Scene: GamePlay
│
├── [Managers] (DontDestroyOnLoad)
│   ├── GameManager
│   ├── ResourceManager
│   ├── CardManager
│   ├── CharacterManager
│   ├── EventManager
│   ├── SaveLoadManager
│   ├── AudioManager
│   └── UIManager
│
├── [UI]
│   ├── Canvas
│   │   ├── GameHUD
│   │   │   ├── DayDisplay
│   │   │   ├── ResourceBars
│   │   │   │   ├── PopularityBar
│   │   │   │   ├── StabilityBar
│   │   │   │   ├── MediaTrustBar
│   │   │   │   └── EconomicHealthBar
│   │   │   └── QuickStats
│   │   │
│   │   ├── CardDisplay
│   │   │   ├── CardBackground
│   │   │   ├── CardTitle
│   │   │   ├── CardDescription
│   │   │   ├── CharacterPortrait
│   │   │   └── ChoiceButtons
│   │   │
│   │   ├── NewsTicker
│   │   ├── PauseMenu
│   │   └── SettingsMenu
│   │
│   └── EndingScreen (separate scene)
│
├── [Audio]
│   ├── MusicSource
│   ├── SFXSource
│   └── UISource
│
└── [Camera]
    └── Main Camera
```

---

## 🔐 Security & Best Practices

### Code Quality
- ✅ XML documentation comments
- ✅ Null reference checks
- ✅ Error handling
- ✅ Debug logging (conditional)
- ✅ Input validation

### Performance
- ✅ Object pooling for UI elements
- ✅ Event unsubscription in OnDestroy
- ✅ Efficient data structures (Dictionary lookups)
- ✅ Coroutines for async operations
- ✅ Minimal Update() usage

### Architecture
- ✅ Single Responsibility Principle
- ✅ Dependency Injection where needed
- ✅ Loose coupling via events
- ✅ High cohesion within managers
- ✅ Clear separation of concerns

---

## 🚀 Extensibility

### Adding New Systems

```csharp
// 1. Create new manager
public class AchievementManager : Singleton<AchievementManager>
{
    // Your code here
}

// 2. Subscribe to relevant events
GameManager.OnGameEnd += OnGameEnd;
CardManager.OnCardResolved += OnCardResolved;

// 3. Add to scene
// Drag AchievementManager script to scene

// 4. Reference in other managers if needed
var achievements = AchievementManager.Instance;
```

### Adding New Content

```csharp
// Cards: Right-click → Create → Executive Disorder → Decision Card
// Characters: Right-click → Create → Executive Disorder → Character
// Endings: Right-click → Create → Executive Disorder → Ending

// All automatically loaded via Resources.LoadAll<T>()
```

---

**Architecture Version:** 1.0  
**Last Updated:** October 2025  
**Complexity:** Medium  
**Maintainability:** High  
**Extensibility:** High
