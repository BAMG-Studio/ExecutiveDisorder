using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ExecutiveDisorder.AI
{
    /// <summary>
    /// Generates procedural game content using AI and algorithmic methods.
    /// Creates decision cards, events, character dialogue, and narrative content.
    /// </summary>
    public class AIContentGenerator : MonoBehaviour
    {
        [Header("Content Generation Settings")]
        [SerializeField] private bool useAIGeneration = true;
        [SerializeField] private bool useFallbackGeneration = true;
        [SerializeField] private int minCardsPerDay = 1;
        [SerializeField] private int maxCardsPerDay = 3;
        
        [Header("Content Templates")]
        [SerializeField] private TextAsset cardTemplatesJSON;
        [SerializeField] private TextAsset eventTemplatesJSON;
        [SerializeField] private TextAsset dialogueTemplatesJSON;
        
        [Header("Generated Content Storage")]
        [SerializeField] private string generatedContentPath = "Assets/Resources/Generated/";
        
        // Content pools
        private List<CardTemplate> _cardTemplates = new List<CardTemplate>();
        private List<EventTemplate> _eventTemplates = new List<EventTemplate>();
        private List<DialogueTemplate> _dialogueTemplates = new List<DialogueTemplate>();
        
        // Generated content tracking
        private Dictionary<int, List<DecisionCardData>> _generatedCardsByDay = new Dictionary<int, List<DecisionCardData>>();
        private List<string> _usedPrompts = new List<string>();
        
        // Singleton
        private static AIContentGenerator _instance;
        public static AIContentGenerator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<AIContentGenerator>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("AIContentGenerator");
                        _instance = go.AddComponent<AIContentGenerator>();
                    }
                }
                return _instance;
            }
        }
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
            
            InitializeTemplates();
        }
        
        #region Initialization
        
        private void InitializeTemplates()
        {
            LoadTemplatesFromJSON();
            CreateDefaultTemplates();
        }
        
        private void LoadTemplatesFromJSON()
        {
            // Load templates from JSON files if provided
            if (cardTemplatesJSON != null)
            {
                // Parse JSON and populate _cardTemplates
                Debug.Log("Loading card templates from JSON");
            }
            
            if (eventTemplatesJSON != null)
            {
                Debug.Log("Loading event templates from JSON");
            }
            
            if (dialogueTemplatesJSON != null)
            {
                Debug.Log("Loading dialogue templates from JSON");
            }
        }
        
        private void CreateDefaultTemplates()
        {
            // Create fallback templates if none loaded
            if (_cardTemplates.Count == 0)
            {
                _cardTemplates.AddRange(GetDefaultCardTemplates());
            }
            
            if (_eventTemplates.Count == 0)
            {
                _eventTemplates.AddRange(GetDefaultEventTemplates());
            }
            
            if (_dialogueTemplates.Count == 0)
            {
                _dialogueTemplates.AddRange(GetDefaultDialogueTemplates());
            }
        }
        
        #endregion
        
        #region Card Generation
        
        /// <summary>
        /// Generate decision cards for a specific day
        /// </summary>
        public void GenerateCardsForDay(int day, System.Action<List<DecisionCardData>> onComplete)
        {
            // Check if already generated
            if (_generatedCardsByDay.ContainsKey(day))
            {
                onComplete?.Invoke(_generatedCardsByDay[day]);
                return;
            }
            
            int cardCount = Random.Range(minCardsPerDay, maxCardsPerDay + 1);
            List<DecisionCardData> cards = new List<DecisionCardData>();
            int cardsGenerated = 0;
            
            for (int i = 0; i < cardCount; i++)
            {
                GenerateCard(day, (card) =>
                {
                    if (card != null)
                    {
                        cards.Add(card);
                    }
                    cardsGenerated++;
                    
                    if (cardsGenerated >= cardCount)
                    {
                        _generatedCardsByDay[day] = cards;
                        onComplete?.Invoke(cards);
                    }
                });
            }
        }
        
        /// <summary>
        /// Generate a single decision card
        /// </summary>
        public void GenerateCard(int day, System.Action<DecisionCardData> onComplete)
        {
            // Select appropriate template based on day
            CardTemplate template = SelectCardTemplate(day);
            
            if (useAIGeneration && AIAssetManager.Instance.IsConfigured())
            {
                GenerateCardWithAI(template, day, onComplete);
            }
            else if (useFallbackGeneration)
            {
                DecisionCardData card = GenerateCardProcedrually(template, day);
                onComplete?.Invoke(card);
            }
            else
            {
                Debug.LogWarning("Card generation disabled and no fallback available");
                onComplete?.Invoke(null);
            }
        }
        
        private void GenerateCardWithAI(CardTemplate template, int day, System.Action<DecisionCardData> onComplete)
        {
            string prompt = BuildCardGenerationPrompt(template, day);
            
            AIAssetManager.Instance.GenerateCardText(prompt, 
                (generatedText) =>
                {
                    DecisionCardData card = ParseGeneratedCardText(generatedText, template, day);
                    onComplete?.Invoke(card);
                },
                (error) =>
                {
                    Debug.LogWarning($"AI generation failed: {error}. Using procedural fallback.");
                    if (useFallbackGeneration)
                    {
                        DecisionCardData card = GenerateCardProcedrually(template, day);
                        onComplete?.Invoke(card);
                    }
                    else
                    {
                        onComplete?.Invoke(null);
                    }
                });
        }
        
        private DecisionCardData GenerateCardProcedrually(CardTemplate template, int day)
        {
            // Create ScriptableObject instance
            DecisionCardData card = ScriptableObject.CreateInstance<DecisionCardData>();
            
            // Fill in basic info
            card.cardTitle = ProcessTemplate(template.titleTemplate, day);
            card.description = ProcessTemplate(template.descriptionTemplate, day);
            card.cardType = template.cardType;
            card.dayRequirement = day;
            
            // Generate choices
            List<DecisionCardData.Choice> choices = new List<DecisionCardData.Choice>();
            foreach (var choiceTemplate in template.choiceTemplates)
            {
                DecisionCardData.Choice choice = new DecisionCardData.Choice
                {
                    choiceText = ProcessTemplate(choiceTemplate.text, day),
                    popularityChange = choiceTemplate.popularityRange.Random(),
                    stabilityChange = choiceTemplate.stabilityRange.Random(),
                    mediaTrustChange = choiceTemplate.mediaTrustRange.Random(),
                    economicHealthChange = choiceTemplate.economicRange.Random()
                };
                choices.Add(choice);
            }
            
            // Assign choices array (this would need to be done via reflection or a custom method)
            // card.choices = choices.ToArray(); // Note: This won't work directly with private serialized fields
            
            return card;
        }
        
        private string BuildCardGenerationPrompt(CardTemplate template, int day)
        {
            return $@"Generate a decision card for Executive Disorder, a political simulation game.
            
Context:
- Current Day: {day}
- Theme: {template.theme}
- Card Type: {template.cardType}
- Difficulty: {template.difficulty}

Requirements:
1. Create an engaging title (10-15 words max)
2. Write a compelling description of the situation (30-50 words)
3. Provide 2-3 choice options, each with:
   - Choice text (15-25 words)
   - Consequences for: Popularity, Stability, Media Trust, Economic Health (values between -20 to +20)

Theme Guidelines: {template.themeGuidelines}

Format the response as JSON:
{{
    ""title"": ""Card Title"",
    ""description"": ""Situation description"",
    ""choices"": [
        {{
            ""text"": ""Choice option"",
            ""popularity"": 0,
            ""stability"": 0,
            ""mediaTrust"": 0,
            ""economicHealth"": 0
        }}
    ]
}}";
        }
        
        private DecisionCardData ParseGeneratedCardText(string jsonText, CardTemplate template, int day)
        {
            DecisionCardData card = ScriptableObject.CreateInstance<DecisionCardData>();
            
            try
            {
                // Parse JSON response
                // In production, use a proper JSON library like Newtonsoft.Json
                // This is a simplified placeholder
                
                card.cardTitle = ExtractJsonValue(jsonText, "title");
                card.description = ExtractJsonValue(jsonText, "description");
                card.cardType = template.cardType;
                card.dayRequirement = day;
                
                // Parse choices (simplified)
                // In production, properly parse the choices array
                
                return card;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to parse generated card: {e.Message}");
                return GenerateCardProcedrually(template, day);
            }
        }
        
        private string ExtractJsonValue(string json, string key)
        {
            // Simple JSON value extraction - replace with proper JSON parser
            string searchKey = $"\"{key}\":\"";
            int startIndex = json.IndexOf(searchKey);
            if (startIndex == -1) return "";
            
            startIndex += searchKey.Length;
            int endIndex = json.IndexOf("\"", startIndex);
            if (endIndex == -1) return "";
            
            return json.Substring(startIndex, endIndex - startIndex);
        }
        
        #endregion
        
        #region Event Generation
        
        /// <summary>
        /// Generate a random event based on current game state
        /// </summary>
        public void GenerateRandomEvent(int day, Dictionary<string, float> resourceLevels, System.Action<string> onComplete)
        {
            EventTemplate template = SelectEventTemplate(day, resourceLevels);
            
            if (useAIGeneration && AIAssetManager.Instance.IsConfigured())
            {
                string prompt = BuildEventGenerationPrompt(template, day, resourceLevels);
                AIAssetManager.Instance.GenerateCardText(prompt, onComplete, 
                    (error) =>
                    {
                        Debug.LogWarning($"AI event generation failed: {error}");
                        onComplete?.Invoke(ProcessTemplate(template.eventText, day));
                    });
            }
            else
            {
                onComplete?.Invoke(ProcessTemplate(template.eventText, day));
            }
        }
        
        private string BuildEventGenerationPrompt(EventTemplate template, int day, Dictionary<string, float> resourceLevels)
        {
            return $@"Generate a news event for Executive Disorder game.

Current State:
- Day: {day}
- Popularity: {resourceLevels["Popularity"]}%
- Stability: {resourceLevels["Stability"]}%
- Media Trust: {resourceLevels["MediaTrust"]}%
- Economic Health: {resourceLevels["EconomicHealth"]}%

Event Type: {template.eventType}
Tone: {template.tone}

Generate a short news headline or event description (20-40 words) that reflects the current state and creates atmosphere.";
        }
        
        #endregion
        
        #region Dialogue Generation
        
        /// <summary>
        /// Generate character dialogue based on loyalty and context
        /// </summary>
        public void GenerateCharacterDialogue(string characterName, int loyalty, string context, System.Action<string> onComplete)
        {
            DialogueTemplate template = SelectDialogueTemplate(characterName, loyalty);
            
            if (useAIGeneration && AIAssetManager.Instance.IsConfigured())
            {
                string prompt = BuildDialoguePrompt(characterName, loyalty, context, template);
                AIAssetManager.Instance.GenerateCardText(prompt, onComplete,
                    (error) =>
                    {
                        Debug.LogWarning($"AI dialogue generation failed: {error}");
                        onComplete?.Invoke(ProcessTemplate(template.dialogueText, 0));
                    });
            }
            else
            {
                onComplete?.Invoke(ProcessTemplate(template.dialogueText, 0));
            }
        }
        
        private string BuildDialoguePrompt(string characterName, int loyalty, string context, DialogueTemplate template)
        {
            string loyaltyLevel = loyalty > 70 ? "loyal" : loyalty > 40 ? "neutral" : "hostile";
            
            return $@"Generate dialogue for {characterName} in Executive Disorder.

Character Info:
- Name: {characterName}
- Role: {template.characterRole}
- Loyalty: {loyalty}/100 ({loyaltyLevel})
- Personality: {template.personality}

Context: {context}

Generate a short dialogue response (15-30 words) that reflects their loyalty level and personality.";
        }
        
        #endregion
        
        #region Template Selection
        
        private CardTemplate SelectCardTemplate(int day)
        {
            // Filter templates appropriate for current day
            var appropriateTemplates = _cardTemplates.Where(t => 
                day >= t.minDay && day <= t.maxDay).ToList();
            
            if (appropriateTemplates.Count == 0)
                appropriateTemplates = _cardTemplates;
            
            // Weight selection by difficulty and day
            float difficultyWeight = Mathf.Clamp01(day / 100f);
            var template = appropriateTemplates[Random.Range(0, appropriateTemplates.Count)];
            
            return template;
        }
        
        private EventTemplate SelectEventTemplate(int day, Dictionary<string, float> resourceLevels)
        {
            // Select based on resource levels and day
            var criticalResources = resourceLevels.Where(r => r.Value < 30 || r.Value > 70).ToList();
            
            if (criticalResources.Count > 0 && _eventTemplates.Count > 0)
            {
                return _eventTemplates[Random.Range(0, _eventTemplates.Count)];
            }
            
            return _eventTemplates.Count > 0 ? _eventTemplates[0] : new EventTemplate();
        }
        
        private DialogueTemplate SelectDialogueTemplate(string characterName, int loyalty)
        {
            var templates = _dialogueTemplates.Where(t => 
                t.characterRole.ToLower().Contains(characterName.ToLower())).ToList();
            
            if (templates.Count == 0)
                templates = _dialogueTemplates;
            
            return templates.Count > 0 ? templates[Random.Range(0, templates.Count)] : new DialogueTemplate();
        }
        
        #endregion
        
        #region Template Processing
        
        private string ProcessTemplate(string template, int day)
        {
            // Replace template variables
            string result = template;
            result = result.Replace("{DAY}", day.ToString());
            result = result.Replace("{SEASON}", GetSeason(day));
            result = result.Replace("{YEAR}", GetYear(day));
            
            // Add more template variable replacements as needed
            
            return result;
        }
        
        private string GetSeason(int day)
        {
            int seasonIndex = (day / 25) % 4;
            string[] seasons = { "Spring", "Summer", "Fall", "Winter" };
            return seasons[seasonIndex];
        }
        
        private string GetYear(int day)
        {
            int year = 2025 + (day / 365);
            return year.ToString();
        }
        
        #endregion
        
        #region Default Templates
        
        private List<CardTemplate> GetDefaultCardTemplates()
        {
            return new List<CardTemplate>
            {
                new CardTemplate
                {
                    theme = "Economic Policy",
                    titleTemplate = "Budget Crisis on Day {DAY}",
                    descriptionTemplate = "The treasury reports a significant budget shortfall. How do you respond?",
                    cardType = DecisionCardData.CardType.Standard,
                    difficulty = 1,
                    minDay = 1,
                    maxDay = 100,
                    themeGuidelines = "Focus on economic trade-offs and public reaction",
                    choiceTemplates = new List<ChoiceTemplate>
                    {
                        new ChoiceTemplate { text = "Cut spending", popularityRange = new IntRange(-15, -5), stabilityRange = new IntRange(-10, 5), economicRange = new IntRange(5, 15) },
                        new ChoiceTemplate { text = "Raise taxes", popularityRange = new IntRange(-20, -10), mediaTrustRange = new IntRange(-5, 5), economicRange = new IntRange(10, 20) }
                    }
                },
                new CardTemplate
                {
                    theme = "Social Policy",
                    titleTemplate = "Healthcare Reform Debate",
                    descriptionTemplate = "Protests demand universal healthcare. The medical lobby opposes it.",
                    cardType = DecisionCardData.CardType.Standard,
                    difficulty = 2,
                    minDay = 10,
                    maxDay = 100,
                    themeGuidelines = "Balance public welfare with economic concerns",
                    choiceTemplates = new List<ChoiceTemplate>
                    {
                        new ChoiceTemplate { text = "Implement reform", popularityRange = new IntRange(5, 15), economicRange = new IntRange(-15, -5) },
                        new ChoiceTemplate { text = "Maintain status quo", popularityRange = new IntRange(-10, 0), stabilityRange = new IntRange(5, 10) }
                    }
                }
            };
        }
        
        private List<EventTemplate> GetDefaultEventTemplates()
        {
            return new List<EventTemplate>
            {
                new EventTemplate { eventType = "Economic", tone = "neutral", eventText = "Markets remain stable as Day {DAY} trading concludes." },
                new EventTemplate { eventType = "Social", tone = "positive", eventText = "Community groups praise recent policy decisions." },
                new EventTemplate { eventType = "Political", tone = "negative", eventText = "Opposition criticizes administration's handling of recent events." }
            };
        }
        
        private List<DialogueTemplate> GetDefaultDialogueTemplates()
        {
            return new List<DialogueTemplate>
            {
                new DialogueTemplate { characterRole = "Chief of Staff", personality = "Pragmatic", dialogueText = "We need to consider all angles here." },
                new DialogueTemplate { characterRole = "Press Secretary", personality = "Diplomatic", dialogueText = "I'll prepare a statement for the media." },
                new DialogueTemplate { characterRole = "Military Advisor", personality = "Direct", dialogueText = "Decisive action is needed now." }
            };
        }
        
        #endregion
    }
    
    #region Template Data Structures
    
    [System.Serializable]
    public class CardTemplate
    {
        public string theme;
        public string titleTemplate;
        public string descriptionTemplate;
        public string themeGuidelines;
        public DecisionCardData.CardType cardType;
        public int difficulty;
        public int minDay;
        public int maxDay;
        public List<ChoiceTemplate> choiceTemplates = new List<ChoiceTemplate>();
    }
    
    [System.Serializable]
    public class ChoiceTemplate
    {
        public string text;
        public IntRange popularityRange;
        public IntRange stabilityRange;
        public IntRange mediaTrustRange;
        public IntRange economicRange;
    }
    
    [System.Serializable]
    public class EventTemplate
    {
        public string eventType;
        public string tone;
        public string eventText;
    }
    
    [System.Serializable]
    public class DialogueTemplate
    {
        public string characterRole;
        public string personality;
        public string dialogueText;
    }
    
    [System.Serializable]
    public class IntRange
    {
        public int min;
        public int max;
        
        public IntRange(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
        
        public int Random()
        {
            return UnityEngine.Random.Range(min, max + 1);
        }
    }
    
    #endregion
}
