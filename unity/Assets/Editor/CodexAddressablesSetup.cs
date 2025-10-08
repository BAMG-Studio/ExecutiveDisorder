using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ExecutiveDisorder.EditorTools
{
    public static class CodexAddressablesSetup
    {
        private const string PackageId = "com.unity.addressables";

        [MenuItem("Codex/Setup/Ensure Addressables Package")]
        public static void EnsurePackageAndSettings()
        {
            try
            {
                // 1) Ensure package is installed
                var listRequest = UnityEditor.PackageManager.Client.List(true);
                EditorApplication.update += CheckListComplete;

                void CheckListComplete()
                {
                    if (!listRequest.IsCompleted) return;
                    EditorApplication.update -= CheckListComplete;

                    bool installed = listRequest.Result.Any(p => p.name == PackageId);
                    if (!installed)
                    {
                        Debug.Log("Installing Addressables...");
                        UnityEditor.PackageManager.Client.Add(PackageId);
                    }
                    else
                    {
                        Debug.Log("Addressables already installed.");
                        EnsureSettings();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Addressables setup skipped: {ex.Message}");
            }
        }

        [MenuItem("Codex/Setup/Create Addressables Settings")] 
        public static void EnsureSettings()
        {
            try
            {
                var defaultObjType = Type.GetType("UnityEditor.AddressableAssets.Settings.AddressableAssetSettingsDefaultObject, Unity.Addressables.Editor");
                if (defaultObjType == null)
                {
                    Debug.Log("Addressables editor assembly not loaded yet. Reopen Unity or try again after install.");
                    return;
                }

                var getSettingsMethod = defaultObjType.GetMethod("GetSettings", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(bool) }, null);
                getSettingsMethod?.Invoke(null, new object[] { true });
                Debug.Log("✅ Addressables settings ensured.");
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"EnsureSettings failed: {ex.Message}");
            }
        }
    }
}

