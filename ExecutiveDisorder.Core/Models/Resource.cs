namespace ExecutiveDisorder.Core.Models;

/// <summary>
/// Represents the four key resources in Executive Disorder
/// </summary>
public enum ResourceType
{
    Popularity,      // Public approval rating
    Stability,       // Government/institutional stability
    MediaTrust,      // Media and press relations
    EconomicHealth   // Economic indicators
}

/// <summary>
/// A single game resource with value constraints
/// </summary>
public class Resource
{
    public ResourceType Type { get; init; }
    public string Name { get; init; }
    public string Icon { get; init; }
    public int Value { get; private set; }
    public int MinValue { get; init; } = 0;
    public int MaxValue { get; init; } = 100;
    public ResourceTrend Trend { get; private set; } = ResourceTrend.Stable;

    public Resource(ResourceType type, string name, string icon, int initialValue = 50)
    {
        Type = type;
        Name = name;
        Icon = icon;
        Value = Math.Clamp(initialValue, MinValue, MaxValue);
    }

    /// <summary>
    /// Modify resource value and return the actual change amount
    /// </summary>
    public int Modify(int amount)
    {
        int oldValue = Value;
        Value = Math.Clamp(Value + amount, MinValue, MaxValue);
        int actualChange = Value - oldValue;
        
        UpdateTrend(actualChange);
        return actualChange;
    }

    public void SetValue(int value)
    {
        Value = Math.Clamp(value, MinValue, MaxValue);
    }

    private void UpdateTrend(int change)
    {
        Trend = change switch
        {
            > 0 => ResourceTrend.Increasing,
            < 0 => ResourceTrend.Decreasing,
            _ => ResourceTrend.Stable
        };
    }

    public bool IsCritical() => Value <= 20;
    public bool IsHealthy() => Value >= 70;
    public bool IsDepleted() => Value <= 0;
}

public enum ResourceTrend
{
    Increasing,
    Stable,
    Decreasing
}
