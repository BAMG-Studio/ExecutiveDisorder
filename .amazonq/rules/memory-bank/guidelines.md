# Development Guidelines

## Code Quality Standards

### Formatting and Structure
- **Namespace Organization**: Use file-scoped or block-scoped namespaces matching directory structure (e.g., `ExecutiveDisorder.Core.Data`, `ExecutiveDisorder.Core.Systems`)
- **Line Length**: Keep lines readable; wrap long method chains and complex expressions
- **Indentation**: Consistent 4-space indentation (C#) or Unity default (Unity scripts)
- **Bracing Style**: Opening braces on same line for properties/initializers, new line for methods/classes

### Naming Conventions
- **Classes/Structs**: PascalCase (e.g., `CardDatabase`, `ConsequenceEngine`, `AudioManager`)
- **Methods**: PascalCase with descriptive verbs (e.g., `ProcessChoice`, `GenerateHeadlines`, `PlaySFX`)
- **Private Fields**: Underscore prefix with camelCase (e.g., `_allCards`, `_resourceManager`, `_sfxPool`)
- **Public Properties**: PascalCase (e.g., `ActiveState`, `TotalCards`, `CurrentDay`)
- **Parameters**: camelCase (e.g., `card`, `choice`, `fadeSeconds`)
- **Constants/Enums**: PascalCase (e.g., `CardCategory.Crisis`, `AppState.GameStart`)
- **Serialized Fields**: camelCase with `[SerializeField]` attribute (e.g., `[SerializeField] private int sfxPoolSize`)

### Documentation
- **XML Comments**: Use `/// <summary>` tags for public APIs and complex methods
- **Inline Comments**: Explain "why" not "what"; used sparingly for complex logic
- **TODO Comments**: Mark incomplete features with `//TODO:` prefix
- **Section Comments**: Use `#region` blocks for logical grouping in large classes

## Architectural Patterns

### Singleton Pattern (Unity)
```csharp
public static AudioManager Instance { get; private set; }

private void Awake()
{
    if (Instance != null && Instance != this) { Destroy(gameObject); return; }
    Instance = this;
    DontDestroyOnLoad(gameObject);
}
```
- Used extensively in Unity managers (AudioManager, StateManager)
- Ensures single instance with DontDestroyOnLoad for persistence
- Early return pattern prevents duplicate instances

### Manager Pattern
- Centralized control classes: `ResourceManager`, `AudioManager`, `StateManager`
- Managers handle specific domains (audio, state, resources)
- Expose public APIs for controlled access to subsystems
- Use events for decoupled communication

### State Machine Pattern
```csharp
public void SwitchState(AppState newState)
{
    // Exit previous state
    foreach (var mainController in _mainStateControllers.Values)
    {
        if (mainController.AppState == _activeState)
            mainController.Exit();
    }
    
    // Enter new state
    _activeState = newState;
    if (_mainStateControllers.TryGetValue(_activeState, out var controller))
    {
        controller.Enter();
    }
}
```
- StateManager implements hierarchical state machine (main states + sub-states)
- Controllers registered via dictionary lookup
- Explicit Enter/Exit lifecycle methods
- Validation before state transitions

### Event-Driven Architecture
```csharp
public event EventHandler<ResourceChangedEventArgs>? ResourceChanged;
public event EventHandler<ConsequenceEventArgs>? ConsequenceGenerated;

// Invocation
ConsequenceGenerated?.Invoke(this, new ConsequenceEventArgs(result));

// Subscription
_resourceManager.ResourceChanged += OnResourceChanged;
```
- Nullable event handlers with null-conditional operator
- Custom EventArgs classes for typed event data
- Used for cross-system communication without tight coupling

### Object Pool Pattern
```csharp
private readonly List<AudioSource> _sfxPool = new();
private int _nextSfxIndex = 0;

private AudioSource GetNextSfxSource()
{
    // Find free source first
    for (int i = 0; i < _sfxPool.Count; i++)
    {
        int idx = (i + _nextSfxIndex) % _sfxPool.Count;
        if (!_sfxPool[idx].isPlaying) { _nextSfxIndex = (idx + 1) % _sfxPool.Count; return _sfxPool[idx]; }
    }
    // Expand pool if needed
    if (expandPoolIfNeeded)
    {
        var src = CreateSource("SFX_" + _sfxPool.Count, sfxGroup, loop: false);
        _sfxPool.Add(src);
        return src;
    }
    // Round-robin overwrite
    var chosen = _sfxPool[_nextSfxIndex];
    _nextSfxIndex = (_nextSfxIndex + 1) % _sfxPool.Count;
    return chosen;
}
```
- Pre-allocated AudioSource pool for SFX to avoid runtime allocation
- Round-robin selection with optional dynamic expansion
- Reuse pattern minimizes garbage collection

### Data-Driven Design
```csharp
private void InitializeCards()
{
    _allCards.Add(CreateNuclearSubmarineCrisis());
    _allCards.Add(CreateTwitterIncident());
    // ... more cards
}

private DecisionCard CreateNuclearSubmarineCrisis()
{
    return new DecisionCard("CRISIS_001", "THE NUCLEAR OPTION", "...")
    {
        Category = CardCategory.Crisis,
        Choices = new List<CardChoice> { /* ... */ }
    };
}
```
- Game content defined through data structures (DecisionCard, CardChoice)
- Factory methods create configured objects
- JSON files used for external data (cards, characters, endings)
- Separation of data from logic

### Coroutine Pattern (Unity)
```csharp
private Coroutine _musicFadeRoutine;

private void Crossfade(AudioClip target, bool isMusic, float fadeSeconds, bool loop)
{
    StopCoroutineSafe(isMusic ? _musicFadeRoutine : _ambFadeRoutine);
    
    if (isMusic)
        _musicFadeRoutine = StartCoroutine(CrossfadeRoutine(active, idle, ms, () => _musicUsingA = idle == _musicA));
}

private IEnumerator CrossfadeRoutine(AudioSource from, AudioSource to, int ms, Action onDone)
{
    float t = 0f, dur = Mathf.Max(0.001f, ms / 1000f);
    while (t < dur)
    {
        t += Time.unscaledDeltaTime;
        float k = t / dur;
        if (from != null) from.volume = Mathf.Lerp(fromStart, 0f, k);
        to.volume = Mathf.Lerp(0f, 1f, k);
        yield return null;
    }
    onDone?.Invoke();
}
```
- Store Coroutine references for cancellation
- Use `Time.unscaledDeltaTime` for UI/audio (pause-independent)
- Callbacks via Action delegates for completion notification
- Safe cleanup with null checks

## Common Code Idioms

### Dictionary Initialization with Collection Expressions
```csharp
Effects = new Dictionary<ResourceType, int>
{
    [ResourceType.Stability] = -20,
    [ResourceType.Popularity] = +10,
    [ResourceType.MediaTrust] = -15
}
```

### LINQ for Queries
```csharp
var availableCards = _allCards.Where(c => !_playedCards.Contains(c)).ToList();
var negativeCount = changes.Values.Count(v => v < -10);
return _newsHeadlines.TakeLast(count).ToList();
```

### Null-Conditional and Null-Coalescing
```csharp
ConsequenceGenerated?.Invoke(this, new ConsequenceEventArgs(result));
float mul = sound.volumeMul <= 0 ? 1f : sound.volumeMul;
return _clips.GetValueOrDefault(id);
```

### Pattern Matching (Switch Expressions)
```csharp
Console.ForegroundColor = card.Urgency switch
{
    UrgencyLevel.Critical => ConsoleColor.Red,
    UrgencyLevel.Urgent => ConsoleColor.DarkYellow,
    UrgencyLevel.Elevated => ConsoleColor.Yellow,
    _ => ConsoleColor.White
};
```

### Range Attributes for Inspector
```csharp
[Range(1, 64)][SerializeField] private int sfxPoolSize = 16;
[Range(0f, 2f)] public float volumeMul;
```

### String Interpolation
```csharp
Console.WriteLine($"║  EXECUTIVE DISORDER - Day {_gameState!.CurrentDay,3} | Phase: {_gameState.CurrentPhase,-12}  ║");
Debug.Log($"Switching from {_activeState} to {newState}");
```

### Early Return Pattern
```csharp
if (Instance != null && Instance != this) { Destroy(gameObject); return; }
if (clip == null) return;
if (!_clips.TryGetValue(id, out var clip) || clip == null) return;
```

## Unity-Specific Patterns

### SerializeField for Private Inspector Access
```csharp
[Header("Library")]
[SerializeField] private List<Sound> sounds = new List<Sound>();

[Header("Mixer (optional, but recommended)")]
[SerializeField] private AudioMixer mixer;
```

### Header Attributes for Organization
- Group related fields in Inspector with `[Header("Section Name")]`
- Improves designer workflow and readability

### Component Lifecycle
```csharp
private void Awake() { /* Initialization, singleton setup */ }
private void Start() { /* After all Awake calls */ }
IEnumerator TestInit() { yield return new WaitForSeconds(1); /* Delayed init */ }
```

### GameObject Hierarchy Management
```csharp
var go = new GameObject(name);
go.transform.SetParent(transform);
var src = go.AddComponent<AudioSource>();
```

### TryGetValue Pattern
```csharp
if (_mainStateControllers.TryGetValue(_activeState, out var controller))
{
    controller.Enter();
}
```

## Best Practices

### Nullable Reference Types
- Enable with `<Nullable>enable</Nullable>` in .csproj
- Use `?` for nullable types: `public event EventHandler<ConsequenceEventArgs>? ConsequenceGenerated;`
- Null-forgiving operator `!` when certainty exists: `_gameState!.CurrentDay`

### Immutability Where Appropriate
```csharp
public IReadOnlyList<string> GetRecentHeadlines(int count = 5)
{
    return _newsHeadlines.TakeLast(count).ToList();
}
```

### Defensive Programming
```csharp
if (!IsDayStateAllowed(newState))
{
    Debug.LogWarning($"[StateManager] Cannot switch to {newState} during {_currentDayState}.");
    return;
}
```

### Logging and Debugging
```csharp
Debug.Log($"Switching from {_activeState} to {newState}");
Debug.LogWarning($"[StateManager] No controller found for sub-state {subState}.");
```
- Prefix warnings with component name in brackets
- Use structured logging with interpolation

### Resource Management
- Pool frequently allocated objects (AudioSources)
- Reuse collections where possible
- Clear lists instead of recreating: `_activeSubStates.Clear();`

### Separation of Concerns
- Core game logic in `ExecutiveDisorder.Core` (platform-agnostic)
- Unity-specific code in `unity/Assets/Scripts`
- Engine layer bridges core and Unity
- Backend services separate from game logic

### Configuration Over Code
```csharp
[SerializeField] private int musicFadeMs = 800;
[SerializeField] private bool expandPoolIfNeeded = true;
```
- Expose tunable parameters via SerializeField
- Allows designers to adjust without code changes

### Enum-Based Type Safety
```csharp
public enum SoundType { None, Stamp, Correct, Incorrect, /* ... */ }
public enum AppState { None, MainMenu, FakeNews, Characters, /* ... */ }
```
- Strongly typed identifiers instead of strings
- Compile-time validation and IntelliSense support
