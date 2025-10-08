using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using UnityEditor.Build.Reporting;

/// <summary>
/// Automated build script for Codex CLI integration
/// Builds WebGL and other platforms via Unity command line
/// </summary>
public class CodexBuildScript
{
    // Build output paths
    private static readonly string BUILD_ROOT = "Builds";
    private static readonly string WEBGL_PATH = Path.Combine(BUILD_ROOT, "WebGL");
    private static readonly string WINDOWS_PATH = Path.Combine(BUILD_ROOT, "Windows");
    private static readonly string LINUX_PATH = Path.Combine(BUILD_ROOT, "Linux");
    private static readonly string MAC_PATH = Path.Combine(BUILD_ROOT, "MacOS");

    /// <summary>
    /// Build WebGL version - called by Codex CLI
    /// Usage: unity-editor -batchmode -nographics -executeMethod CodexBuildScript.BuildWebGL -quit
    /// </summary>
    [MenuItem("Codex/Build WebGL")]
    public static void BuildWebGL()
    {
        Debug.Log("🚀 Codex CLI: Starting WebGL build...");

        // Ensure data is up-to-date before building
        try { CodexDataImporter.RunImportIfNeeded(); }
        catch (System.Exception ex) { Debug.LogWarning($"Data import skipped or failed: {ex.Message}"); }

        string outputPath = Path.Combine(WEBGL_PATH, GetVersionString());
        
        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = GetScenePaths(),
            locationPathName = outputPath,
            target = BuildTarget.WebGL,
            options = BuildOptions.None
        };

        ConfigureWebGLSettings();
        
        BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
        
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log($"✅ WebGL build succeeded: {outputPath}");
            Debug.Log($"📊 Build size: {FormatBytes(report.summary.totalSize)}");
            Debug.Log($"⏱️ Build time: {report.summary.totalTime}");
            
            // Create build info file for Codex CLI
            CreateBuildInfo(outputPath, "WebGL", report);
        }
        else
        {
            Debug.LogError($"❌ WebGL build failed!");
            EditorApplication.Exit(1); // Exit with error code
        }
    }

    /// <summary>
    /// Build Windows standalone - called by Codex CLI
    /// </summary>
    [MenuItem("Codex/Build Windows")]
    public static void BuildWindows()
    {
        Debug.Log("🚀 Codex CLI: Starting Windows build...");
        
        string outputPath = Path.Combine(WINDOWS_PATH, GetVersionString(), "ExecutiveDisorder.exe");
        
        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = GetScenePaths(),
            locationPathName = outputPath,
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
        
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log($"✅ Windows build succeeded: {outputPath}");
            CreateBuildInfo(Path.GetDirectoryName(outputPath), "Windows", report);
        }
        else
        {
            Debug.LogError($"❌ Windows build failed!");
            EditorApplication.Exit(1);
        }
    }

    /// <summary>
    /// Build Linux standalone
    /// </summary>
    [MenuItem("Codex/Build Linux")]
    public static void BuildLinux()
    {
        Debug.Log("🚀 Codex CLI: Starting Linux build...");
        
        string outputPath = Path.Combine(LINUX_PATH, GetVersionString(), "ExecutiveDisorder.x86_64");
        
        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = GetScenePaths(),
            locationPathName = outputPath,
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
        
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log($"✅ Linux build succeeded: {outputPath}");
            CreateBuildInfo(Path.GetDirectoryName(outputPath), "Linux", report);
        }
        else
        {
            Debug.LogError($"❌ Linux build failed!");
            EditorApplication.Exit(1);
        }
    }

    /// <summary>
    /// Build all platforms - for complete release
    /// </summary>
    [MenuItem("Codex/Build All Platforms")]
    public static void BuildAll()
    {
        Debug.Log("🚀 Codex CLI: Building all platforms...");
        
        BuildWebGL();
        BuildWindows();
        BuildLinux();
        
        Debug.Log("✅ All platform builds completed!");
    }

    /// <summary>
    /// Configure WebGL-specific settings for optimization
    /// </summary>
    private static void ConfigureWebGLSettings()
    {
        // WebGL Player Settings
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip;
        PlayerSettings.WebGL.decompressionFallback = true;
        PlayerSettings.WebGL.dataCaching = true;
        PlayerSettings.WebGL.debugSymbols = false;
        
        // Optimization
        PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.WebGL, ManagedStrippingLevel.Medium);
        PlayerSettings.stripEngineCode = true;
        
        // Quality for WebGL
        QualitySettings.SetQualityLevel(2); // Medium quality
        
        Debug.Log("⚙️ WebGL settings configured for optimization");
    }

    /// <summary>
    /// Get all scene paths from Build Settings
    /// </summary>
    private static string[] GetScenePaths()
    {
        var scenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        
        if (scenes.Length == 0)
        {
            Debug.LogError("❌ No scenes in Build Settings! Add scenes first.");
        }
        
        Debug.Log($"📋 Building {scenes.Length} scenes:");
        foreach (var scene in scenes)
        {
            Debug.Log($"   - {scene}");
        }
        
        return scenes;
    }

    /// <summary>
    /// Generate version string from current date/time
    /// </summary>
    private static string GetVersionString()
    {
        return $"v{Application.version}_{DateTime.Now:yyyyMMdd_HHmmss}";
    }

    /// <summary>
    /// Create build info JSON file for Codex CLI
    /// </summary>
    private static void CreateBuildInfo(string outputPath, string platform, BuildReport report)
    {
        var buildInfo = new
        {
            platform = platform,
            version = Application.version,
            buildTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            unityVersion = Application.unityVersion,
            totalSize = report.summary.totalSize,
            totalSizeFormatted = FormatBytes(report.summary.totalSize),
            buildDuration = report.summary.totalTime.ToString(),
            scenesIncluded = GetScenePaths(),
            success = report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded
        };

        string json = JsonUtility.ToJson(buildInfo, true);
        string infoPath = Path.Combine(outputPath, "build-info.json");
        
        File.WriteAllText(infoPath, json);
        Debug.Log($"📄 Build info saved: {infoPath}");
    }

    /// <summary>
    /// Format bytes to human-readable size
    /// </summary>
    private static string FormatBytes(ulong bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        
        return $"{len:0.##} {sizes[order]}";
    }

    /// <summary>
    /// Verify build prerequisites before building
    /// </summary>
    [MenuItem("Codex/Verify Build Setup")]
    public static void VerifyBuildSetup()
    {
        Debug.Log("🔍 Verifying build setup...");
        
        bool allGood = true;
        
        // Check scenes
        if (EditorBuildSettings.scenes.Length == 0)
        {
            Debug.LogError("❌ No scenes in Build Settings!");
            allGood = false;
        }
        else
        {
            Debug.Log($"✅ {EditorBuildSettings.scenes.Length} scenes configured");
        }
        
        // Check player settings
        if (string.IsNullOrEmpty(PlayerSettings.companyName))
        {
            Debug.LogWarning("⚠️ Company name not set in Player Settings");
        }
        
        if (string.IsNullOrEmpty(PlayerSettings.productName))
        {
            Debug.LogWarning("⚠️ Product name not set in Player Settings");
        }
        
        // Check build output directory
        if (!Directory.Exists(BUILD_ROOT))
        {
            Directory.CreateDirectory(BUILD_ROOT);
            Debug.Log($"✅ Created build output directory: {BUILD_ROOT}");
        }
        
        if (allGood)
        {
            Debug.Log("✅ Build setup verified - ready to build!");
        }
        else
        {
            Debug.LogError("❌ Build setup has issues - fix before building");
        }
    }
}
