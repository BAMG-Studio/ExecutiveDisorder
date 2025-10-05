using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExecutiveDisorder.Core
{
    /// <summary>
    /// Manages all characters, their loyalty, and interactions
    /// </summary>
    public class CharacterManager : Singleton<CharacterManager>
    {
        [Header("Character Configuration")]
        [SerializeField] private List<CharacterData> allCharacters = new List<CharacterData>();
        [SerializeField] private bool autoLoadFromResources = true;
        [SerializeField] private string resourcesPath = "Characters";

        [Header("Debug")]
        [SerializeField] private bool showDebugLogs = false;

        // Active characters
        private Dictionary<string, CharacterData> characterLookup = new Dictionary<string, CharacterData>();

        // Events
        public static event Action<CharacterData, int, int> OnLoyaltyChanged; // character, oldLoyalty, newLoyalty
        public static event Action<CharacterData> OnCharacterBecameLoyal;
        public static event Action<CharacterData> OnCharacterBecameHostile;
        public static event Action<CharacterData> OnCharacterLeft;
        public static event Action<CharacterData> OnCharacterJoined;

        protected override void Awake()
        {
            base.Awake();
            
            if (autoLoadFromResources)
            {
                LoadCharactersFromResources();
            }
        }

        /// <summary>
        /// Load characters from Resources folder
        /// </summary>
        private void LoadCharactersFromResources()
        {
            var loadedCharacters = Resources.LoadAll<CharacterData>(resourcesPath);
            if (loadedCharacters != null && loadedCharacters.Length > 0)
            {
                allCharacters = loadedCharacters.ToList();
                if (showDebugLogs)
                    Debug.Log($"[CharacterManager] Loaded {allCharacters.Count} characters from Resources/{resourcesPath}");
            }
        }

        /// <summary>
        /// Initialize all characters
        /// </summary>
        public void InitializeCharacters()
        {
            characterLookup.Clear();

            foreach (var character in allCharacters)
            {
                if (character != null)
                {
                    character.Reset();
                    characterLookup[character.characterId] = character;
                }
            }

            if (showDebugLogs)
                Debug.Log($"[CharacterManager] Initialized {characterLookup.Count} characters");
        }

        /// <summary>
        /// Get character by ID
        /// </summary>
        public CharacterData GetCharacter(string characterId)
        {
            return characterLookup.ContainsKey(characterId) ? characterLookup[characterId] : null;
        }

        /// <summary>
        /// Get all active characters
        /// </summary>
        public IEnumerable<CharacterData> GetAllCharacters()
        {
            return characterLookup.Values.Where(c => c.isActive);
        }

        /// <summary>
        /// Get characters by archetype
        /// </summary>
        public IEnumerable<CharacterData> GetCharactersByArchetype(CharacterArchetype archetype)
        {
            return allCharacters.Where(c => c.archetype == archetype && c.isActive);
        }

        /// <summary>
        /// Modify character loyalty
        /// </summary>
        public void ModifyLoyalty(string characterId, int amount)
        {
            var character = GetCharacter(characterId);
            if (character == null)
            {
                Debug.LogWarning($"[CharacterManager] Character not found: {characterId}");
                return;
            }

            int oldLoyalty = character.currentLoyalty;
            character.ModifyLoyalty(amount);
            int newLoyalty = character.currentLoyalty;

            if (oldLoyalty != newLoyalty)
            {
                OnLoyaltyChanged?.Invoke(character, oldLoyalty, newLoyalty);

                // Check threshold crossings
                CheckLoyaltyThresholds(character, oldLoyalty, newLoyalty);

                if (showDebugLogs)
                    Debug.Log($"[CharacterManager] {character.characterName} loyalty: {oldLoyalty} → {newLoyalty}");
            }
        }

        /// <summary>
        /// Modify multiple character loyalties
        /// </summary>
        public void ModifyLoyalties(Dictionary<string, int> changes)
        {
            foreach (var change in changes)
            {
                ModifyLoyalty(change.Key, change.Value);
            }
        }

        /// <summary>
        /// Check if loyalty crossed important thresholds
        /// </summary>
        private void CheckLoyaltyThresholds(CharacterData character, int oldLoyalty, int newLoyalty)
        {
            // Became loyal (crossed 70)
            if (newLoyalty >= 70 && oldLoyalty < 70)
            {
                OnCharacterBecameLoyal?.Invoke(character);
            }

            // Became hostile (crossed 30)
            if (newLoyalty <= 30 && oldLoyalty > 30)
            {
                OnCharacterBecameHostile?.Invoke(character);
            }

            // Left (hit 0)
            if (newLoyalty <= 0 && oldLoyalty > 0)
            {
                character.isActive = false;
                OnCharacterLeft?.Invoke(character);
            }
        }

        /// <summary>
        /// Get random dialogue from character
        /// </summary>
        public string GetDialogue(string characterId, DialogueType type)
        {
            var character = GetCharacter(characterId);
            if (character == null)
                return "";

            return character.GetRandomDialogue(type);
        }

        /// <summary>
        /// Update character relationships based on player actions
        /// </summary>
        public void UpdateRelationships()
        {
            foreach (var character in GetAllCharacters())
            {
                // Characters with allies and rivals affect each other
                foreach (var allyId in character.alliedCharacterIds)
                {
                    var ally = GetCharacter(allyId);
                    if (ally != null && ally.isActive)
                    {
                        // Allies slightly boost each other's loyalty
                        if (character.currentLoyalty > 60)
                        {
                            ModifyLoyalty(allyId, 1);
                        }
                    }
                }

                foreach (var rivalId in character.rivalCharacterIds)
                {
                    var rival = GetCharacter(rivalId);
                    if (rival != null && rival.isActive)
                    {
                        // If character is loyal, rival becomes less loyal
                        if (character.currentLoyalty > 70)
                        {
                            ModifyLoyalty(rivalId, -1);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check for special ending conditions based on characters
        /// </summary>
        public bool CheckSpecialEndings()
        {
            // All characters hostile - Military Coup
            bool allHostile = true;
            foreach (var character in GetAllCharacters())
            {
                if (character.currentLoyalty > 30)
                {
                    allHostile = false;
                    break;
                }
            }

            if (allHostile)
            {
                if (showDebugLogs)
                    Debug.Log("[CharacterManager] All characters hostile - special ending triggered");
                return true;
            }

            // POTUS-9000 at max loyalty - Robot Overlord ending
            var potus = GetCharacter("potus_9000");
            if (potus != null && potus.currentLoyalty >= 100)
            {
                if (showDebugLogs)
                    Debug.Log("[CharacterManager] POTUS-9000 at max loyalty - special ending");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get number of loyal characters
        /// </summary>
        public int GetLoyalCharacterCount()
        {
            return GetAllCharacters().Count(c => c.currentLoyalty >= 70);
        }

        /// <summary>
        /// Get number of hostile characters
        /// </summary>
        public int GetHostileCharacterCount()
        {
            return GetAllCharacters().Count(c => c.currentLoyalty <= 30);
        }

        /// <summary>
        /// Save character state
        /// </summary>
        public CharacterStateSaveData SaveState()
        {
            var stateData = new CharacterStateSaveData
            {
                characterStates = new Dictionary<string, CharacterState>()
            };

            foreach (var character in allCharacters)
            {
                if (character != null)
                {
                    stateData.characterStates[character.characterId] = new CharacterState
                    {
                        loyalty = character.currentLoyalty,
                        isActive = character.isActive
                    };
                }
            }

            return stateData;
        }

        /// <summary>
        /// Load character state
        /// </summary>
        public void LoadState(CharacterStateSaveData saveData)
        {
            if (saveData == null || saveData.characterStates == null)
                return;

            foreach (var state in saveData.characterStates)
            {
                var character = GetCharacter(state.Key);
                if (character != null)
                {
                    character.currentLoyalty = state.Value.loyalty;
                    character.isActive = state.Value.isActive;
                }
            }

            if (showDebugLogs)
                Debug.Log("[CharacterManager] State loaded from save");
        }
    }

    /// <summary>
    /// Character state for save/load
    /// </summary>
    [System.Serializable]
    public class CharacterState
    {
        public int loyalty;
        public bool isActive;
    }

    /// <summary>
    /// Character state save data
    /// </summary>
    [System.Serializable]
    public class CharacterStateSaveData
    {
        public Dictionary<string, CharacterState> characterStates;
    }
}
