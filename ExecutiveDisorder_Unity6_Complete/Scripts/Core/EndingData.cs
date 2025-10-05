using UnityEngine;
using System.Collections.Generic;

namespace ExecutiveDisorder.Core
{
    /// <summary>
    /// ScriptableObject representing a game ending
    /// </summary>
    [CreateAssetMenu(fileName = "New Ending", menuName = "Executive Disorder/Ending")]
    public class EndingData : ScriptableObject
    {
        [Header("Ending Identity")]
        public string endingId = "ending_001";
        public string endingName = "Ending Name";
        public EndingType endingType = EndingType.Neutral;

        [Header("Description")]
        [TextArea(3, 6)]
        public string shortDescription = "What happened...";
        
        [TextArea(5, 10)]
        public string fullDescription = "Detailed ending narration...";

        [Header("Conditions")]
        [Tooltip("Resource conditions that trigger this ending")]
        public List<EndingCondition> conditions = new List<EndingCondition>();

        [Header("Visual")]
        public Sprite endingImage;
        public Color endingColor = Color.white;
        public Sprite backgroundImage;

        [Header("Audio")]
        public AudioClip endingMusic;
        public AudioClip endingNarration;

        [Header("Statistics")]
        [TextArea(2, 4)]
        public string statisticsSummary = "Your decisions led to...";
        public int scoreMultiplier = 1;

        [Header("Unlock")]
        public bool isSecret = false;
        public string unlockHint = "";
    }

    /// <summary>
    /// Condition that can trigger an ending
    /// </summary>
    [System.Serializable]
    public class EndingCondition
    {
        public ResourceType resourceType;
        public ComparisonType comparisonType;
        public float threshold;
    }

    public enum EndingType
    {
        Victory,        // Good ending
        Neutral,        // Mixed ending
        Defeat,         // Bad ending
        Absurd,         // Satirical ending
        Secret          // Hidden ending
    }

    public enum ComparisonType
    {
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        Equal
    }
}
