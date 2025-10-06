# Character Selection Scene - Complete Implementation Guide

## 🎯 Overview
This document provides a complete implementation plan for the Character Selection scene in Executive Disorder, integrating AI backend features, AudioManager, EventManager, and Unity 6.2 capabilities.

---

## 1️⃣ AI Backend Integration

### Available AI Endpoints (Flask Backend)

#### 🎨 Visual Generation (DALL-E 3)
```csharp
// Generate character portraits dynamically
POST /api/generate-character-portrait
{
    "character_name": "Rex Scaleston III",
    "description": "Professional portrait of an iguana in a business suit, political leader"
}
```

#### 🧠 Content Generation (GPT-5)
```csharp
// Generate character profiles
POST /api/generate-character
{
    "character_type": "populist",
    "traits": ["charismatic", "controversial", "media-savvy"]
}
```

#### 🎙️ Voice Generation (ElevenLabs)
```csharp
// Generate character introduction voice
POST /api/generate-character-voice
{
    "text": "Welcome to my administration. Together, we'll make history.",
    "voice_id": "politician_male"
}
```

### Implementation: AICharacterGenerator.cs
```csharp
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class AICharacterGenerator : MonoBehaviour
{
    private const string API_BASE = "https://your-replit-url.repl.co";
    
    public IEnumerator GenerateCharacterPortrait(string characterName, string description, Action<Texture2D> onComplete)
    {
        var request = new UnityWebRequest($"{API_BASE}/api/generate-character-portrait", "POST");
        string json = JsonUtility.ToJson(new { character_name = characterName, description = description });
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerTexture();
        request.SetRequestHeader("Content-Type", "application/json");
        
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            onComplete?.Invoke(texture);
        }
        else
        {
            Debug.LogError($"Portrait generation failed: {request.error}");
            onComplete?.Invoke(null);
        }
    }
    
    public IEnumerator GenerateCharacterVoice(string text, string voiceId, Action<AudioClip> onComplete)
    {
        var request = UnityWebRequestMultimedia.GetAudioClip($"{API_BASE}/api/generate-character-voice?text={text}&voice_id={voiceId}", AudioType.MPEG);
        
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
            onComplete?.Invoke(clip);
        }
        else
        {
            Debug.LogError($"Voice generation failed: {request.error}");
            onComplete?.Invoke(null);
        }
    }
}
```

---

## 2️⃣ AudioManager Integration

### Character Selection Audio Setup

```csharp
public class CharacterSelectionAudio : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioManager.SoundType backgroundMusic = AudioManager.SoundType.RelaxingGuitar;
    
    [Header("SFX")]
    [SerializeField] private AudioManager.SoundType cardHoverSound = AudioManager.SoundType.Pop;
    [SerializeField] private AudioManager.SoundType cardSelectSound = AudioManager.SoundType.Stamp;
    [SerializeField] private AudioManager.SoundType confirmSound = AudioManager.SoundType.Correct;
    
    void Start()
    {
        // Play background music with crossfade
        AudioManager.Instance.PlayMusic(backgroundMusic, fadeSeconds: 2f);
    }
    
    public void OnCharacterHover()
    {
        AudioManager.Instance.PlaySFX(cardHoverSound, volume: 0.5f);
    }
    
    public void OnCharacterSelect()
    {
        AudioManager.Instance.PlaySFX(cardSelectSound, volume: 0.8f);
    }
    
    public void OnStartCampaign()
    {
        AudioManager.Instance.PlaySFX(confirmSound);
        // Fade out music before scene transition
        AudioManager.Instance.StopMusic(fadeSeconds: 1f);
    }
    
    public void PlayCharacterVoice(AudioClip voiceClip)
    {
        if (voiceClip != null)
        {
            AudioManager.Instance.PlayClip(voiceClip, volume: 1f);
        }
    }
}
```

---

## 3️⃣ EventManager Integration

### Custom Events for Character Selection

```csharp
// Add to EventManager.cs
public static event EventController.MethodContainer OnCharacterHovered;
public static event EventController.MethodContainer OnCharacterSelected;
public static event EventController.MethodContainer OnCharacterConfirmed;

public void CallOnCharacterHovered(EventData data = null) => OnCharacterHovered?.Invoke(data);
public void CallOnCharacterSelected(EventData data = null) => OnCharacterSelected?.Invoke(data);
public void CallOnCharacterConfirmed(EventData data = null) => OnCharacterConfirmed?.Invoke(data);
```

### Event Data Structure
```csharp
[Serializable]
public class CharacterEventData : EventData
{
    public string characterName;
    public int startingPopularity;
    public int startingStability;
    public int startingMedia;
    public int startingEconomy;
}
```

---

## 4️⃣ Character Data Structure

### PoliticalCharacter.cs
```csharp
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Executive Disorder/Political Character")]
public class PoliticalCharacter : ScriptableObject
{
    [Header("Identity")]
    public string characterName;
    public string title; // "The Populist", "The Diplomat"
    public string shortBio;
    [TextArea(3, 6)]
    public string fullBio;
    
    [Header("Starting Stats")]
    [Range(0, 100)] public int startingPopularity = 50;
    [Range(0, 100)] public int startingStability = 50;
    [Range(0, 100)] public int startingMedia = 50;
    [Range(0, 100)] public int startingEconomy = 50;
    
    [Header("Visuals")]
    public Sprite portrait;
    public Color themeColor = Color.white;
    
    [Header("Audio")]
    public AudioClip introVoiceLine;
    
    [Header("Unique Traits")]
    public string[] specialAbilities;
    
    [Header("AI Generation")]
    public bool useAIPortrait = false;
    public string aiPortraitPrompt;
}
```

---

## 5️⃣ Character Selection Manager

### CharacterSelectionManager.cs
```csharp
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelectionManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform characterGridParent;
    [SerializeField] private GameObject characterCardPrefab;
    [SerializeField] private GameObject selectedCharacterPanel;
    [SerializeField] private Button startCampaignButton;
    [SerializeField] private Button randomButton;
    
    [Header("Selected Character Display")]
    [SerializeField] private Image selectedPortrait;
    [SerializeField] private TextMeshProUGUI selectedName;
    [SerializeField] private TextMeshProUGUI selectedBio;
    [SerializeField] private TextMeshProUGUI selectedStats;
    
    [Header("Character Data")]
    [SerializeField] private List<PoliticalCharacter> characters;
    
    [Header("Components")]
    [SerializeField] private CharacterSelectionAudio audioController;
    [SerializeField] private AICharacterGenerator aiGenerator;
    
    private PoliticalCharacter selectedCharacter;
    private List<CharacterCard> characterCards = new List<CharacterCard>();
    
    void Start()
    {
        GenerateCharacterCards();
        startCampaignButton.interactable = false;
        selectedCharacterPanel.SetActive(false);
        
        // Subscribe to events
        EventManager.OnCharacterSelected += OnCharacterSelectedEvent;
        
        // Setup button listeners
        startCampaignButton.onClick.AddListener(OnStartCampaignClicked);
        randomButton.onClick.AddListener(OnRandomCharacterClicked);
        
        // Generate AI portraits if needed
        StartCoroutine(GenerateAIPortraits());
    }
    
    void OnDestroy()
    {
        EventManager.OnCharacterSelected -= OnCharacterSelectedEvent;
    }
    
    void GenerateCharacterCards()
    {
        foreach (var character in characters)
        {
            GameObject cardObj = Instantiate(characterCardPrefab, characterGridParent);
            CharacterCard card = cardObj.GetComponent<CharacterCard>();
            card.Initialize(character, OnCharacterSelected);
            characterCards.Add(card);
        }
    }
    
    IEnumerator GenerateAIPortraits()
    {
        foreach (var character in characters)
        {
            if (character.useAIPortrait && character.portrait == null)
            {
                yield return aiGenerator.GenerateCharacterPortrait(
                    character.characterName,
                    character.aiPortraitPrompt,
                    texture =>
                    {
                        if (texture != null)
                        {
                            character.portrait = Sprite.Create(
                                texture,
                                new Rect(0, 0, texture.width, texture.height),
                                new Vector2(0.5f, 0.5f)
                            );
                            UpdateCharacterCard(character);
                        }
                    }
                );
            }
        }
    }
    
    void UpdateCharacterCard(PoliticalCharacter character)
    {
        var card = characterCards.Find(c => c.Character == character);
        if (card != null)
        {
            card.UpdatePortrait(character.portrait);
        }
    }
    
    void OnCharacterSelected(PoliticalCharacter character)
    {
        selectedCharacter = character;
        
        // Play selection sound
        audioController.OnCharacterSelect();
        
        // Update UI
        UpdateSelectedCharacterPanel(character);
        
        // Enable start button
        startCampaignButton.interactable = true;
        selectedCharacterPanel.SetActive(true);
        
        // Play character voice
        if (character.introVoiceLine != null)
        {
            audioController.PlayCharacterVoice(character.introVoiceLine);
        }
        
        // Trigger event
        var eventData = new CharacterEventData
        {
            characterName = character.characterName,
            startingPopularity = character.startingPopularity,
            startingStability = character.startingStability,
            startingMedia = character.startingMedia,
            startingEconomy = character.startingEconomy
        };
        EventManager.Instance.CallOnCharacterSelected(eventData);
    }
    
    void UpdateSelectedCharacterPanel(PoliticalCharacter character)
    {
        selectedPortrait.sprite = character.portrait;
        selectedName.text = $"{character.characterName}\n<size=60%>{character.title}</size>";
        selectedBio.text = character.fullBio;
        
        selectedStats.text = $"Popularity: {GetStarRating(character.startingPopularity)}\n" +
                            $"Stability: {GetStarRating(character.startingStability)}\n" +
                            $"Media Trust: {GetStarRating(character.startingMedia)}\n" +
                            $"Economy: {GetStarRating(character.startingEconomy)}";
    }
    
    string GetStarRating(int value)
    {
        int stars = Mathf.RoundToInt(value / 20f);
        return new string('★', stars) + new string('☆', 5 - stars);
    }
    
    public void OnStartCampaignClicked()
    {
        audioController.OnStartCampaign();
        
        // Save to game state
        GameStateManager.Instance.SetCharacter(selectedCharacter);
        
        // Trigger confirmation event
        EventManager.Instance.CallOnCharacterConfirmed();
        
        // Load gameplay scene
        StateManager.Instance.SwitchState(AppState.GameStart);
    }
    
    public void OnRandomCharacterClicked()
    {
        int randomIndex = Random.Range(0, characters.Count);
        OnCharacterSelected(characters[randomIndex]);
    }
    
    void OnCharacterSelectedEvent(EventData data)
    {
        Debug.Log($"Character selected event received: {((CharacterEventData)data).characterName}");
    }
}
```

---

## 6️⃣ Character Card Component

### CharacterCard.cs
```csharp
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class CharacterCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Elements")]
    [SerializeField] private Image portraitImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI bioText;
    [SerializeField] private Button selectButton;
    [SerializeField] private Image backgroundImage;
    
    private PoliticalCharacter character;
    private Action<PoliticalCharacter> onSelectCallback;
    
    public PoliticalCharacter Character => character;
    
    public void Initialize(PoliticalCharacter character, Action<PoliticalCharacter> onSelect)
    {
        this.character = character;
        this.onSelectCallback = onSelect;
        
        UpdateDisplay();
        selectButton.onClick.AddListener(OnSelectClicked);
    }
    
    void UpdateDisplay()
    {
        portraitImage.sprite = character.portrait;
        nameText.text = character.characterName;
        titleText.text = character.title;
        bioText.text = character.shortBio;
        backgroundImage.color = character.themeColor;
    }
    
    public void UpdatePortrait(Sprite newPortrait)
    {
        portraitImage.sprite = newPortrait;
    }
    
    void OnSelectClicked()
    {
        onSelectCallback?.Invoke(character);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(AudioManager.SoundType.Pop, volume: 0.3f);
        transform.localScale = Vector3.one * 1.05f;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}
```

---

## 7️⃣ 10 Character Profiles

### Character Definitions

1. **Rex Scaleston III** - The Iguana King
   - Popularity: 70, Stability: 40, Media: 60, Economy: 50
   - Trait: High charisma, low institutional respect

2. **Donald J. Executive** - The 45th
   - Popularity: 60, Stability: 30, Media: 20, Economy: 80
   - Trait: Business-focused, media antagonist

3. **POTUS-9000** - The AI President
   - Popularity: 50, Stability: 90, Media: 70, Economy: 60
   - Trait: Logical, predictable, lacks human touch

4. **Alexandria Sanders-Warren** - The Progressive
   - Popularity: 65, Stability: 55, Media: 80, Economy: 40
   - Trait: Media darling, economic reformer

5. **Richard M. Moneybags III** - The Lobbyist
   - Popularity: 30, Stability: 60, Media: 40, Economy: 95
   - Trait: Corporate puppet, economic genius

6. **General James 'Ironside' Steel** - The Hawk
   - Popularity: 45, Stability: 85, Media: 50, Economy: 55
   - Trait: Military strength, authoritarian tendencies

7. **Diana Newsworthy** - The Media Mogul
   - Popularity: 55, Stability: 50, Media: 95, Economy: 60
   - Trait: Controls narrative, image-obsessed

8. **Johnny Q. Public** - The Populist
   - Popularity: 85, Stability: 35, Media: 45, Economy: 50
   - Trait: People's champion, institutional chaos

9. **Dr. Evelyn Technocrat** - The Scientist
   - Popularity: 40, Stability: 70, Media: 60, Economy: 75
   - Trait: Data-driven, lacks charisma

10. **Senator Marcus Tradition** - The Conservative
    - Popularity: 50, Stability: 80, Media: 55, Economy: 65
    - Trait: Institutional stability, resistant to change

---

## 8️⃣ Scene Hierarchy

```
CharacterSelection Scene
├── Canvas
│   ├── Background (Image - Political office)
│   ├── Header
│   │   ├── Title (TMP) - "SELECT YOUR LEADER"
│   │   └── Subtitle (TMP) - "Each character has unique strengths"
│   ├── CharacterGrid (Scroll View)
│   │   └── Content (Grid Layout Group)
│   │       └── CharacterCard Prefabs (x10)
│   ├── SelectedCharacterPanel
│   │   ├── Portrait (Large Image)
│   │   ├── Name & Title (TMP)
│   │   ├── Full Bio (TMP)
│   │   ├── Stats Display (TMP)
│   │   └── StartCampaignButton
│   └── Footer
│       ├── BackButton
│       └── RandomButton
├── Managers
│   ├── CharacterSelectionManager
│   ├── CharacterSelectionAudio
│   └── AICharacterGenerator
└── EventSystem
```

---

## 9️⃣ Integration Checklist

- [ ] Create PoliticalCharacter ScriptableObjects (10 characters)
- [ ] Design CharacterCard prefab with UI elements
- [ ] Implement CharacterSelectionManager
- [ ] Setup AudioManager sound effects
- [ ] Add custom events to EventManager
- [ ] Create AICharacterGenerator for portraits
- [ ] Build scene hierarchy in Unity
- [ ] Test AI backend endpoints
- [ ] Implement voice generation integration
- [ ] Add scene transition to GameStart state

---

## 🎯 Summary

This implementation provides:
- ✅ Full AI backend integration (portraits, voices, content)
- ✅ AudioManager for music and SFX
- ✅ EventManager for decoupled communication
- ✅ 10 unique political characters
- ✅ Dynamic AI-generated content
- ✅ Complete scene architecture
- ✅ State management integration

**Next Steps**: Create the scene in Unity, implement the scripts, and test with your Flask backend!
