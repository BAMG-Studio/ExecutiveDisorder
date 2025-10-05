using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace ExecutiveDisorder.UI
{
    /// <summary>
    /// Displays game ending screen with results
    /// </summary>
    public class EndingScreenController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI endingNameText;
        [SerializeField] private TextMeshProUGUI endingDescriptionText;
        [SerializeField] private Image endingImage;
        [SerializeField] private Image backgroundImage;

        [Header("Statistics")]
        [SerializeField] private TextMeshProUGUI daysSurvivedText;
        [SerializeField] private TextMeshProUGUI finalScoreText;
        [SerializeField] private GameObject statisticsPanel;

        [Header("Resource Final Values")]
        [SerializeField] private TextMeshProUGUI popularityFinalText;
        [SerializeField] private TextMeshProUGUI stabilityFinalText;
        [SerializeField] private TextMeshProUGUI mediaTrustFinalText;
        [SerializeField] private TextMeshProUGUI economicHealthFinalText;

        [Header("Buttons")]
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button shareButton;

        [Header("Animation")]
        [SerializeField] private float fadeInDuration = 1f;
        [SerializeField] private CanvasGroup canvasGroup;

        private Core.EndingData currentEnding;

        private void Start()
        {
            // Setup button listeners
            if (mainMenuButton != null)
            {
                mainMenuButton.onClick.AddListener(OnMainMenuClicked);
            }

            if (playAgainButton != null)
            {
                playAgainButton.onClick.AddListener(OnPlayAgainClicked);
            }

            if (shareButton != null)
            {
                shareButton.onClick.AddListener(OnShareClicked);
            }

            // Hide initially
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
            }
        }

        /// <summary>
        /// Display ending with data
        /// </summary>
        public void DisplayEnding(Core.EndingData ending)
        {
            if (ending == null)
            {
                Debug.LogWarning("[EndingScreenController] Null ending provided");
                return;
            }

            currentEnding = ending;

            // Set ending info
            if (endingNameText != null)
            {
                endingNameText.text = ending.endingName;
                endingNameText.color = ending.endingColor;
            }

            if (endingDescriptionText != null)
            {
                endingDescriptionText.text = ending.fullDescription;
            }

            if (endingImage != null && ending.endingImage != null)
            {
                endingImage.sprite = ending.endingImage;
            }

            if (backgroundImage != null && ending.backgroundImage != null)
            {
                backgroundImage.sprite = ending.backgroundImage;
            }

            // Display statistics
            DisplayStatistics();

            // Display final resource values
            DisplayFinalResources();

            // Fade in
            FadeIn();

            // Play ending music
            if (ending.endingMusic != null)
            {
                Audio.AudioManager.Instance?.PlayMusic(ending.endingMusic);
            }
            else
            {
                Audio.AudioManager.Instance?.PlayEndingMusic();
            }

            // Play narration if available
            if (ending.endingNarration != null)
            {
                Audio.AudioManager.Instance?.PlaySound(ending.endingNarration);
            }
        }

        /// <summary>
        /// Display game statistics
        /// </summary>
        private void DisplayStatistics()
        {
            int daysSurvived = Core.GameManager.Instance?.CurrentDay ?? 0;
            
            if (daysSurvivedText != null)
            {
                daysSurvivedText.text = $"Days Survived: {daysSurvived}";
            }

            // Calculate final score
            int score = CalculateFinalScore(daysSurvived);

            if (finalScoreText != null)
            {
                finalScoreText.text = $"Final Score: {score}";
            }
        }

        /// <summary>
        /// Display final resource values
        /// </summary>
        private void DisplayFinalResources()
        {
            var resources = Core.ResourceManager.Instance?.GetAllResources();
            if (resources == null)
                return;

            if (popularityFinalText != null && resources.ContainsKey(Core.ResourceType.Popularity))
            {
                popularityFinalText.text = $"Popularity: {resources[Core.ResourceType.Popularity]:F0}%";
            }

            if (stabilityFinalText != null && resources.ContainsKey(Core.ResourceType.Stability))
            {
                stabilityFinalText.text = $"Stability: {resources[Core.ResourceType.Stability]:F0}%";
            }

            if (mediaTrustFinalText != null && resources.ContainsKey(Core.ResourceType.MediaTrust))
            {
                mediaTrustFinalText.text = $"Media Trust: {resources[Core.ResourceType.MediaTrust]:F0}%";
            }

            if (economicHealthFinalText != null && resources.ContainsKey(Core.ResourceType.EconomicHealth))
            {
                economicHealthFinalText.text = $"Economic Health: {resources[Core.ResourceType.EconomicHealth]:F0}%";
            }
        }

        /// <summary>
        /// Calculate final score based on performance
        /// </summary>
        private int CalculateFinalScore(int daysSurvived)
        {
            int score = daysSurvived * 100;

            // Bonus for resource balance
            float resourceHealth = Core.ResourceManager.Instance?.GetOverallHealth() ?? 0f;
            score += (int)(resourceHealth * 1000);

            // Bonus for loyal characters
            int loyalCharacters = Core.CharacterManager.Instance?.GetLoyalCharacterCount() ?? 0;
            score += loyalCharacters * 500;

            // Multiply by ending score multiplier
            if (currentEnding != null)
            {
                score *= currentEnding.scoreMultiplier;
            }

            return score;
        }

        /// <summary>
        /// Fade in ending screen
        /// </summary>
        private void FadeIn()
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.DOFade(1f, fadeInDuration);
            }
        }

        // Button Handlers
        private void OnMainMenuClicked()
        {
            Audio.AudioManager.Instance?.PlayButtonClick();
            Core.GameManager.Instance?.QuitToMenu();
        }

        private void OnPlayAgainClicked()
        {
            Audio.AudioManager.Instance?.PlayButtonClick();
            
            // Reload gameplay scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
            
            // Or restart through GameManager
            Core.GameManager.Instance?.StartNewGame();
        }

        private void OnShareClicked()
        {
            Audio.AudioManager.Instance?.PlayButtonClick();
            
            // Share score/ending (implement platform-specific sharing)
            string shareText = $"I just got the '{currentEnding.endingName}' ending in Executive Disorder! #ExecutiveDisorder";
            
            // Platform-specific implementation
            Debug.Log($"Share: {shareText}");
        }
    }
}
