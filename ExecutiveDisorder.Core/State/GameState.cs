using ExecutiveDisorder.Core.Models;
using ExecutiveDisorder.Core.Systems;

namespace ExecutiveDisorder.Core.State;

/// <summary>
/// Central game state manager
/// </summary>
public class GameState
{
    public int CurrentDay { get; private set; } = 1;
    public int TotalDecisions { get; private set; } = 0;
    public GamePhase CurrentPhase { get; private set; } = GamePhase.Introduction;
    public List<string> DecisionHistory { get; private set; } = new();
    public Dictionary<string, int> CharacterLoyalties { get; private set; } = new();
    public DateTime GameStartTime { get; private set; }
    public int ChaosScore { get; private set; } = 0;

    public ResourceManager Resources { get; private set; }

    public event EventHandler<DayChangedEventArgs>? DayChanged;
    public event EventHandler<PhaseChangedEventArgs>? PhaseChanged;

    public GameState(ResourceManager resourceManager)
    {
        Resources = resourceManager;
        GameStartTime = DateTime.Now;
    }

    public void AdvanceDay()
    {
        CurrentDay++;
        DayChanged?.Invoke(this, new DayChangedEventArgs(CurrentDay));

        // Check for phase transitions
        CheckPhaseTransition();
    }

    public void RecordDecision(string cardId, int choiceId)
    {
        TotalDecisions++;
        DecisionHistory.Add($"Day {CurrentDay}: Card {cardId}, Choice {choiceId}");
    }

    public void AddChaos(int amount)
    {
        ChaosScore += amount;
    }

    private void CheckPhaseTransition()
    {
        var newPhase = CurrentDay switch
        {
            <= 10 => GamePhase.EarlyGame,
            <= 30 => GamePhase.MidGame,
            <= 60 => GamePhase.LateGame,
            _ => GamePhase.Endgame
        };

        if (newPhase != CurrentPhase)
        {
            var oldPhase = CurrentPhase;
            CurrentPhase = newPhase;
            PhaseChanged?.Invoke(this, new PhaseChangedEventArgs(oldPhase, newPhase));
        }
    }

    public EndingType DetermineEnding()
    {
        var overallHealth = Resources.CalculateOverallHealth();
        var popularity = Resources.GetResource(ResourceType.Popularity).Value;
        var stability = Resources.GetResource(ResourceType.Stability).Value;
        var economic = Resources.GetResource(ResourceType.EconomicHealth).Value;

        // Check for special endings first
        if (DecisionHistory.Any(d => d.Contains("ALIEN_ALLIANCE")))
            return EndingType.AlienOverlords;

        if (DecisionHistory.Any(d => d.Contains("NUCLEAR")))
            return EndingType.NuclearWinter;

        if (ChaosScore >= 1000)
            return EndingType.TimeLoopParadox;

        // Standard endings based on resources
        if (stability >= 70 && popularity >= 60)
            return EndingType.DemocraticVictory;

        if (stability <= 20)
            return EndingType.AutocraticEmpire;

        if (economic <= 10)
            return EndingType.EconomicCollapse;

        if (overallHealth >= 70)
            return EndingType.PeacefulTransition;

        if (overallHealth <= 30)
            return EndingType.ImpeachmentEnding;

        return EndingType.MediocrePresident;
    }

    public GameStats GetStats()
    {
        return new GameStats
        {
            DaysInOffice = CurrentDay,
            TotalDecisions = TotalDecisions,
            ChaosGenerated = ChaosScore,
            FinalPopularity = Resources.GetResource(ResourceType.Popularity).Value,
            FinalStability = Resources.GetResource(ResourceType.Stability).Value,
            OverallScore = CalculateScore()
        };
    }

    private int CalculateScore()
    {
        var baseScore = CurrentDay * 100;
        var resourceBonus = Resources.CalculateOverallHealth() * 10;
        var chaosMultiplier = (int)(ChaosScore * 0.5);
        
        return baseScore + resourceBonus + chaosMultiplier;
    }
}

public enum GamePhase
{
    Introduction,
    EarlyGame,
    MidGame,
    LateGame,
    Endgame,
    GameOver
}

public enum EndingType
{
    DemocraticVictory,
    AutocraticEmpire,
    EconomicCollapse,
    NuclearWinter,
    AlienOverlords,
    ImpeachmentEnding,
    MilitaryCoup,
    MediaRevolution,
    ChaosReigns,
    TimeLoopParadox,
    PeacefulTransition,
    MediocrePresident
}

public class GameStats
{
    public int DaysInOffice { get; init; }
    public int TotalDecisions { get; init; }
    public int ChaosGenerated { get; init; }
    public int FinalPopularity { get; init; }
    public int FinalStability { get; init; }
    public int OverallScore { get; init; }
}

public class DayChangedEventArgs : EventArgs
{
    public int NewDay { get; }
    public DayChangedEventArgs(int newDay) => NewDay = newDay;
}

public class PhaseChangedEventArgs : EventArgs
{
    public GamePhase OldPhase { get; }
    public GamePhase NewPhase { get; }
    
    public PhaseChangedEventArgs(GamePhase oldPhase, GamePhase newPhase)
    {
        OldPhase = oldPhase;
        NewPhase = newPhase;
    }
}
