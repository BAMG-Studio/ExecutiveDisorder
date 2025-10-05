using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening; // DOTween for animations

namespace ExecutiveDisorder.UI
{
    /// <summary>
    /// Displays decision card UI with choices
    /// </summary>
    public class CardUIController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject cardContainer;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Image cardImage;
        [SerializeField] private Image cardBackground;

        [Header("Choice Buttons")]
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private Transform choiceButtonContainer;
        private System.Collections.Generic.List<GameObject> activeChoiceButtons = new System.Collections.Generic.List<GameObject>();

        [Header("Character Display")]
        [SerializeField] private Image characterPortrait;
        [SerializeField] private TextMeshProUGUI characterNameText;
        [SerializeField] private GameObject characterPanel;

        [Header("Timer")]
        [SerializeField] private GameObject timerPanel;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Image timerFillImage;

        [Header("Animation Settings")]
        [SerializeField] private float cardAppearDuration = 0.5f;
        [SerializeField] private float cardDisappearDuration = 0.3f;

        [Header("Debug")]
        [SerializeField] private bool showDebugLogs = false;

        private Core.DecisionCardData currentCard;
        private bool isTimerActive = false;
        private float timeRemaining = 0f;

        private void Start()
        {
            // Hide card initially
            if (cardContainer != null)
            {
                cardContainer.SetActive(false);
            }

            // Subscribe to card events
            Core.CardManager.OnCardPresented += OnCardPresented;
        }

        private void OnDestroy()
        {
            if (Core.CardManager.Instance != null)
            {
                Core.CardManager.OnCardPresented -= OnCardPresented;
            }
        }

        private void Update()
        {
            // Update timer if active
            if (isTimerActive && timeRemaining > 0f)
            {
                timeRemaining -= Time.deltaTime;

                if (timerText != null)
                {
                    timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
                }

                if (timerFillImage != null)
                {
                    timerFillImage.fillAmount = timeRemaining / currentCard.timeLimit;
                }

                // Time's up!
                if (timeRemaining <= 0f)
                {
                    isTimerActive = false;
                    OnTimeUp();
                }
            }
        }

        /// <summary>
        /// Display a decision card
        /// </summary>
        public void DisplayCard(Core.DecisionCardData card)
        {
            if (card == null)
            {
                Debug.LogWarning("[CardUIController] Null card provided");
                return;
            }

            currentCard = card;

            // Set card content
            if (titleText != null)
                titleText.text = card.title;

            if (descriptionText != null)
                descriptionText.text = card.description;

            if (cardImage != null && card.cardImage != null)
            {
                cardImage.sprite = card.cardImage;
                cardImage.gameObject.SetActive(true);
            }
            else if (cardImage != null)
            {
                cardImage.gameObject.SetActive(false);
            }

            // Set background color based on category
            if (cardBackground != null)
            {
                cardBackground.color = GetColorForCategory(card.category);
            }

            // Display character if specified
            if (!string.IsNullOrEmpty(card.characterId))
            {
                DisplayCharacter(card.characterId);
            }
            else if (characterPanel != null)
            {
                characterPanel.SetActive(false);
            }

            // Create choice buttons
            CreateChoiceButtons(card.choices);

            // Setup timer for crisis cards
            if (card.timeLimit > 0 && card.category == Core.CardCategory.Crisis)
            {
                SetupTimer(card.timeLimit);
            }
            else if (timerPanel != null)
            {
                timerPanel.SetActive(false);
            }

            // Show card with animation
            ShowCard();

            // Play sound
            if (card.cardSound != null)
            {
                Audio.AudioManager.Instance?.PlaySound(card.cardSound);
            }
            else
            {
                Audio.AudioManager.Instance?.PlayCardFlip();
            }

            if (showDebugLogs)
                Debug.Log($"[CardUIController] Displaying card: {card.title}");
        }

        /// <summary>
        /// Create choice buttons for card options
        /// </summary>
        private void CreateChoiceButtons(System.Collections.Generic.List<Core.CardChoiceData> choices)
        {
            // Clear existing buttons
            foreach (var button in activeChoiceButtons)
            {
                Destroy(button);
            }
            activeChoiceButtons.Clear();

            // Create new buttons
            for (int i = 0; i < choices.Count; i++)
            {
                var choice = choices[i];
                int choiceIndex = i; // Capture for lambda

                GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceButtonContainer);
                activeChoiceButtons.Add(buttonObj);

                // Set button text
                var buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = choice.text;
                }

                // Add button click handler
                var button = buttonObj.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(() => OnChoiceSelected(choiceIndex));
                }

                // Set choice icon if available
                var iconImage = buttonObj.transform.Find("Icon")?.GetComponent<Image>();
                if (iconImage != null && choice.choiceIcon != null)
                {
                    iconImage.sprite = choice.choiceIcon;
                    iconImage.gameObject.SetActive(true);
                }
            }
        }

        /// <summary>
        /// Display character info
        /// </summary>
        private void DisplayCharacter(string characterId)
        {
            var character = Core.CharacterManager.Instance?.GetCharacter(characterId);
            if (character == null)
                return;

            if (characterPanel != null)
            {
                characterPanel.SetActive(true);
            }

            if (characterPortrait != null)
            {
                characterPortrait.sprite = character.portraitNeutral;
            }

            if (characterNameText != null)
            {
                characterNameText.text = character.characterName;
            }
        }

        /// <summary>
        /// Setup timer for crisis cards
        /// </summary>
        private void SetupTimer(float timeLimit)
        {
            if (timerPanel != null)
            {
                timerPanel.SetActive(true);
            }

            timeRemaining = timeLimit;
            isTimerActive = true;

            if (timerFillImage != null)
            {
                timerFillImage.fillAmount = 1f;
            }
        }

        /// <summary>
        /// Handle choice selection
        /// </summary>
        private void OnChoiceSelected(int choiceIndex)
        {
            if (currentCard == null)
                return;

            if (showDebugLogs)
                Debug.Log($"[CardUIController] Choice selected: {choiceIndex}");

            // Stop timer
            isTimerActive = false;

            // Disable all buttons
            foreach (var buttonObj in activeChoiceButtons)
            {
                var button = buttonObj.GetComponent<Button>();
                if (button != null)
                {
                    button.interactable = false;
                }
            }

            // Play sound
            Audio.AudioManager.Instance?.PlayButtonClick();

            // Process choice through CardManager
            Core.CardManager.Instance?.MakeChoice(choiceIndex);

            // Hide card after delay
            StartCoroutine(HideCardAfterDelay(1f));
        }

        /// <summary>
        /// Handle timer expiration
        /// </summary>
        private void OnTimeUp()
        {
            // Auto-select random choice (or last choice as penalty)
            int randomChoice = Random.Range(0, currentCard.choices.Count);
            OnChoiceSelected(randomChoice);

            // Play alert sound
            Audio.AudioManager.Instance?.PlayCrisisAlert();
        }

        /// <summary>
        /// Show card with animation
        /// </summary>
        private void ShowCard()
        {
            if (cardContainer == null)
                return;

            cardContainer.SetActive(true);

            // Animate card appearance
            cardContainer.transform.localScale = Vector3.zero;
            cardContainer.transform.DOScale(Vector3.one, cardAppearDuration)
                .SetEase(Ease.OutBack);
        }

        /// <summary>
        /// Hide card
        /// </summary>
        public void HideCard()
        {
            if (cardContainer == null)
                return;

            // Animate card disappearance
            cardContainer.transform.DOScale(Vector3.zero, cardDisappearDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    cardContainer.SetActive(false);
                });
        }

        /// <summary>
        /// Hide card after delay
        /// </summary>
        private IEnumerator HideCardAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            HideCard();
        }

        /// <summary>
        /// Get color for card category
        /// </summary>
        private Color GetColorForCategory(Core.CardCategory category)
        {
            return category switch
            {
                Core.CardCategory.Normal => new Color(0.9f, 0.9f, 0.9f),
                Core.CardCategory.Crisis => new Color(1f, 0.3f, 0.3f),
                Core.CardCategory.Scandal => new Color(1f, 0.5f, 0.8f),
                Core.CardCategory.Opportunity => new Color(0.3f, 1f, 0.3f),
                Core.CardCategory.Character => new Color(0.5f, 0.5f, 1f),
                Core.CardCategory.Absurd => new Color(1f, 1f, 0.3f),
                _ => Color.white
            };
        }

        // Event Handlers
        private void OnCardPresented(Core.DecisionCardData card)
        {
            DisplayCard(card);
        }
    }
}
