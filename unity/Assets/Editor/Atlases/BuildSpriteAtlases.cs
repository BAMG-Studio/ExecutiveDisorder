#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;
using System.IO;

public static class BuildSpriteAtlases
{
    [MenuItem("Tools/Build Sprite Atlases")]
    public static void Build()
    {
        string atlasesRoot = "Assets/Art/Atlases";
        if (!AssetDatabase.IsValidFolder(atlasesRoot))
        {
            string artRoot = "Assets/Art";
            if (!AssetDatabase.IsValidFolder(artRoot)) AssetDatabase.CreateFolder("Assets", "Art");
            AssetDatabase.CreateFolder(artRoot, "Atlases");
        }

        CreateOrUpdateAtlas(Path.Combine(atlasesRoot, "UI.spriteatlas"), new []{
            "Assets/Art/UI/Icons",
            "Assets/Art/UI/FramesAndCards"
        });

        CreateOrUpdateAtlas(Path.Combine(atlasesRoot, "Portraits.spriteatlas"), new []{
            "Assets/Art/Portraits/Executives",
            "Assets/Art/Portraits/Staff",
            "Assets/Art/Portraits/Stakeholders",
            "Assets/Art/Portraits/Crisis"
        });

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Sprite Atlases built/updated.");
    }

    static void CreateOrUpdateAtlas(string assetPath, string[] includeFolders)
    {
        var atlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(assetPath);
        if (atlas == null)
        {
            atlas = new SpriteAtlas();
            var pack = new SpriteAtlasPackingParameters { enableTightPacking = false, padding = 2 }; 
            var tex = new SpriteAtlasTextureSettings { readable = false, generateMipMaps = false, sRGB = true }; 
            atlas.SetPackingSettings(pack);
            atlas.SetTextureSettings(tex);
            var p = new TextureImporterPlatformSettings { maxTextureSize = 2048, format = TextureImporterFormat.Automatic, textureCompression = TextureImporterCompression.CompressedHQ, overridden = true, name = "WebGL" };
            atlas.SetPlatformSettings(p);
            AssetDatabase.CreateAsset(atlas, assetPath);
        }
        var so = new SerializedObject(atlas);
        var soObjs = new Object[includeFolders.Length];
        for (int i = 0; i < includeFolders.Length; i++)
        {
            if (AssetDatabase.IsValidFolder(includeFolders[i])) soObjs[i] = AssetDatabase.LoadAssetAtPath<Object>(includeFolders[i]);
        }
        SpriteAtlasExtensions.Add(atlas, soObjs);
        SpriteAtlasUtility.PackAtlases(new []{ atlas }, EditorUserBuildSettings.activeBuildTarget);
        EditorUtility.SetDirty(atlas);
    }
}
#endif

