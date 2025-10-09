// Auto-apply sensible import settings for WebGL-ready assets
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class DefaultImportSettings : AssetPostprocessor
{
    static bool IsUIPath(string p) => p.Contains("/Art/UI/") || p.Contains("/UI/") || p.Contains("/Cards/") || p.Contains("/Portraits/");
    static bool IsPortrait(string p) => p.ToLower().Contains("portrait") || p.ToLower().Contains("fullbody");
    static bool IsBackground(string p) => p.ToLower().Contains("mainbg") || p.ToLower().Contains("background") || p.ToLower().Contains("scene");
    static bool IsMusic(string p) => p.ToLower().Contains("music") || p.ToLower().Contains("bgm") || p.ToLower().Contains("theme");

    void OnPreprocessTexture()
    {
        var ti = (TextureImporter)assetImporter;
        string path = assetPath.Replace('\\','/');

        // General defaults
        ti.alphaIsTransparency = true;
        ti.sRGBTexture = true;
        ti.mipmapEnabled = false;
        ti.wrapMode = TextureWrapMode.Clamp;
        ti.textureCompression = TextureImporterCompression.CompressedHQ;

        // Category-specific
        if (IsUIPath(path))
        {
            ti.textureType = TextureImporterType.Sprite;
            ti.spritePixelsPerUnit = 100;
            ti.filterMode = FilterMode.Bilinear;
            ti.mipmapEnabled = false;
            if (IsPortrait(path)) ti.maxTextureSize = 1024; // 512–1024
            else ti.maxTextureSize = 2048;
        }
        else if (IsBackground(path))
        {
            ti.textureType = TextureImporterType.Default;
            ti.filterMode = FilterMode.Trilinear;
            ti.mipmapEnabled = true;
            ti.maxTextureSize = 2048;
        }
    }

    void OnPreprocessAudio()
    {
        var ai = (AudioImporter)assetImporter;
        string path = assetPath.Replace('\\','/');
        ai.loadInBackground = true;
        ai.forceToMono = false;
        var settings = ai.defaultSampleSettings;
        settings.sampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;
        if (IsMusic(path))
        {
            settings.compressionFormat = AudioCompressionFormat.Vorbis;
            settings.quality = 0.5f; // ~128-160kbps
            settings.loadType = AudioClipLoadType.Streaming;
        }
        else
        {
            settings.compressionFormat = AudioCompressionFormat.Vorbis;
            settings.quality = 0.45f; // SFX smaller
            settings.loadType = AudioClipLoadType.CompressedInMemory;
        }
        ai.defaultSampleSettings = settings;
    }
}
#endif

