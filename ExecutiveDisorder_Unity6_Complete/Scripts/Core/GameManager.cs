using UnityEngine;
using System;
using System.Collections;

namespace ExecutiveDisorder.Core
{
    /// <summary>
    /// Central game manager - controls overall game flow and coordinates all systems
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        [Header("Game Configuration")]
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private bool debugMode = false;

        [Header("Game State")]
        [SerializeField] private int currentDay = 1;
        [SerializeField] private int totalDays = 100;
        [SerializeField] private float gameSpeed = 1f;

        // Events
        public static event Action OnGameStart;
        public static event Action OnGameEnd;
        public static event Action<int> OnDayChanged;
        public static event Action OnGamePaused;
        public static event Action OnGameResumed;

        // Properties
        public int CurrentDay => currentDay;
        public int TotalDays => totalDays;
        public float GameSpeed => gameSpeed;
        public bool IsGameActive { get; private set; }
        public bool IsPaused { get; private set; }
        public GameSettings Settings => gameSettings;

        protected override void Awake()
        {
            base.Awake();
            
            // Initialize core systems
            InitializeSystems();
        }

        private void Start()
        {
            // Don't auto-start - wait for player to click "New Game"
            if (debugMode)
            {
                Debug.Log("[GameManager] Ready - waiting for game start");
            }
        }

        private void InitializeSystems()
        {
            // Ensure all managers are present
            if (ResourceManager.Instance == null)
                Debug.LogError("[GameManager] ResourceManager not found!");
            
            if (CardManager.Instance == null)
                Debug.LogError("[GameManager] CardManager not found!");
            
            if (CharacterManager.Instance == null)
                Debug.LogError("[GameManager] CharacterManager not found!");

            if (AudioManager.Instance == null)
                Debug.LogWarning("[GameManager] AudioManager not found - game will run without audio");

            if (debugMode)
                Debug.Log("[GameManager] Systems initialized");
        }

        /// <summary>
        /// Start a new game
        /// </summary>
        public void StartNewGame()
        {
            if (debugMode)
                Debug.Log("[GameManager] Starting new game...");

            // Reset game state
            currentDay = 1;
            IsGameActive = true;
            IsPaused = false;

            // Initialize all managers
            ResourceManager.Instance?.InitializeResources();
            CardManager.Instance?.InitializeDeck();
            CharacterManager.Instance?.InitializeCharacters();

            // Fire event
            OnGameStart?.Invoke();
            OnDayChanged?.Invoke(currentDay);

            // Start gameplay
            StartCoroutine(GameLoop());
        }

        /// <summary>
        /// Load existing game
        /// </summary>
        public void LoadGame(string saveSlot)
        {
            if (debugMode)
                Debug.Log($"[GameManager] Loading game from slot: {saveSlot}");

            var saveData = SaveLoadManager.Instance?.LoadGame(saveSlot);
            if (saveData != null)
            {
                // Restore game state
                currentDay = saveData.currentDay;
                IsGameActive = true;
                IsPaused = false;

                // Restore all managers
                ResourceManager.Instance?.LoadState(saveData.resources);
                CardManager.Instance?.LoadState(saveData.cardState);
                CharacterManager.Instance?.LoadState(saveData.characterState);

                // Fire events
                OnGameStart?.Invoke();
                OnDayChanged?.Invoke(currentDay);

                // Resume gameplay
                StartCoroutine(GameLoop());
            }
            else
            {
                Debug.LogError("[GameManager] Failed to load game!");
            }
        }

        /// <summary>
        /// Main game loop
        /// </summary>
        private IEnumerator GameLoop()
        {
            while (IsGameActive && currentDay <= totalDays)
            {
                // Wait if paused
                while (IsPaused)
                {
                    yield return null;
                }

                // Present decision card for the day
                CardManager.Instance?.PresentNextCard();

                // Wait for player decision
                yield return new WaitUntil(() => CardManager.Instance.IsWaitingForNextCard == false);

                // Process consequences
                yield return ProcessDayEnd();

                // Check for game over conditions
                if (CheckGameOverConditions())
                {
                    EndGame();
                    yield break;
                }

                // Advance to next day
                AdvanceDay();

                // Small delay between days
                yield return new WaitForSeconds(0.5f / gameSpeed);
            }

            // Reached max days without game over
            if (currentDay > totalDays)
            {
                EndGame();
            }
        }

        /// <summary>
        /// Process end of day events
        /// </summary>
        private IEnumerator ProcessDayEnd()
        {
            // Trigger random events
            if (UnityEngine.Random.value < 0.2f) // 20% chance per day
            {
                EventManager.Instance?.TriggerRandomEvent();
            }

            // Update character relationships
            CharacterManager.Instance?.UpdateRelationships();

            // Check for cascading effects
            ResourceManager.Instance?.ProcessCascadingEffects();

            yield return new WaitForSeconds(0.5f);
        }

        /// <summary>
        /// Advance to next day
        /// </summary>
        private void AdvanceDay()
        {
            currentDay++;
            OnDayChanged?.Invoke(currentDay);

            if (debugMode)
                Debug.Log($"[GameManager] Day {currentDay}/{totalDays}");

            // Auto-save every 10 days
            if (currentDay % 10 == 0)
            {
                SaveLoadManager.Instance?.QuickSave();
            }
        }

        /// <summary>
        /// Check if game should end
        /// </summary>
        private bool CheckGameOverConditions()
        {
            // Check resource thresholds
            var resources = ResourceManager.Instance?.GetAllResources();
            if (resources != null)
            {
                foreach (var resource in resources)
                {
                    if (resource.Value <= 0 || resource.Value >= 100)
                    {
                        if (debugMode)
                            Debug.Log($"[GameManager] Game over: {resource.Key} at {resource.Value}");
                        return true;
                    }
                }
            }

            // Check for special ending conditions
            if (CharacterManager.Instance?.CheckSpecialEndings() == true)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// End the game and show ending
        /// </summary>
        public void EndGame()
        {
            IsGameActive = false;

            if (debugMode)
                Debug.Log("[GameManager] Game ended");

            // Determine ending
            var ending = DetermineEnding();

            // Fire event
            OnGameEnd?.Invoke();

            // Show ending screen
            UIManager.Instance?.ShowEndingScreen(ending);

            // Stop game loop
            StopAllCoroutines();
        }

        /// <summary>
        /// Determine which ending to show based on game state
        /// </summary>
        private EndingData DetermineEnding()
        {
            var resources = ResourceManager.Instance?.GetAllResources();
            var characters = CharacterManager.Instance?.GetAllCharacters();

            // Check for specific ending conditions
            if (resources == null)
                return null;

            // Nuclear Winter - Stability at 0
            if (resources.ContainsKey(ResourceType.Stability) && resources[ResourceType.Stability] <= 0)
            {
                return EndingDatabase.Instance?.GetEnding("nuclear_winter");
            }

            // Economic Collapse - Economic Health at 0
            if (resources.ContainsKey(ResourceType.EconomicHealth) && resources[ResourceType.EconomicHealth] <= 0)
            {
                return EndingDatabase.Instance?.GetEnding("economic_collapse");
            }

            // Autocratic Empire - Stability at 100
            if (resources.ContainsKey(ResourceType.Stability) && resources[ResourceType.Stability] >= 100)
            {
                return EndingDatabase.Instance?.GetEnding("autocratic_empire");
            }

            // Media Revolution - Media Trust at 0
            if (resources.ContainsKey(ResourceType.MediaTrust) && resources[ResourceType.MediaTrust] <= 0)
            {
                return EndingDatabase.Instance?.GetEnding("media_revolution");
            }

            // Impeachment - Popularity at 0
            if (resources.ContainsKey(ResourceType.Popularity) && resources[ResourceType.Popularity] <= 0)
            {
                return EndingDatabase.Instance?.GetEnding("impeachment");
            }

            // Check for balanced ending
            bool allBalanced = true;
            foreach (var resource in resources)
            {
                if (resource.Value < 40 || resource.Value > 60)
                {
                    allBalanced = false;
                    break;
                }
            }

            if (allBalanced)
            {
                return EndingDatabase.Instance?.GetEnding("democratic_victory");
            }

            // Default - survived to day 100
            return EndingDatabase.Instance?.GetEnding("chaos_reigns");
        }

        /// <summary>
        /// Pause the game
        /// </summary>
        public void PauseGame()
        {
            if (!IsPaused)
            {
                IsPaused = true;
                Time.timeScale = 0f;
                OnGamePaused?.Invoke();

                if (debugMode)
                    Debug.Log("[GameManager] Game paused");
            }
        }

        /// <summary>
        /// Resume the game
        /// </summary>
        public void ResumeGame()
        {
            if (IsPaused)
            {
                IsPaused = false;
                Time.timeScale = gameSpeed;
                OnGameResumed?.Invoke();

                if (debugMode)
                    Debug.Log("[GameManager] Game resumed");
            }
        }

        /// <summary>
        /// Quit to main menu
        /// </summary>
        public void QuitToMenu()
        {
            IsGameActive = false;
            IsPaused = false;
            Time.timeScale = 1f;
            
            StopAllCoroutines();
            
            // Load main menu scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        /// <summary>
        /// Set game speed multiplier
        /// </summary>
        public void SetGameSpeed(float speed)
        {
            gameSpeed = Mathf.Clamp(speed, 0.5f, 3f);
            if (!IsPaused)
            {
                Time.timeScale = gameSpeed;
            }
        }

        private void OnApplicationQuit()
        {
            // Auto-save on quit
            if (IsGameActive)
            {
                SaveLoadManager.Instance?.QuickSave();
            }
        }
    }
}
