using UnityEngine;
using TMPro;
using System.Collections;

namespace ExecutiveDisorder.UI
{
    /// <summary>
    /// Scrolling news ticker for displaying headlines and updates
    /// </summary>
    public class NewsTickerController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI tickerText;
        [SerializeField] private RectTransform tickerTransform;

        [Header("Scroll Settings")]
        [SerializeField] private float scrollSpeed = 50f;
        [SerializeField] private bool autoScroll = true;

        [Header("Headlines")]
        [SerializeField] private string[] defaultHeadlines = new string[]
        {
            "Breaking: Democracy still optional",
            "Markets react to latest chaos",
            "Experts baffled by your decisions",
            "Citizens question reality of situation"
        };

        private Queue<string> headlineQueue = new Queue<string>();
        private string currentHeadline = "";
        private bool isScrolling = false;

        private void Start()
        {
            // Subscribe to events
            Core.EventManager.Instance?.Subscribe("NewsHeadline", OnNewsHeadline);
            Core.CardManager.OnCardResolved += OnCardResolved;

            // Load default headlines
            foreach (var headline in defaultHeadlines)
            {
                headlineQueue.Enqueue(headline);
            }

            // Start scrolling
            if (autoScroll)
            {
                StartCoroutine(ScrollHeadlines());
            }
        }

        private void OnDestroy()
        {
            if (Core.EventManager.Instance != null)
            {
                Core.EventManager.Instance.Unsubscribe("NewsHeadline", OnNewsHeadline);
            }

            if (Core.CardManager.Instance != null)
            {
                Core.CardManager.OnCardResolved -= OnCardResolved;
            }
        }

        /// <summary>
        /// Show message in ticker
        /// </summary>
        public void ShowMessage(string message, float duration = 0f)
        {
            if (string.IsNullOrEmpty(message))
                return;

            headlineQueue.Enqueue(message);
        }

        /// <summary>
        /// Scroll headlines coroutine
        /// </summary>
        private IEnumerator ScrollHeadlines()
        {
            isScrolling = true;

            while (isScrolling)
            {
                // Get next headline
                if (headlineQueue.Count > 0)
                {
                    currentHeadline = headlineQueue.Dequeue();
                    
                    if (tickerText != null)
                    {
                        tickerText.text = $"📰 {currentHeadline} 📰";
                    }
                }

                // Wait before next headline
                yield return new WaitForSeconds(5f);
            }
        }

        /// <summary>
        /// Generate headline from card
        /// </summary>
        private string GenerateHeadlineFromCard(Core.DecisionCardData card)
        {
            if (!string.IsNullOrEmpty(card.newsHeadline))
            {
                return card.newsHeadline;
            }

            // Generate generic headline
            return $"President makes decision on '{card.title}'";
        }

        // Event Handlers
        private void OnNewsHeadline(object data)
        {
            if (data is string headline)
            {
                ShowMessage(headline);
            }
        }

        private void OnCardResolved(Core.DecisionCardData card)
        {
            string headline = GenerateHeadlineFromCard(card);
            ShowMessage(headline);
        }
    }
}
