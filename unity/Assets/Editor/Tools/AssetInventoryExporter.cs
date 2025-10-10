#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class AssetInventoryExporter
{
    [MenuItem("Tools/Assets/Export Asset Inventory CSVs")]
    public static void ExportCSVs()
    {
        var root = Path.GetDirectoryName(Application.dataPath); // unity project root
        var outDir = Path.Combine(root, "docs", "assets");
        Directory.CreateDirectory(outDir);
        var assets = AssetDatabase.FindAssets("")
            .Select(g => AssetDatabase.GUIDToAssetPath(g))
            .Where(p => p.StartsWith("Assets/"))
            .ToArray();

        using (var w = new StreamWriter(Path.Combine(outDir, "assets_inventory.csv")))
        {
            w.WriteLine("Category,RelativePath,Extension,SizeKB,Width,Height");
            foreach (var p in assets)
            {
                var ext = Path.GetExtension(p).ToLowerInvariant();
                if (!new []{".png",".jpg",".jpeg",".psd",".ttf",".otf",".wav",".mp3",".ogg",".mp4",".webm",".fbx",".obj",".prefab",".anim",".controller"}.Contains(ext)) continue;
                string cat = Categorize(p);
                long size = 0;
                int wPx = 0, hPx = 0;
                var fsPath = Path.Combine(root, p.Replace("/","\\"));
                if (File.Exists(fsPath)) size = new FileInfo(fsPath).Length/1024;
                if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                {
                    var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(p);
                    if (tex != null) { wPx = tex.width; hPx = tex.height; }
                }
                w.WriteLine($"{cat},{p},{ext},{size},{wPx},{hPx}");
            }
        }

        using (var w = new StreamWriter(Path.Combine(outDir, "missing_assets.csv")))
        {
            w.WriteLine("Category,Required,Present,Missing,RecommendedFormat,RecommendedSize,Notes");
            WriteMissing(w, "Portraits.Executives", 24, "png", "1024x1024", "6 execs x 4 emotions (neutral, happy, angry, stressed)");
            WriteMissing(w, "Portraits.Staff", 12, "png", "512x512", "6 staff x 2 emotions");
            WriteMissing(w, "Portraits.Stakeholders", 12, "png", "512x512", "board, media, public (12 total)");
            WriteMissing(w, "Portraits.Crisis", 16, "png", "512x512", "protesters, whistleblowers, rivals");
            WriteMissing(w, "Scenes.Backgrounds", 9, "png", "1920x1080", "office day/evening/night, press room, war room, protest, tv studio, crisis");
            WriteMissing(w, "UI.Icons", 40, "png", "128x128", "resources, statuses, actions, severity");
            WriteMissing(w, "UI.FramesCards", 12, "png", "1024x1536 (cards)", "card frames by rarity + event banners");
            WriteMissing(w, "Brand.Logos", 2, "png/svg", "1024x1024", "studio + game");
            WriteMissing(w, "Cards.Events", 50, "png", "512x512", "card thumbnails");
            WriteMissing(w, "Fonts", 4, "ttf/otf", "TMP assets", "heading/body/numeric/fallback");
            WriteMissing(w, "Audio.Music", 3, "ogg", "128-160kbps", "menu, gameplay, crisis");
            WriteMissing(w, "Audio.SFX", 25, "wav/ogg", "short", "ui, cards, stingers");
            WriteMissing(w, "Video.Cinematics", 10, "webm", "720p max for WebGL", "opening (1), stingers (3), meme overlays (6)");
        }

        Debug.Log($"Exported inventory to: {outDir}");
    }

    static string Categorize(string path)
    {
        var p = path.ToLowerInvariant();
        if (p.Contains("/art/ui/icons") || p.Contains("icon") || p.Contains("arrow") || p.Contains("warning")) return "UI.Icons";
        if (p.Contains("/art/ui/") || (p.Contains("card") && p.Contains("frame"))) return "UI.FramesCards";
        if (p.Contains("logo")) return "Brand.Logos";
        if (p.Contains("portrait") || p.Contains("fullbody") || p.Contains("technocrat") || p.Contains("madamcash") || p.Contains("presi")) return "Portraits.Executives";
        if (p.Contains("mainbg") || p.Contains("background") || p.Contains("scene") || p.Contains("crisisnews") || p.Contains("newscrisis")) return "Scenes.Backgrounds";
        if (path.EndsWith(".ttf") || path.EndsWith(".otf")) return "Fonts";
        if (path.EndsWith(".wav") || path.EndsWith(".mp3") || path.EndsWith(".ogg")) return p.Contains("music") || p.Contains("bgm") || p.Contains("theme") ? "Audio.Music" : "Audio.SFX";
        if (path.EndsWith(".prefab")) return "Prefabs";
        if (path.EndsWith(".anim") || path.EndsWith(".controller")) return "Animations";
        if (path.EndsWith(".fbx") || path.EndsWith(".obj")) return "Models3D";
        if (p.Contains("card") || p.Contains("event")) return "Cards.Events";
        return "Uncategorized";
    }

    static void WriteMissing(StreamWriter w, string category, int required, string format, string size, string notes)
    {
        int present = AssetDatabase.FindAssets("")
            .Select(AssetDatabase.GUIDToAssetPath)
            .Count(p => Categorize(p) == category);
        int missing = Mathf.Max(0, required - present);
        w.WriteLine($"{category},{required},{present},{missing},{format},{size},{notes}");
    }
}
#endif

