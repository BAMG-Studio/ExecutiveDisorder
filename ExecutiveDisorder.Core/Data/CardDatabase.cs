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
        return new DecisionCard("EDX_001", "THE NUCLEAR OPTION", 
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
        return new DecisionCard("EDX_017", "The Twitter Incident",
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
        return new DecisionCard("EDX_018", "THE NUCLEAR TWEET",
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
        return new DecisionCard("EDX_019", "NATIONAL ANIMAL DEBATE",
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
        return new DecisionCard("EDX_020", "FIRST CONTACT",
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
        return new DecisionCard("EDX_021", "The Iguana King's Warning",
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
        return new DecisionCard("EDX_022", "The Art of the Deal",
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
        return new DecisionCard("EDX_023", "Budget Proposal",
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
        return new DecisionCard("EDX_024", "ECONOMIC MELTDOWN",
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
        return new DecisionCard("EDX_025", "The Leak",
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
        return new DecisionCard("EDX_026", "Mass Resignation",
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
        return new DecisionCard("EDX_027", "TIME ZONE CHAOS",
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
        return new DecisionCard("EDX_028", "POTUS-9000 Malfunction",
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
        return new DecisionCard("EDX_029", "Foreign Relations",
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
        return new DecisionCard("EDX_030", "Healthcare Reform",
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
        _allCards.Add(CreateCyberAttack());
        _allCards.Add(CreatePandemic());
        _allCards.Add(CreateSpaceDebris());
        _allCards.Add(CreateVolcanicEruption());
        _allCards.Add(CreateTerroristThreat());
        _allCards.Add(CreateNaturalDisaster());
        _allCards.Add(CreateEnergyBlackout());
        _allCards.Add(CreateWaterCrisis());
        _allCards.Add(CreateFoodShortage());
        _allCards.Add(CreateCyberWarfare());
        _allCards.Add(CreateMascotRobotRecall());
        _allCards.Add(CreateNationalSauceShortage());
        
        _allCards.Add(CreateBriberyScandal());
        _allCards.Add(CreateSexScandal());
        _allCards.Add(CreateTaxEvasion());
        _allCards.Add(CreateNepotismAccusation());
        _allCards.Add(CreateEmbezzlement());
        _allCards.Add(CreatePlagiarismScandal());
        _allCards.Add(CreateDrunkDialing());
        _allCards.Add(CreateHotMicIncident());
        _allCards.Add(CreateDeepfakeVideo());
        _allCards.Add(CreateWhistleblower());
        _allCards.Add(CreateCabinetKaraokeLeaks());
        _allCards.Add(CreateAIBudgetRhymes());
        _allCards.Add(CreateWeatherBalloonPresser());
        _allCards.Add(CreateAIPatriotTest());
        _allCards.Add(CreateExecutiveHotTub());
        
        _allCards.Add(CreatePizzaGate());
        _allCards.Add(CreateNationalDance());
        _allCards.Add(CreateMemeWar());
        _allCards.Add(CreateFlatEarthMovement());
        _allCards.Add(CreateTimeTravel());
        _allCards.Add(CreateCloneArmy());
        _allCards.Add(CreateGhostWhiteHouse());
        _allCards.Add(CreateTalkingStatue());
        _allCards.Add(CreatePortalBasement());
        _allCards.Add(CreateAIPresident());
        _allCards.Add(CreateNationalBirdDrone());
        _allCards.Add(CreateTimeLottery());
        _allCards.Add(CreateNationalMemeCouncil());
        
        _allCards.Add(CreateSpinDoctor());
        _allCards.Add(CreateGeneralWarmonger());
        _allCards.Add(CreateTechBillionaire());
        _allCards.Add(CreateFirstSpouse());
        _allCards.Add(CreateVicePresident());
        _allCards.Add(CreateChiefOfStaff());
        _allCards.Add(CreatePressSecretary());
        _allCards.Add(CreateSecretService());
        _allCards.Add(CreateWhiteHouseChef());
        _allCards.Add(CreatePresidentialDog());
        
        _allCards.Add(CreateEducationReform());
        _allCards.Add(CreateInfrastructureBill());
        _allCards.Add(CreateTaxReform());
        _allCards.Add(CreateImmigrationPolicy());
        _allCards.Add(CreateClimateAction());
        _allCards.Add(CreateMilitaryBudget());
        _allCards.Add(CreateTradeAgreement());
        _allCards.Add(CreateJudicialNomination());
        _allCards.Add(CreateVeteranAffairs());
        _allCards.Add(CreateSpaceProgram());
        _allCards.Add(CreateDrugPolicy());
        _allCards.Add(CreateGunControl());
        _allCards.Add(CreateSocialSecurity());
        _allCards.Add(CreateMinimumWage());
        _allCards.Add(CreateNetNeutrality());
        _allCards.Add(CreatePrivacyLaws());
        _allCards.Add(CreateAntitrust());
        _allCards.Add(CreateFarmSubsidies());
        _allCards.Add(CreateHousingCrisis());
        _allCards.Add(CreateStudentDebt());
        _allCards.Add(CreatePoliceReform());
        _allCards.Add(CreateElectionSecurity());
        _allCards.Add(CreateCampaignFinance());
        _allCards.Add(CreateTermLimits());
        _allCards.Add(CreateSupremeCourtExpansion());
        _allCards.Add(CreateStatehoodDebate());
        _allCards.Add(CreateNationalHoliday());
        _allCards.Add(CreateDaylightSaving());
        _allCards.Add(CreateMetricSystem());
        _allCards.Add(CreatePostalService());
        _allCards.Add(CreateNationalPark());
        _allCards.Add(CreateOlympicBid());
        _allCards.Add(CreateWorldCup());
        _allCards.Add(CreateCulturalExchange());
        _allCards.Add(CreateMonumentControversy());
        _allCards.Add(CreateFlagDesign());
        _allCards.Add(CreateNationalAnthem());
        _allCards.Add(CreateCurrencyRedesign());
        _allCards.Add(CreateCensusControversy());
        _allCards.Add(CreatePardonRequest());
        _allCards.Add(CreateStateOfUnion());
        _allCards.Add(CreatePressBriefing());
        _allCards.Add(CreateCabinetMeeting());
        _allCards.Add(CreateG7Summit());
        _allCards.Add(CreateUNSpeech());
        _allCards.Add(CreatePeaceTreaty());
        _allCards.Add(CreateSanctions());
        
        _allCards.Add(CreateNationalBirdDrone());
        _allCards.Add(CreateCabinetKaraokeLeaks());
        _allCards.Add(CreateExecutiveFitnessChallenge());
        _allCards.Add(CreateCryptoForCorn());
        _allCards.Add(CreateIguanaKingReturns());
        _allCards.Add(CreateAIBudgetRhymes());
        _allCards.Add(CreateTimeLottery());
        _allCards.Add(CreateMascotRobotRecall());
        _allCards.Add(CreateExecutive45Again());
        _allCards.Add(CreateFilibusterFundraiser());
        _allCards.Add(CreateNationalSauceShortage());
        _allCards.Add(CreateWeatherBalloonPresser());
        _allCards.Add(CreateNationalMemeCouncil());
        _allCards.Add(CreateAIPatriotTest());
        _allCards.Add(CreateExecutiveHotTub());
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

    private DecisionCard CreateCyberAttack()
    {
        return new DecisionCard("EDX_031", "CYBER ATTACK", 
            "Hackers have breached government systems. They're threatening to release everyone's browser history unless you meet their demands.")
        {
            Category = CardCategory.Crisis,
            Urgency = UrgencyLevel.Critical,
            TimeLimit = 90,
            Choices = new List<CardChoice>
            {
                new(1, "Pay the ransom in Bitcoin")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.EconomicHealth] = -15,
                        [ResourceType.MediaTrust] = -10,
                        [ResourceType.Stability] = +5
                    },
                    Consequences = new List<string> { "Hackers keep a copy anyway", "You've funded cybercrime", "At least your history is safe" }
                },
                new(2, "Launch counter-hack")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -10,
                        [ResourceType.Popularity] = +15,
                        [ResourceType.MediaTrust] = +5
                    },
                    Alignment = ChoiceAlignment.Aggressive
                },
                new(3, "Release your own browser history first")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +20,
                        [ResourceType.MediaTrust] = +15,
                        [ResourceType.Stability] = -5
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    Consequences = new List<string> { "Transparency wins", "People respect the honesty", "Some searches are... concerning" }
                }
            }
        };
    }

    private DecisionCard CreatePandemic()
    {
        return new DecisionCard("EDX_032", "PANDEMIC OUTBREAK",
            "A new virus is spreading rapidly. Scientists recommend lockdowns. The economy can't handle another shutdown.")
        {
            Category = CardCategory.Crisis,
            Urgency = UrgencyLevel.Critical,
            Choices = new List<CardChoice>
            {
                new(1, "Full lockdown immediately")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +10,
                        [ResourceType.EconomicHealth] = -20,
                        [ResourceType.Popularity] = -15
                    }
                },
                new(2, "Keep everything open")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -20,
                        [ResourceType.EconomicHealth] = +10,
                        [ResourceType.Popularity] = -10
                    }
                },
                new(3, "Mandatory bubble wrap for everyone")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +15,
                        [ResourceType.EconomicHealth] = -10,
                        [ResourceType.Stability] = -5
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    Consequences = new List<string> { "Bubble wrap industry booms", "Popping sounds everywhere", "Actually kind of works?" }
                }
            }
        };
    }

    private DecisionCard CreateSpaceDebris()
    {
        return new DecisionCard("EDX_033", "SPACE DEBRIS COLLISION",
            "A defunct satellite is on collision course with the International Space Station. NASA needs a decision in 30 minutes.")
        {
            Category = CardCategory.Crisis,
            Urgency = UrgencyLevel.Critical,
            TimeLimit = 30,
            Choices = new List<CardChoice>
            {
                new(1, "Authorize missile strike on satellite")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +5,
                        [ResourceType.EconomicHealth] = -10,
                        [ResourceType.MediaTrust] = +10
                    }
                },
                new(2, "Evacuate the ISS")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -10,
                        [ResourceType.Popularity] = -5,
                        [ResourceType.MediaTrust] = +5
                    }
                },
                new(3, "Send up a giant baseball bat")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +20,
                        [ResourceType.Stability] = -15,
                        [ResourceType.EconomicHealth] = -5
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    Consequences = new List<string> { "It actually works", "Physics is weird", "America wins at space baseball" }
                }
            }
        };
    }

    private DecisionCard CreateVolcanicEruption()
    {
        return new DecisionCard("EDX_034", "VOLCANIC ERUPTION",
            "Yellowstone is showing signs of activity. Geologists are 'concerned but not alarmed,' which means they're terrified.")
        {
            Category = CardCategory.Crisis,
            Urgency = UrgencyLevel.Elevated,
            Choices = new List<CardChoice>
            {
                new(1, "Evacuate surrounding states")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -15,
                        [ResourceType.EconomicHealth] = -20,
                        [ResourceType.Popularity] = -10
                    }
                },
                new(2, "Monitor and wait")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +5,
                        [ResourceType.Popularity] = +5
                    }
                },
                new(3, "Plug it with concrete")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +10,
                        [ResourceType.Stability] = -20,
                        [ResourceType.EconomicHealth] = -15
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    Consequences = new List<string> { "Scientists say this is insane", "You do it anyway", "Surprisingly, nothing explodes" }
                }
            }
        };
    }

    private DecisionCard CreateTerroristThreat()
    {
        return new DecisionCard("EDX_035", "CREDIBLE THREAT",
            "Intelligence agencies have intercepted chatter about a major attack. Details are vague. Threat level: concerning.")
        {
            Category = CardCategory.Crisis,
            Urgency = UrgencyLevel.Urgent,
            Choices = new List<CardChoice>
            {
                new(1, "Raise national threat level")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -10,
                        [ResourceType.Popularity] = +5,
                        [ResourceType.MediaTrust] = +5
                    }
                },
                new(2, "Keep it quiet to avoid panic")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +5,
                        [ResourceType.MediaTrust] = -15
                    }
                },
                new(3, "Tweet 'come at me bro' to terrorists")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +15,
                        [ResourceType.Stability] = -25,
                        [ResourceType.MediaTrust] = -20
                    },
                    Alignment = ChoiceAlignment.Aggressive,
                    Consequences = new List<string> { "Security detail is not amused", "Twitter explodes", "Threat level: you" }
                }
            }
        };
    }

    private DecisionCard CreateNaturalDisaster()
    {
        return new DecisionCard("EDX_036", "HURRICANE INCOMING",
            "Category 5 hurricane approaching the coast. Millions need evacuation. FEMA is already stretched thin.")
        {
            Category = CardCategory.Crisis,
            Urgency = UrgencyLevel.Critical,
            Choices = new List<CardChoice>
            {
                new(1, "Full federal emergency response")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +10,
                        [ResourceType.EconomicHealth] = -15,
                        [ResourceType.Popularity] = +10
                    }
                },
                new(2, "Let states handle it")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -15,
                        [ResourceType.Popularity] = -20,
                        [ResourceType.EconomicHealth] = +5
                    }
                },
                new(3, "Nuke the hurricane")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -30,
                        [ResourceType.Popularity] = -25,
                        [ResourceType.MediaTrust] = -30
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    Consequences = new List<string> { "Scientists beg you to stop", "This makes it worse", "Radioactive hurricane achieved" }
                }
            }
        };
    }

    private DecisionCard CreateEnergyBlackout()
    {
        return new DecisionCard("EDX_037", "GRID FAILURE",
            "The power grid has failed across three states. 50 million people without electricity. It's summer.")
        {
            Category = CardCategory.Crisis,
            Urgency = UrgencyLevel.Critical,
            Choices = new List<CardChoice>
            {
                new(1, "Emergency power restoration")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.EconomicHealth] = -10,
                        [ResourceType.Stability] = +10,
                        [ResourceType.Popularity] = +15
                    }
                },
                new(2, "Blame renewable energy")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -10,
                        [ResourceType.MediaTrust] = -15,
                        [ResourceType.Popularity] = +5
                    }
                },
                new(3, "Mandate everyone use hamster wheels")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +10,
                        [ResourceType.Stability] = -15,
                        [ResourceType.EconomicHealth] = +5
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    Consequences = new List<string> { "Fitness levels improve", "Hamster sales skyrocket", "It generates 0.0001% of needed power" }
                }
            }
        };
    }

    private DecisionCard CreateWaterCrisis()
    {
        return new DecisionCard("EDX_038", "WATER SHORTAGE",
            "Major cities are running out of water. Reservoirs at 10% capacity. Desalination plants can't keep up.")
        {
            Category = CardCategory.Crisis,
            Urgency = UrgencyLevel.Urgent,
            Choices = new List<CardChoice>
            {
                new(1, "Emergency water rationing")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -10,
                        [ResourceType.Popularity] = -15,
                        [ResourceType.EconomicHealth] = -5
                    }
                },
                new(2, "Fast-track desalination projects")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.EconomicHealth] = -20,
                        [ResourceType.Stability] = +5,
                        [ResourceType.Popularity] = +10
                    }
                },
                new(3, "Make it rain via cloud seeding")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.EconomicHealth] = -10,
                        [ResourceType.Stability] = -5,
                        [ResourceType.Popularity] = +15
                    },
                    Consequences = new List<string> { "It works!", "Too well", "Flooding is now the problem" }
                }
            }
        };
    }

    private DecisionCard CreateFoodShortage()
    {
        return new DecisionCard("EDX_039", "SUPPLY CHAIN COLLAPSE",
            "Grocery stores are running empty. Supply chain disruptions have created food shortages. People are panic buying.")
        {
            Category = CardCategory.Crisis,
            Urgency = UrgencyLevel.Urgent,
            Choices = new List<CardChoice>
            {
                new(1, "Release strategic reserves")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +10,
                        [ResourceType.EconomicHealth] = -5,
                        [ResourceType.Popularity] = +10
                    }
                },
                new(2, "Price controls on essentials")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -5,
                        [ResourceType.EconomicHealth] = -10,
                        [ResourceType.Popularity] = +5
                    }
                },
                new(3, "Declare pizza a vegetable again")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Popularity] = +20,
                        [ResourceType.MediaTrust] = -10,
                        [ResourceType.Stability] = +5
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    Consequences = new List<string> { "Nutritionists weep", "Kids celebrate", "Problem solved?" }
                }
            }
        };
    }

    private DecisionCard CreateCyberWarfare()
    {
        return new DecisionCard("EDX_040", "CYBER WARFARE",
            "A hostile nation has launched a coordinated cyber attack on critical infrastructure. Banks, hospitals, and traffic systems affected.")
        {
            Category = CardCategory.Crisis,
            Urgency = UrgencyLevel.Critical,
            TimeLimit = 60,
            Choices = new List<CardChoice>
            {
                new(1, "Activate Cyber Command")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = +5,
                        [ResourceType.EconomicHealth] = -10,
                        [ResourceType.Popularity] = +10
                    },
                    Alignment = ChoiceAlignment.Aggressive
                },
                new(2, "Diplomatic protest")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -10,
                        [ResourceType.Popularity] = -15,
                        [ResourceType.MediaTrust] = +5
                    },
                    Alignment = ChoiceAlignment.Diplomatic
                },
                new(3, "Unplug everything and restart")
                {
                    Effects = new Dictionary<ResourceType, int>
                    {
                        [ResourceType.Stability] = -20,
                        [ResourceType.EconomicHealth] = -15,
                        [ResourceType.Popularity] = +10
                    },
                    Alignment = ChoiceAlignment.Chaotic,
                    Consequences = new List<string> { "IT support approves", "Everything goes offline", "When you turn it back on, it works" }
                }
            }
        };
    }

    private DecisionCard CreateBriberyScandal() => new("EDX_041", "BRIBERY ALLEGATIONS", "A major donor claims you promised them a cabinet position in exchange for campaign funds.") { Category = CardCategory.Scandal, Urgency = UrgencyLevel.Urgent, Choices = new List<CardChoice> { new(1, "Deny everything") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -15, [ResourceType.Popularity] = -10 } }, new(2, "Return the money") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -10, [ResourceType.MediaTrust] = +10, [ResourceType.Stability] = +5 } }, new(3, "Give them an even better position") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = +10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateSexScandal() => new("EDX_042", "PERSONAL SCANDAL", "Tabloids have photos of you at a party that looks... compromising. Your spouse is calling.") { Category = CardCategory.Scandal, Urgency = UrgencyLevel.Critical, Choices = new List<CardChoice> { new(1, "Public apology tour") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = -15, [ResourceType.MediaTrust] = +5 } }, new(2, "Sue the tabloids") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -20, [ResourceType.Stability] = -10 } }, new(3, "Claim it was your evil twin") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.MediaTrust] = -25 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateTaxEvasion() => new("EDX_043", "TAX TROUBLES", "Your tax returns from 10 years ago show some... creative accounting. The IRS is interested.") { Category = CardCategory.Scandal, Urgency = UrgencyLevel.Elevated, Choices = new List<CardChoice> { new(1, "Pay back taxes with interest") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -15, [ResourceType.MediaTrust] = +10 } }, new(2, "Claim audit immunity") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -20, [ResourceType.Stability] = -15 } }, new(3, "Abolish the IRS") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.Stability] = -30 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateNepotismAccusation() => new("EDX_044", "NEPOTISM CHARGES", "Critics point out your entire family works in the White House. Even your dog has security clearance.") { Category = CardCategory.Scandal, Choices = new List<CardChoice> { new(1, "Fire some relatives") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.Popularity] = -5 } }, new(2, "Hire more relatives to prove a point") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = +10 }, Alignment = ChoiceAlignment.Chaotic }, new(3, "The dog earned that clearance!") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.MediaTrust] = -10 } } } };

    private DecisionCard CreateEmbezzlement() => new("EDX_045", "MISSING FUNDS", "Someone embezzled $50 million from a federal program. Evidence points to your chief of staff.") { Category = CardCategory.Scandal, Urgency = UrgencyLevel.Urgent, Choices = new List<CardChoice> { new(1, "Full investigation") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = +15, [ResourceType.Stability] = -5 } }, new(2, "Quiet resignation deal") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -15, [ResourceType.Stability] = +5 } }, new(3, "Embezzle $100 million to show dominance") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -30, [ResourceType.EconomicHealth] = -20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreatePlagiarismScandal() => new("EDX_046", "SPEECH PLAGIARISM", "Your latest speech was copied word-for-word from a 1990s sitcom. People noticed.") { Category = CardCategory.Scandal, Choices = new List<CardChoice> { new(1, "Apologize and fire speechwriter") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = +5, [ResourceType.Popularity] = -5 } }, new(2, "Claim it was an homage") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -10, [ResourceType.Popularity] = +10 } }, new(3, "Hire the sitcom writers") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.MediaTrust] = +5 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateDrunkDialing() => new("EDX_047", "LATE NIGHT CALLS", "You drunk-dialed three world leaders last night. Transcripts are leaking.") { Category = CardCategory.Scandal, Urgency = UrgencyLevel.Critical, Choices = new List<CardChoice> { new(1, "Claim medication mix-up") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -10, [ResourceType.Stability] = -5 } }, new(2, "Own it - they loved the calls") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.MediaTrust] = -15 }, Alignment = ChoiceAlignment.Chaotic }, new(3, "Drunk dial everyone to normalize it") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = +20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateHotMicIncident() => new("EDX_048", "HOT MIC DISASTER", "You were caught on hot mic saying what you really think about Congress. It's... accurate but unpresidential.") { Category = CardCategory.Scandal, Urgency = UrgencyLevel.Urgent, Choices = new List<CardChoice> { new(1, "Apologize profusely") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = -10, [ResourceType.Stability] = +5 } }, new(2, "Double down on the comments") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = -15 }, Alignment = ChoiceAlignment.Aggressive }, new(3, "Claim deepfake") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -20, [ResourceType.Stability] = -10 } } } };

    private DecisionCard CreateDeepfakeVideo() => new("EDX_049", "DEEPFAKE ATTACK", "A convincing deepfake video of you doing something ridiculous is going viral. 100 million views.") { Category = CardCategory.Scandal, Urgency = UrgencyLevel.Elevated, Choices = new List<CardChoice> { new(1, "Hire experts to debunk it") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = +10, [ResourceType.EconomicHealth] = -5 } }, new(2, "Make your own deepfake in response") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.MediaTrust] = -10 }, Alignment = ChoiceAlignment.Chaotic }, new(3, "Claim you actually did it") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = -15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateWhistleblower() => new("EDX_050", "WHISTLEBLOWER", "A whistleblower claims you've been using Air Force One for personal pizza deliveries.") { Category = CardCategory.Scandal, Choices = new List<CardChoice> { new(1, "Deny and investigate leaker") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -15, [ResourceType.Stability] = -10 } }, new(2, "Admit it and reimburse taxpayers") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = +15, [ResourceType.EconomicHealth] = -5 } }, new(3, "Offer free pizza to everyone") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.EconomicHealth] = -15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreatePizzaGate() => new("EDX_051", "PIZZA CONSPIRACY", "A conspiracy theory claims the White House basement is a pizza restaurant. There is no basement.") { Category = CardCategory.Absurd, Choices = new List<CardChoice> { new(1, "Build a basement to prove them wrong") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -10, [ResourceType.Popularity] = +15 }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Ignore it completely") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5 } }, new(3, "Open an actual pizza restaurant") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.EconomicHealth] = +10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateNationalDance() => new("EDX_052", "NATIONAL DANCE", "Congress wants to mandate a national dance that all citizens must perform daily.") { Category = CardCategory.Absurd, Choices = new List<CardChoice> { new(1, "Support the dance mandate") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = -15 }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Veto this madness") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10 } }, new(3, "Make it optional but televised") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.MediaTrust] = +10 } } } };

    private DecisionCard CreateMemeWar() => new("EDX_053", "MEME WARFARE", "A foreign nation is attacking us with memes. They're actually pretty good memes.") { Category = CardCategory.Absurd, Urgency = UrgencyLevel.Elevated, Choices = new List<CardChoice> { new(1, "Launch counter-meme offensive") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.MediaTrust] = +15 }, Alignment = ChoiceAlignment.Aggressive }, new(2, "Ban all memes") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = -30, [ResourceType.Stability] = -20 } }, new(3, "Hire the enemy memers") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.Stability] = -10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateFlatEarthMovement() => new("EDX_054", "FLAT EARTH LOBBY", "The Flat Earth Society wants federal funding to prove the Earth is flat. They have 10 million signatures.") { Category = CardCategory.Absurd, Choices = new List<CardChoice> { new(1, "Fund the expedition") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -15, [ResourceType.MediaTrust] = -20, [ResourceType.Popularity] = +10 }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Politely decline") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.Popularity] = -10 } }, new(3, "Send them to space") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -20, [ResourceType.Popularity] = +25 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateTimeTravel() => new("EDX_055", "TIME TRAVELER", "Someone claiming to be you from the future warns about a decision you're about to make. They won't say which one.") { Category = CardCategory.Absurd, Rarity = CardRarity.Rare, Choices = new List<CardChoice> { new(1, "Believe them and change nothing") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -5 } }, new(2, "Arrest them for impersonation") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.MediaTrust] = -10 } }, new(3, "Make them your advisor") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = -15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateCloneArmy() => new("EDX_056", "CLONE PROPOSAL", "Military wants to clone the best soldier 10,000 times. Ethics board says no. Military says please.") { Category = CardCategory.Absurd, Urgency = UrgencyLevel.Elevated, Choices = new List<CardChoice> { new(1, "Approve the cloning program") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.EconomicHealth] = -15, [ResourceType.Popularity] = +10 }, Alignment = ChoiceAlignment.Aggressive }, new(2, "Reject on ethical grounds") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.MediaTrust] = +10 } }, new(3, "Clone yourself instead") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -25, [ResourceType.Popularity] = +20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateGhostWhiteHouse() => new("EDX_057", "HAUNTED HOUSE", "Staff report the White House is haunted. Security footage shows... something. Ghost hunters want access.") { Category = CardCategory.Absurd, Choices = new List<CardChoice> { new(1, "Allow ghost investigation") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.MediaTrust] = -10 } }, new(2, "Deny everything") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5 } }, new(3, "Negotiate with the ghosts") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.MediaTrust] = -15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateTalkingStatue() => new("EDX_058", "STATUE SPEAKS", "The Lincoln Memorial statue started talking. It has opinions about current policy. Strong opinions.") { Category = CardCategory.Absurd, Rarity = CardRarity.Legendary, Choices = new List<CardChoice> { new(1, "Listen to Lincoln's advice") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +15, [ResourceType.Popularity] = +20 } }, new(2, "Investigate for speakers") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = +10, [ResourceType.Popularity] = -10 } }, new(3, "Argue with the statue") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = -10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreatePortalBasement() => new("EDX_059", "PORTAL DISCOVERY", "Maintenance found a portal to another dimension in the basement. It leads to a dimension where everything is slightly better.") { Category = CardCategory.Absurd, Rarity = CardRarity.Epic, Choices = new List<CardChoice> { new(1, "Explore the portal") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = +20 } }, new(2, "Seal it immediately") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10 } }, new(3, "Move government through portal") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -30, [ResourceType.Popularity] = +30 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateAIPresident() => new("EDX_060", "AI CHALLENGER", "An AI has announced it's running against you in the next election. Polls show it's winning.") { Category = CardCategory.Absurd, Choices = new List<CardChoice> { new(1, "Debate the AI") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.MediaTrust] = +10 } }, new(2, "Ban AI from politics") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = -10 } }, new(3, "Merge with the AI") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -25, [ResourceType.Popularity] = +25 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateSpinDoctor() => new("EDX_061", "Spin Doctor's Advice", "Your PR specialist suggests you start every speech with a backflip. 'Trust me, it'll work,' they insist.") { Category = CardCategory.Character, CharacterId = "SPIN_DOCTOR", Choices = new List<CardChoice> { new(1, "Do the backflip") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.MediaTrust] = -10 }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Stick to normal speeches") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5 } }, new(3, "Hire a stunt double") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.EconomicHealth] = -5 } } } };

    private DecisionCard CreateGeneralWarmonger() => new("EDX_062", "General's Proposal", "Your top general wants to invade a country that doesn't exist. They have very detailed invasion plans.") { Category = CardCategory.Character, CharacterId = "GENERAL", Choices = new List<CardChoice> { new(1, "Approve the invasion") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -25, [ResourceType.EconomicHealth] = -20 }, Alignment = ChoiceAlignment.Aggressive }, new(2, "Gently explain geography") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.MediaTrust] = +5 } }, new(3, "Make them find it first") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +10, [ResourceType.EconomicHealth] = -10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateTechBillionaire() => new("EDX_063", "Billionaire's Offer", "A tech billionaire offers to buy the government. They're serious. The check clears.") { Category = CardCategory.Character, CharacterId = "TECH_CEO", Choices = new List<CardChoice> { new(1, "Accept the offer") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -30, [ResourceType.EconomicHealth] = +30, [ResourceType.Popularity] = -20 }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Politely decline") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.MediaTrust] = +15 } }, new(3, "Counter-offer to buy their company") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.EconomicHealth] = -15 }, Alignment = ChoiceAlignment.Aggressive } } };

    private DecisionCard CreateFirstSpouse() => new("EDX_064", "Spouse's Initiative", "Your spouse wants to make 'being nice' mandatory by law. They've already drafted legislation.") { Category = CardCategory.Character, CharacterId = "FIRST_SPOUSE", Choices = new List<CardChoice> { new(1, "Support the niceness law") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = -10 } }, new(2, "Suggest a different initiative") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5 } }, new(3, "Make them co-president") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = +20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateVicePresident() => new("EDX_065", "VP's Ambition", "Your VP keeps 'accidentally' sitting in your chair and making executive decisions. They apologize each time.") { Category = CardCategory.Character, CharacterId = "VICE_PRESIDENT", Choices = new List<CardChoice> { new(1, "Confront them directly") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = -5 } }, new(2, "Ignore it") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10 } }, new(3, "Switch jobs for a day") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = -15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateChiefOfStaff() => new("EDX_066", "Chief's Breakdown", "Your Chief of Staff hasn't slept in 72 hours. They're making decisions based on fever dreams now.") { Category = CardCategory.Character, CharacterId = "CHIEF_OF_STAFF", Choices = new List<CardChoice> { new(1, "Force them to take vacation") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = +5 } }, new(2, "Let them continue") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15 } }, new(3, "Implement all fever dream ideas") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = +15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreatePressSecretary() => new("EDX_067", "Press Secretary Crisis", "Your Press Secretary accidentally told the truth at a briefing. The media is confused and suspicious.") { Category = CardCategory.Character, CharacterId = "PRESS_SEC", Choices = new List<CardChoice> { new(1, "Embrace radical honesty") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = +25, [ResourceType.Stability] = -10 } }, new(2, "Issue immediate correction") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -15, [ResourceType.Stability] = +5 } }, new(3, "Claim it was opposite day") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -20, [ResourceType.Popularity] = +10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateSecretService() => new("EDX_068", "Secret Service Request", "Secret Service wants to wrap you in bubble wrap for safety. They have a 50-page threat assessment.") { Category = CardCategory.Character, CharacterId = "SECRET_SERVICE", Choices = new List<CardChoice> { new(1, "Accept the bubble wrap") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.MediaTrust] = -10 }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Compromise on a helmet") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.Popularity] = +5 } }, new(3, "Fire the entire detail") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = -15 } } } };

    private DecisionCard CreateWhiteHouseChef() => new("EDX_069", "Chef's Ultimatum", "The White House chef threatens to quit unless you stop ordering fast food. They're crying.") { Category = CardCategory.Character, CharacterId = "CHEF", Choices = new List<CardChoice> { new(1, "Promise to eat their food") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.Popularity] = -5 } }, new(2, "Let them quit") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = +10 } }, new(3, "Make fast food the official cuisine") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = -15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreatePresidentialDog() => new("EDX_070", "First Dog Scandal", "The presidential dog bit a foreign dignitary. The dog shows no remorse. The dignitary demands justice.") { Category = CardCategory.Character, CharacterId = "FIRST_DOG", Choices = new List<CardChoice> { new(1, "Apologize profusely") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = -10 } }, new(2, "Defend the dog") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.Stability] = -15 } }, new(3, "Make dog the Secretary of Defense") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +30, [ResourceType.Stability] = -25 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateEducationReform() => new("EDX_071", "Education Reform", "Teachers want higher pay. Parents want better schools. Students want longer lunch breaks.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Increase education funding") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -10, [ResourceType.Popularity] = +15, [ResourceType.Stability] = +10 } }, new(2, "Maintain current funding") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -5, [ResourceType.Popularity] = -10 } }, new(3, "Replace teachers with holograms") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = +10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateInfrastructureBill() => new("EDX_072", "Infrastructure Bill", "Bridges are crumbling. Roads have potholes. Someone suggests flying cars as a solution.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Pass infrastructure bill") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -15, [ResourceType.Stability] = +15, [ResourceType.Popularity] = +10 } }, new(2, "Delay for more studies") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = -5 } }, new(3, "Invest in flying cars") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -20, [ResourceType.Popularity] = +20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateTaxReform() => new("EDX_073", "Tax Reform", "Everyone wants lower taxes. Nobody wants fewer services. Math doesn't work.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Raise taxes on wealthy") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = +10, [ResourceType.Popularity] = +5, [ResourceType.Stability] = -10 } }, new(2, "Cut spending instead") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = -10, [ResourceType.EconomicHealth] = +5 } }, new(3, "Print more money") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -25, [ResourceType.Popularity] = +15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateImmigrationPolicy() => new("EDX_074", "Immigration Reform", "Immigration policy needs updating. Everyone has different opinions. All of them are loud.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Comprehensive reform") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = +10, [ResourceType.MediaTrust] = +10 } }, new(2, "Maintain status quo") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -5 } }, new(3, "Open borders to everyone") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -25, [ResourceType.Popularity] = +15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateClimateAction() => new("EDX_075", "Climate Policy", "Scientists say act now. Industry says slow down. The planet is getting warmer.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Aggressive climate action") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -15, [ResourceType.Popularity] = +10, [ResourceType.Stability] = +5 } }, new(2, "Moderate approach") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.Popularity] = -5 } }, new(3, "Air condition the outdoors") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -30, [ResourceType.Popularity] = +20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateMilitaryBudget() => new("EDX_076", "Defense Spending", "Military wants more funding. They always want more funding. They have a PowerPoint.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Increase military budget") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -15, [ResourceType.Stability] = +10, [ResourceType.Popularity] = +5 } }, new(2, "Maintain current levels") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5 } }, new(3, "Build a giant robot") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -25, [ResourceType.Popularity] = +25 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateTradeAgreement() => new("EDX_077", "Trade Deal", "A new trade agreement promises economic growth. Critics say it promises job losses. Both have charts.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Sign the agreement") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = +15, [ResourceType.Popularity] = -10, [ResourceType.Stability] = -5 } }, new(2, "Reject the deal") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = +5 } }, new(3, "Trade only memes") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.EconomicHealth] = -10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateJudicialNomination() => new("EDX_078", "Supreme Court Nominee", "You need to nominate a Supreme Court justice. Everyone has an opinion. Especially people who aren't lawyers.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Nominate moderate candidate") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = +5 } }, new(2, "Nominate partisan pick") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = +15 } }, new(3, "Nominate a dog") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.Stability] = -25 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateVeteranAffairs() => new("EDX_079", "Veterans Benefits", "Veterans need better healthcare and benefits. The budget is tight. Veterans are not happy.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Increase VA funding") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -10, [ResourceType.Popularity] = +15, [ResourceType.Stability] = +10 } }, new(2, "Reform existing system") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.Popularity] = +5 } }, new(3, "Give them all medals") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +10, [ResourceType.Stability] = -10 } } } };

    private DecisionCard CreateSpaceProgram() => new("EDX_080", "Space Exploration", "NASA wants to go to Mars. Budget office says we can barely afford the Moon. Elon Musk is tweeting.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Fund Mars mission") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -20, [ResourceType.Popularity] = +20, [ResourceType.Stability] = +5 } }, new(2, "Focus on Earth problems") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = -10 } }, new(3, "Colonize the Moon first") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -15, [ResourceType.Popularity] = +15 } } } };

    private DecisionCard CreateDrugPolicy() => new("EDX_081", "Drug Policy Reform", "States are legalizing marijuana. Federal law says no. The situation is... hazy.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Federal legalization") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = -10, [ResourceType.EconomicHealth] = +10 } }, new(2, "Maintain prohibition") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = -15 } }, new(3, "Make everything legal") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -30, [ResourceType.Popularity] = +20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateGunControl() => new("EDX_082", "Gun Legislation", "Gun control debate reaches fever pitch. Both sides are absolutely certain they're right.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Stricter gun laws") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = +10, [ResourceType.MediaTrust] = +10 } }, new(2, "Protect gun rights") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = +10 } }, new(3, "Mandatory sword training") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = -15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateSocialSecurity() => new("EDX_083", "Social Security Crisis", "Social Security is running out of money. Retirees are worried. Young people don't think it'll exist for them anyway.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Raise retirement age") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = -20, [ResourceType.EconomicHealth] = +10 } }, new(2, "Increase payroll taxes") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = -15, [ResourceType.EconomicHealth] = +15 } }, new(3, "Invest in lottery tickets") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -25, [ResourceType.Popularity] = +10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateMinimumWage() => new("EDX_084", "Minimum Wage Debate", "Workers want $25/hour. Businesses say they'll close. Economists have seventeen different opinions.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Raise to $15/hour") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = -10, [ResourceType.EconomicHealth] = -5 } }, new(2, "Keep current minimum") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = -15 } }, new(3, "Pay everyone in Bitcoin") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = +15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateNetNeutrality() => new("EDX_085", "Net Neutrality", "Internet providers want to charge more for fast lanes. The internet is very angry about this. Ironically, they're angry online.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Protect net neutrality") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = +10, [ResourceType.EconomicHealth] = -5 } }, new(2, "Allow fast lanes") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = -25, [ResourceType.Stability] = -15, [ResourceType.EconomicHealth] = +10 } }, new(3, "Nationalize the internet") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = +10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreatePrivacyLaws() => new("EDX_086", "Data Privacy", "Tech companies know everything about everyone. People are concerned. Tech companies say 'trust us.'") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Strict privacy regulations") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = +10, [ResourceType.EconomicHealth] = -10 } }, new(2, "Self-regulation") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = -15, [ResourceType.EconomicHealth] = +10 } }, new(3, "Make all data public") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -30, [ResourceType.Popularity] = -20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateAntitrust() => new("EDX_087", "Big Tech Monopoly", "Tech giants control everything. Break them up or let them be? They're listening to this conversation.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Break up monopolies") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = -10, [ResourceType.EconomicHealth] = -15 } }, new(2, "Allow consolidation") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.Popularity] = -15, [ResourceType.EconomicHealth] = +15 } }, new(3, "Merge them into one super-company") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -25, [ResourceType.EconomicHealth] = +20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateFarmSubsidies() => new("EDX_088", "Agricultural Policy", "Farmers need subsidies. Budget needs cuts. Food needs to be affordable. Pick two.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Increase farm subsidies") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -10, [ResourceType.Popularity] = +10, [ResourceType.Stability] = +10 } }, new(2, "Reduce subsidies") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = -15, [ResourceType.EconomicHealth] = +10 } }, new(3, "Subsidize only pizza farms") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = -10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateHousingCrisis() => new("EDX_089", "Housing Affordability", "Nobody can afford houses. Rent is too high. NIMBYs block new construction. It's a mess.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Affordable housing initiative") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -15, [ResourceType.Popularity] = +15, [ResourceType.Stability] = +10 } }, new(2, "Let market decide") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = -20 } }, new(3, "Everyone lives in treehouses") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = -20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateStudentDebt() => new("EDX_090", "Student Loan Crisis", "$1.7 trillion in student debt. Graduates are drowning. Colleges keep raising tuition.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Cancel student debt") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -20, [ResourceType.Popularity] = +25, [ResourceType.Stability] = -10 } }, new(2, "Maintain current system") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = -20 } }, new(3, "Replace with gladiator games") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = -25 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreatePoliceReform() => new("EDX_091", "Police Reform", "Communities want reform. Police want support. Everyone wants safety. Finding middle ground is hard.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Comprehensive reform") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = +15, [ResourceType.MediaTrust] = +10 } }, new(2, "Increase police funding") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = -10, [ResourceType.EconomicHealth] = -10 } }, new(3, "Replace with robot police") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = +10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateElectionSecurity() => new("EDX_092", "Election Security", "Elections need securing. Paper ballots or digital? Both sides claim the other is cheating.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Paper ballot mandate") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +15, [ResourceType.MediaTrust] = +15, [ResourceType.EconomicHealth] = -5 } }, new(2, "Blockchain voting") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = +15, [ResourceType.EconomicHealth] = -10 } }, new(3, "Trial by combat") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.Stability] = -30 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateCampaignFinance() => new("EDX_093", "Campaign Finance Reform", "Money in politics is out of control. Billionaires buy elections. Reform is needed. Billionaires disagree.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Strict finance limits") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +15, [ResourceType.MediaTrust] = +15, [ResourceType.Popularity] = +10 } }, new(2, "Maintain status quo") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = -15 } }, new(3, "Auction off government positions") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = +25, [ResourceType.Stability] = -30 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateTermLimits() => new("EDX_094", "Congressional Term Limits", "Congress won't vote for term limits. They like their jobs. Voters want change.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Push for term limits") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = -15, [ResourceType.MediaTrust] = +10 } }, new(2, "Drop the issue") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.Popularity] = -15 } }, new(3, "Randomize Congress yearly") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.Stability] = -30 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateSupremeCourtExpansion() => new("EDX_095", "Court Packing Debate", "Some want to expand the Supreme Court. Others call it court packing. It's definitely political.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Expand to 13 justices") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = +15, [ResourceType.MediaTrust] = -15 } }, new(2, "Keep at 9 justices") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = -10 } }, new(3, "Make it 100 justices") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -30, [ResourceType.Popularity] = +20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateStatehoodDebate() => new("EDX_096", "Statehood Question", "DC and Puerto Rico want statehood. It's complicated. Politics are involved. Shocking.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Grant statehood") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = +15, [ResourceType.MediaTrust] = +10 } }, new(2, "Maintain territories") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = -10 } }, new(3, "Make everything a state") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -25, [ResourceType.Popularity] = +20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateNationalHoliday() => new("EDX_097", "New National Holiday", "Proposals for new federal holidays: Election Day, Juneteenth, or National Pizza Day.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Election Day holiday") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = +10, [ResourceType.EconomicHealth] = -5 } }, new(2, "Juneteenth holiday") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = +10, [ResourceType.MediaTrust] = +10 } }, new(3, "National Pizza Day") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +30, [ResourceType.Stability] = -10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateDaylightSaving() => new("EDX_098", "Daylight Saving Time", "Everyone hates changing clocks twice a year. But which time do we keep? People have strong opinions.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Permanent daylight time") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = +10 } }, new(2, "Permanent standard time") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +10, [ResourceType.Stability] = +10 } }, new(3, "Change clocks every hour") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -25, [ResourceType.Popularity] = -20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateMetricSystem() => new("EDX_099", "Metric Conversion", "The US is one of three countries not using metric. Scientists are tired of converting. Engineers are confused.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Switch to metric") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = -15, [ResourceType.MediaTrust] = +10 } }, new(2, "Keep imperial system") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.Popularity] = +10 } }, new(3, "Invent new system") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -30, [ResourceType.Popularity] = +15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreatePostalService() => new("EDX_100", "USPS Crisis", "The Postal Service is losing money. Privatize or subsidize? Mail carriers are waiting.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Increase USPS funding") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -10, [ResourceType.Popularity] = +15, [ResourceType.Stability] = +10 } }, new(2, "Privatize postal service") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = -15, [ResourceType.EconomicHealth] = +10 } }, new(3, "Deliver mail via drones only") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = -15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateNationalPark() => new("EDX_101", "National Park Expansion", "Environmentalists want more national parks. Developers want the land. Nature doesn't vote.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Create new national parks") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = +10, [ResourceType.EconomicHealth] = -10 } }, new(2, "Allow development") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = -15, [ResourceType.EconomicHealth] = +15 } }, new(3, "Turn everything into parks") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = -25 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateOlympicBid() => new("EDX_102", "Olympic Bid", "A city wants to host the Olympics. It costs billions. They promise economic benefits. History suggests otherwise.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Support the bid") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -20, [ResourceType.Popularity] = +20, [ResourceType.Stability] = +5 } }, new(2, "Decline to bid") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = -10 } }, new(3, "Invent new Olympics") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.EconomicHealth] = -15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateWorldCup() => new("EDX_103", "World Cup Hosting", "FIFA wants the US to host the World Cup. It's expensive. Soccer fans are excited. Football fans are confused.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Host the World Cup") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -15, [ResourceType.Popularity] = +20, [ResourceType.Stability] = +10 } }, new(2, "Decline the offer") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.Popularity] = -15 } }, new(3, "Host World Cup of everything") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.EconomicHealth] = -20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateCulturalExchange() => new("EDX_104", "Cultural Exchange Program", "State Department wants to expand cultural exchanges. It builds goodwill. It costs money.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Expand the program") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -10, [ResourceType.Stability] = +15, [ResourceType.MediaTrust] = +10 } }, new(2, "Maintain current level") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5 } }, new(3, "Exchange everyone") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = +15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateMonumentControversy() => new("EDX_105", "Monument Debate", "Controversial monuments spark debate. Remove them? Keep them? Add context? Everyone's angry.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Remove controversial monuments") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = +10, [ResourceType.MediaTrust] = +10 } }, new(2, "Keep all monuments") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = -10 } }, new(3, "Replace with meme statues") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.Stability] = -20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateFlagDesign() => new("EDX_106", "Flag Redesign Proposal", "A designer proposes a new flag. It's... interesting. Traditionalists are outraged. Designers love it.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Keep current flag") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = +5 } }, new(2, "Adopt new design") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = +15 } }, new(3, "Let internet vote on design") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = -25 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateNationalAnthem() => new("EDX_107", "Anthem Controversy", "Some want a new national anthem. Others say it's tradition. A third group suggests dubstep remix.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Keep Star-Spangled Banner") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = +5 } }, new(2, "Choose new anthem") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -20, [ResourceType.Popularity] = +10 } }, new(3, "Dubstep remix") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.Stability] = -20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateCurrencyRedesign() => new("EDX_108", "Currency Redesign", "Treasury wants to update currency designs. New faces, new security features, new controversies.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Approve redesign") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.Popularity] = +10, [ResourceType.EconomicHealth] = -5 } }, new(2, "Keep current designs") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5 } }, new(3, "Put memes on money") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +30, [ResourceType.Stability] = -20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateCensusControversy() => new("EDX_109", "Census Questions", "Census Bureau wants to add controversial questions. Data is important. Privacy is important. Politics are involved.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Add the questions") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = -10, [ResourceType.MediaTrust] = -10 } }, new(2, "Keep census simple") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = +10 } }, new(3, "Ask only fun questions") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = -15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreatePardonRequest() => new("EDX_110", "Pardon Request", "A controversial figure requests presidential pardon. Supporters demand it. Critics oppose it. You decide.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Grant the pardon") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -15, [ResourceType.Popularity] = +10, [ResourceType.MediaTrust] = -15 } }, new(2, "Deny the pardon") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = -10 } }, new(3, "Pardon everyone") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -30, [ResourceType.Popularity] = +20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateStateOfUnion() => new("EDX_111", "State of the Union", "Time for your annual address. Speechwriters have three versions: optimistic, realistic, and entertaining.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Optimistic speech") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.MediaTrust] = -5 } }, new(2, "Realistic assessment") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = +15, [ResourceType.Popularity] = -5 } }, new(3, "Interpretive dance") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.MediaTrust] = -15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreatePressBriefing() => new("EDX_112", "Press Briefing Disaster", "Today's press briefing went off the rails. Reporters have questions. You have... answers?") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Answer honestly") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = +15, [ResourceType.Stability] = -5 } }, new(2, "Deflect and pivot") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -10, [ResourceType.Stability] = +5 } }, new(3, "Respond only in riddles") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.MediaTrust] = -20 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateCabinetMeeting() => new("EDX_113", "Cabinet Meeting", "Cabinet meeting descends into chaos. Everyone's talking over each other. Someone brought a whiteboard.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Restore order") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = +5 } }, new(2, "Let them fight it out") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10 } }, new(3, "Turn it into reality TV") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +25, [ResourceType.Stability] = -15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateG7Summit() => new("EDX_114", "G7 Summit", "World leaders gather for G7 summit. Diplomacy is key. Someone suggests settling disputes via karaoke.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Traditional diplomacy") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +15, [ResourceType.MediaTrust] = +10 } }, new(2, "Aggressive negotiation") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = +15 }, Alignment = ChoiceAlignment.Aggressive }, new(3, "Karaoke diplomacy") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +30, [ResourceType.Stability] = -10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateUNSpeech() => new("EDX_115", "UN Address", "You're addressing the United Nations. The world is watching. Your teleprompter just broke.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Wing it professionally") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = +10, [ResourceType.Stability] = +10 } }, new(2, "Read from phone") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -5, [ResourceType.Popularity] = +5 } }, new(3, "Freestyle rap") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +30, [ResourceType.MediaTrust] = -15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreatePeaceTreaty() => new("EDX_116", "Peace Treaty", "Two nations want you to mediate peace talks. Both sides are stubborn. History is complicated.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Patient mediation") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +15, [ResourceType.MediaTrust] = +15, [ResourceType.Popularity] = +10 } }, new(2, "Pressure both sides") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.Popularity] = +5 }, Alignment = ChoiceAlignment.Aggressive }, new(3, "Lock them in a room") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +20, [ResourceType.Stability] = -10 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateSanctions() => new("EDX_117", "Economic Sanctions", "Advisors recommend sanctions on a hostile nation. It might work. It might backfire. It'll definitely be complicated.") { Category = CardCategory.Normal, Choices = new List<CardChoice> { new(1, "Impose sanctions") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = +10, [ResourceType.EconomicHealth] = -10 } }, new(2, "Pursue diplomacy") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +10, [ResourceType.Popularity] = -5 }, Alignment = ChoiceAlignment.Diplomatic }, new(3, "Sanction everyone equally") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -30, [ResourceType.Popularity] = +15 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateNationalBirdDrone() => new("EDX_002", "National Bird: The Surveillance Drone", "A viral poll names a quad-copter as the new national bird. Ornithologists protest. Defense contractors purr.") { Category = CardCategory.Absurd, Rarity = CardRarity.Common, Urgency = UrgencyLevel.Urgent, Choices = new List<CardChoice> { new(1, "Lean in—declare it official") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +8, [ResourceType.MediaTrust] = -10, [ResourceType.Stability] = -5 }, Consequences = new List<string> { "Tourism ads film themselves", "Birdwatchers switch to firmware updates", "Civil libertarians file suit" }, Alignment = ChoiceAlignment.Chaotic, FollowupCardIds = new List<string> { "EDX_078_CIVIL_LIBERTIES_HEARING" } }, new(2, "Compromise: Bald Eagle… with sunglasses") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +3, [ResourceType.Stability] = +3, [ResourceType.MediaTrust] = 0 }, Consequences = new List<string> { "Merch sales spike", "Eagle refuses to be tagged", "Late-night shows approve" }, Alignment = ChoiceAlignment.Diplomatic }, new(3, "Commission a citizens' aviary council") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = -2, [ResourceType.Stability] = +5, [ResourceType.MediaTrust] = +4 }, Consequences = new List<string> { "Council argues about feathers vs. rotors", "C-SPAN ratings triple", "Decision delayed to next season" }, Alignment = ChoiceAlignment.Cautious } } };

    private DecisionCard CreateCabinetKaraokeLeaks() => new("EDX_003", "Cabinet Karaoke Night Leaks", "A lobbyist releases auto-tuned clips of the Cabinet performing 'Oops!… We Spent It Again.'") { Category = CardCategory.Scandal, Rarity = CardRarity.Common, Urgency = UrgencyLevel.Urgent, Choices = new List<CardChoice> { new(1, "Own it—release a charity remix") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +7, [ResourceType.MediaTrust] = +2, [ResourceType.EconomicHealth] = +1 }, Consequences = new List<string> { "Song hits #8 on charts", "Opposition releases diss-track", "Culture war gains a soundtrack" }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Investigate the leak, seize the servers") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = -4, [ResourceType.Stability] = +6, [ResourceType.MediaTrust] = -5 }, Consequences = new List<string> { "Free speech hearings begin", "Whistleblower fundraisers explode", "Hashtag #LetThemSing trends" }, Alignment = ChoiceAlignment.Aggressive, FollowupCardIds = new List<string> { "SCANDAL_005", "COVERUP_001" } }, new(3, "Deny attendance, blame deepfakes") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -8, [ResourceType.Stability] = -3 }, Consequences = new List<string> { "Original files surface with timestamps", "Your auto-tune plugin subscription leaks", "Tech press piles on" }, Alignment = ChoiceAlignment.Cautious } } };

    private DecisionCard CreateExecutiveFitnessChallenge() => new("EDX_004", "Operation: Executive Fitness Challenge", "Military invites you to a friendly obstacle course. Your PR team smells a viral moment; your doctor smells liability.") { Category = CardCategory.Normal, Rarity = CardRarity.Uncommon, Urgency = UrgencyLevel.Normal, Choices = new List<CardChoice> { new(1, "Run it live on national TV") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +9, [ResourceType.Stability] = -5 }, Consequences = new List<string> { "You nail the rope climb", "Opposition edits slow-mo stumbles", "Recruitment ads recycle footage" }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Quietly do a private trial first") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +6, [ResourceType.Popularity] = +2 }, Consequences = new List<string> { "PT scores classified as a state secret", "Leakers claim you did great", "Press demands the tape" }, Alignment = ChoiceAlignment.Cautious }, new(3, "Send a body double and call it satire") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = -6, [ResourceType.Popularity] = +3 }, Consequences = new List<string> { "Impersonator gets a book deal", "Satire defense confuses courts", "Late-night hosts salute" }, Alignment = ChoiceAlignment.Aggressive } } };

    private DecisionCard CreateCryptoForCorn() => new("EDX_005", "Crypto for Corn Subsidies", "Farm lobby proposes a stablecoin pegged to a bushel. Tech lobby proposes the opposite.") { Category = CardCategory.Normal, Rarity = CardRarity.Rare, Urgency = UrgencyLevel.Urgent, Choices = new List<CardChoice> { new(1, "Pilot 'CornCoin' in three states") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = +8, [ResourceType.Stability] = -6, [ResourceType.MediaTrust] = -2 }, Consequences = new List<string> { "Commodity memes invade markets", "Wheat asks for equal time", "Speculation season opens" }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Study it with Treasury & Ag") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = +3, [ResourceType.Stability] = +5, [ResourceType.MediaTrust] = +4 }, Consequences = new List<string> { "Whitepaper breaks the internet", "Pilot delayed pending charts", "Investors nap, farmers clap" }, Alignment = ChoiceAlignment.Cautious }, new(3, "Ban commodity coins as market manipulation") { Effects = new Dictionary<ResourceType, int> { [ResourceType.EconomicHealth] = -2, [ResourceType.Stability] = +7, [ResourceType.Popularity] = -4 }, Consequences = new List<string> { "Lobbyists switch to soy NFTs", "Courts schedule arguments", "Talk radio riots (peacefully)" }, Alignment = ChoiceAlignment.Aggressive } } };

    private DecisionCard CreateIguanaKingReturns() => new("EDX_006", "The Iguana King Returns", "A fringe pundit claims the Iguana King controls weather patterns—and your schedule.") { Category = CardCategory.Character, Rarity = CardRarity.Uncommon, Urgency = UrgencyLevel.Normal, CharacterId = "IGUANA_KING", Choices = new List<CardChoice> { new(1, "Invite them for 'climate talks'") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +5, [ResourceType.MediaTrust] = -6, [ResourceType.Stability] = -4 }, Consequences = new List<string> { "Forecast includes 'scaly overcast'", "Merch appears: 'Rain? Reptile.'", "Science teachers weep" }, Alignment = ChoiceAlignment.Chaotic, FollowupCardIds = new List<string> { "IGUANA_SUMMIT_001" } }, new(2, "Debunk publicly with NOAA") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = +7, [ResourceType.Stability] = +5, [ResourceType.Popularity] = -3 }, Consequences = new List<string> { "Conspiracy mutates immediately", "NOAA gains fan club", "Iguanas trend on PetTok" }, Alignment = ChoiceAlignment.Cautious }, new(3, "Appoint a 'Lizard Liaison'") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +2, [ResourceType.Stability] = -3 }, Consequences = new List<string> { "Lobby registers under 'herpetology'", "Congress requests heat lamps", "Mascots hatch plans" }, Alignment = ChoiceAlignment.Aggressive } } };

    private DecisionCard CreateAIBudgetRhymes() => new("EDX_007", "AI Writes the Budget (In Rhymes)", "Your staff fed the budget into a poetry model. It came back… surprisingly popular.") { Category = CardCategory.Scandal, Rarity = CardRarity.Rare, Urgency = UrgencyLevel.Critical, Choices = new List<CardChoice> { new(1, "Publish the rhyming budget") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +10, [ResourceType.MediaTrust] = -7, [ResourceType.Stability] = -6 }, Consequences = new List<string> { "Appropriations become open-mic night", "Auditors request meter and rhyme scheme", "Markets clap on beat" }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Quietly rewrite it in prose") { Effects = new Dictionary<ResourceType, int> { [ResourceType.MediaTrust] = +6, [ResourceType.Stability] = +6, [ResourceType.Popularity] = -4 }, Consequences = new List<string> { "Poets Union pickets", "Think tanks sigh with relief", "Late-night loses a bit" }, Alignment = ChoiceAlignment.Cautious }, new(3, "Blame autocorrect, fire the model") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = -2, [ResourceType.Stability] = +2 }, Consequences = new List<string> { "Model posts a diss-poem", "Ethics board requests a stanza", "RhymeGate hearings begin" }, Alignment = ChoiceAlignment.Aggressive } } };

    private DecisionCard CreateTimeLottery() => new("EDX_008", "Time Zone Lottery", "To boost morale, staff suggests rotating time zones weekly. Airlines send fruit baskets.") { Category = CardCategory.Absurd, Rarity = CardRarity.Uncommon, Urgency = UrgencyLevel.Normal, Choices = new List<CardChoice> { new(1, "Spin the clock—chaos with confetti") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +6, [ResourceType.Stability] = -10, [ResourceType.EconomicHealth] = -4 }, Consequences = new List<string> { "Meetings occur yesterday", "Server cron jobs unionize", "Stock bell rings whenever" }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Pilot optional 'Flex-Time Nation'") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.Popularity] = +2 }, Consequences = new List<string> { "Calendars add a 'Maybe' column", "Families eat brunch more", "IT writes a novella about time" }, Alignment = ChoiceAlignment.Cautious }, new(3, "Declare 'Standard Time Forever'") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +7, [ResourceType.Popularity] = -2 }, Consequences = new List<string> { "Sun feels judged", "Insomniacs protest at noon", "You sleep slightly better" }, Alignment = ChoiceAlignment.Aggressive } } };

    private DecisionCard CreateMascotRobotRecall() => new("EDX_009", "Mascot Robot Recall", "The national robot mascot keeps photobombing funerals with party mode.") { Category = CardCategory.Crisis, Rarity = CardRarity.Uncommon, Urgency = UrgencyLevel.Urgent, Choices = new List<CardChoice> { new(1, "Emergency patch—ship it live") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -5, [ResourceType.MediaTrust] = -4, [ResourceType.Popularity] = +3 }, Consequences = new List<string> { "Patch fixes party mode", "Accidentally adds disco", "Funeral turns rave" }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Full recall & apology tour") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +6, [ResourceType.MediaTrust] = +5, [ResourceType.Popularity] = -3 }, Consequences = new List<string> { "Robot bows respectfully", "PR costs spike", "Merch sales oddly rise" }, Alignment = ChoiceAlignment.Cautious }, new(3, "Blame a foreign firmware vendor") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -2, [ResourceType.Popularity] = +2, [ResourceType.MediaTrust] = -6 }, Consequences = new List<string> { "Diplomatic notes exchanged", "Developers post receipts", "Customs detain party hats" }, Alignment = ChoiceAlignment.Aggressive } } };

    private DecisionCard CreateExecutive45Again() => new("EDX_010", "Executive 45 Sends Advice (Again)", "A late-night fax machine whirs. Advice arrives in all caps and ketchup stains.") { Category = CardCategory.Character, Rarity = CardRarity.Rare, Urgency = UrgencyLevel.Normal, CharacterId = "EXECUTIVE_45", Choices = new List<CardChoice> { new(1, "Quote it verbatim at a rally") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +9, [ResourceType.MediaTrust] = -7, [ResourceType.Stability] = -5 }, Consequences = new List<string> { "Crowd goes wild", "Editors develop migraines", "Merch prints itself" }, Alignment = ChoiceAlignment.Chaotic }, new(2, "File it as 'historical correspondence'") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +6, [ResourceType.MediaTrust] = +4 }, Consequences = new List<string> { "Archivists ask for napkins", "Biographers camp outside", "Fax machine gains Secret Service name" }, Alignment = ChoiceAlignment.Cautious }, new(3, "Reply with a haiku") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +2, [ResourceType.MediaTrust] = +1 }, Consequences = new List<string> { "Poetry budget doubles", "Talk radio angry in 5-7-5", "Fax beeps in syllables" }, Alignment = ChoiceAlignment.Aggressive } } };

    private DecisionCard CreateFilibusterFundraiser() => new("EDX_011", "Filibuster-A-Thon Fundraiser", "A veteran senator launches a livestreamed speech marathon with donation pop-ups.") { Category = CardCategory.Character, Rarity = CardRarity.Uncommon, Urgency = UrgencyLevel.Normal, Choices = new List<CardChoice> { new(1, "Endorse it publicly") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +8, [ResourceType.MediaTrust] = -5, [ResourceType.Stability] = -3 }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Stay neutral") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5 } }, new(3, "Launch your own counter-stream") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +10, [ResourceType.Stability] = -8 }, Alignment = ChoiceAlignment.Aggressive } } };

    private DecisionCard CreateNationalSauceShortage() => new("EDX_012", "National Sauce Shortage", "A viral challenge consumes strategic ketchup reserves. Restaurants panic.") { Category = CardCategory.Crisis, Rarity = CardRarity.Common, Urgency = UrgencyLevel.Urgent, Choices = new List<CardChoice> { new(1, "Release emergency reserves") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +8, [ResourceType.Popularity] = +10 } }, new(2, "Ration condiments") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -10, [ResourceType.Popularity] = -15 } }, new(3, "Declare mustard the new standard") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = -12 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateWeatherBalloonPresser() => new("EDX_013", "Weather Balloon Press Conference", "Communications wants a balloon-themed presser. Intel begs you not to.") { Category = CardCategory.Scandal, Rarity = CardRarity.Common, Urgency = UrgencyLevel.Normal, Choices = new List<CardChoice> { new(1, "Do the balloon presser") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +7, [ResourceType.MediaTrust] = -8 }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Cancel it quietly") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.MediaTrust] = +3 } }, new(3, "Compromise: virtual balloons") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +3, [ResourceType.Stability] = +2 } } } };

    private DecisionCard CreateNationalMemeCouncil() => new("EDX_014", "National Meme Council", "Proposed agency to regulate meme density per capita. Think pieces ignite.") { Category = CardCategory.Absurd, Rarity = CardRarity.Uncommon, Urgency = UrgencyLevel.Normal, Choices = new List<CardChoice> { new(1, "Establish the council") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +12, [ResourceType.Stability] = -15, [ResourceType.MediaTrust] = -10 }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Reject as unconstitutional") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +8, [ResourceType.MediaTrust] = +5 } }, new(3, "Make it advisory only") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +5, [ResourceType.Stability] = -3 } } } };

    private DecisionCard CreateAIPatriotTest() => new("EDX_015", "AI Patriot Test", "Agencies test if chatbots recite the pledge correctly. Results… mixed.") { Category = CardCategory.Scandal, Rarity = CardRarity.Uncommon, Urgency = UrgencyLevel.Urgent, Choices = new List<CardChoice> { new(1, "Mandate AI patriotism training") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = -8, [ResourceType.Popularity] = +6 }, Alignment = ChoiceAlignment.Aggressive }, new(2, "Dismiss the tests") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +5, [ResourceType.MediaTrust] = +4 } }, new(3, "Require AI citizenship oaths") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +10, [ResourceType.Stability] = -12 }, Alignment = ChoiceAlignment.Chaotic } } };

    private DecisionCard CreateExecutiveHotTub() => new("EDX_016", "Budget Line Item: Executive Hot Tub", "Buried on page 942 is a heated controversy.") { Category = CardCategory.Scandal, Rarity = CardRarity.Rare, Urgency = UrgencyLevel.Normal, Choices = new List<CardChoice> { new(1, "Defend it as stress relief") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +8, [ResourceType.MediaTrust] = -12, [ResourceType.Stability] = -5 }, Alignment = ChoiceAlignment.Chaotic }, new(2, "Remove the line item") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Stability] = +6, [ResourceType.MediaTrust] = +8 } }, new(3, "Make it a public pool") { Effects = new Dictionary<ResourceType, int> { [ResourceType.Popularity] = +15, [ResourceType.Stability] = -8 }, Alignment = ChoiceAlignment.Chaotic } } };
}
