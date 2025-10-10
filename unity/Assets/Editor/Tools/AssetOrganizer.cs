#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class AssetOrganizer
{
    const string DropboxPrefKey = "ED_DropboxRoot";

    [MenuItem("Tools/Assets/Set Dropbox Root...")]
    public static void SetDropboxRoot()
    {
        var current = EditorPrefs.GetString(DropboxPrefKey, GuessDropboxRoot());
        string selected = EditorUtility.OpenFolderPanel("Select Dropbox Folder", current, "");
        if (!string.IsNullOrEmpty(selected))
        {
            EditorPrefs.SetString(DropboxPrefKey, selected);
            EditorUtility.DisplayDialog("Dropbox Root", $"Set to:\n{selected}", "OK");
        }
    }

    [MenuItem("Tools/Assets/Create Dropbox Structure")]
    public static void CreateDropboxStructure()
    {
        string root = GetDropboxRootOrWarn();
        if (string.IsNullOrEmpty(root)) return;
        string basePath = Path.Combine(root, "ExecutiveDisorder", "Art");
        string[] rel = new []{
            "Portraits/Executives","Portraits/Staff","Portraits/Stakeholders","Portraits/Crisis",
            "Scenes/Backgrounds","Scenes/Crisis","UI/Icons","UI/FramesAndCards","Cards/Thumbnails",
            "Brand/Logos","Fonts","Audio/Music","Audio/SFX","Video/Openings","Prefabs/UI",
            "3D/Models","3D/Props"
        };
        foreach (var r in rel)
        {
            string p = Path.Combine(basePath, r);
            Directory.CreateDirectory(p);
        }
        EditorUtility.RevealInFinder(basePath);
        Debug.Log($"Created/verified Dropbox structure at: {basePath}");
    }

    [MenuItem("Tools/Assets/Normalize Existing Assets")]
    public static void NormalizeExistingAssets()
    {
        EnsureArtFolders();
        string[] imageGuids = AssetDatabase.FindAssets("t:Texture2D");
        foreach (var guid in imageGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (!path.StartsWith("Assets/")) continue;
            var lower = path.ToLowerInvariant();
            string target = null;
            if (lower.Contains("portrait") || lower.Contains("fullbody") || lower.Contains("technocrat") || lower.Contains("madamcash") || lower.Contains("presi"))
                target = "Assets/Art/Portraits/Executives";
            else if (lower.Contains("icon") || lower.Contains("arrow") || lower.Contains("warning"))
                target = "Assets/Art/UI/Icons";
            else if (lower.Contains("mainbg") || lower.Contains("background") || lower.Contains("crisisnews") || lower.Contains("newscrisis"))
                target = "Assets/Art/Scenes/Backgrounds";

            if (target != null && !path.Replace("\\","/").StartsWith(target))
            {
                string fileName = Path.GetFileName(path);
                string dest = Path.Combine(target, fileName).Replace("\\","/");
                string result = AssetDatabase.MoveAsset(path, dest);
                if (!string.IsNullOrEmpty(result)) Debug.LogWarning($"Move failed: {path} -> {dest}: {result}");
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Asset normalization complete.");
    }

    [MenuItem("Tools/Assets/Import From Dropbox And Build Atlases")]
    public static void ImportFromDropboxAndBuild()
    {
        string root = GetDropboxRootOrWarn();
        if (string.IsNullOrEmpty(root)) return;
        string src = Path.Combine(root, "ExecutiveDisorder", "Art");
        if (!Directory.Exists(src))
        {
            EditorUtility.DisplayDialog("Dropbox Import", $"Source not found:\n{src}", "OK");
            return;
        }
        EnsureArtFolders();
        // Mirror known subfolders into Assets/Art
        string dstBase = "Assets/Art";
        var subdirs = new[]{
            "Portraits/Executives","Portraits/Staff","Portraits/Stakeholders","Portraits/Crisis",
            "Scenes/Backgrounds","Scenes/Crisis","UI/Icons","UI/FramesAndCards","Cards/Thumbnails",
            "Brand/Logos","Fonts"
        };
        foreach (var rel in subdirs)
        {
            string s = Path.Combine(src, rel);
            string d = Path.Combine(dstBase, rel);
            if (!Directory.Exists(s)) continue;
            Directory.CreateDirectory(d);
            foreach (var file in Directory.GetFiles(s))
            {
                var ext = Path.GetExtension(file).ToLowerInvariant();
                if (!new[]{".png",".jpg",".jpeg",".psd",".ttf",".otf",".ogg",".wav",".mp3",".webm",".mp4"}.Contains(ext)) continue;
                string dest = Path.Combine(d, Path.GetFileName(file));
                try
                {
                    FileUtil.ReplaceFile(file, dest);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"Copy failed: {file} -> {dest}: {ex.Message}");
                }
            }
        }
        AssetDatabase.Refresh();
        BuildSpriteAtlases.Build();
        Debug.Log("Imported from Dropbox and rebuilt sprite atlases.");
    }

    [MenuItem("Tools/Assets/Reimport + Build Atlases")]
    public static void ReimportAndBuild()
    {
        AssetDatabase.Refresh();
        BuildSpriteAtlases.Build();
        Debug.Log("Reimported and built sprite atlases.");
    }

    static void EnsureArtFolders()
    {
        string[] paths = {
            "Assets/Art",
            "Assets/Art/Portraits/Executives",
            "Assets/Art/Portraits/Staff",
            "Assets/Art/Portraits/Stakeholders",
            "Assets/Art/Portraits/Crisis",
            "Assets/Art/Scenes/Backgrounds",
            "Assets/Art/Scenes/Crisis",
            "Assets/Art/UI/Icons",
            "Assets/Art/UI/FramesAndCards",
            "Assets/Art/Cards/Thumbnails",
            "Assets/Art/Brand/Logos"
        };
        foreach (var p in paths)
        {
            CreateFolderPath(p);
        }
    }

    static void CreateFolderPath(string fullPath)
    {
        var parts = fullPath.Split('/');
        string cur = parts[0];
        for (int i = 1; i < parts.Length; i++)
        {
            string next = cur + "/" + parts[i];
            if (!AssetDatabase.IsValidFolder(next))
            {
                AssetDatabase.CreateFolder(cur, parts[i]);
            }
            cur = next;
        }
    }

    static string GetDropboxRootOrWarn()
    {
        string root = EditorPrefs.GetString(DropboxPrefKey, GuessDropboxRoot());
        if (string.IsNullOrEmpty(root) || !Directory.Exists(root))
        {
            EditorUtility.DisplayDialog("Dropbox Root Not Set", "Please set the Dropbox root via Tools > Assets > Set Dropbox Root...", "OK");
            return null;
        }
        return root;
    }

    static string GuessDropboxRoot()
    {
        string env = Environment.GetEnvironmentVariable("Dropbox");
        if (!string.IsNullOrEmpty(env) && Directory.Exists(env)) return env;
        string user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string guess = Path.Combine(user, "Dropbox");
        return Directory.Exists(guess) ? guess : user;
    }
}
#endif

