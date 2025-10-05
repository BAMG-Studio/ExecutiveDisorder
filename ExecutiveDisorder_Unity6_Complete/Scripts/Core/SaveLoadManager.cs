using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

namespace ExecutiveDisorder.Core
{
    /// <summary>
    /// Manages game save and load functionality
    /// </summary>
    public class SaveLoadManager : Singleton<SaveLoadManager>
    {
        [Header("Save Settings")]
        [SerializeField] private string saveFileName = "savegame";
        [SerializeField] private string saveFileExtension = ".json";
        [SerializeField] private int maxSaveSlots = 3;
        [SerializeField] private bool useEncryption = false;

        [Header("Auto Save")]
        [SerializeField] private bool enableAutoSave = true;
        [SerializeField] private float autoSaveInterval = 300f; // 5 minutes

        [Header("Debug")]
        [SerializeField] private bool showDebugLogs = false;

        private string SaveFolderPath => Application.persistentDataPath + "/Saves";
        private float autoSaveTimer = 0f;

        protected override void Awake()
        {
            base.Awake();
            
            // Ensure save folder exists
            if (!Directory.Exists(SaveFolderPath))
            {
                Directory.CreateDirectory(SaveFolderPath);
                if (showDebugLogs)
                    Debug.Log($"[SaveLoadManager] Created save folder: {SaveFolderPath}");
            }
        }

        private void Update()
        {
            // Auto-save timer
            if (enableAutoSave && GameManager.Instance != null && GameManager.Instance.IsGameActive)
            {
                autoSaveTimer += Time.deltaTime;
                if (autoSaveTimer >= autoSaveInterval)
                {
                    QuickSave();
                    autoSaveTimer = 0f;
                }
            }
        }

        /// <summary>
        /// Save game to specific slot
        /// </summary>
        public void SaveGame(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= maxSaveSlots)
            {
                Debug.LogError($"[SaveLoadManager] Invalid save slot: {slotIndex}");
                return;
            }

            var saveData = GatherSaveData();
            string filePath = GetSaveFilePath(slotIndex);

            try
            {
                string json = JsonUtility.ToJson(saveData, true);

                if (useEncryption)
                {
                    json = EncryptString(json);
                }

                File.WriteAllText(filePath, json);

                if (showDebugLogs)
                    Debug.Log($"[SaveLoadManager] Game saved to slot {slotIndex}: {filePath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveLoadManager] Save failed: {e.Message}");
            }
        }

        /// <summary>
        /// Load game from specific slot
        /// </summary>
        public GameSaveData LoadGame(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= maxSaveSlots)
            {
                Debug.LogError($"[SaveLoadManager] Invalid save slot: {slotIndex}");
                return null;
            }

            string filePath = GetSaveFilePath(slotIndex);

            if (!File.Exists(filePath))
            {
                if (showDebugLogs)
                    Debug.Log($"[SaveLoadManager] No save file found at slot {slotIndex}");
                return null;
            }

            try
            {
                string json = File.ReadAllText(filePath);

                if (useEncryption)
                {
                    json = DecryptString(json);
                }

                var saveData = JsonUtility.FromJson<GameSaveData>(json);

                if (showDebugLogs)
                    Debug.Log($"[SaveLoadManager] Game loaded from slot {slotIndex}");

                return saveData;
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveLoadManager] Load failed: {e.Message}");
                return null;
            }
        }

        /// <summary>
        /// Quick save to auto-save slot
        /// </summary>
        public void QuickSave()
        {
            SaveGame(0); // Slot 0 is quick save / auto save
        }

        /// <summary>
        /// Quick load from auto-save slot
        /// </summary>
        public GameSaveData QuickLoad()
        {
            return LoadGame(0);
        }

        /// <summary>
        /// Delete save file from slot
        /// </summary>
        public void DeleteSave(int slotIndex)
        {
            string filePath = GetSaveFilePath(slotIndex);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                if (showDebugLogs)
                    Debug.Log($"[SaveLoadManager] Deleted save file at slot {slotIndex}");
            }
        }

        /// <summary>
        /// Check if save exists in slot
        /// </summary>
        public bool SaveExists(int slotIndex)
        {
            return File.Exists(GetSaveFilePath(slotIndex));
        }

        /// <summary>
        /// Get save file info
        /// </summary>
        public SaveFileInfo GetSaveInfo(int slotIndex)
        {
            string filePath = GetSaveFilePath(slotIndex);

            if (!File.Exists(filePath))
                return null;

            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                string json = File.ReadAllText(filePath);

                if (useEncryption)
                {
                    json = DecryptString(json);
                }

                var saveData = JsonUtility.FromJson<GameSaveData>(json);

                return new SaveFileInfo
                {
                    slotIndex = slotIndex,
                    saveDate = fileInfo.LastWriteTime,
                    currentDay = saveData.currentDay,
                    playerName = saveData.playerName
                };
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gather all game data for saving
        /// </summary>
        private GameSaveData GatherSaveData()
        {
            var saveData = new GameSaveData
            {
                saveVersion = "1.0",
                saveDate = DateTime.Now.ToString(),
                playerName = "President",
                currentDay = GameManager.Instance?.CurrentDay ?? 1,
                resources = ResourceManager.Instance?.SaveState(),
                cardState = CardManager.Instance?.SaveState(),
                characterState = CharacterManager.Instance?.SaveState()
            };

            return saveData;
        }

        /// <summary>
        /// Get save file path for slot
        /// </summary>
        private string GetSaveFilePath(int slotIndex)
        {
            return Path.Combine(SaveFolderPath, $"{saveFileName}_{slotIndex}{saveFileExtension}");
        }

        /// <summary>
        /// Simple encryption (XOR-based - not secure, just obfuscation)
        /// </summary>
        private string EncryptString(string text)
        {
            // Simple XOR encryption for obfuscation
            char[] buffer = text.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (char)(buffer[i] ^ 0x42); // XOR with key
            }
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(buffer));
        }

        /// <summary>
        /// Simple decryption
        /// </summary>
        private string DecryptString(string encryptedText)
        {
            byte[] data = Convert.FromBase64String(encryptedText);
            char[] buffer = System.Text.Encoding.UTF8.GetString(data).ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (char)(buffer[i] ^ 0x42); // XOR with same key
            }
            return new string(buffer);
        }

        /// <summary>
        /// Load game from string slot identifier
        /// </summary>
        public GameSaveData LoadGame(string saveSlot)
        {
            if (int.TryParse(saveSlot, out int slotIndex))
            {
                return LoadGame(slotIndex);
            }
            else if (saveSlot == "quicksave" || saveSlot == "autosave")
            {
                return QuickLoad();
            }
            
            return null;
        }
    }

    /// <summary>
    /// Complete game save data
    /// </summary>
    [System.Serializable]
    public class GameSaveData
    {
        public string saveVersion;
        public string saveDate;
        public string playerName;
        public int currentDay;
        public ResourceSaveData resources;
        public CardStateSaveData cardState;
        public CharacterStateSaveData characterState;
    }

    /// <summary>
    /// Save file metadata
    /// </summary>
    [System.Serializable]
    public class SaveFileInfo
    {
        public int slotIndex;
        public DateTime saveDate;
        public int currentDay;
        public string playerName;
    }
}
