using System.Text.Json;
using ExecutiveDisorder.Core.Models;

namespace ExecutiveDisorder.Core.State;

public class SaveSystem
{
    private const string SaveDirectory = "saves";
    private const string SaveExtension = ".edsave";

    public static void SaveGame(GameState gameState, string saveName = "autosave")
    {
        Directory.CreateDirectory(SaveDirectory);
        var saveData = new SaveData(gameState);
        var json = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(Path.Combine(SaveDirectory, saveName + SaveExtension), json);
    }

    public static SaveData? LoadGame(string saveName = "autosave")
    {
        var path = Path.Combine(SaveDirectory, saveName + SaveExtension);
        if (!File.Exists(path)) return null;
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<SaveData>(json);
    }

    public static List<string> GetSaveFiles()
    {
        if (!Directory.Exists(SaveDirectory)) return new();
        return Directory.GetFiles(SaveDirectory, "*" + SaveExtension)
            .Select(Path.GetFileNameWithoutExtension).ToList();
    }
}

public class SaveData
{
    public int CurrentDay { get; set; }
    public int TotalDecisions { get; set; }
    public int ChaosScore { get; set; }
    public Dictionary<ResourceType, int> Resources { get; set; } = new();
    public List<string> DecisionHistory { get; set; } = new();
    public DateTime SaveTime { get; set; }

    public SaveData() { }

    public SaveData(GameState gameState)
    {
        CurrentDay = gameState.CurrentDay;
        TotalDecisions = gameState.TotalDecisions;
        ChaosScore = gameState.ChaosScore;
        DecisionHistory = new(gameState.DecisionHistory);
        SaveTime = DateTime.Now;
        
        foreach (var (type, resource) in gameState.Resources.GetAllResources())
            Resources[type] = resource.Value;
    }

    public void ApplyTo(GameState gameState)
    {
        foreach (var (type, value) in Resources)
            gameState.Resources.GetResource(type).SetValue(value);
    }
}
