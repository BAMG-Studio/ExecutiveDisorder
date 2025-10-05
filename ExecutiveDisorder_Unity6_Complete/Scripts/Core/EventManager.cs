using UnityEngine;
using System;
using System.Collections.Generic;

namespace ExecutiveDisorder.Core
{
    /// <summary>
    /// Central event system for game-wide events and messaging
    /// </summary>
    public class EventManager : Singleton<EventManager>
    {
        [Header("Debug")]
        [SerializeField] private bool showDebugLogs = false;

        // Event dictionary
        private Dictionary<string, Action<object>> eventDictionary = new Dictionary<string, Action<object>>();

        // Random events
        [SerializeField] private List<RandomEventData> randomEvents = new List<RandomEventData>();

        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// Subscribe to an event
        /// </summary>
        public void Subscribe(string eventName, Action<object> listener)
        {
            if (!eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] = null;
            }

            eventDictionary[eventName] += listener;

            if (showDebugLogs)
                Debug.Log($"[EventManager] Subscribed to event: {eventName}");
        }

        /// <summary>
        /// Unsubscribe from an event
        /// </summary>
        public void Unsubscribe(string eventName, Action<object> listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] -= listener;
            }
        }

        /// <summary>
        /// Trigger an event
        /// </summary>
        public void TriggerEvent(string eventName, object data = null)
        {
            if (eventDictionary.ContainsKey(eventName) && eventDictionary[eventName] != null)
            {
                eventDictionary[eventName].Invoke(data);

                if (showDebugLogs)
                    Debug.Log($"[EventManager] Triggered event: {eventName}");
            }
        }

        /// <summary>
        /// Trigger consequences from card choice
        /// </summary>
        public void TriggerConsequences(List<string> consequences)
        {
            foreach (var consequence in consequences)
            {
                ProcessConsequence(consequence);
            }
        }

        /// <summary>
        /// Process a single consequence
        /// </summary>
        private void ProcessConsequence(string consequenceId)
        {
            // Parse consequence format: "type:parameter:value"
            // Examples:
            // "resource:popularity:+10"
            // "character:rex_scaleston:+5"
            // "event:scandal_revealed"
            // "news:Breaking News!"

            var parts = consequenceId.Split(':');
            if (parts.Length < 2)
            {
                Debug.LogWarning($"[EventManager] Invalid consequence format: {consequenceId}");
                return;
            }

            string type = parts[0].ToLower();

            switch (type)
            {
                case "resource":
                    if (parts.Length >= 3)
                    {
                        if (Enum.TryParse<ResourceType>(parts[1], true, out var resourceType))
                        {
                            if (float.TryParse(parts[2], out float value))
                            {
                                ResourceManager.Instance?.ModifyResource(resourceType, value);
                            }
                        }
                    }
                    break;

                case "character":
                    if (parts.Length >= 3)
                    {
                        string characterId = parts[1];
                        if (int.TryParse(parts[2], out int loyaltyChange))
                        {
                            CharacterManager.Instance?.ModifyLoyalty(characterId, loyaltyChange);
                        }
                    }
                    break;

                case "event":
                    TriggerEvent(parts[1]);
                    break;

                case "news":
                    if (parts.Length >= 2)
                    {
                        string headline = string.Join(":", parts, 1, parts.Length - 1);
                        TriggerEvent("NewsHeadline", headline);
                    }
                    break;

                default:
                    if (showDebugLogs)
                        Debug.Log($"[EventManager] Unknown consequence type: {type}");
                    break;
            }
        }

        /// <summary>
        /// Trigger a random event
        /// </summary>
        public void TriggerRandomEvent()
        {
            if (randomEvents.Count == 0)
                return;

            var randomEvent = randomEvents[UnityEngine.Random.Range(0, randomEvents.Count)];
            
            if (showDebugLogs)
                Debug.Log($"[EventManager] Random event: {randomEvent.eventName}");

            // Apply event effects
            if (randomEvent.resourceEffects != null)
            {
                ResourceManager.Instance?.ModifyResources(randomEvent.resourceEffects);
            }

            // Show event message
            TriggerEvent("RandomEventOccurred", randomEvent);
        }
    }

    /// <summary>
    /// Random event data
    /// </summary>
    [System.Serializable]
    public class RandomEventData
    {
        public string eventName;
        [TextArea(2, 4)]
        public string description;
        public Dictionary<ResourceType, float> resourceEffects;
        public Sprite eventIcon;
    }
}
