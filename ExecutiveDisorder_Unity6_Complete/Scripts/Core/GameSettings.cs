using UnityEngine;

namespace ExecutiveDisorder.Core
{
    /// <summary>
    /// Global game settings and configuration
    /// </summary>
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Executive Disorder/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Game Configuration")]
        [Tooltip("Total days in a playthrough")]
        public int totalGameDays = 100;

        [Tooltip("Default game speed multiplier")]
        [Range(0.5f, 3f)]
        public float defaultGameSpeed = 1f;

        [Header("Resource Settings")]
        [Tooltip("Starting value for all resources")]
        [Range(0f, 100f)]
        public float defaultStartingResource = 50f;

        [Tooltip("Minimum resource value before game over")]
        public float minResourceThreshold = 0f;

        [Tooltip("Maximum resource value before game over")]
        public float maxResourceThreshold = 100f;

        [Header("Card Settings")]
        [Tooltip("Maximum cards in player hand")]
        public int maxHandSize = 3;

        [Tooltip("Shuffle deck at start")]
        public bool shuffleDeck = true;

        [Tooltip("Time limit for crisis cards (seconds)")]
        public float crisisTimeLimit = 30f;

        [Header("Character Settings")]
        [Tooltip("Starting loyalty for all characters")]
        [Range(0, 100)]
        public int defaultCharacterLoyalty = 50;

        [Tooltip("Loyalty threshold for loyal characters")]
        [Range(0, 100)]
        public int loyalThreshold = 70;

        [Tooltip("Loyalty threshold for hostile characters")]
        [Range(0, 100)]
        public int hostileThreshold = 30;

        [Header("Difficulty Settings")]
        [Tooltip("Overall game difficulty")]
        public DifficultyLevel difficulty = DifficultyLevel.Normal;

        [Tooltip("Resource decay rate per day")]
        [Range(0f, 5f)]
        public float resourceDecayRate = 0.5f;

        [Tooltip("Crisis frequency (0-1, where 1 = every day)")]
        [Range(0f, 1f)]
        public float crisisFrequency = 0.2f;

        [Header("UI Settings")]
        [Tooltip("Enable animations")]
        public bool enableAnimations = true;

        [Tooltip("Animation speed multiplier")]
        [Range(0.5f, 2f)]
        public float animationSpeed = 1f;

        [Tooltip("Show tutorial hints")]
        public bool showTutorial = true;

        [Header("Audio Settings")]
        [Tooltip("Enable background music")]
        public bool enableMusic = true;

        [Tooltip("Enable sound effects")]
        public bool enableSoundEffects = true;

        [Tooltip("Master volume")]
        [Range(0f, 1f)]
        public float masterVolume = 1f;

        [Header("Save/Load Settings")]
        [Tooltip("Enable auto-save")]
        public bool enableAutoSave = true;

        [Tooltip("Auto-save interval (seconds)")]
        public float autoSaveInterval = 300f;

        [Tooltip("Maximum save slots")]
        public int maxSaveSlots = 3;

        [Header("Debug Settings")]
        [Tooltip("Enable debug mode")]
        public bool debugMode = false;

        [Tooltip("Show debug UI")]
        public bool showDebugUI = false;

        [Tooltip("Enable console logs")]
        public bool enableDebugLogs = false;

        [Tooltip("Invincibility mode (resources never hit 0 or 100)")]
        public bool godMode = false;
    }

    public enum DifficultyLevel
    {
        Easy,       // Forgiving resource changes
        Normal,     // Standard game
        Hard,       // Punishing resource changes
        Chaos       // Extreme difficulty
    }
}
