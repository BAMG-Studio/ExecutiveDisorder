using UnityEditor;

// Keep WebGL textures small by default; can be overridden per-asset later.
public class ArtTexturePostprocessor : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        var importer = (TextureImporter)assetImporter;
        importer.textureCompression = TextureImporterCompression.CompressedHQ;
        importer.crunchedCompression = true;
        importer.compressionQuality = 75;
        importer.maxTextureSize = 1024;
        importer.mipmapEnabled = false;
    }
}

