using ExecutiveDisorder.Core.Models;

namespace ExecutiveDisorder.Core.Systems;

/// <summary>
/// Manages all four game resources and crisis thresholds
/// </summary>
public class ResourceManager
{
    private readonly Dictionary<ResourceType, Resource> _resources;
    
    public event EventHandler<ResourceChangedEventArgs>? ResourceChanged;
    public event EventHandler<CrisisEventArgs>? CrisisTriggered;
    public event EventHandler? GameOver;

    public ResourceManager()
    {
        _resources = new Dictionary<ResourceType, Resource>
        {
            [ResourceType.Popularity] = new Resource(ResourceType.Popularity, "Popularity", "👥", 50),
            [ResourceType.Stability] = new Resource(ResourceType.Stability, "Stability", "🏛️", 50),
            [ResourceType.MediaTrust] = new Resource(ResourceType.MediaTrust, "Media Trust", "📺", 50),
            [ResourceType.EconomicHealth] = new Resource(ResourceType.EconomicHealth, "Economic Health", "💰", 50)
        };
    }

    public Resource GetResource(ResourceType type) => _resources[type];

    public IReadOnlyDictionary<ResourceType, Resource> GetAllResources() => _resources;

    /// <summary>
    /// Apply resource changes from a decision and trigger events
    /// </summary>
    public Dictionary<ResourceType, int> ApplyChanges(Dictionary<ResourceType, int> changes)
    {
        var actualChanges = new Dictionary<ResourceType, int>();

        foreach (var (type, amount) in changes)
        {
            var resource = _resources[type];
            int actualChange = resource.Modify(amount);
            actualChanges[type] = actualChange;

            // Raise events
            ResourceChanged?.Invoke(this, new ResourceChangedEventArgs(type, resource.Value, actualChange));

            // Check for crisis thresholds
            if (resource.IsCritical())
            {
                CrisisTriggered?.Invoke(this, new CrisisEventArgs(type, CrisisLevel.Critical));
            }

            // Check for game over
            if (resource.IsDepleted())
            {
                GameOver?.Invoke(this, EventArgs.Empty);
            }
        }

        return actualChanges;
    }

    /// <summary>
    /// Calculate overall government health (0-100)
    /// </summary>
    public int CalculateOverallHealth()
    {
        return (int)_resources.Values.Average(r => r.Value);
    }

    /// <summary>
    /// Check if any resource is in critical state
    /// </summary>
    public bool IsInCrisis()
    {
        return _resources.Values.Any(r => r.IsCritical());
    }

    /// <summary>
    /// Get crisis level based on resources
    /// </summary>
    public CrisisLevel GetCrisisLevel()
    {
        var criticalCount = _resources.Values.Count(r => r.IsCritical());
        var depletedCount = _resources.Values.Count(r => r.IsDepleted());

        if (depletedCount > 0) return CrisisLevel.GameOver;
        if (criticalCount >= 3) return CrisisLevel.Critical;
        if (criticalCount >= 2) return CrisisLevel.Severe;
        if (criticalCount >= 1) return CrisisLevel.Warning;
        
        return CrisisLevel.Normal;
    }
}

public class ResourceChangedEventArgs : EventArgs
{
    public ResourceType Type { get; }
    public int NewValue { get; }
    public int Change { get; }

    public ResourceChangedEventArgs(ResourceType type, int newValue, int change)
    {
        Type = type;
        NewValue = newValue;
        Change = change;
    }
}

public class CrisisEventArgs : EventArgs
{
    public ResourceType ResourceType { get; }
    public CrisisLevel Level { get; }

    public CrisisEventArgs(ResourceType resourceType, CrisisLevel level)
    {
        ResourceType = resourceType;
        Level = level;
    }
}

public enum CrisisLevel
{
    Normal,
    Warning,
    Severe,
    Critical,
    GameOver
}
