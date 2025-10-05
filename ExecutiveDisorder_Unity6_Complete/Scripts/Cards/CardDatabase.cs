using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ExecutiveDisorder.Core
{
    /// <summary>
    /// Database of all decision cards in the game
    /// </summary>
    [CreateAssetMenu(fileName = "CardDatabase", menuName = "Executive Disorder/Card Database")]
    public class CardDatabase : ScriptableObject
    {
        [Header("Card Collections")]
        [SerializeField] private List<DecisionCardData> allCards = new List<DecisionCardData>();

        [Header("Auto-Load Settings")]
        [SerializeField] private bool autoLoadFromResources = true;
        [SerializeField] private string resourcesPath = "Cards";

        private Dictionary<string, DecisionCardData> cardLookup;

        private void OnEnable()
        {
            if (autoLoadFromResources)
            {
                LoadCardsFromResources();
            }

            BuildLookup();
        }

        /// <summary>
        /// Load all cards from Resources folder
        /// </summary>
        private void LoadCardsFromResources()
        {
            var loadedCards = Resources.LoadAll<DecisionCardData>(resourcesPath);
            if (loadedCards != null && loadedCards.Length > 0)
            {
                allCards = loadedCards.ToList();
                Debug.Log($"[CardDatabase] Loaded {allCards.Count} cards from Resources/{resourcesPath}");
            }
        }

        /// <summary>
        /// Build quick lookup dictionary
        /// </summary>
        private void BuildLookup()
        {
            cardLookup = new Dictionary<string, DecisionCardData>();
            foreach (var card in allCards)
            {
                if (card != null && !string.IsNullOrEmpty(card.id))
                {
                    if (!cardLookup.ContainsKey(card.id))
                    {
                        cardLookup[card.id] = card;
                    }
                    else
                    {
                        Debug.LogWarning($"[CardDatabase] Duplicate card ID: {card.id}");
                    }
                }
            }
        }

        /// <summary>
        /// Get all cards
        /// </summary>
        public IEnumerable<DecisionCardData> GetAllCards()
        {
            return allCards;
        }

        /// <summary>
        /// Get card by ID
        /// </summary>
        public DecisionCardData GetCardById(string id)
        {
            if (cardLookup == null)
                BuildLookup();

            return cardLookup.ContainsKey(id) ? cardLookup[id] : null;
        }

        /// <summary>
        /// Get cards by category
        /// </summary>
        public IEnumerable<DecisionCardData> GetCardsByCategory(CardCategory category)
        {
            return allCards.Where(c => c.category == category);
        }

        /// <summary>
        /// Get cards by rarity
        /// </summary>
        public IEnumerable<DecisionCardData> GetCardsByRarity(CardRarity rarity)
        {
            return allCards.Where(c => c.rarity == rarity);
        }

        /// <summary>
        /// Get random card
        /// </summary>
        public DecisionCardData GetRandomCard()
        {
            if (allCards.Count == 0)
                return null;

            return allCards[Random.Range(0, allCards.Count)];
        }

        /// <summary>
        /// Get random card by category
        /// </summary>
        public DecisionCardData GetRandomCardByCategory(CardCategory category)
        {
            var categoryCards = GetCardsByCategory(category).ToList();
            if (categoryCards.Count == 0)
                return null;

            return categoryCards[Random.Range(0, categoryCards.Count)];
        }

#if UNITY_EDITOR
        [ContextMenu("Generate Sample Cards")]
        private void GenerateSampleCards()
        {
            // This would create sample cards for testing
            Debug.Log("[CardDatabase] Sample card generation - implement in editor");
        }

        [ContextMenu("Validate All Cards")]
        private void ValidateAllCards()
        {
            int validCards = 0;
            int invalidCards = 0;

            foreach (var card in allCards)
            {
                if (card == null)
                {
                    invalidCards++;
                    continue;
                }

                if (string.IsNullOrEmpty(card.id))
                {
                    Debug.LogWarning($"[CardDatabase] Card '{card.name}' has no ID");
                    invalidCards++;
                    continue;
                }

                if (card.choices == null || card.choices.Count < 2)
                {
                    Debug.LogWarning($"[CardDatabase] Card '{card.id}' has less than 2 choices");
                    invalidCards++;
                    continue;
                }

                validCards++;
            }

            Debug.Log($"[CardDatabase] Validation: {validCards} valid, {invalidCards} invalid");
        }
#endif
    }
}
