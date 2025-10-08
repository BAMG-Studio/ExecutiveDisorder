using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using ExecutiveDisorder.Game.Data;

/// <summary>
/// Converts JSON content in Assets/Game/Data into ScriptableObject assets
/// under Assets/Game/Generated. Provides menu items and a CLI entrypoint.
/// </summary>
public static class CodexDataImporter
{
    private const string DataRoot = "Assets/Game/Data";
    private const string OutRoot = "Assets/Game/Generated";

    [Serializable]
    private class CardList { public List<CardJson> cards = new(); }
    [Serializable]
    private class LeaderList { public List<LeaderJson> leaders = new(); }
    [Serializable]
    private class FactionList { public List<FactionJson> factions = new(); }
    [Serializable]
    private class CrisisList { public List<CrisisJson> crises = new(); }

    [Serializable]
    private class CardJson
    {
        public string id;
        public string name;
        public string description;
        public int cost;
        public string rarity;
        public List<string> tags = new();
        public List<EffectJson> effects = new();
        public string artKey;
    }

    [Serializable]
    private class EffectJson
    {
        public string type;
        public float value;
        public string withTag;
        public string target;
    }

    [Serializable]
    private class LeaderJson
    {
        public string id;
        public string name;
        public string bio;
        public List<string> traitTags = new();
        public List<string> startingDeck = new();
        public string portraitKey;
    }

    [Serializable]
    private class FactionJson
    {
        public string id;
        public string name;
        public string description;
        public string color; // hex #RRGGBB or #RRGGBBAA
        public List<string> tags = new();
    }

    [Serializable]
    private class CrisisJson
    {
        public string id;
        public string name;
        public string description;
        public int severity;
        public List<string> tags = new();
        public List<EffectJson> effects = new();
        public List<string> next = new();
    }

    [MenuItem("Codex/Data/Import All")]
    public static void ImportAll()
    {
        EnsureDirs();
        var database = GetOrCreateDatabase();

        var cards = ImportCards(Path.Combine(DataRoot, "cards.json"));
        var leaders = ImportLeaders(Path.Combine(DataRoot, "leaders.json"));
        var factions = ImportFactions(Path.Combine(DataRoot, "factions.json"));
        var crises = ImportCrises(Path.Combine(DataRoot, "crises.json"));

        database.cards = cards;
        database.leaders = leaders;
        database.factions = factions;
        database.crises = crises;

        EditorUtility.SetDirty(database);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"✅ CodexDataImporter: Imported {cards.Count} cards, {leaders.Count} leaders, {factions.Count} factions, {crises.Count} crises.");
    }

    /// <summary>
    /// Called from build to avoid stale data. Imports only if JSON is newer than generated assets.
    /// </summary>
    public static void RunImportIfNeeded()
    {
        try
        {
            if (!Directory.Exists(DataRoot)) return;

            var jsonFiles = Directory.GetFiles(DataRoot, "*.json", SearchOption.TopDirectoryOnly);
            if (jsonFiles.Length == 0) return;

            var newestJson = jsonFiles.Select(File.GetLastWriteTimeUtc).DefaultIfEmpty(DateTime.MinValue).Max();

            if (!Directory.Exists(OutRoot))
            {
                Debug.Log("📥 CodexDataImporter: No generated assets found; importing data.");
                ImportAll();
                return;
            }

            var generatedFiles = Directory.GetFiles(OutRoot, "*.asset", SearchOption.AllDirectories);
            var newestAsset = generatedFiles.Select(File.GetLastWriteTimeUtc).DefaultIfEmpty(DateTime.MinValue).Max();

            if (newestJson > newestAsset)
            {
                Debug.Log("📥 CodexDataImporter: JSON changed; re-importing.");
                ImportAll();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"CodexDataImporter: RunImportIfNeeded failed: {ex}");
        }
    }

    private static void EnsureDirs()
    {
        Directory.CreateDirectory(DataRoot);
        Directory.CreateDirectory(OutRoot);
        Directory.CreateDirectory(Path.Combine(OutRoot, "Cards"));
        Directory.CreateDirectory(Path.Combine(OutRoot, "Leaders"));
        Directory.CreateDirectory(Path.Combine(OutRoot, "Factions"));
        Directory.CreateDirectory(Path.Combine(OutRoot, "Crises"));
        Directory.CreateDirectory("Assets/Resources/Generated");
    }

    private static GameDatabase GetOrCreateDatabase()
    {
        const string dbPath = "Assets/Resources/Generated/GameDatabase.asset";
        var db = AssetDatabase.LoadAssetAtPath<GameDatabase>(dbPath);
        if (db == null)
        {
            db = ScriptableObject.CreateInstance<GameDatabase>();
            AssetDatabase.CreateAsset(db, dbPath);
        }
        return db;
    }

    private static List<CardDef> ImportCards(string path)
    {
        var result = new List<CardDef>();
        if (!File.Exists(path)) return result;

        var json = File.ReadAllText(path);
        var container = JsonUtility.FromJson<CardList>(json) ?? new CardList();
        foreach (var cj in container.cards)
        {
            var assetPath = OutRoot + "/Cards/" + cj.id + ".asset";
            var asset = AssetDatabase.LoadAssetAtPath<CardDef>(assetPath);
            var isNew = false;
            if (asset == null) { asset = ScriptableObject.CreateInstance<CardDef>(); isNew = true; }

            asset.id = cj.id;
            asset.displayName = cj.name;
            asset.description = cj.description;
            asset.cost = cj.cost;
            asset.rarity = cj.rarity;
            asset.tags = cj.tags ?? new List<string>();
            asset.artKey = cj.artKey;

            asset.effects = new List<EffectSpec>();
            if (cj.effects != null)
            {
                foreach (var e in cj.effects)
                {
                    asset.effects.Add(new EffectSpec
                    {
                        type = e.type,
                        value = e.value,
                        withTag = e.withTag,
                        target = e.target
                    });
                }
            }

            if (isNew) AssetDatabase.CreateAsset(asset, assetPath);
            EditorUtility.SetDirty(asset);
            TryMarkAddressable(asset, $"content/cards/{asset.id}", "cards");
            result.Add(asset);
        }
        return result;
    }

    private static List<LeaderDef> ImportLeaders(string path)
    {
        var result = new List<LeaderDef>();
        if (!File.Exists(path)) return result;

        var json = File.ReadAllText(path);
        var container = JsonUtility.FromJson<LeaderList>(json) ?? new LeaderList();
        foreach (var lj in container.leaders)
        {
            var assetPath = OutRoot + "/Leaders/" + lj.id + ".asset";
            var asset = AssetDatabase.LoadAssetAtPath<LeaderDef>(assetPath);
            var isNew = false;
            if (asset == null) { asset = ScriptableObject.CreateInstance<LeaderDef>(); isNew = true; }

            asset.id = lj.id;
            asset.displayName = lj.name;
            asset.bio = lj.bio;
            asset.traitTags = lj.traitTags ?? new List<string>();
            asset.startingDeck = lj.startingDeck ?? new List<string>();
            asset.portraitKey = lj.portraitKey;

            if (isNew) AssetDatabase.CreateAsset(asset, assetPath);
            EditorUtility.SetDirty(asset);
            TryMarkAddressable(asset, $"content/leaders/{asset.id}", "leaders");
            result.Add(asset);
        }
        return result;
    }

    private static List<FactionDef> ImportFactions(string path)
    {
        var result = new List<FactionDef>();
        if (!File.Exists(path)) return result;

        var json = File.ReadAllText(path);
        var container = JsonUtility.FromJson<FactionList>(json) ?? new FactionList();
        foreach (var fj in container.factions)
        {
            var assetPath = OutRoot + "/Factions/" + fj.id + ".asset";
            var asset = AssetDatabase.LoadAssetAtPath<FactionDef>(assetPath);
            var isNew = false;
            if (asset == null) { asset = ScriptableObject.CreateInstance<FactionDef>(); isNew = true; }

            asset.id = fj.id;
            asset.displayName = fj.name;
            asset.description = fj.description;
            asset.tags = fj.tags ?? new List<string>();
            asset.color = ParseColorHex(fj.color, Color.white);

            if (isNew) AssetDatabase.CreateAsset(asset, assetPath);
            EditorUtility.SetDirty(asset);
            TryMarkAddressable(asset, $"content/factions/{asset.id}", "factions");
            result.Add(asset);
        }
        return result;
    }

    private static List<CrisisDef> ImportCrises(string path)
    {
        var result = new List<CrisisDef>();
        if (!File.Exists(path)) return result;

        var json = File.ReadAllText(path);
        var container = JsonUtility.FromJson<CrisisList>(json) ?? new CrisisList();
        foreach (var cj in container.crises)
        {
            var assetPath = OutRoot + "/Crises/" + cj.id + ".asset";
            var asset = AssetDatabase.LoadAssetAtPath<CrisisDef>(assetPath);
            var isNew = false;
            if (asset == null) { asset = ScriptableObject.CreateInstance<CrisisDef>(); isNew = true; }

            asset.id = cj.id;
            asset.displayName = cj.name;
            asset.description = cj.description;
            asset.severity = Mathf.Clamp(cj.severity, 1, 5);
            asset.tags = cj.tags ?? new List<string>();
            asset.nextCrisisIds = cj.next ?? new List<string>();

            asset.effects = new List<EffectSpec>();
            if (cj.effects != null)
            {
                foreach (var e in cj.effects)
                {
                    asset.effects.Add(new EffectSpec
                    {
                        type = e.type,
                        value = e.value,
                        withTag = e.withTag,
                        target = e.target
                    });
                }
            }

            if (isNew) AssetDatabase.CreateAsset(asset, assetPath);
            EditorUtility.SetDirty(asset);
            TryMarkAddressable(asset, $"content/crises/{asset.id}", "crises");
            result.Add(asset);
        }
        return result;
    }

    private static Color ParseColorHex(string hex, Color fallback)
    {
        if (string.IsNullOrEmpty(hex)) return fallback;
        if (ColorUtility.TryParseHtmlString(hex, out var c)) return c;
        return fallback;
    }

    private static void TryMarkAddressable(UnityEngine.Object asset, string address, string label)
    {
        if (asset == null) return;

        try
        {
            var settingsType = Type.GetType("UnityEditor.AddressableAssets.Settings.AddressableAssetSettings, Unity.Addressables.Editor");
            var defaultObjectType = Type.GetType("UnityEditor.AddressableAssets.Settings.AddressableAssetSettingsDefaultObject, Unity.Addressables.Editor");
            if (settingsType == null || defaultObjectType == null) return;

            var getSettings = defaultObjectType.GetMethod("GetSettings", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(bool) }, null);
            if (getSettings == null) return;

            var settings = getSettings.Invoke(null, new object[] { true });
            if (settings == null) return;

            var assetPath = AssetDatabase.GetAssetPath(asset);
            if (string.IsNullOrEmpty(assetPath)) return;
            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            if (string.IsNullOrEmpty(guid)) return;

            var defaultGroupProp = settingsType.GetProperty("DefaultGroup", BindingFlags.Public | BindingFlags.Instance);
            var group = defaultGroupProp?.GetValue(settings);
            if (group == null)
            {
                var groupsProp = settingsType.GetProperty("groups", BindingFlags.Public | BindingFlags.Instance);
                if (groupsProp?.GetValue(settings) is System.Collections.IList groups && groups.Count > 0)
                {
                    group = groups[0];
                }
            }
            if (group == null) return;

            MethodInfo createOrMoveEntry = null;
            foreach (var method in settingsType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                var parameters = method.GetParameters();
                if (method.Name == "CreateOrMoveEntry" && parameters.Length >= 2 && parameters[0].ParameterType == typeof(string))
                {
                    createOrMoveEntry = method;
                    break;
                }
            }

            if (createOrMoveEntry == null) return;

            object entry;
            var paramCount = createOrMoveEntry.GetParameters().Length;
            if (paramCount == 4)
            {
                entry = createOrMoveEntry.Invoke(settings, new object[] { guid, group, false, false });
            }
            else if (paramCount == 3)
            {
                entry = createOrMoveEntry.Invoke(settings, new object[] { guid, group, false });
            }
            else
            {
                entry = createOrMoveEntry.Invoke(settings, new object[] { guid, group });
            }

            if (entry == null) return;

            var entryType = entry.GetType();
            entryType.GetProperty("address", BindingFlags.Public | BindingFlags.Instance)?.SetValue(entry, address);

            MethodInfo setLabel = null;
            foreach (var method in entryType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                if (method.Name == "SetLabel" && method.GetParameters().Length > 0)
                {
                    setLabel = method;
                    break;
                }
            }

            if (setLabel != null)
            {
                var parameters = setLabel.GetParameters();
                var args = new object[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i == 0)
                    {
                        args[i] = label;
                    }
                    else
                    {
                        args[i] = true;
                    }
                }
                setLabel.Invoke(entry, args);
            }

            if (settings is ScriptableObject so)
            {
                EditorUtility.SetDirty(so);
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"CodexDataImporter: Addressables assignment failed for {asset.name}: {ex.Message}");
        }
    }
}
