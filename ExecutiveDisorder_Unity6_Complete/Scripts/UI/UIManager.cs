using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

namespace ExecutiveDisorder.UI
{
    /// <summary>
    /// Central UI coordinator - manages all UI screens and transitions
    /// </summary>
    public class UIManager : Core.Singleton<UIManager>
    {
        [Header("UI Screens")]
        [SerializeField] private GameObject mainMenuScreen;
        [SerializeField] private GameObject gameplayScreen;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject endingScreen;
        [SerializeField] private GameObject settingsScreen;

        [Header("Gameplay UI")]
        [SerializeField] private GameHUDController hudController;
        [SerializeField] private CardUIController cardUIController;
        [SerializeField] private NewsTickerController newsTickerController;

        [Header("Resource Bars")]
        [SerializeField] private ResourceBarUI popularityBar;
        [SerializeField] private ResourceBarUI stabilityBar;
        [SerializeField] private ResourceBarUI mediaTrustBar;
        [SerializeField] private ResourceBarUI economicHealthBar;

        [Header("Panels")]
        [SerializeField] private GameObject characterPanel;
        [SerializeField] private GameObject newsPanel;
        [SerializeField] private GameObject statsPanel;

        [Header("Debug")]
        [SerializeField] private bool showDebugLogs = false;

        // Current screen
        private GameObject currentScreen;

        protected override void Awake()
        {
            base.Awake();
            
            // Subscribe to game events
            Core.GameManager.OnGameStart += OnGameStart;
            Core.GameManager.OnGameEnd += OnGameEnd;
            Core.GameManager.OnGamePaused += OnGamePaused;
            Core.GameManager.OnGameResumed += OnGameResumed;
            Core.GameManager.OnDayChanged += OnDayChanged;
            
            Core.ResourceManager.OnResourceChanged += OnResourceChanged;
        }

        private void Start()
        {
            // Show main menu on start
            ShowMainMenu();
        }

        private void OnDestroy()
        {
            // Unsubscribe from events
            if (Core.GameManager.Instance != null)
            {
                Core.GameManager.OnGameStart -= OnGameStart;
                Core.GameManager.OnGameEnd -= OnGameEnd;
                Core.GameManager.OnGamePaused -= OnGamePaused;
                Core.GameManager.OnGameResumed -= OnGameResumed;
                Core.GameManager.OnDayChanged -= OnDayChanged;
            }

            if (Core.ResourceManager.Instance != null)
            {
                Core.ResourceManager.OnResourceChanged -= OnResourceChanged;
            }
        }

        /// <summary>
        /// Show main menu
        /// </summary>
        public void ShowMainMenu()
        {
            SwitchScreen(mainMenuScreen);
            
            if (showDebugLogs)
                Debug.Log("[UIManager] Showing main menu");
        }

        /// <summary>
        /// Show gameplay screen
        /// </summary>
        public void ShowGameplay()
        {
            SwitchScreen(gameplayScreen);
            
            if (hudController != null)
                hudController.Show();

            if (showDebugLogs)
                Debug.Log("[UIManager] Showing gameplay");
        }

        /// <summary>
        /// Show pause screen
        /// </summary>
        public void ShowPauseMenu()
        {
            if (pauseScreen != null)
            {
                pauseScreen.SetActive(true);
            }

            if (showDebugLogs)
                Debug.Log("[UIManager] Showing pause menu");
        }

        /// <summary>
        /// Hide pause screen
        /// </summary>
        public void HidePauseMenu()
        {
            if (pauseScreen != null)
            {
                pauseScreen.SetActive(false);
            }
        }

        /// <summary>
        /// Show ending screen with specific ending data
        /// </summary>
        public void ShowEndingScreen(Core.EndingData ending)
        {
            SwitchScreen(endingScreen);

            // Get ending screen controller
            var endingController = endingScreen?.GetComponent<EndingScreenController>();
            if (endingController != null && ending != null)
            {
                endingController.DisplayEnding(ending);
            }

            if (showDebugLogs)
                Debug.Log($"[UIManager] Showing ending: {ending?.endingName}");
        }

        /// <summary>
        /// Show settings screen
        /// </summary>
        public void ShowSettings()
        {
            if (settingsScreen != null)
            {
                settingsScreen.SetActive(true);
            }
        }

        /// <summary>
        /// Hide settings screen
        /// </summary>
        public void HideSettings()
        {
            if (settingsScreen != null)
            {
                settingsScreen.SetActive(false);
            }
        }

        /// <summary>
        /// Switch between UI screens
        /// </summary>
        private void SwitchScreen(GameObject newScreen)
        {
            // Hide current screen
            if (currentScreen != null)
            {
                currentScreen.SetActive(false);
            }

            // Show new screen
            if (newScreen != null)
            {
                newScreen.SetActive(true);
                currentScreen = newScreen;
            }
        }

        /// <summary>
        /// Update resource bar display
        /// </summary>
        private void UpdateResourceBar(Core.ResourceType type, float value)
        {
            ResourceBarUI targetBar = type switch
            {
                Core.ResourceType.Popularity => popularityBar,
                Core.ResourceType.Stability => stabilityBar,
                Core.ResourceType.MediaTrust => mediaTrustBar,
                Core.ResourceType.EconomicHealth => economicHealthBar,
                _ => null
            };

            if (targetBar != null)
            {
                targetBar.SetValue(value);
            }
        }

        /// <summary>
        /// Show a temporary message/notification
        /// </summary>
        public void ShowNotification(string message, float duration = 3f)
        {
            if (newsTickerController != null)
            {
                newsTickerController.ShowMessage(message, duration);
            }
        }

        /// <summary>
        /// Show card UI
        /// </summary>
        public void ShowCard(Core.DecisionCardData card)
        {
            if (cardUIController != null)
            {
                cardUIController.DisplayCard(card);
            }
        }

        /// <summary>
        /// Hide card UI
        /// </summary>
        public void HideCard()
        {
            if (cardUIController != null)
            {
                cardUIController.HideCard();
            }
        }

        // Event Handlers
        private void OnGameStart()
        {
            ShowGameplay();
            UpdateAllResourceBars();
        }

        private void OnGameEnd()
        {
            // Ending screen shown by GameManager
        }

        private void OnGamePaused()
        {
            ShowPauseMenu();
        }

        private void OnGameResumed()
        {
            HidePauseMenu();
        }

        private void OnDayChanged(int day)
        {
            if (hudController != null)
            {
                hudController.UpdateDay(day);
            }
        }

        private void OnResourceChanged(Core.ResourceType type, float oldValue, float newValue)
        {
            UpdateResourceBar(type, newValue);
        }

        private void UpdateAllResourceBars()
        {
            var resources = Core.ResourceManager.Instance?.GetAllResources();
            if (resources != null)
            {
                foreach (var resource in resources)
                {
                    UpdateResourceBar(resource.Key, resource.Value);
                }
            }
        }
    }
}
