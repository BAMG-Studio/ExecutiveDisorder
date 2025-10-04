using ExecutiveDisorder.Core.Models;

namespace ExecutiveDisorder.Core.Cards;

/// <summary>
/// Represents a single decision card in the game
/// </summary>
public class DecisionCard
{
    public string Id { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public CardCategory Category { get; init; }
    public CardRarity Rarity { get; init; }
    public UrgencyLevel Urgency { get; init; }
    public int? TimeLimit { get; init; } // Seconds for crisis cards
    
    public List<CardChoice> Choices { get; init; } = new();
    public CardRequirements? Requirements { get; init; }
    
    public string? CharacterId { get; init; } // Which character presents this
    public string? ImageId { get; init; }
    public string? SoundId { get; init; }

    public DecisionCard(string id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }
}

/// <summary>
/// A choice the player can make on a decision card
/// </summary>
public class CardChoice
{
    public int ChoiceId { get; init; }
    public string Text { get; init; } = string.Empty;
    public Dictionary<ResourceType, int> Effects { get; init; } = new();
    public List<string> Consequences { get; init; } = new();
    public List<string> FollowupCardIds { get; init; } = new();
    public string? ConsequencePreview { get; init; }
    public ChoiceAlignment Alignment { get; init; }

    public CardChoice(int id, string text)
    {
        ChoiceId = id;
        Text = text;
    }
}

/// <summary>
/// Requirements for a card to appear
/// </summary>
public class CardRequirements
{
    public int? MinDay { get; init; }
    public int? MaxDay { get; init; }
    public Dictionary<ResourceType, int>? MinResourceValues { get; init; }
    public Dictionary<ResourceType, int>? MaxResourceValues { get; init; }
    public List<string>? RequiredPreviousCards { get; init; }
    public List<string>? BlockedByCards { get; init; }
    public string? RequiredCharacterPresent { get; init; }
}

public enum CardCategory
{
    Normal,
    Crisis,
    Scandal,
    Opportunity,
    Character,
    Absurd,
    EndGame
}

public enum CardRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public enum UrgencyLevel
{
    Normal,
    Elevated,
    Urgent,
    Critical
}

public enum ChoiceAlignment
{
    Neutral,
    Cautious,
    Aggressive,
    Chaotic,
    Diplomatic,
    Authoritarian
}
