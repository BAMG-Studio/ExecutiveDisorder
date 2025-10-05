using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ExecutiveDisorder.UI
{
    /// <summary>
    /// Controls the main gameplay HUD display
    /// </summary>
    public class GameHUDController : MonoBehaviour
    {
        [Header("Day Display")]
        [SerializeField] private TextMeshProUGUI dayText;
        [SerializeField] private TextMeshProUGUI dayProgressText;

        [Header("Buttons")]
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button helpButton;

        [Header("Panels")]
        [SerializeField] private GameObject quickStatsPanel;
        [SerializeField] private TextMeshProUGUI loyalCharactersText;
        [SerializeField] private TextMeshProUGUI hostileCharactersText;

        [Header("Notifications")]
        [SerializeField] private GameObject notificationPanel;
        [SerializeField] private TextMeshProUGUI notificationText;

        private void Start()
        {
            // Setup button listeners
            if (pauseButton != null)
            {
                pauseButton.onClick.AddListener(OnPauseClicked);
            }

            if (settingsButton != null)
            {
                settingsButton.onClick.AddListener(OnSettingsClicked);
            }

            if (helpButton != null)
            {
                helpButton.onClick.AddListener(OnHelpClicked);
            }

            // Subscribe to game events
            Core.GameManager.OnDayChanged += UpdateDay;
            Core.CharacterManager.OnLoyaltyChanged += OnLoyaltyChanged;

            // Hide notification initially
            if (notificationPanel != null)
            {
                notificationPanel.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            if (Core.GameManager.Instance != null)
            {
                Core.GameManager.OnDayChanged -= UpdateDay;
            }

            if (Core.CharacterManager.Instance != null)
            {
                Core.CharacterManager.OnLoyaltyChanged -= OnLoyaltyChanged;
            }
        }

        /// <summary>
        /// Show HUD
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
            UpdateDay(Core.GameManager.Instance?.CurrentDay ?? 1);
            UpdateQuickStats();
        }

        /// <summary>
        /// Hide HUD
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Update day display
        /// </summary>
        public void UpdateDay(int day)
        {
            if (dayText != null)
            {
                dayText.text = $"Day {day}";
            }

            int totalDays = Core.GameManager.Instance?.TotalDays ?? 100;
            if (dayProgressText != null)
            {
                dayProgressText.text = $"{day} / {totalDays}";
            }
        }

        /// <summary>
        /// Update quick stats panel
        /// </summary>
        private void UpdateQuickStats()
        {
            if (Core.CharacterManager.Instance == null)
                return;

            int loyal = Core.CharacterManager.Instance.GetLoyalCharacterCount();
            int hostile = Core.CharacterManager.Instance.GetHostileCharacterCount();

            if (loyalCharactersText != null)
            {
                loyalCharactersText.text = $"Loyal: {loyal}";
            }

            if (hostileCharactersText != null)
            {
                hostileCharactersText.text = $"Hostile: {hostile}";
            }
        }

        /// <summary>
        /// Show notification message
        /// </summary>
        public void ShowNotification(string message, float duration = 3f)
        {
            if (notificationPanel != null && notificationText != null)
            {
                notificationText.text = message;
                notificationPanel.SetActive(true);
                
                CancelInvoke(nameof(HideNotification));
                Invoke(nameof(HideNotification), duration);
            }
        }

        private void HideNotification()
        {
            if (notificationPanel != null)
            {
                notificationPanel.SetActive(false);
            }
        }

        // Button Handlers
        private void OnPauseClicked()
        {
            Audio.AudioManager.Instance?.PlayButtonClick();
            Core.GameManager.Instance?.PauseGame();
        }

        private void OnSettingsClicked()
        {
            Audio.AudioManager.Instance?.PlayButtonClick();
            UIManager.Instance?.ShowSettings();
        }

        private void OnHelpClicked()
        {
            Audio.AudioManager.Instance?.PlayButtonClick();
            // Show help/tutorial
        }

        // Event Handlers
        private void OnLoyaltyChanged(Core.CharacterData character, int oldLoyalty, int newLoyalty)
        {
            UpdateQuickStats();

            // Show notification on threshold crossing
            if (newLoyalty >= 70 && oldLoyalty < 70)
            {
                ShowNotification($"{character.characterName} is now loyal!");
            }
            else if (newLoyalty <= 30 && oldLoyalty > 30)
            {
                ShowNotification($"{character.characterName} is now hostile!");
            }
        }
    }
}
