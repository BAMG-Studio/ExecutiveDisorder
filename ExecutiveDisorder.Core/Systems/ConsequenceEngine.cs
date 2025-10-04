using ExecutiveDisorder.Core.Cards;
using ExecutiveDisorder.Core.Models;

namespace ExecutiveDisorder.Core.Systems;

/// <summary>
/// Processes decision card effects and generates consequences
/// </summary>
public class ConsequenceEngine
{
    private readonly ResourceManager _resourceManager;
    private readonly List<string> _newsHeadlines = new();
    private readonly List<string> _consequenceHistory = new();

    public event EventHandler<ConsequenceEventArgs>? ConsequenceGenerated;

    public ConsequenceEngine(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
    }

    /// <summary>
    /// Process a player's choice and generate all consequences
    /// </summary>
    public ConsequenceResult ProcessChoice(DecisionCard card, CardChoice choice)
    {
        var result = new ConsequenceResult
        {
            CardId = card.Id,
            ChoiceId = choice.ChoiceId,
            ChoiceText = choice.Text
        };

        // Apply resource changes
        result.ResourceChanges = _resourceManager.ApplyChanges(choice.Effects);

        // Generate news headlines
        result.Headlines = GenerateHeadlines(card, choice, result.ResourceChanges);
        _newsHeadlines.AddRange(result.Headlines);

        // Add scripted consequences
        result.Consequences.AddRange(choice.Consequences);

        // Generate dynamic consequences based on effects
        result.Consequences.AddRange(GenerateDynamicConsequences(choice, result.ResourceChanges));

        // Determine followup cards
        result.FollowupCards.AddRange(choice.FollowupCardIds);

        // Check for cascade events
        if (ShouldTriggerCascade(result.ResourceChanges))
        {
            result.CascadeTriggered = true;
            result.CascadeEvent = GenerateCascadeEvent();
        }

        // Track for history
        _consequenceHistory.Add($"Day {DateTime.Now.Day}: {choice.Text} -> {string.Join(", ", result.Headlines)}");

        ConsequenceGenerated?.Invoke(this, new ConsequenceEventArgs(result));

        return result;
    }

    /// <summary>
    /// Generate satirical news headlines based on the decision
    /// </summary>
    private List<string> GenerateHeadlines(DecisionCard card, CardChoice choice, Dictionary<ResourceType, int> changes)
    {
        var headlines = new List<string>();

        // Check for major resource changes
        foreach (var (resource, change) in changes)
        {
            if (Math.Abs(change) >= 15)
            {
                headlines.Add(GenerateResourceHeadline(resource, change));
            }
        }

        // Add card-specific headline if available
        if (card.Category == CardCategory.Crisis)
        {
            headlines.Add($"BREAKING: {card.Title} - {choice.Text}");
        }

        // Add satirical headline if nothing major happened
        if (headlines.Count == 0)
        {
            headlines.Add(GetRandomSatiricalHeadline());
        }

        return headlines;
    }

    private string GenerateResourceHeadline(ResourceType resource, int change)
    {
        return resource switch
        {
            ResourceType.Popularity when change > 0 => 
                "President's Approval Rating Surges After Bold Move",
            ResourceType.Popularity when change < 0 => 
                "Nationwide Protests Erupt Following Presidential Decision",
            
            ResourceType.Stability when change > 0 => 
                "Markets Rally as Government Shows Strength",
            ResourceType.Stability when change < 0 => 
                "Institutional Crisis: Confidence in Government Plummets",
            
            ResourceType.MediaTrust when change > 0 => 
                "Media Praises Presidential Transparency",
            ResourceType.MediaTrust when change < 0 => 
                "Press Declares 'Attack on Free Media' After Latest Decision",
            
            ResourceType.EconomicHealth when change > 0 => 
                "Economic Boom: Markets Hit All-Time High",
            ResourceType.EconomicHealth when change < 0 => 
                "Economic Catastrophe Looms as Markets Crash",
            
            _ => "Nation Reacts to Presidential Decision"
        };
    }

    private List<string> GenerateDynamicConsequences(CardChoice choice, Dictionary<ResourceType, int> changes)
    {
        var consequences = new List<string>();

        var totalImpact = changes.Values.Sum(Math.Abs);

        if (totalImpact > 30)
        {
            consequences.Add("Your decision sends shockwaves through the capital");
            consequences.Add("International community 'deeply concerned' about recent events");
        }
        else if (totalImpact > 15)
        {
            consequences.Add("Cabinet members exchange worried glances");
            consequences.Add("Social media explodes with reactions");
        }

        // Check for multiple negative impacts
        var negativeCount = changes.Values.Count(v => v < -10);
        if (negativeCount >= 2)
        {
            consequences.Add("Critics call for immediate resignation");
            consequences.Add("Emergency cabinet meeting scheduled");
        }

        return consequences;
    }

    private bool ShouldTriggerCascade(Dictionary<ResourceType, int> changes)
    {
        // Cascade if any single change is huge or crisis level reached
        return changes.Values.Any(v => Math.Abs(v) >= 20) || _resourceManager.GetCrisisLevel() >= CrisisLevel.Severe;
    }

    private CascadeEvent GenerateCascadeEvent()
    {
        var crisisLevel = _resourceManager.GetCrisisLevel();
        
        return new CascadeEvent
        {
            Title = "CASCADE EVENT TRIGGERED",
            Description = crisisLevel switch
            {
                CrisisLevel.Severe => "Your decisions have triggered a constitutional crisis!",
                CrisisLevel.Critical => "The government is on the brink of collapse!",
                CrisisLevel.GameOver => "GAME OVER: The administration has fallen.",
                _ => "Tensions are rising across the nation."
            },
            Severity = crisisLevel
        };
    }

    private string GetRandomSatiricalHeadline()
    {
        var headlines = new[]
        {
            "President's Latest Decision Leaves Nation Confused",
            "Experts Baffled by Presidential Strategy",
            "White House: 'Everything is Fine, Probably'",
            "'This is Fine' Says President as Chaos Unfolds",
            "Nation Collectively Shrugs at Latest Executive Action",
            "Presidential Decision Described as 'Certainly a Choice'",
            "Historians Will Have Questions About This Era",
            "President Continues to President, Nation Watches"
        };

        return headlines[Random.Shared.Next(headlines.Length)];
    }

    public IReadOnlyList<string> GetRecentHeadlines(int count = 5)
    {
        return _newsHeadlines.TakeLast(count).ToList();
    }
}

public class ConsequenceResult
{
    public string CardId { get; set; } = string.Empty;
    public int ChoiceId { get; set; }
    public string ChoiceText { get; set; } = string.Empty;
    public Dictionary<ResourceType, int> ResourceChanges { get; set; } = new();
    public List<string> Headlines { get; set; } = new();
    public List<string> Consequences { get; set; } = new();
    public List<string> FollowupCards { get; set; } = new();
    public bool CascadeTriggered { get; set; }
    public CascadeEvent? CascadeEvent { get; set; }
}

public class CascadeEvent
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CrisisLevel Severity { get; set; }
}

public class ConsequenceEventArgs : EventArgs
{
    public ConsequenceResult Result { get; }

    public ConsequenceEventArgs(ConsequenceResult result)
    {
        Result = result;
    }
}
