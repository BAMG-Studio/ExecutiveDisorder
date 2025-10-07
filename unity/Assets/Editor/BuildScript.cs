using UnityEditor;
using UnityEngine;
using System;

/// <summary>
/// Build script for command-line Unity builds
/// Allows building without opening Unity Editor UI
/// </summary>
public class BuildScript
{
    // Build paths
    private static string BuildBasePath = "Builds";
    
    /// <summary>
    /// Build WebGL version from command line
    /// Usage: Unity.exe -quit -batchmode -executeMethod BuildScript.BuildWebGL
    /// </summary>
    public static void BuildWebGL()
    {
        string buildPath = $"{BuildBasePath}/WebGL/{DateTime.Now:yyyyMMdd_HHmmss}";
        
        Debug.Log($"[BuildScript] Starting WebGL build...");
        Debug.Log($"[BuildScript] Output path: {buildPath}");
        
        // Get scenes from Build Settings
        string[] scenes = GetScenesFromBuildSettings();
        
        if (scenes.Length == 0)
        {
            Debug.LogError("[BuildScript] No scenes in build settings!");
            EditorApplication.Exit(1);
            return;
        }
        
        Debug.Log($"[BuildScript] Building {scenes.Length} scenes:");
        foreach (string scene in scenes)
        {
            Debug.Log($"  - {scene}");
        }
        
        // Configure build options
        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = BuildTarget.WebGL,
            options = BuildOptions.None
        };
        
        // Execute build
        Debug.Log("[BuildScript] Executing build...");
        var report = BuildPipeline.BuildPlayer(buildOptions);
        
        // Check result
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log($"[BuildScript] ✅ BUILD SUCCESSFUL!");
            Debug.Log($"[BuildScript] Size: {report.summary.totalSize / (1024 * 1024)} MB");
            Debug.Log($"[BuildScript] Time: {report.summary.totalTime}");
            EditorApplication.Exit(0);
        }
        else
        {
            Debug.LogError($"[BuildScript] ❌ BUILD FAILED: {report.summary.result}");
            EditorApplication.Exit(1);
        }
    }
    
    /// <summary>
    /// Build Windows Standalone from command line
    /// </summary>
    public static void BuildWindows()
    {
        string buildPath = $"{BuildBasePath}/Windows/{DateTime.Now:yyyyMMdd_HHmmss}/ExecutiveDisorder.exe";
        
        Debug.Log($"[BuildScript] Starting Windows build...");
        
        string[] scenes = GetScenesFromBuildSettings();
        
        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.None
        };
        
        var report = BuildPipeline.BuildPlayer(buildOptions);
        
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log($"[BuildScript] ✅ BUILD SUCCESSFUL!");
            EditorApplication.Exit(0);
        }
        else
        {
            Debug.LogError($"[BuildScript] ❌ BUILD FAILED: {report.summary.result}");
            EditorApplication.Exit(1);
        }
    }
    
    /// <summary>
    /// Build Linux Standalone from command line
    /// </summary>
    public static void BuildLinux()
    {
        string buildPath = $"{BuildBasePath}/Linux/{DateTime.Now:yyyyMMdd_HHmmss}/ExecutiveDisorder.x86_64";
        
        Debug.Log($"[BuildScript] Starting Linux build...");
        
        string[] scenes = GetScenesFromBuildSettings();
        
        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.None
        };
        
        var report = BuildPipeline.BuildPlayer(buildOptions);
        
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log($"[BuildScript] ✅ BUILD SUCCESSFUL!");
            EditorApplication.Exit(0);
        }
        else
        {
            Debug.LogError($"[BuildScript] ❌ BUILD FAILED: {report.summary.result}");
            EditorApplication.Exit(1);
        }
    }
    
    /// <summary>
    /// Build all platforms from command line
    /// </summary>
    public static void BuildAll()
    {
        Debug.Log("[BuildScript] Building all platforms...");
        
        BuildWebGL();
        BuildWindows();
        BuildLinux();
        
        Debug.Log("[BuildScript] All builds complete!");
        EditorApplication.Exit(0);
    }
    
    /// <summary>
    /// Get scenes from Build Settings
    /// </summary>
    private static string[] GetScenesFromBuildSettings()
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        string[] scenePaths = new string[scenes.Length];
        
        for (int i = 0; i < scenes.Length; i++)
        {
            scenePaths[i] = scenes[i].path;
        }
        
        return scenePaths;
    }
    
    /// <summary>
    /// Configure build settings from command line
    /// Usage: Unity.exe -executeMethod BuildScript.ConfigureBuildSettings -scenes "Assets/Scenes/MainMenu.unity,Assets/Scenes/Game.unity"
    /// </summary>
    public static void ConfigureBuildSettings()
    {
        Debug.Log("[BuildScript] Configuring build settings...");
        
        // This can be extended to configure settings from command-line arguments
        string[] commandLineArgs = Environment.GetCommandLineArgs();
        
        for (int i = 0; i < commandLineArgs.Length; i++)
        {
            if (commandLineArgs[i] == "-scenes" && i + 1 < commandLineArgs.Length)
            {
                string scenesArg = commandLineArgs[i + 1];
                string[] scenePaths = scenesArg.Split(',');
                
                EditorBuildSettingsScene[] buildScenes = new EditorBuildSettingsScene[scenePaths.Length];
                for (int j = 0; j < scenePaths.Length; j++)
                {
                    buildScenes[j] = new EditorBuildSettingsScene(scenePaths[j], true);
                }
                
                EditorBuildSettings.scenes = buildScenes;
                Debug.Log($"[BuildScript] Configured {buildScenes.Length} scenes");
            }
        }
        
        EditorApplication.Exit(0);
    }
}
