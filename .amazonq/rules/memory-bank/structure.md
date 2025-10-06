# Project Structure

## Directory Organization

### Core C# Projects
- **ExecutiveDisorder.Core/**: Core game logic library (.NET 9.0)
  - `Cards/`: Card definitions and behaviors (DecisionCard)
  - `Characters/`: Character models and attributes
  - `Data/`: Database systems (CardDatabase, CharacterDatabase)
  - `Entity/`: Base game object entities
  - `Models/`: Data models (Resource)
  - `State/`: Game state management and save system
  - `Systems/`: Core game systems (ConsequenceEngine, ResourceManager)

- **ExecutiveDisorder.Engine/**: Game engine layer
  - `GameEngine.cs`: Main engine orchestration

- **ExecutiveDisorder.Game/**: Executable game application
  - `Program.cs`: Entry point for standalone C# version

- **ExecutiveDisorder.Tests/**: Unit test project
  - Test coverage for core systems

### Unity Project
- **unity/**: Unity 6.2 project with AI/ML integration
  - `Assets/Scripts/`: Unity-specific game scripts
    - `Managers/`: AudioManager, GameManager, etc.
    - `StateManager/`: Unity state management integration
    - `UI/`: User interface controllers
    - `Cards/`, `Characters/`, `Resources/`: Unity component wrappers
    - `AI/`: AI/ML systems (AIContentGenerator, SentisInferenceManager, PlayerSimulationAgent, AIAssetManager)
  - `Assets/Scenes/`: Game scenes
  - `Assets/Prefabs/`: Reusable game objects
  - `Assets/Art/`: Visual assets
  - `Assets/Audio/`: Sound effects and music
  - `Assets/Data/`: JSON data files (cards, characters, endings)
  - `ProjectSettings/`: Unity configuration
  - `ML-Agents/`: Machine learning training configurations and scripts

### Infrastructure
- **backend/**: Python Flask backend service
  - `app/app.py`: REST API server
  - `schema.sql`: Database schema
  - `Dockerfile`: Container configuration

- **infrastructure/**: Docker Compose setup
- **kubernetes/**: K8s policies (network, security)
- **terraform/**: Infrastructure as Code modules
- **scripts/**: Automation scripts (bootstrap, sync)

### Documentation
- **docs/**: Comprehensive project documentation
  - Implementation guides, action plans, testing guides
  - Unity project documentation, workspace digests

## Core Components

### Game Loop Architecture
1. **Data Layer**: CardDatabase, CharacterDatabase load JSON data
2. **State Layer**: GameState tracks resources, decisions, progress
3. **Logic Layer**: ConsequenceEngine processes decisions, ResourceManager handles resources
4. **Presentation Layer**: Unity UI displays state, handles input
5. **Persistence Layer**: SaveSystem manages game state serialization

### Component Relationships
```
Unity Frontend (Assets/Scripts)
    ↓
ExecutiveDisorder.Engine (GameEngine)
    ↓
ExecutiveDisorder.Core (Core Logic)
    ↓
Data Files (JSON) + Save System
```

## Architectural Patterns
- **Separation of Concerns**: Core logic isolated from Unity-specific code
- **Data-Driven Design**: Game content defined in JSON files
- **Manager Pattern**: Centralized managers for audio, resources, state
- **Component-Based**: Unity GameObject composition
- **Repository Pattern**: Database classes abstract data access
