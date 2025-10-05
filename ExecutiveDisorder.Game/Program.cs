using System;
using ExecutiveDisorder.Core.Cards;
using ExecutiveDisorder.Core.Characters;
using ExecutiveDisorder.Core.Data;
using ExecutiveDisorder.Core.Models;
using ExecutiveDisorder.Core.State;
using ExecutiveDisorder.Core.Systems;

namespace ExecutiveDisorder.Game;

class Program
{
    private static ResourceManager? _resourceManager;
    private static ConsequenceEngine? _consequenceEngine;
    private static CardDatabase? _cardDatabase;
    private static CharacterDatabase? _characterDatabase;
    private static GameState? _gameState;
    private static bool _isRunning = true;

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.SetWindowSize(Math.Min(120, Console.LargestWindowWidth), Math.Min(40, Console.LargestWindowHeight));

        ShowTitleScreen();
        InitializeGame();
        ShowCrisisOpening();
        GameLoop();
        ShowEnding();
    }

    static void ShowTitleScreen()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        
        string[] title = {
            "╔═══════════════════════════════════════════════════════════════════╗",
            "║                                                                   ║",
            "║              ███████╗██╗  ██╗███████╗ ██████╗██╗   ██╗          ║",
            "║              ██╔════╝╚██╗██╔╝██╔════╝██╔════╝██║   ██║          ║",
            "║              █████╗   ╚███╔╝ █████╗  ██║     ██║   ██║          ║",
            "║              ██╔══╝   ██╔██╗ ██╔══╝  ██║     ██║   ██║          ║",
            "║              ███████╗██╔╝ ██╗███████╗╚██████╗╚██████╔╝          ║",
            "║              ╚══════╝╚═╝  ╚═╝╚══════╝ ╚═════╝ ╚═════╝           ║",
            "║                                                                   ║",
            "║            ██████╗ ██╗███████╗ ██████╗ ██████╗ ██████╗ ███████╗ ║",
            "║            ██╔══██╗██║██╔════╝██╔═══██╗██╔══██╗██╔══██╗██╔════╝ ║",
            "║            ██║  ██║██║███████╗██║   ██║██████╔╝██║  ██║█████╗   ║",
            "║            ██║  ██║██║╚════██║██║   ██║██╔══██╗██║  ██║██╔══╝   ║",
            "║            ██████╔╝██║███████║╚██████╔╝██║  ██║██████╔╝███████╗ ║",
            "║            ╚═════╝ ╚═╝╚══════╝ ╚═════╝ ╚═╝  ╚═╝╚═════╝ ╚══════╝ ║",
            "║                                                                   ║",
            "╚═══════════════════════════════════════════════════════════════════╝"
        };

        foreach (var line in title)
        {
            Console.WriteLine(line);
            Thread.Sleep(100);
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n                    Lead by Absolute Chaos. Rule with Memes.");
        Console.ResetColor();
        Console.WriteLine("\n                         Press any key to ASSUME POWER...");
        Console.ReadKey(true);
    }

    static void InitializeGame()
    {
        _resourceManager = new ResourceManager();
        _consequenceEngine = new ConsequenceEngine(_resourceManager);
        _cardDatabase = new CardDatabase();
        _characterDatabase = new CharacterDatabase();
        _gameState = new GameState(_resourceManager);

        // Subscribe to events
        _resourceManager.ResourceChanged += OnResourceChanged;
        _resourceManager.CrisisTriggered += OnCrisisTriggered;
        _resourceManager.GameOver += OnGameOver;
        _consequenceEngine.ConsequenceGenerated += OnConsequenceGenerated;
    }

    static void ShowCrisisOpening()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n╔══════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                     ⚠️  CRISIS ALERT ⚠️                              ║");
        Console.WriteLine("║                      DEFCON 2 - IMMEDIATE THREAT                     ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();

        TypewriterEffect("Mr./Madam President, you need to see this immediately...\n\n");
        Thread.Sleep(1000);
        TypewriterEffect("An unidentified nuclear submarine has breached our territorial waters.\n");
        Thread.Sleep(500);
        TypewriterEffect("Intelligence suggests possible first strike capability.\n");
        Thread.Sleep(500);
        TypewriterEffect("The Joint Chiefs await your orders.\n\n");
        Thread.Sleep(1000);
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        TypewriterEffect("The world is watching.\n\n");
        Console.ResetColor();

        Console.WriteLine("Press any key to begin your first decision...");
        Console.ReadKey(true);
    }

    static void GameLoop()
    {
        while (_isRunning && _gameState!.CurrentDay <= 100)
        {
            Console.Clear();
            DisplayGameHeader();
            DisplayResources();
            
            // Get next decision card
            var card = _cardDatabase!.GetRandomCard();
            DisplayCard(card);
            
            // Get player choice
            int choice = GetPlayerChoice(card.Choices.Count);
            
            // Process choice
            var selectedChoice = card.Choices[choice - 1];
            var result = _consequenceEngine!.ProcessChoice(card, selectedChoice);
            _gameState.RecordDecision(card.Id, choice);
            _cardDatabase.MarkCardPlayed(card);
            
            // Show consequences
            ShowConsequences(result);
            
            // Advance day
            _gameState.AdvanceDay();
            
            // Check for game over conditions
            if (_resourceManager!.GetCrisisLevel() == CrisisLevel.GameOver)
            {
                _isRunning = false;
            }

            if (_gameState.CurrentDay > 100)
            {
                _isRunning = false;
            }
        }
    }

    static void DisplayGameHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"╔════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  EXECUTIVE DISORDER - Day {_gameState!.CurrentDay,3} | Phase: {_gameState.CurrentPhase,-12} | Decisions: {_gameState.TotalDecisions,3}  ║");
        Console.WriteLine($"╚════════════════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
    }

    static void DisplayResources()
    {
        Console.WriteLine();
        var resources = _resourceManager!.GetAllResources();
        
        foreach (var (type, resource) in resources)
        {
            DisplayResourceBar(resource);
        }
        
        Console.WriteLine();
        
        // Show crisis level
        var crisisLevel = _resourceManager.GetCrisisLevel();
        if (crisisLevel > CrisisLevel.Normal)
        {
            Console.ForegroundColor = crisisLevel switch
            {
                CrisisLevel.Warning => ConsoleColor.Yellow,
                CrisisLevel.Severe => ConsoleColor.DarkYellow,
                CrisisLevel.Critical => ConsoleColor.Red,
                _ => ConsoleColor.White
            };
            Console.WriteLine($"⚠️  CRISIS LEVEL: {crisisLevel} ⚠️");
            Console.ResetColor();
            Console.WriteLine();
        }
    }

    static void DisplayResourceBar(Resource resource)
    {
        Console.Write($"{resource.Icon} {resource.Name,-16}: [");
        
        int barLength = 30;
        int filled = (int)((resource.Value / 100.0) * barLength);
        
        // Color based on value
        if (resource.Value >= 70)
            Console.ForegroundColor = ConsoleColor.Green;
        else if (resource.Value >= 40)
            Console.ForegroundColor = ConsoleColor.Yellow;
        else if (resource.Value >= 20)
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        else
            Console.ForegroundColor = ConsoleColor.Red;

        Console.Write(new string('█', filled));
        Console.ResetColor();
        Console.Write(new string('░', barLength - filled));
        Console.Write($"] {resource.Value,3}% ");
        
        // Trend indicator
        string trend = resource.Trend switch
        {
            ResourceTrend.Increasing => "↑",
            ResourceTrend.Decreasing => "↓",
            _ => "→"
        };
        Console.WriteLine(trend);
    }

    static void DisplayCard(DecisionCard card)
    {
        Console.ForegroundColor = card.Urgency switch
        {
            UrgencyLevel.Critical => ConsoleColor.Red,
            UrgencyLevel.Urgent => ConsoleColor.DarkYellow,
            UrgencyLevel.Elevated => ConsoleColor.Yellow,
            _ => ConsoleColor.White
        };

        Console.WriteLine("═══════════════════════════════════════════════════════════════════════════");
        Console.WriteLine($"  {card.Title.ToUpper()}");
        if (card.Category != CardCategory.Normal)
        {
            Console.WriteLine($"  [{card.Category}] {(card.TimeLimit.HasValue ? $"⏰ {card.TimeLimit}s" : "")}");
        }
        Console.WriteLine("═══════════════════════════════════════════════════════════════════════════");
        Console.ResetColor();
        
        Console.WriteLine();
        WrapText(card.Description, 75);
        Console.WriteLine();

        // Show character if present
        if (!string.IsNullOrEmpty(card.CharacterId))
        {
            var character = _characterDatabase!.GetCharacter(card.CharacterId);
            if (character != null)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"[{character.Name} - {character.Title}]");
                Console.WriteLine($"\"{character.GetDialogue(DialogueType.Advice)}\"");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        Console.WriteLine("YOUR OPTIONS:");
        Console.WriteLine();

        for (int i = 0; i < card.Choices.Count; i++)
        {
            var choice = card.Choices[i];
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"  [{i + 1}] ");
            Console.ResetColor();
            Console.WriteLine(choice.Text);
            
            if (!string.IsNullOrEmpty(choice.ConsequencePreview))
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"      {choice.ConsequencePreview}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    static int GetPlayerChoice(int maxChoices)
    {
        while (true)
        {
            Console.Write($"Enter your decision (1-{maxChoices}): ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= maxChoices)
            {
                return choice;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid choice. Try again.");
            Console.ResetColor();
        }
    }

    static void ShowConsequences(ConsequenceResult result)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n═══════════════════════════════════════════════════════════════════════════");
        Console.WriteLine("                            CONSEQUENCES");
        Console.WriteLine("═══════════════════════════════════════════════════════════════════════════\n");
        Console.ResetColor();

        TypewriterEffect($"You chose: {result.ChoiceText}\n\n", 30);
        Thread.Sleep(500);

        // Show resource changes
        Console.WriteLine("IMMEDIATE EFFECTS:");
        foreach (var (type, change) in result.ResourceChanges)
        {
            if (change != 0)
            {
                Console.ForegroundColor = change > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                string arrow = change > 0 ? "↑" : "↓";
                Console.WriteLine($"  {arrow} {type}: {(change > 0 ? "+" : "")}{change}");
            }
        }
        Console.ResetColor();
        Console.WriteLine();
        Thread.Sleep(1000);

        // Show headlines
        if (result.Headlines.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📰 BREAKING NEWS:");
            Console.ResetColor();
            foreach (var headline in result.Headlines)
            {
                TypewriterEffect($"  • {headline}\n", 40);
                Thread.Sleep(800);
            }
            Console.WriteLine();
        }

        // Show consequences
        if (result.Consequences.Count > 0)
        {
            Console.WriteLine("WHAT HAPPENS NEXT:");
            foreach (var consequence in result.Consequences)
            {
                TypewriterEffect($"  → {consequence}\n", 40);
                Thread.Sleep(600);
            }
            Console.WriteLine();
        }

        // Show cascade if triggered
        if (result.CascadeTriggered && result.CascadeEvent != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n⚠️  CASCADE EVENT TRIGGERED! ⚠️");
            Console.WriteLine($"  {result.CascadeEvent.Title}");
            Console.WriteLine($"  {result.CascadeEvent.Description}");
            Console.ResetColor();
            Console.WriteLine();
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey(true);
    }

    static void ShowEnding()
    {
        Console.Clear();
        var ending = _gameState!.DetermineEnding();
        var stats = _gameState.GetStats();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n╔══════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                          GAME OVER                                   ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"                       ENDING: {ending}");
        Console.ResetColor();
        Console.WriteLine();

        string endingDescription = ending switch
        {
            EndingType.DemocraticVictory => "Against all odds, democracy survived. You successfully navigated the chaos and maintained institutional integrity. Historians will note this era with cautious optimism.",
            EndingType.AutocraticEmpire => "Democracy has fallen. You've consolidated absolute power. Long live the Emperor. (This is not what the founding fathers had in mind)",
            EndingType.EconomicCollapse => "The economy has completely collapsed. Future generations will study this in economics textbooks under 'What Not To Do.'",
            EndingType.NuclearWinter => "The nuclear option was exercised. The good news: you won. The bad news: there's nothing left to win.",
            EndingType.AlienOverlords => "Earth is now part of the Intergalactic Federation. Congratulations on being humanity's representative!",
            EndingType.MediocrePresident => "You were... fine. History will remember you as 'that one president.' Could have been worse!",
            _ => "Your presidency has ended. The nation continues, somehow."
        };

        WrapText(endingDescription, 70);
        Console.WriteLine();
        Console.WriteLine("\n═══════════════════════════════════════════════════════════════════════════");
        Console.WriteLine("                              YOUR LEGACY");
        Console.WriteLine("═══════════════════════════════════════════════════════════════════════════\n");

        Console.WriteLine($"  Days in Office:      {stats.DaysInOffice}");
        Console.WriteLine($"  Total Decisions:     {stats.TotalDecisions}");
        Console.WriteLine($"  Chaos Generated:     {stats.ChaosGenerated}");
        Console.WriteLine($"  Final Popularity:    {stats.FinalPopularity}%");
        Console.WriteLine($"  Final Stability:     {stats.FinalStability}%");
        Console.WriteLine($"  Overall Score:       {stats.OverallScore:N0}");
        Console.WriteLine();

        Console.WriteLine("\n═══════════════════════════════════════════════════════════════════════════");
        Console.WriteLine("         Thank you for playing EXECUTIVE DISORDER");
        Console.WriteLine("           Democracy: Optional. Chaos: Guaranteed.");
        Console.WriteLine("═══════════════════════════════════════════════════════════════════════════\n");

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey(true);
    }

    static void TypewriterEffect(string text, int delayMs = 20)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delayMs);
        }
    }

    static void WrapText(string text, int maxWidth)
    {
        var words = text.Split(' ');
        int currentLineLength = 0;

        foreach (var word in words)
        {
            if (currentLineLength + word.Length + 1 > maxWidth)
            {
                Console.WriteLine();
                currentLineLength = 0;
            }

            if (currentLineLength > 0)
            {
                Console.Write(" ");
                currentLineLength++;
            }

            Console.Write(word);
            currentLineLength += word.Length;
        }
        Console.WriteLine();
    }

    // Event handlers
    static void OnResourceChanged(object? sender, ResourceChangedEventArgs e)
    {
        // Could add sound effects or additional feedback here
    }

    static void OnCrisisTriggered(object? sender, CrisisEventArgs e)
    {
        _gameState?.AddChaos(25);
    }

    static void OnGameOver(object? sender, EventArgs e)
    {
        _isRunning = false;
    }

    static void OnConsequenceGenerated(object? sender, ConsequenceEventArgs e)
    {
        // Could add analytics tracking here
    }
}
