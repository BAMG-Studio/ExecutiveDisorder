using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ExecutiveDisorder.Core
{
    /// <summary>
    /// Database of all game endings
    /// </summary>
    [CreateAssetMenu(fileName = "EndingDatabase", menuName = "Executive Disorder/Ending Database")]
    public class EndingDatabase : ScriptableObject
    {
        private static EndingDatabase instance;
        public static EndingDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<EndingDatabase>("EndingDatabase");
                }
                return instance;
            }
        }

        [Header("Endings")]
        [SerializeField] private List<EndingData> allEndings = new List<EndingData>();

        [Header("Auto-Load Settings")]
        [SerializeField] private bool autoLoadFromResources = true;
        [SerializeField] private string resourcesPath = "Endings";

        private Dictionary<string, EndingData> endingLookup;

        private void OnEnable()
        {
            instance = this;

            if (autoLoadFromResources)
            {
                LoadEndingsFromResources();
            }

            BuildLookup();
        }

        /// <summary>
        /// Load all endings from Resources folder
        /// </summary>
        private void LoadEndingsFromResources()
        {
            var loadedEndings = Resources.LoadAll<EndingData>(resourcesPath);
            if (loadedEndings != null && loadedEndings.Length > 0)
            {
                allEndings = loadedEndings.ToList();
                Debug.Log($"[EndingDatabase] Loaded {allEndings.Count} endings from Resources/{resourcesPath}");
            }
        }

        /// <summary>
        /// Build quick lookup dictionary
        /// </summary>
        private void BuildLookup()
        {
            endingLookup = new Dictionary<string, EndingData>();
            foreach (var ending in allEndings)
            {
                if (ending != null && !string.IsNullOrEmpty(ending.endingId))
                {
                    endingLookup[ending.endingId] = ending;
                }
            }
        }

        /// <summary>
        /// Get ending by ID
        /// </summary>
        public EndingData GetEnding(string endingId)
        {
            if (endingLookup == null)
                BuildLookup();

            return endingLookup.ContainsKey(endingId) ? endingLookup[endingId] : null;
        }

        /// <summary>
        /// Get all endings
        /// </summary>
        public IEnumerable<EndingData> GetAllEndings()
        {
            return allEndings;
        }

        /// <summary>
        /// Get endings by type
        /// </summary>
        public IEnumerable<EndingData> GetEndingsByType(EndingType type)
        {
            return allEndings.Where(e => e.endingType == type);
        }
    }
}
