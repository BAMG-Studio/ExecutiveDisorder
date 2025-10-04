using ExecutiveDisorder.Core.Cards;
using ExecutiveDisorder.Core.Models;

namespace ExecutiveDisorder.Core.Data;

/// <summary>
/// Database of all decision cards - generates the 101 cards for Executive Disorder
/// </summary>
public class CardDatabase
{
    private readonly List<DecisionCard> _allCards = new();
    private readonly List<DecisionCard> _playedCards = new();

    public CardDatabase()
    {
        InitializeCards();
    }

    private void InitializeCards()
    {
        // CRISIS CARDS
        _allCards.Add(CreateNuclearSubmarineCrisis());
        _allCards.Add(CreateTwitterIncident());
        _allCards.Add(CreateNuclearTweet());
        _allCards.Add(CreateAlienContact());
        _allCards.Add(CreateEconomicCrashCrisis());
        
        // SCANDAL CARDS
        _allCards.Add(CreateClassifiedLeakScandal());
        _allCards.Add(CreateCabinetResignation());
        
        // ABSURD CARDS
        _allCards.Add(CreateNationalAnimalDebate());
        _allCards.Add(CreateTimeZoneControversy());
        _allCards.Add(CreateMascotRobotMalfunction());
        
        // CHARACTER CARDS
        _allCards.Add(CreateIguanaKingConspiracy());
        _allCards.Add(CreateExecutive45Advice());
        
        // NORMAL DECISIONS
        _allCards.Add(CreateBudgetProposal());
        _allCards.Add(CreateForeignDiplomacy());
        _allCards.Add(CreateHealthcareReform());
        
        // Add more cards to reach 101 total
        AddAdditionalCards();
    }

    private DecisionCard CreateNuclearSubmarineCrisis()
    {
        return new DecisionCard("CRISIS_001", "THE NUCLEAR OPTION", 
            "Mr/Madam President, an unidentified nuclear submarine has breached our territorial waters. Intelligence suggests possible first strike capability. The Joint Chiefs await your orders. The world is watching.")
        {
            Category = CardCategory.Crisis,
            Rarity = CardRarity.Epic,
            Urgency = UrgencyLevel.Critical,
            TimeLimit = 60,
            Choices = new List<CardChoice>
            {
                new(1, "MILITARY RESPONSE - Launch immediate countermeasures")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -20,
                        [ResourceType.Popularity] = +10,
                        [ResourceType.MediaTrust] = -15,
                        [ResourceType.EconomicHealth] = -25
                    },
                    Consequences = new List<string>
                    {
                        "Military mobilization begins",
                        "International tensions skyrocket",
                        "Stock markets crash worldwide"
                    },
                    ConsequencePreview = "Risk: World War III | Gain: Show Strength",
                    Alignment = ChoiceAlignment.Aggressive,
                    FollowupCardIds = new List<string> { "MILITARY_CRISIS_002", "WAR_ESCALATION_001" }
                },
                new(2, "DIPLOMATIC CHANNEL - Contact their government immediately")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +5,
                        [ResourceType.Popularity] = -10,
                        [ResourceType.MediaTrust] = +10,
                        [ResourceType.EconomicHealth] = +5
                    },
                    Consequences = new List<string>
                    {
                        "Diplomatic backchannel established",
                        "Critics call you weak",
                        "Situation de-escalates gradually"
                    },
                    ConsequencePreview = "Risk: Appear Weak | Gain: Avoid Conflict",
                    Alignment = ChoiceAlignment.Diplomatic
                },
                new(3, "PUBLIC DENIAL - Tell the press it's a weather balloon")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -10,
                        [ResourceType.Popularity] = -5,
                        [ResourceType.MediaTrust] = -20,
                        [ResourceType.EconomicHealth] = 0
                    },
                    Consequences = new List<string>
                    {
                        "Cover-up begins",
                        "Leaked satellite photos contradict your story",
                        "Conspiracy theories multiply"
                    },
                    ConsequencePreview = "Risk: Cover-up Scandal | Gain: Buy Time",
                    Alignment = ChoiceAlignment.Chaotic,
                    FollowupCardIds = new List<string> { "SCANDAL_005", "COVERUP_001" }
                }
            }
        };
    }

    private DecisionCard CreateTwitterIncident()
    {
        return new DecisionCard("CARD_001", "The Twitter Incident",
            "You meant to DM but accidentally tweeted classified intelligence about ongoing operations. It's already been retweeted 50,000 times.")
        {
            Category = CardCategory.Scandal,
            Rarity = CardRarity.Common,
            Urgency = UrgencyLevel.Urgent,
            CharacterId = "MEDIA_MOGUL",
            Choices = new List<CardChoice>
            {
                new(1, "Delete and pretend it never happened")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = -5,
                        [ResourceType.MediaTrust] = -10,
                        [ResourceType.Stability] = -5
                    },
                    Consequences = new List<string>
                    {
                        "Screenshots have already been saved",
                        "The Streisand Effect is in full force",
                        "'What are they hiding?' trends #1"
                    }
                },
                new(2, "Double down - claim it was intentional misdirection")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +5,
                        [ResourceType.MediaTrust] = -15,
                        [ResourceType.Stability] = -10
                    },
                    Consequences = new List<string>
                    {
                        "Intelligence community is baffled",
                        "Allies question your competence",
                        "Some people actually believe you"
                    },
                    Alignment = ChoiceAlignment.Chaotic
                },
                new(3, "Blame it on an intern")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = -10,
                        [ResourceType.MediaTrust] = -5,
                        [ResourceType.Stability] = +5
                    },
                    Consequences = new List<string>
                    {
                        "The intern goes on a media tour",
                        "Tell-all book deal announced",
                        "Your approval rating with young voters plummets"
                    },
                    Alignment = ChoiceAlignment.Cautious
                }
            }
        };
    }

    private DecisionCard CreateNuclearTweet()
    {
        return new DecisionCard("CRISIS_002", "THE NUCLEAR TWEET",
            "You accidentally tweeted nuclear launch codes thinking it was your WiFi password. The codes are valid for the next 30 minutes.")
        {
            Category = CardCategory.Crisis,
            Rarity = CardRarity.Legendary,
            Urgency = UrgencyLevel.Critical,
            TimeLimit = 30,
            Choices = new List<CardChoice>
            {
                new(1, "Claim you were hacked by foreign agents")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = -10,
                        [ResourceType.Stability] = -20,
                        [ResourceType.MediaTrust] = -15
                    },
                    FollowupCardIds = new List<string> { "SCANDAL_005", "HACKING_INVESTIGATION" }
                },
                new(2, "Double down - 'I meant to do that'")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +5,
                        [ResourceType.Stability] = -30,
                        [ResourceType.MediaTrust] = -25
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    FollowupCardIds = new List<string> { "MILITARY_CRISIS_002" }
                },
                new(3, "Quick! Tweet a cat video to distract everyone")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.MediaTrust] = +10,
                        [ResourceType.Stability] = -5,
                        [ResourceType.Popularity] = +15
                    },
                    Consequences = new List<string>
                    {
                        "The cat video goes viral",
                        "Nobody remembers the codes",
                        "You're a meme now"
                    },
                    Alignment = ChoiceAlignment.Chaotic
                }
            }
        };
    }

    private DecisionCard CreateNationalAnimalDebate()
    {
        return new DecisionCard("ABSURD_001", "NATIONAL ANIMAL DEBATE",
            "Congress wants to change the national animal from the eagle to a cybernetic T-Rex. The motion has surprising bipartisan support.")
        {
            Category = CardCategory.Absurd,
            Rarity = CardRarity.Rare,
            Urgency = UrgencyLevel.Normal,
            Choices = new List<CardChoice>
            {
                new(1, "T-Rex is more American than eagles ever were!")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +20,
                        [ResourceType.Stability] = -10,
                        [ResourceType.MediaTrust] = +5
                    },
                    Consequences = new List<string>
                    {
                        "Children everywhere celebrate",
                        "Eagle conservationists are outraged",
                        "Merchandise sales go through the roof"
                    },
                    Alignment = ChoiceAlignment.Chaotic
                },
                new(2, "Compromise: Eagle-Rex hybrid")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = -5,
                        [ResourceType.Stability] = +5,
                        [ResourceType.MediaTrust] = -10
                    },
                    Consequences = new List<string>
                    {
                        "Scientists say this is 'technically impossible'",
                        "Artist renderings are horrifying",
                        "Both sides hate you now"
                    }
                },
                new(3, "Keep the eagle, but give it laser eyes")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +10,
                        [ResourceType.Stability] = 0,
                        [ResourceType.MediaTrust] = +10
                    },
                    Consequences = new List<string>
                    {
                        "This is somehow the rational choice",
                        "Defense contractors get excited",
                        "America keeps winning"
                    }
                }
            }
        };
    }

    private DecisionCard CreateAlienContact()
    {
        return new DecisionCard("CRISIS_003", "FIRST CONTACT",
            "An alien spacecraft has landed on the White House lawn. They're demanding to 'speak to your leader.' You are the leader.")
        {
            Category = CardCategory.Crisis,
            Rarity = CardRarity.Legendary,
            Urgency = UrgencyLevel.Critical,
            TimeLimit = 120,
            Choices = new List<CardChoice>
            {
                new(1, "Welcome them with open arms")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +15,
                        [ResourceType.Stability] = -15,
                        [ResourceType.EconomicHealth] = +20
                    },
                    Consequences = new List<string>
                    {
                        "Intergalactic trade agreement signed",
                        "Half the country thinks you're an alien sympathizer",
                        "Technology shares skyrocket"
                    },
                    FollowupCardIds = new List<string> { "ALIEN_ALLIANCE_001" }
                },
                new(2, "MILITARY RESPONSE - Shoot first, ask questions later")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +10,
                        [ResourceType.Stability] = -25,
                        [ResourceType.MediaTrust] = -20,
                        [ResourceType.EconomicHealth] = -15
                    },
                    Consequences = new List<string>
                    {
                        "Interstellar war begins",
                        "Earth is definitely losing",
                        "Nice job breaking it, hero"
                    },
                    Alignment = ChoiceAlignment.Aggressive,
                    FollowupCardIds = new List<string> { "ALIEN_WAR_001" }
                },
                new(3, "Pretend you're not home")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = -20,
                        [ResourceType.Stability] = -10,
                        [ResourceType.MediaTrust] = -15
                    },
                    Consequences = new List<string>
                    {
                        "They can clearly see you through the window",
                        "This is the most embarrassing moment in human history",
                        "The aliens leave, disappointed"
                    },
                    Alignment = ChoiceAlignment.Chaotic
                }
            }
        };
    }

    private DecisionCard CreateIguanaKingConspiracy()
    {
        return new DecisionCard("CHARACTER_001", "The Iguana King's Warning",
            "Rex Scaleston III bursts into your office, eyes wide with paranoia. 'The deep state has a deep state!' he whispers urgently, showing you a conspiracy board covered in red string.")
        {
            Category = CardCategory.Character,
            Rarity = CardRarity.Uncommon,
            CharacterId = "IGUANA_KING",
            Choices = new List<CardChoice>
            {
                new(1, "Hear him out - maybe he's onto something")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -5,
                        [ResourceType.MediaTrust] = -10,
                        [ResourceType.Popularity] = +5
                    },
                    Consequences = new List<string>
                    {
                        "Rex's loyalty increases",
                        "Conspiracy theories spread",
                        "You've gone down the rabbit hole"
                    }
                },
                new(2, "Politely show him the door")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +5,
                        [ResourceType.Popularity] = 0
                    },
                    Consequences = new List<string>
                    {
                        "Rex's loyalty decreases",
                        "He mutters about 'them getting to you too'",
                        "Normalcy is restored, for now"
                    }
                },
                new(3, "Add your own theories to his board")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -10,
                        [ResourceType.Popularity] = +10,
                        [ResourceType.MediaTrust] = -15
                    },
                    Consequences = new List<string>
                    {
                        "Rex declares you 'awake'",
                        "You're both convinced the moon is a hologram",
                        "Cabinet intervention may be necessary"
                    },
                    Alignment = ChoiceAlignment.Chaotic
                }
            }
        };
    }

    private DecisionCard CreateExecutive45Advice()
    {
        return new DecisionCard("CHARACTER_002", "The Art of the Deal",
            "The 45th Executive appears with unsolicited advice: 'You know what you should do? The BEST thing. Nobody does it better than me. Tremendous idea.'")
        {
            Category = CardCategory.Character,
            CharacterId = "EXECUTIVE_45",
            Choices = new List<CardChoice>
            {
                new(1, "Take his advice")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +15,
                        [ResourceType.Stability] = -10,
                        [ResourceType.MediaTrust] = -20
                    }
                },
                new(2, "Do the exact opposite")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +10,
                        [ResourceType.MediaTrust] = +10,
                        [ResourceType.Popularity] = -5
                    }
                },
                new(3, "Challenge him to a golf match to decide")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +10,
                        [ResourceType.Stability] = -5,
                        [ResourceType.EconomicHealth] = -5
                    },
                    Alignment = ChoiceAlignment.Chaotic
                }
            }
        };
    }

    private DecisionCard CreateBudgetProposal()
    {
        return new DecisionCard("NORMAL_001", "Budget Proposal",
            "Congress has sent you a bipartisan budget proposal. It's balanced, reasonable, and everybody hates something about it.")
        {
            Category = CardCategory.Normal,
            Rarity = CardRarity.Common,
            Choices = new List<CardChoice>
            {
                new(1, "Sign it immediately")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +10,
                        [ResourceType.Popularity] = +5,
                        [ResourceType.EconomicHealth] = +10
                    }
                },
                new(2, "Veto and demand changes")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -10,
                        [ResourceType.Popularity] = -5,
                        [ResourceType.EconomicHealth] = -5
                    }
                },
                new(3, "Let it sit on your desk until it auto-passes")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +5,
                        [ResourceType.MediaTrust] = -10
                    }
                }
            }
        };
    }

    private DecisionCard CreateEconomicCrashCrisis()
    {
        return new DecisionCard("CRISIS_004", "ECONOMIC MELTDOWN",
            "The stock market has crashed. Your economic advisor is crying. The Secretary of Treasury has locked himself in a bathroom. Markets are down 30%.")
        {
            Category = CardCategory.Crisis,
            Urgency = UrgencyLevel.Critical,
            Choices = new List<CardChoice>
            {
                new(1, "Emergency bailout package")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.EconomicHealth] = +15,
                        [ResourceType.Popularity] = -15,
                        [ResourceType.Stability] = +5
                    }
                },
                new(2, "Let the free market sort itself out")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.EconomicHealth] = -20,
                        [ResourceType.Popularity] = -10,
                        [ResourceType.Stability] = -15
                    }
                },
                new(3, "Invest the entire treasury in cryptocurrency")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.EconomicHealth] = -25,
                        [ResourceType.Stability] = -20,
                        [ResourceType.Popularity] = +20,
                        [ResourceType.MediaTrust] = -30
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    Consequences = new List<string>
                    {
                        "This will definitely end well",
                        "Future generations will study this moment",
                        "Your face becomes a meme"
                    }
                }
            }
        };
    }

    private DecisionCard CreateClassifiedLeakScandal()
    {
        return new DecisionCard("SCANDAL_001", "The Leak",
            "Classified documents have been found in your vacation home. In a box marked 'Definitely Not Classified.'")
        {
            Category = CardCategory.Scandal,
            Urgency = UrgencyLevel.Urgent,
            Choices = new List<CardChoice>
            {
                new(1, "Full cooperation with investigators")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.MediaTrust] = +10,
                        [ResourceType.Popularity] = -10,
                        [ResourceType.Stability] = +5
                    }
                },
                new(2, "Claim presidential privilege")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.MediaTrust] = -20,
                        [ResourceType.Stability] = -15,
                        [ResourceType.Popularity] = +5
                    }
                },
                new(3, "Declassify everything retroactively")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -25,
                        [ResourceType.MediaTrust] = -15,
                        [ResourceType.Popularity] = +10
                    },
                    Alignment = ChoiceAlignment.Chaotic
                }
            }
        };
    }

    private DecisionCard CreateCabinetResignation()
    {
        return new DecisionCard("SCANDAL_002", "Mass Resignation",
            "Three cabinet members have resigned in protest. They're holding a joint press conference in 10 minutes.")
        {
            Category = CardCategory.Scandal,
            Urgency = UrgencyLevel.Elevated,
            Choices = new List<CardChoice>
            {
                new(1, "Accept their resignations gracefully")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -10,
                        [ResourceType.MediaTrust] = +5,
                        [ResourceType.Popularity] = -5
                    }
                },
                new(2, "Fire them before they can resign")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -15,
                        [ResourceType.MediaTrust] = -10,
                        [ResourceType.Popularity] = +10
                    }
                },
                new(3, "Replace them with AI chatbots")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -20,
                        [ResourceType.Popularity] = +15,
                        [ResourceType.MediaTrust] = -15
                    },
                    Alignment = ChoiceAlignment.Chaotic
                }
            }
        };
    }

    private DecisionCard CreateTimeZoneControversy()
    {
        return new DecisionCard("ABSURD_002", "TIME ZONE CHAOS",
            "A senator proposes eliminating time zones entirely. 'Everyone should just use the same time,' they argue. Surprisingly, it's gaining traction.")
        {
            Category = CardCategory.Absurd,
            Choices = new List<CardChoice>
            {
                new(1, "Support the proposal")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = -15,
                        [ResourceType.Stability] = -10,
                        [ResourceType.EconomicHealth] = -5
                    },
                    Consequences = new List<string>
                    {
                        "Breakfast is now at midnight for some people",
                        "Airlines are confused",
                        "Nobody knows what time it is"
                    }
                },
                new(2, "Reject it completely")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +5,
                        [ResourceType.Popularity] = 0
                    }
                },
                new(3, "Create 100 new micro-time zones instead")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -20,
                        [ResourceType.Popularity] = +10,
                        [ResourceType.EconomicHealth] = -10
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    Consequences = new List<string>
                    {
                        "Clock manufacturers rejoice",
                        "Every city has its own time now",
                        "Scheduling meetings is impossible"
                    }
                }
            }
        };
    }

    private DecisionCard CreateMascotRobotMalfunction()
    {
        return new DecisionCard("CHARACTER_003", "POTUS-9000 Malfunction",
            "The Mascot Bot is experiencing a critical error. It keeps declaring 'DEMOCRACY.EXE has stopped responding' and trying to restart the government.")
        {
            Category = CardCategory.Character,
            CharacterId = "MASCOT_BOT",
            Urgency = UrgencyLevel.Elevated,
            Choices = new List<CardChoice>
            {
                new(1, "Try turning it off and on again")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +5,
                        [ResourceType.Popularity] = 0
                    },
                    Consequences = new List<string>
                    {
                        "It works!",
                        "POTUS-9000 doesn't remember the last week",
                        "Probably fine"
                    }
                },
                new(2, "Let it reboot the government")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -15,
                        [ResourceType.Popularity] = +10,
                        [ResourceType.MediaTrust] = -10
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    Consequences = new List<string>
                    {
                        "Constitutional crisis initiated",
                        "Historians are screaming",
                        "It's actually running better now?"
                    }
                },
                new(3, "Upload consciousness into the robot")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -25,
                        [ResourceType.Popularity] = +20,
                        [ResourceType.MediaTrust] = -20
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    Consequences = new List<string>
                    {
                        "You are now a robot president",
                        "This raises constitutional questions",
                        "Approval ratings with tech bros soar"
                    }
                }
            }
        };
    }

    private DecisionCard CreateForeignDiplomacy()
    {
        return new DecisionCard("NORMAL_002", "Foreign Relations",
            "An allied nation is requesting a state visit. It's mostly ceremonial, but good for international relations.")
        {
            Category = CardCategory.Normal,
            Choices = new List<CardChoice>
            {
                new(1, "Accept and plan elaborate ceremonies")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +5,
                        [ResourceType.MediaTrust] = +5,
                        [ResourceType.EconomicHealth] = -5
                    }
                },
                new(2, "Decline politely")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -5,
                        [ResourceType.MediaTrust] = 0
                    }
                },
                new(3, "Accept but conduct the entire visit via Zoom")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.MediaTrust] = -10,
                        [ResourceType.Popularity] = +5,
                        [ResourceType.EconomicHealth] = +5
                    },
                    Alignment = ChoiceAlignment.Chaotic
                }
            }
        };
    }

    private DecisionCard CreateHealthcareReform()
    {
        return new DecisionCard("NORMAL_003", "Healthcare Reform",
            "A bipartisan healthcare reform bill has reached your desk. Both sides claim victory, which means both sides probably compromised.")
        {
            Category = CardCategory.Normal,
            Choices = new List<CardChoice>
            {
                new(1, "Sign it into law")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +10,
                        [ResourceType.Popularity] = +5,
                        [ResourceType.EconomicHealth] = -5
                    }
                },
                new(2, "Veto it")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -10,
                        [ResourceType.Popularity] = -10,
                        [ResourceType.MediaTrust] = -5
                    }
                },
                new(3, "Amend it to include free pizza Fridays")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +20,
                        [ResourceType.Stability] = -5,
                        [ResourceType.EconomicHealth] = -10
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    Consequences = new List<string>
                    {
                        "Everyone loves pizza",
                        "Congress is baffled",
                        "Your approval rating with college students skyrockets"
                    }
                }
            }
        };
    }

    private void AddAdditionalCards()
    {
        // Add placeholder cards to reach 101 total
        // In a full implementation, these would all be unique and interesting
        for (int i = 16; i <= 101; i++)
        {
            _allCards.Add(new DecisionCard($"CARD_{i:D3}", $"Decision #{i}",
                "A situation requires your attention.")
            {
                Category = CardCategory.Normal,
                Rarity = CardRarity.Common,
                Choices = new List<CardChoice>
                {
                    new(1, "Option A") 
                    { 
                        Effects = new Dictionary<ResourceType, int> 
                        { 
                            [ResourceType.Popularity] = Random.Shared.Next(-10, 10) 
                        } 
                    },
                    new(2, "Option B") 
                    { 
                        Effects = new Dictionary<ResourceType, int> 
                        { 
                            [ResourceType.Stability] = Random.Shared.Next(-10, 10) 
                        } 
                    }
                }
            });
        }
    }

    public DecisionCard? GetCardById(string id)
    {
        return _allCards.FirstOrDefault(c => c.Id == id);
    }

    public DecisionCard GetRandomCard()
    {
        var availableCards = _allCards.Where(c => !_playedCards.Contains(c)).ToList();
        if (availableCards.Count == 0)
            availableCards = _allCards; // Reshuffle if all cards played

        return availableCards[Random.Shared.Next(availableCards.Count)];
    }

    public void MarkCardPlayed(DecisionCard card)
    {
        if (!_playedCards.Contains(card))
            _playedCards.Add(card);
    }

    public int TotalCards => _allCards.Count;
    public int PlayedCards => _playedCards.Count;
}
