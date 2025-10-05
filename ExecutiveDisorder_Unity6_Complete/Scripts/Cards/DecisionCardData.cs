using UnityEngine;
using System.Collections.Generic;

namespace ExecutiveDisorder.Core
{
    /// <summary>
    /// ScriptableObject representing a decision card
    /// </summary>
    [CreateAssetMenu(fileName = "New Decision Card", menuName = "Executive Disorder/Decision Card")]
    public class DecisionCardData : ScriptableObject
    {
        [Header("Card Identity")]
        public string id = "card_001";
        public string title = "A Difficult Decision";
        
        [TextArea(3, 6)]
        public string description = "You must decide...";

        [Header("Card Properties")]
        public CardCategory category = CardCategory.Normal;
        public CardRarity rarity = CardRarity.Common;
        public UrgencyLevel urgency = UrgencyLevel.Normal;
        public int timeLimit = 0; // Seconds for crisis cards (0 = no limit)

        [Header("Choices")]
        public List<CardChoiceData> choices = new List<CardChoiceData>();

        [Header("Requirements")]
        public CardRequirements requirements;

        [Header("Presentation")]
        public string characterId; // Which character presents this card
        public Sprite cardImage;
        public AudioClip cardSound;
        public Color cardColor = Color.white;

        [Header("Narrative")]
        [TextArea(2, 4)]
        public string newsHeadline = ""; // Generated headline after choice
        [TextArea(2, 4)]
        public string flavorText = ""; // Additional context

        private void OnValidate()
        {
            // Ensure at least 2 choices
            if (choices.Count < 2)
            {
                while (choices.Count < 2)
                {
                    choices.Add(new CardChoiceData());
                }
            }
        }
    }

    /// <summary>
    /// A choice the player can make on a card
    /// </summary>
    [System.Serializable]
    public class CardChoiceData
    {
        [TextArea(1, 3)]
        public string text = "Make this choice";

        [Header("Resource Effects")]
        public Dictionary<ResourceType, float> resourceEffects = new Dictionary<ResourceType, float>();

        [Header("Character Effects")]
        public Dictionary<string, int> loyaltyEffects = new Dictionary<string, int>(); // characterId -> loyalty change

        [Header("Consequences")]
        public List<string> consequences = new List<string>(); // Event IDs to trigger
        
        [TextArea(1, 2)]
        public string consequencePreview = ""; // Hint about what happens

        [Header("Follow-up")]
        public List<string> followupCardIds = new List<string>(); // Cards that appear after this choice

        [Header("Flavor")]
        public ChoiceAlignment alignment = ChoiceAlignment.Neutral;
        public Sprite choiceIcon;
    }

    /// <summary>
    /// Requirements for a card to appear
    /// </summary>
    [System.Serializable]
    public class CardRequirements
    {
        [Header("Day Requirements")]
        public int? minDay;
        public int? maxDay;

        [Header("Resource Requirements")]
        public Dictionary<ResourceType, float> minResourceValues;
        public Dictionary<ResourceType, float> maxResourceValues;

        [Header("Card History")]
        public List<string> requiredPreviousCards; // Must have played these cards
        public List<string> blockedByCards; // Can't appear if these were played

        [Header("Character Requirements")]
        public string requiredCharacterPresent; // Character must be active
        public int? requiredMinLoyalty; // Character loyalty threshold
    }

    public enum CardCategory
    {
        Normal,      // Standard policy decision
        Crisis,      // Urgent situation
        Scandal,     // Political scandal
        Opportunity, // Positive event
        Character,   // Character-specific
        Absurd,      // Satirical/ridiculous
        EndGame      // Late-game event
    }

    public enum CardRarity
    {
        Common,      // Appears frequently
        Uncommon,    // Appears occasionally
        Rare,        // Appears rarely
        Epic,        // Very rare
        Legendary    // Unique/special
    }

    public enum UrgencyLevel
    {
        Normal,      // Standard card
        Elevated,    // Slight urgency
        Urgent,      // High urgency
        Critical     // Immediate action required
    }

    public enum ChoiceAlignment
    {
        Neutral,       // Balanced choice
        Cautious,      // Conservative/safe
        Aggressive,    // Bold/risky
        Chaotic,       // Unpredictable
        Diplomatic,    // Peaceful/negotiated
        Authoritarian  // Strong-handed
    }
}
