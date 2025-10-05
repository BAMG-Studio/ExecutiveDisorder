# Executive Disorder - Game Design Document

## 🎮 Game Overview

**Title:** Executive Disorder™  
**Genre:** Political Satire, Decision-Making, Card Game  
**Platform:** PC, Mac, WebGL  
**Engine:** Unity 6  
**Target Audience:** Ages 16+, fans of satirical games, political humor

**Elevator Pitch:**  
*"Manage a nation through absurd political chaos. Balance four critical resources, navigate 100+ satirical decision cards, and try to survive 100 days without triggering nuclear war or alien invasion. Democracy optional, chaos guaranteed."*

---

## 📖 Core Concept

Players take on the role of a newly elected leader thrust into a world of political absurdity. Every day presents new decisions through a card-based system. Each choice affects four core resources: Popularity, Stability, Media Trust, and Economic Health.

The game satirizes modern politics through exaggerated characters, ridiculous scenarios, and unexpected consequences. No choice is straightforward, and the "right" answer is often the most absurd one.

---

## 🎯 Core Gameplay Loop

```
1. Day Starts
   ↓
2. Decision Card Presented
   ↓
3. Player Chooses Option (2-4 choices)
   ↓
4. Resources Change Based on Choice
   ↓
5. Character Reactions & Dialogue
   ↓
6. News Headline Generated
   ↓
7. Random Events (20% chance)
   ↓
8. Check Game Over Conditions
   ↓
9. Advance to Next Day or End Game
```

---

## 💰 Resources System

### Four Core Resources (0-100%)

**1. Popularity (👥)**
- **Description:** Public approval rating
- **Critical Low (<20%):** Protests, impeachment threats
- **Critical High (>80%):** Cult of personality, excessive power
- **Game Over:** 0% = Impeachment, 100% = Autocratic takeover

**2. Stability (🏛️)**
- **Description:** Government/institutional integrity
- **Critical Low (<20%):** Civil unrest, institutional collapse
- **Critical High (>80%):** Authoritarian control, loss of freedoms
- **Game Over:** 0% = Nuclear winter/chaos, 100% = Military state

**3. Media Trust (📺)**
- **Description:** Press relations and information control
- **Critical Low (<20%):** Fake news epidemic, no credible reporting
- **Critical High (>80%):** State-controlled propaganda
- **Game Over:** 0% = Media revolution, 100% = Total censorship

**4. Economic Health (💰)**
- **Description:** National economic indicators
- **Critical Low (<20%):** Recession, mass unemployment
- **Critical High (>80%):** Economic bubble, unsustainable growth
- **Game Over:** 0% = Economic collapse, 100% = Corporate takeover

### Resource Interactions

- Low Popularity → Decreases Stability
- Low Economic Health → Decreases Popularity
- Low Media Trust → Affects all resources negatively
- Resources cascade and influence each other daily

---

## 🃏 Card System

### Card Categories

**1. Normal Cards (60%)**
- Standard policy decisions
- Examples: "Budget Allocation," "Foreign Relations," "Healthcare Reform"

**2. Crisis Cards (15%)**
- Urgent situations requiring immediate action
- Timed decisions (30 seconds)
- Higher resource impacts
- Examples: "Nuclear Threat," "Pandemic Outbreak," "Economic Crash"

**3. Scandal Cards (10%)**
- Political controversies and embarrassments
- Damage control situations
- Examples: "Tax Records Leaked," "Inappropriate Tweet," "Affair Revealed"

**4. Character Cards (10%)**
- Character-specific events
- Tied to character loyalty and relationships
- Examples: "Rex Demands Recognition," "POTUS-9000 Malfunction"

**5. Absurd Cards (5%)**
- Satirical, ridiculous scenarios
- High entertainment value
- Unpredictable outcomes
- Examples: "Alien Contact," "Time Travel Offer," "Reality TV Presidency"

### Card Structure

Each card contains:
- **Title:** Brief, punchy headline
- **Description:** Situation explanation (2-3 sentences)
- **Choices:** 2-4 options with varying impacts
- **Consequences:** Resource changes, character reactions, follow-up events
- **Rarity:** Common, Uncommon, Rare, Epic, Legendary

### Card Effects

**Resource Effects:**
- Small: ±5-10%
- Medium: ±15-25%
- Large: ±30-50%
- Extreme: ±60%+

**Character Effects:**
- Loyalty changes: ±5-20 points
- Character activation/deactivation
- Relationship shifts

**Narrative Effects:**
- Unlock follow-up cards
- Trigger special events
- Generate news headlines

---

## 👥 Character System

### 8 Political Archetypes

**1. Rex Scaleston III - "The Iguana King"**
- **Archetype:** Conspiracy Theorist Monarch
- **Personality:** Paranoid, eccentric, believes in lizard people
- **Special Ability:** Crisis Escalation - Makes every situation dramatic
- **Dialogue Style:** Wild theories, dramatic proclamations
- **Starting Loyalty:** 50

**2. Donald J. Executive - "The 45th"**
- **Archetype:** Former Everything
- **Personality:** Superlative-obsessed, brash, confident
- **Special Ability:** Media Manipulation - Shifts media narratives
- **Dialogue Style:** "Very tremendous," "Nobody knows more than me"
- **Starting Loyalty:** 45

**3. POTUS-9000 - "Mascot Bot"**
- **Archetype:** Sentient AI Assistant
- **Personality:** Logical but glitchy, learning humor badly
- **Special Ability:** Algorithmic Solutions - Optimal but inhuman choices
- **Dialogue Style:** [PROCESSING], Binary jokes, meme references
- **Starting Loyalty:** 60

**4. Alexandria Sanders-Warren - "The Progressive"**
- **Archetype:** Idealistic Reformer
- **Personality:** Passionate, principled, occasionally naive
- **Special Ability:** Popular Support - Boosts popularity on reform
- **Dialogue Style:** Earnest, policy-focused, hopeful
- **Starting Loyalty:** 55

**5. Richard M. Moneybags III - "Corporate Lobbyist"**
- **Archetype:** Business Synergist
- **Personality:** Smooth-talking, profit-driven, pragmatic
- **Special Ability:** Economic Boost - Improves economy at other costs
- **Dialogue Style:** "Win-win," "Synergy," corporate buzzwords
- **Starting Loyalty:** 50

**6. General James 'Ironside' Steel - "Military Hawk"**
- **Archetype:** Force Advocate
- **Personality:** Disciplined, hawkish, solutions through strength
- **Special Ability:** Stability Through Force - Increases stability
- **Dialogue Style:** Military jargon, decisive, no-nonsense
- **Starting Loyalty:** 50

**7. Diana Newsworthy - "Media Mogul"**
- **Archetype:** Narrative Controller
- **Personality:** Charismatic, influential, agenda-driven
- **Special Ability:** Spin Control - Manipulates media perception
- **Dialogue Style:** Headlines, soundbites, dramatic framing
- **Starting Loyalty:** 50

**8. Johnny Q. Public - "The Populist"**
- **Archetype:** Voice of "The People"
- **Personality:** Down-to-earth, reactive, inconsistent
- **Special Ability:** Public Mood - Reflects current popularity
- **Dialogue Style:** Folksy, emotional, contradictory
- **Starting Loyalty:** 50

### Character Mechanics

**Loyalty System (0-100)**
- **0-30:** Hostile - Undermines player, negative events
- **31-69:** Neutral - Standard interactions
- **70-100:** Loyal - Supports player, positive events

**Character Interactions:**
- **Allies:** Support each other's loyalty
- **Rivals:** Decrease each other's loyalty
- **Special Relationships:** Unique dynamics between specific characters

**Character Events:**
- Triggered when loyalty crosses thresholds
- Character-specific cards appear
- Can leave or betray player at 0 loyalty

---

## 🏆 Endings System

### 10 Unique Endings

**1. Democratic Victory**
- **Trigger:** All resources balanced (40-60%) at day 100
- **Type:** Victory
- **Description:** Democracy survives, you're a decent leader

**2. Autocratic Empire**
- **Trigger:** Stability hits 100%
- **Type:** Neutral
- **Description:** Total control achieved, at what cost?

**3. Economic Collapse**
- **Trigger:** Economic Health hits 0%
- **Type:** Defeat
- **Description:** The economy crashed, nation in ruins

**4. Nuclear Winter**
- **Trigger:** Stability hits 0%
- **Type:** Defeat
- **Description:** Someone pressed the button

**5. Alien Overlords**
- **Trigger:** Accept alien treaty in absurd card sequence
- **Type:** Absurd
- **Description:** Earth joins Intergalactic Federation

**6. Impeachment**
- **Trigger:** Popularity hits 0%
- **Type:** Defeat
- **Description:** Removed from office in disgrace

**7. Military Coup**
- **Trigger:** All characters at <30 loyalty
- **Type:** Defeat
- **Description:** The military takes control

**8. Media Revolution**
- **Trigger:** Media Trust hits 0%
- **Type:** Neutral
- **Description:** The press controls everything now

**9. Chaos Reigns**
- **Trigger:** Survive 100 days with mixed resources
- **Type:** Neutral
- **Description:** Somehow you made it, barely

**10. Time Loop Paradox**
- **Trigger:** Secret condition - accept time travel card 3 times
- **Type:** Secret
- **Description:** Stuck repeating the same 100 days forever

---

## 🎨 Art Direction

### Visual Style
- **UI:** Clean, modern, minimalist with political poster aesthetics
- **Characters:** Stylized portraits, exaggerated features, satirical design
- **Cards:** Bold graphics, newspaper-style layouts
- **Colors:** Red, white, blue base with dramatic accents

### Color Coding
- **Popularity:** Blue (#3498db)
- **Stability:** Green (#2ecc71)
- **Media Trust:** Purple (#9b59b6)
- **Economic Health:** Gold (#f39c12)
- **Crisis:** Red (#e74c3c)

---

## 🔊 Audio Design

### Music
- **Main Menu:** Patriotic orchestral with ironic twist
- **Gameplay:** Subtle, tension-building background
- **Crisis:** Dramatic, urgent music
- **Ending:** Varies by ending type

### Sound Effects
- **Card Flip:** Satisfying paper sound
- **Button Click:** Clean, responsive
- **Resource Gain:** Positive chime
- **Resource Loss:** Warning buzz
- **Crisis Alert:** Urgent alarm
- **Character Voice:** Brief audio stingers per character

---

## 🎯 Player Experience Goals

1. **Laugh:** Satirical humor throughout
2. **Think:** Meaningful choices with consequences
3. **Stress:** Tension from resource management
4. **Surprise:** Unexpected outcomes and absurd scenarios
5. **Replay:** Multiple endings and paths encourage replays

---

## 📊 Progression & Replayability

### Replay Incentives
- 10 different endings to discover
- Unlock all character storylines
- Secret cards and events
- High score/survival challenges
- Different difficulty levels

### Meta-Progression
- Unlock new starting scenarios
- Character bios and lore
- Achievement system
- Statistics tracking

---

## 🎓 Accessibility & Difficulty

### Difficulty Modes
- **Easy:** Forgiving resource changes, more time for crises
- **Normal:** Balanced challenge
- **Hard:** Harsh penalties, frequent crises
- **Chaos:** Extreme difficulty, RNG chaos

### Accessibility
- Colorblind modes for resources
- Text size options
- Skip animations option
- Pause anytime
- Save/load system

---

**Version:** 1.0  
**Last Updated:** October 2025  
**Status:** Complete Design
