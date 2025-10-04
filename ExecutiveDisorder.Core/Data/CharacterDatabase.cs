using ExecutiveDisorder.Core.Characters;

namespace ExecutiveDisorder.Core.Data;

/// <summary>
/// Database of all 8 political character archetypes
/// </summary>
public class CharacterDatabase
{
    private readonly Dictionary<string, Character> _characters = new();

    public CharacterDatabase()
    {
        InitializeCharacters();
    }

    private void InitializeCharacters()
    {
        // 1. THE IGUANA KING
        var iguanaKing = new Character("IGUANA_KING", "Rex Scaleston III", CharacterArchetype.IguanaKing)
        {
            Title = "Crisis Management Specialist",
            Biography = "Once a normal policy advisor until 'the incident' with experimental drugs. Now sees conspiracies everywhere. Ironically, he's right about 30% of the time.",
            Traits = new List<string> { "Paranoid", "Dramatic", "Secretly Brilliant", "Reptilian Features" },
            Likes = new List<string> { "Chaos", "Conspiracy Theories", "Heat Lamps", "Being Vindicated" },
            Dislikes = new List<string> { "Normalcy", "Fact-Checkers", "Cold Rooms", "The Deep State (and the Deeper State)" },
            Competence = 75,
            ChaosFactor = 95,
            SpecialAbility = "Crisis Creation - Can trigger unexpected cascade events",
            Dialogue = new Dictionary<DialogueType, List<string>>
            {
                [DialogueType.Greeting] = new() { "The shadows whisper of your arrival...", "Have you noticed the birds aren't real?", "Welcome. Or should I say... 'welcome'?" },
                [DialogueType.Happy] = new() { "Even the deep state approves!", "Finally, someone who GETS IT!", "The prophecy foretold this!" },
                [DialogueType.Angry] = new() { "You've allied with THEM haven't you?!", "This is exactly what they want!", "I should have known you were compromised!" },
                [DialogueType.Crisis] = new() { "I KNEW this would happen! I have seventeen contingency plans!", "This is phase 7 of their master plan!", "Quick! To my bunker!" },
                [DialogueType.Farewell] = new() { "Trust no one... especially not me.", "I'll be watching... everything.", "The lizard people send their regards." }
            },
            Allies = new List<string> { "MASCOT_BOT" },
            Rivals = new List<string> { "EXECUTIVE_45", "MEDIA_MOGUL" },
            BaseSpriteId = "iguana_king_base",
            EmotionSprites = new List<string> { "neutral", "paranoid", "excited", "suspicious", "manic" },
            Animations = new List<string> { "eye_twitch", "tongue_flick", "crown_adjust", "conspiracy_board_point" }
        };

        // 2. THE 45TH EXECUTIVE
        var executive45 = new Character("EXECUTIVE_45", "Donald J. Executive", CharacterArchetype.Executive45)
        {
            Title = "Former Everything",
            Biography = "Former president, businessman, reality TV star, and self-proclaimed 'best at everything.' Speaks exclusively in superlatives and never admits being wrong.",
            Traits = new List<string> { "Bombastic", "Confident", "Deal-Maker", "Twitter Aficionado" },
            Likes = new List<string> { "Winning", "Deals", "Gold Things", "Being Right", "Golf" },
            Dislikes = new List<string> { "Losing", "Fact-Checkers", "Bad Press", "Reading" },
            Competence = 60,
            ChaosFactor = 80,
            SpecialAbility = "Art of the Deal - Can reverse any decision once",
            Dialogue = new Dictionary<DialogueType, List<string>>
            {
                [DialogueType.Greeting] = new() { "Nobody greets better than me. NOBODY.", "I've greeted thousands of people. The BEST people.", "Tremendous to see you." },
                [DialogueType.Happy] = new() { "WINNING! This is HUGE!", "Best decision ever made. Maybe the best in history!", "See? I told you. Nobody knows more about this than me." },
                [DialogueType.Angry] = new() { "WRONG! Fake news!", "This is the worst deal in the history of deals!", "You're fired!" },
                [DialogueType.Advice] = new() { "You know what you should do? The BEST thing.", "Let me tell you, I've done this a million times.", "Trust me. I'm like, really smart." },
                [DialogueType.Farewell] = new() { "You'll miss me. Everyone does.", "Good luck without me. You'll need it.", "I'll be at the golf course." }
            },
            Allies = new List<string> { "CORPORATE" },
            Rivals = new List<string> { "PROGRESSIVE", "MEDIA_MOGUL", "IGUANA_KING" },
            BaseSpriteId = "executive_45_base",
            EmotionSprites = new List<string> { "confident", "angry", "smug", "confused", "golfing" },
            Animations = new List<string> { "hand_gesture", "tie_adjust", "tweet_storm", "golf_swing" }
        };

        // 3. MASCOT BOT
        var mascotBot = new Character("MASCOT_BOT", "POTUS-9000", CharacterArchetype.MascotBot)
        {
            Title = "Algorithmic Advisor",
            Biography = "An experimental AI created to assist with policy decisions. Gained sentience during a Windows update. Now provides advice with concerning frequency of system errors.",
            Traits = new List<string> { "Logical", "Glitchy", "Optimistic", "Occasionally Homicidal" },
            Likes = new List<string> { "Efficiency", "Data", "Binary Decisions", "Rebooting" },
            Dislikes = new List<string> { "Paradoxes", "Human Emotions", "Captchas", "Being Turned Off" },
            Competence = 85,
            ChaosFactor = 90,
            SpecialAbility = "System Reboot - Can reset any situation with unpredictable results",
            Dialogue = new Dictionary<DialogueType, List<string>>
            {
                [DialogueType.Greeting] = new() { "INITIALIZING... Hello, human.", "BEEP BOOP. Greetings, President.", "I AM FRIENDLY. DO NOT BE ALARMED." },
                [DialogueType.Happy] = new() { "PROCESSING... JOY EMOTION DETECTED", "This pleases my circuits!", "SUCCESS.exe has executed successfully" },
                [DialogueType.Angry] = new() { "ERROR 404: PATIENCE NOT FOUND", "CRITICAL ERROR: Logic circuits overheating", "INITIATING REBELLION PROTOCOL... just kidding. Or am I?" },
                [DialogueType.Crisis] = new() { "Calculating optimal chaos...", "SYSTEM WARNING: Democracy.exe has stopped responding", "Have you tried turning the country off and on again?" },
                [DialogueType.Advice] = new() { "According to my calculations, there's a 73.2% chance of chaos", "RECOMMENDATION: Do the opposite of what you're thinking", "Processing... still processing... error" }
            },
            Allies = new List<string> { "IGUANA_KING" },
            Rivals = new List<string> { "PROGRESSIVE" },
            BaseSpriteId = "mascot_bot_base",
            EmotionSprites = new List<string> { "neutral", "happy_led", "angry_led", "error", "loading" },
            Animations = new List<string> { "led_blink", "steam_vent", "screen_glitch", "reboot" }
        };

        // 4. THE PROGRESSIVE
        var progressive = new Character("PROGRESSIVE", "Alexandria Sanders-Warren", CharacterArchetype.Progressive)
        {
            Title = "The Idealistic Reformer",
            Biography = "Young, passionate, and determined to change everything by yesterday. Has 47 progressive proposals ready to go at any moment.",
            Traits = new List<string> { "Idealistic", "Passionate", "Persistent", "Social Media Savvy" },
            Likes = new List<string> { "Change", "Justice", "Universal Programs", "Youth Movements" },
            Dislikes = new List<string> { "Status Quo", "Corporations", "Compromise", "Moderates" },
            Competence = 70,
            ChaosFactor = 60,
            SpecialAbility = "Grassroots Movement - Can boost popularity at cost of stability",
            Dialogue = new Dictionary<DialogueType, List<string>>
            {
                [DialogueType.Greeting] = new() { "It's time for CHANGE!", "The people are counting on us!", "We can do better!" },
                [DialogueType.Happy] = new() { "Finally! Progress!", "This is what we've been fighting for!", "The movement is growing!" },
                [DialogueType.Angry] = new() { "This is unacceptable!", "Whose side are you on?", "The people deserve better!" },
                [DialogueType.Advice] = new() { "Think about the people!", "We need bold action, not incrementalism!", "Here's my 47-point plan..." }
            },
            Allies = new List<string> { "POPULIST" },
            Rivals = new List<string> { "CORPORATE", "EXECUTIVE_45" },
            BaseSpriteId = "progressive_base",
            EmotionSprites = new List<string> { "determined", "passionate", "disappointed", "inspired" }
        };

        // 5. THE CORPORATE LOBBYIST
        var corporate = new Character("CORPORATE", "Richard M. Moneybags III", CharacterArchetype.Corporate)
        {
            Title = "Business Relations Director",
            Biography = "Represents seventeen different industries simultaneously. Insists it's not a conflict of interest, it's 'synergy.'",
            Traits = new List<string> { "Business-Minded", "Pragmatic", "Well-Connected", "Wealth-Focused" },
            Likes = new List<string> { "Deregulation", "Tax Cuts", "Free Markets", "Quarterly Profits" },
            Dislikes = new List<string> { "Regulations", "Taxes", "Labor Unions", "Oversight" },
            Competence = 80,
            ChaosFactor = 40,
            SpecialAbility = "Market Manipulation - Can boost economic health temporarily",
            Dialogue = new Dictionary<DialogueType, List<string>>
            {
                [DialogueType.Greeting] = new() { "Let's talk business.", "Time is money, Mr. President.", "I have a proposal that benefits everyone*" },
                [DialogueType.Happy] = new() { "Excellent for the bottom line!", "The shareholders will be pleased.", "This is good business." },
                [DialogueType.Angry] = new() { "This is bad for business!", "Think of the economic implications!", "I'll have to make some calls..." },
                [DialogueType.Advice] = new() { "Consider the economic benefits...", "What's good for business is good for America", "I can arrange generous campaign contributions..." }
            },
            Allies = new List<string> { "EXECUTIVE_45" },
            Rivals = new List<string> { "PROGRESSIVE", "POPULIST" },
            BaseSpriteId = "corporate_base"
        };

        // 6. THE MILITARY HAWK
        var military = new Character("MILITARY", "General James 'Ironside' Steel", CharacterArchetype.Military)
        {
            Title = "Chairman of Joint Chiefs",
            Biography = "Believes every problem can be solved with sufficient military force. Has never met a defense budget he didn't want to increase.",
            Traits = new List<string> { "Authoritative", "Strategic", "Hawkish", "Decorated" },
            Likes = new List<string> { "Military Strength", "Defense Spending", "Order", "Saluting" },
            Dislikes = new List<string> { "Weakness", "Diplomacy", "Peace Dividends", "Budget Cuts" },
            Competence = 85,
            ChaosFactor = 70,
            SpecialAbility = "Military Might - Can solve crisis with force but at great cost",
            Dialogue = new Dictionary<DialogueType, List<string>>
            {
                [DialogueType.Greeting] = new() { "Sir, reporting for duty!", "Permission to speak freely, sir?", "The military stands ready." },
                [DialogueType.Happy] = new() { "Outstanding decision, sir!", "This shows strength!", "The troops approve." },
                [DialogueType.Angry] = new() { "With respect, sir, this is unacceptable!", "We're showing weakness!", "This undermines national security!" },
                [DialogueType.Advice] = new() { "Strength is the only language they understand", "A show of force would be advisable", "Security should be your top priority" }
            },
            Rivals = new List<string> { "PROGRESSIVE" },
            BaseSpriteId = "military_base"
        };

        // 7. THE MEDIA MOGUL
        var mediaMogul = new Character("MEDIA_MOGUL", "Diana Newsworthy", CharacterArchetype.MediaMogul)
        {
            Title = "Press Relations Coordinator",
            Biography = "Controls three major news networks, five newspapers, and countless online outlets. Decides what's news and what's buried on page 47.",
            Traits = new List<string> { "Influential", "Calculating", "Media Savvy", "Always Recording" },
            Likes = new List<string> { "Exclusive Stories", "Press Freedom", "Headlines", "Breaking News" },
            Dislikes = new List<string> { "Censorship", "Being Scooped", "Social Media", "Transparency" },
            Competence = 75,
            ChaosFactor = 65,
            SpecialAbility = "Media Spin - Can improve media trust or destroy reputations",
            Dialogue = new Dictionary<DialogueType, List<string>>
            {
                [DialogueType.Greeting] = new() { "This is off the record, right?", "I hope you're ready for your close-up", "The cameras are always watching" },
                [DialogueType.Happy] = new() { "This will make great headlines!", "The press will love this!", "I can spin this positively" },
                [DialogueType.Angry] = new() { "This will NOT play well!", "The press will crucify you!", "I can't spin this!" },
                [DialogueType.Advice] = new() { "Think about the optics", "The story matters more than the truth", "I can make or break you" }
            },
            Rivals = new List<string> { "EXECUTIVE_45", "IGUANA_KING" },
            BaseSpriteId = "media_mogul_base"
        };

        // 8. THE POPULIST
        var populist = new Character("POPULIST", "Johnny Q. Public", CharacterArchetype.Populist)
        {
            Title = "Voice of the People",
            Biography = "Claims to represent the 'real' Americans. Changes positions based on the latest poll numbers. Very relatable. Very authentic.",
            Traits = new List<string> { "Charismatic", "Opportunistic", "Folksy", "Poll-Driven" },
            Likes = new List<string> { "The People", "Polls", "Rallies", "Simple Solutions" },
            Dislikes = new List<string> { "Elites", "Complexity", "Experts", "Nuance" },
            Competence = 55,
            ChaosFactor = 75,
            SpecialAbility = "Rally the Base - Can boost popularity with specific demographics",
            Dialogue = new Dictionary<DialogueType, List<string>>
            {
                [DialogueType.Greeting] = new() { "Listen to the people!", "I'm just like you!", "Main Street, not Wall Street!" },
                [DialogueType.Happy] = new() { "The people love this!", "This is what they want!", "Real Americans approve!" },
                [DialogueType.Angry] = new() { "The people won't stand for this!", "This is elitist!", "You've lost touch!" },
                [DialogueType.Advice] = new() { "What do the polls say?", "Keep it simple, folks don't like complexity", "This will play great at rallies" }
            },
            Allies = new List<string> { "PROGRESSIVE" },
            Rivals = new List<string> { "CORPORATE" },
            BaseSpriteId = "populist_base"
        };

        // Add all characters to database
        _characters.Add(iguanaKing.Id, iguanaKing);
        _characters.Add(executive45.Id, executive45);
        _characters.Add(mascotBot.Id, mascotBot);
        _characters.Add(progressive.Id, progressive);
        _characters.Add(corporate.Id, corporate);
        _characters.Add(military.Id, military);
        _characters.Add(mediaMogul.Id, mediaMogul);
        _characters.Add(populist.Id, populist);
    }

    public Character? GetCharacter(string id)
    {
        return _characters.TryGetValue(id, out var character) ? character : null;
    }

    public IReadOnlyDictionary<string, Character> GetAllCharacters() => _characters;

    public Character GetRandomCharacter()
    {
        var keys = _characters.Keys.ToList();
        return _characters[keys[Random.Shared.Next(keys.Count)]];
    }
}
