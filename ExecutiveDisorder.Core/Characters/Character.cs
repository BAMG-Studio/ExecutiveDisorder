namespace ExecutiveDisorder.Core.Characters;

/// <summary>
/// Represents one of the 8 political archetypes
/// </summary>
public class Character
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Biography { get; init; } = string.Empty;
    public CharacterArchetype Archetype { get; init; }
    
    // Personality
    public List<string> Traits { get; init; } = new();
    public List<string> Likes { get; init; } = new();
    public List<string> Dislikes { get; init; } = new();
    
    // Stats
    public int Loyalty { get; private set; }
    public int Competence { get; init; }
    public int ChaosFactor { get; init; }
    public string SpecialAbility { get; init; } = string.Empty;
    
    // Dialogue
    public Dictionary<DialogueType, List<string>> Dialogue { get; init; } = new();
    
    // Relationships
    public List<string> Allies { get; init; } = new();
    public List<string> Rivals { get; init; } = new();
    
    // Visual
    public string BaseSpriteId { get; init; } = string.Empty;
    public List<string> EmotionSprites { get; init; } = new();
    public List<string> Animations { get; init; } = new();

    public Character(string id, string name, CharacterArchetype archetype)
    {
        Id = id;
        Name = name;
        Archetype = archetype;
        Loyalty = 50; // Start neutral
    }

    /// <summary>
    /// Modify character loyalty (0-100)
    /// </summary>
    public void ModifyLoyalty(int amount)
    {
        Loyalty = Math.Clamp(Loyalty + amount, 0, 100);
    }

    /// <summary>
    /// Get a random dialogue line for a specific situation
    /// </summary>
    public string GetDialogue(DialogueType type)
    {
        if (!Dialogue.ContainsKey(type) || Dialogue[type].Count == 0)
            return $"{Name}: ...";

        var lines = Dialogue[type];
        return lines[Random.Shared.Next(lines.Count)];
    }

    public bool IsLoyal() => Loyalty >= 70;
    public bool IsHostile() => Loyalty <= 30;
    public bool IsNeutral() => Loyalty > 30 && Loyalty < 70;
}

public enum CharacterArchetype
{
    IguanaKing,        // Crisis Monarch (Rex Scaleston III)
    Executive45,       // Former Everything (Donald J. Executive)
    MascotBot,         // Chaotic Order (POTUS-9000)
    Progressive,       // Idealistic Reformer
    Corporate,         // Business Lobbyist
    Military,          // Hawkish General
    MediaMogul,        // Press Baron
    Populist           // People's Voice
}

public enum DialogueType
{
    Greeting,
    Happy,
    Angry,
    Crisis,
    Farewell,
    Agreement,
    Disagreement,
    Advice,
    Warning,
    Celebration
}
