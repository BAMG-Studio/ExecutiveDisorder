using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExecutiveDisorder.Core
{
    /// <summary>
    /// Manages all game resources (Popularity, Stability, Media Trust, Economic Health)
    /// </summary>
    public class ResourceManager : Singleton<ResourceManager>
    {
        [Header("Resource Configuration")]
        [SerializeField] private List<ResourceDefinition> resourceDefinitions;
        [SerializeField] private float minValue = 0f;
        [SerializeField] private float maxValue = 100f;

        [Header("Debug")]
        [SerializeField] private bool showDebugLogs = false;

        // Current resource values
        private Dictionary<ResourceType, float> resources = new Dictionary<ResourceType, float>();
        
        // Resource change tracking
        private Dictionary<ResourceType, float> previousValues = new Dictionary<ResourceType, float>();

        // Events
        public static event Action<ResourceType, float, float> OnResourceChanged; // type, oldValue, newValue
        public static event Action<ResourceType> OnResourceCriticalLow; // Below 20%
        public static event Action<ResourceType> OnResourceCriticalHigh; // Above 80%
        public static event Action<ResourceType> OnResourceDepleted; // Hit 0
        public static event Action<ResourceType> OnResourceMaxed; // Hit 100

        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// Initialize all resources to starting values
        /// </summary>
        public void InitializeResources()
        {
            resources.Clear();
            previousValues.Clear();

            foreach (var definition in resourceDefinitions)
            {
                resources[definition.resourceType] = definition.startingValue;
                previousValues[definition.resourceType] = definition.startingValue;
            }

            if (showDebugLogs)
                Debug.Log("[ResourceManager] Resources initialized");
        }

        /// <summary>
        /// Get current value of a resource
        /// </summary>
        public float GetResource(ResourceType type)
        {
            return resources.ContainsKey(type) ? resources[type] : 0f;
        }

        /// <summary>
        /// Get all current resource values
        /// </summary>
        public Dictionary<ResourceType, float> GetAllResources()
        {
            return new Dictionary<ResourceType, float>(resources);
        }

        /// <summary>
        /// Modify a resource value
        /// </summary>
        public void ModifyResource(ResourceType type, float amount)
        {
            if (!resources.ContainsKey(type))
            {
                Debug.LogWarning($"[ResourceManager] Unknown resource type: {type}");
                return;
            }

            float oldValue = resources[type];
            float newValue = Mathf.Clamp(oldValue + amount, minValue, maxValue);
            
            resources[type] = newValue;

            // Fire events
            if (oldValue != newValue)
            {
                OnResourceChanged?.Invoke(type, oldValue, newValue);

                // Check critical thresholds
                CheckResourceThresholds(type, oldValue, newValue);

                if (showDebugLogs)
                    Debug.Log($"[ResourceManager] {type}: {oldValue:F1} → {newValue:F1} ({amount:+0.0;-0.0})");
            }
        }

        /// <summary>
        /// Modify multiple resources at once
        /// </summary>
        public void ModifyResources(Dictionary<ResourceType, float> changes)
        {
            foreach (var change in changes)
            {
                ModifyResource(change.Key, change.Value);
            }
        }

        /// <summary>
        /// Set resource to specific value
        /// </summary>
        public void SetResource(ResourceType type, float value)
        {
            if (!resources.ContainsKey(type))
            {
                Debug.LogWarning($"[ResourceManager] Unknown resource type: {type}");
                return;
            }

            float oldValue = resources[type];
            float newValue = Mathf.Clamp(value, minValue, maxValue);
            
            resources[type] = newValue;

            if (oldValue != newValue)
            {
                OnResourceChanged?.Invoke(type, oldValue, newValue);
                CheckResourceThresholds(type, oldValue, newValue);
            }
        }

        /// <summary>
        /// Check if resource crossed critical thresholds
        /// </summary>
        private void CheckResourceThresholds(ResourceType type, float oldValue, float newValue)
        {
            // Critical Low (20%)
            if (newValue <= 20f && oldValue > 20f)
            {
                OnResourceCriticalLow?.Invoke(type);
            }

            // Critical High (80%)
            if (newValue >= 80f && oldValue < 80f)
            {
                OnResourceCriticalHigh?.Invoke(type);
            }

            // Depleted (0%)
            if (newValue <= 0f && oldValue > 0f)
            {
                OnResourceDepleted?.Invoke(type);
            }

            // Maxed (100%)
            if (newValue >= 100f && oldValue < 100f)
            {
                OnResourceMaxed?.Invoke(type);
            }
        }

        /// <summary>
        /// Process cascading effects between resources
        /// </summary>
        public void ProcessCascadingEffects()
        {
            // Example: Low popularity affects stability
            float popularity = GetResource(ResourceType.Popularity);
            if (popularity < 30f)
            {
                ModifyResource(ResourceType.Stability, -0.5f);
            }

            // Low economic health affects popularity
            float economy = GetResource(ResourceType.EconomicHealth);
            if (economy < 30f)
            {
                ModifyResource(ResourceType.Popularity, -0.3f);
            }

            // Low media trust affects all resources slightly
            float media = GetResource(ResourceType.MediaTrust);
            if (media < 20f)
            {
                ModifyResource(ResourceType.Popularity, -0.2f);
            }
        }

        /// <summary>
        /// Get resource definition by type
        /// </summary>
        public ResourceDefinition GetResourceDefinition(ResourceType type)
        {
            return resourceDefinitions.FirstOrDefault(r => r.resourceType == type);
        }

        /// <summary>
        /// Check if any resource is in critical state
        /// </summary>
        public bool IsAnyCritical()
        {
            foreach (var resource in resources)
            {
                if (resource.Value <= 20f || resource.Value >= 80f)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Get resource health status (0-1, where 0.5 is balanced)
        /// </summary>
        public float GetResourceHealth(ResourceType type)
        {
            float value = GetResource(type);
            // Distance from 50% (ideal balance)
            return 1f - (Mathf.Abs(value - 50f) / 50f);
        }

        /// <summary>
        /// Get overall game health (average of all resources)
        /// </summary>
        public float GetOverallHealth()
        {
            float total = 0f;
            foreach (var resource in resources)
            {
                total += GetResourceHealth(resource.Key);
            }
            return total / resources.Count;
        }

        /// <summary>
        /// Save current resource state
        /// </summary>
        public ResourceSaveData SaveState()
        {
            return new ResourceSaveData
            {
                resourceValues = new Dictionary<ResourceType, float>(resources)
            };
        }

        /// <summary>
        /// Load resource state from save data
        /// </summary>
        public void LoadState(ResourceSaveData saveData)
        {
            if (saveData != null && saveData.resourceValues != null)
            {
                resources = new Dictionary<ResourceType, float>(saveData.resourceValues);
                previousValues = new Dictionary<ResourceType, float>(saveData.resourceValues);

                if (showDebugLogs)
                    Debug.Log("[ResourceManager] State loaded from save");
            }
        }

        /// <summary>
        /// Reset all resources to default values
        /// </summary>
        public void ResetResources()
        {
            InitializeResources();
        }
    }

    /// <summary>
    /// Resource type enumeration
    /// </summary>
    public enum ResourceType
    {
        Popularity,      // 👥 Public approval
        Stability,       // 🏛️ Government stability
        MediaTrust,      // 📺 Press relations
        EconomicHealth   // 💰 Economic indicators
    }

    /// <summary>
    /// Resource definition (ScriptableObject)
    /// </summary>
    [System.Serializable]
    public class ResourceDefinition
    {
        public ResourceType resourceType;
        public string displayName;
        public string description;
        public Color color = Color.white;
        public Sprite icon;
        public float startingValue = 50f;
        public string emoji = "📊";
    }

    /// <summary>
    /// Resource save data
    /// </summary>
    [System.Serializable]
    public class ResourceSaveData
    {
        public Dictionary<ResourceType, float> resourceValues;
    }
}
