using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExecutiveDisorder.Core
{
    /// <summary>
    /// Manages decision card deck, dealing, and card flow
    /// </summary>
    public class CardManager : Singleton<CardManager>
    {
        [Header("Card Configuration")]
        [SerializeField] private CardDatabase cardDatabase;
        [SerializeField] private int maxHandSize = 3;
        [SerializeField] private bool shuffleDeck = true;

        [Header("Debug")]
        [SerializeField] private bool showDebugLogs = false;

        // Card deck and hand
        private List<DecisionCardData> deck = new List<DecisionCardData>();
        private List<DecisionCardData> discardPile = new List<DecisionCardData>();
        private List<DecisionCardData> hand = new List<DecisionCardData>();
        private DecisionCardData currentCard;

        // State
        public bool IsWaitingForNextCard { get; private set; } = true;
        public DecisionCardData CurrentCard => currentCard;

        // Events
        public static event Action<DecisionCardData> OnCardPresented;
        public static event Action<DecisionCardData, int> OnCardChoiceMade; // card, choiceIndex
        public static event Action<DecisionCardData> OnCardResolved;
        public static event Action OnDeckShuffled;
        public static event Action OnDeckEmpty;

        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// Initialize the card deck
        /// </summary>
        public void InitializeDeck()
        {
            if (cardDatabase == null)
            {
                Debug.LogError("[CardManager] No CardDatabase assigned!");
                return;
            }

            // Load all cards from database
            deck = cardDatabase.GetAllCards().ToList();
            discardPile.Clear();
            hand.Clear();
            currentCard = null;
            IsWaitingForNextCard = true;

            // Shuffle if enabled
            if (shuffleDeck)
            {
                ShuffleDeck();
            }

            if (showDebugLogs)
                Debug.Log($"[CardManager] Deck initialized with {deck.Count} cards");
        }

        /// <summary>
        /// Shuffle the deck
        /// </summary>
        public void ShuffleDeck()
        {
            System.Random rng = new System.Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var temp = deck[k];
                deck[k] = deck[n];
                deck[n] = temp;
            }

            OnDeckShuffled?.Invoke();

            if (showDebugLogs)
                Debug.Log("[CardManager] Deck shuffled");
        }

        /// <summary>
        /// Draw a card from the deck
        /// </summary>
        private DecisionCardData DrawCard()
        {
            // Check if deck is empty
            if (deck.Count == 0)
            {
                // Reshuffle discard pile if available
                if (discardPile.Count > 0)
                {
                    deck = new List<DecisionCardData>(discardPile);
                    discardPile.Clear();
                    ShuffleDeck();
                }
                else
                {
                    OnDeckEmpty?.Invoke();
                    Debug.LogWarning("[CardManager] No cards left to draw!");
                    return null;
                }
            }

            // Draw top card
            var card = deck[0];
            deck.RemoveAt(0);

            if (showDebugLogs)
                Debug.Log($"[CardManager] Drew card: {card.title}");

            return card;
        }

        /// <summary>
        /// Present the next card to the player
        /// </summary>
        public void PresentNextCard()
        {
            // Get next eligible card
            currentCard = GetNextEligibleCard();

            if (currentCard != null)
            {
                IsWaitingForNextCard = true;
                OnCardPresented?.Invoke(currentCard);

                if (showDebugLogs)
                    Debug.Log($"[CardManager] Presenting card: {currentCard.title}");
            }
            else
            {
                Debug.LogWarning("[CardManager] No eligible cards available!");
            }
        }

        /// <summary>
        /// Get next card that meets requirements
        /// </summary>
        private DecisionCardData GetNextEligibleCard()
        {
            int attempts = 0;
            int maxAttempts = deck.Count + discardPile.Count;

            while (attempts < maxAttempts)
            {
                var card = DrawCard();
                if (card == null)
                    return null;

                // Check if card meets requirements
                if (IsCardEligible(card))
                {
                    return card;
                }
                else
                {
                    // Put back at bottom of deck
                    deck.Add(card);
                    attempts++;
                }
            }

            // Fallback - just return first card
            return DrawCard();
        }

        /// <summary>
        /// Check if card meets requirements to be played
        /// </summary>
        private bool IsCardEligible(DecisionCardData card)
        {
            if (card.requirements == null)
                return true;

            var requirements = card.requirements;
            int currentDay = GameManager.Instance?.CurrentDay ?? 1;

            // Check day requirements
            if (requirements.minDay.HasValue && currentDay < requirements.minDay.Value)
                return false;

            if (requirements.maxDay.HasValue && currentDay > requirements.maxDay.Value)
                return false;

            // Check resource requirements
            if (requirements.minResourceValues != null)
            {
                foreach (var req in requirements.minResourceValues)
                {
                    float currentValue = ResourceManager.Instance?.GetResource(req.Key) ?? 50f;
                    if (currentValue < req.Value)
                        return false;
                }
            }

            if (requirements.maxResourceValues != null)
            {
                foreach (var req in requirements.maxResourceValues)
                {
                    float currentValue = ResourceManager.Instance?.GetResource(req.Key) ?? 50f;
                    if (currentValue > req.Value)
                        return false;
                }
            }

            // Check character requirements
            if (!string.IsNullOrEmpty(requirements.requiredCharacterPresent))
            {
                var character = CharacterManager.Instance?.GetCharacter(requirements.requiredCharacterPresent);
                if (character == null || !character.isActive)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Player makes a choice on current card
        /// </summary>
        public void MakeChoice(int choiceIndex)
        {
            if (currentCard == null)
            {
                Debug.LogWarning("[CardManager] No current card!");
                return;
            }

            if (choiceIndex < 0 || choiceIndex >= currentCard.choices.Count)
            {
                Debug.LogError($"[CardManager] Invalid choice index: {choiceIndex}");
                return;
            }

            var choice = currentCard.choices[choiceIndex];

            if (showDebugLogs)
                Debug.Log($"[CardManager] Choice made: {choice.text}");

            // Fire event
            OnCardChoiceMade?.Invoke(currentCard, choiceIndex);

            // Apply effects
            ApplyChoiceEffects(choice);

            // Move card to discard
            discardPile.Add(currentCard);

            // Resolve card
            OnCardResolved?.Invoke(currentCard);

            // Check for follow-up cards
            if (choice.followupCardIds != null && choice.followupCardIds.Count > 0)
            {
                QueueFollowupCards(choice.followupCardIds);
            }

            currentCard = null;
            IsWaitingForNextCard = false;
        }

        /// <summary>
        /// Apply the effects of a chosen option
        /// </summary>
        private void ApplyChoiceEffects(CardChoiceData choice)
        {
            // Apply resource changes
            if (choice.resourceEffects != null && choice.resourceEffects.Count > 0)
            {
                ResourceManager.Instance?.ModifyResources(choice.resourceEffects);
            }

            // Apply character loyalty changes
            if (choice.loyaltyEffects != null && choice.loyaltyEffects.Count > 0)
            {
                CharacterManager.Instance?.ModifyLoyalties(choice.loyaltyEffects);
            }

            // Trigger custom consequences
            if (choice.consequences != null && choice.consequences.Count > 0)
            {
                EventManager.Instance?.TriggerConsequences(choice.consequences);
            }
        }

        /// <summary>
        /// Queue follow-up cards to appear next
        /// </summary>
        private void QueueFollowupCards(List<string> cardIds)
        {
            foreach (var cardId in cardIds)
            {
                var followupCard = cardDatabase.GetCardById(cardId);
                if (followupCard != null)
                {
                    // Insert at top of deck
                    deck.Insert(0, followupCard);

                    if (showDebugLogs)
                        Debug.Log($"[CardManager] Queued follow-up card: {followupCard.title}");
                }
            }
        }

        /// <summary>
        /// Save current card state
        /// </summary>
        public CardStateSaveData SaveState()
        {
            return new CardStateSaveData
            {
                deckCardIds = deck.Select(c => c.id).ToList(),
                discardCardIds = discardPile.Select(c => c.id).ToList(),
                currentCardId = currentCard?.id
            };
        }

        /// <summary>
        /// Load card state from save data
        /// </summary>
        public void LoadState(CardStateSaveData saveData)
        {
            if (saveData == null || cardDatabase == null)
                return;

            deck.Clear();
            discardPile.Clear();

            // Restore deck
            foreach (var cardId in saveData.deckCardIds)
            {
                var card = cardDatabase.GetCardById(cardId);
                if (card != null)
                    deck.Add(card);
            }

            // Restore discard
            foreach (var cardId in saveData.discardCardIds)
            {
                var card = cardDatabase.GetCardById(cardId);
                if (card != null)
                    discardPile.Add(card);
            }

            // Restore current card
            if (!string.IsNullOrEmpty(saveData.currentCardId))
            {
                currentCard = cardDatabase.GetCardById(saveData.currentCardId);
            }

            if (showDebugLogs)
                Debug.Log("[CardManager] State loaded from save");
        }

        /// <summary>
        /// Get number of cards remaining in deck
        /// </summary>
        public int GetDeckCount() => deck.Count;

        /// <summary>
        /// Get number of cards in discard pile
        /// </summary>
        public int GetDiscardCount() => discardPile.Count;
    }

    /// <summary>
    /// Card state save data
    /// </summary>
    [System.Serializable]
    public class CardStateSaveData
    {
        public List<string> deckCardIds;
        public List<string> discardCardIds;
        public string currentCardId;
    }
}
