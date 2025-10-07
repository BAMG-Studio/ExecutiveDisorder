# Asset Usage Map - Visual Guide

## 📊 Asset Inventory Summary

**Total Visual Assets**: 40 PNG files
**Total Audio Assets**: 22 MP3 files
**Character Portraits**: 2 (need 8 more)
**Background Assets**: 5 (complete time-of-day system)
**Resource Icons**: 5 (all 4 resources + decision delay)

---

## 🎨 Visual Assets by Category

### Character Assets (2 Complete, 8 Needed)

#### ✅ Existing Portraits
```
MadamCashPortrait.png
├─ Use for: Dr. Evelyn Technocrat or custom character
├─ Format: PNG with transparency
└─ Recommended: 512x512 or higher

TheTechnocratPortrait.png
├─ Use for: Senator Marcus Tradition or custom character
├─ Format: PNG with transparency
└─ Recommended: 512x512 or higher

MadamCashFullbody.png
├─ Use for: Full character display in dialogue scenes
└─ Apply: FullBodyMask.png for consistent framing

TheTechnocrat.png
├─ Use for: Full character display in dialogue scenes
└─ Apply: FullBodyMask.png for consistent framing
```

#### ⚠️ Portraits Needed (Generate with AI)
```
1. Rex Scaleston III - The Iguana King
2. Donald J. Executive - The 45th
3. POTUS-9000 - The AI President
4. Alexandria Sanders-Warren - The Progressive
5. Richard M. Moneybags III - The Corporate Lobbyist
6. General James 'Ironside' Steel - The Military Hawk
7. Diana Newsworthy - The Media Mogul
8. Johnny Q. Public - The Populist
```

---

### Background System (Complete! ✅)

#### Time-of-Day Backgrounds
```
MainBGMorning.png
├─ Use: Character selection, early game (Day 1-3)
├─ Mood: Fresh, optimistic, new beginnings
└─ Lighting: Bright, warm sunrise tones

MainBGAfternoon.png
├─ Use: Mid-game gameplay (Day 4-7)
├─ Mood: Active, busy, peak activity
└─ Lighting: Bright, neutral daylight

MainBGNight.png
├─ Use: Late game, crisis moments, endings
├─ Mood: Tense, dramatic, reflective
└─ Lighting: Dark, moody, artificial lights

MainBGDesk.png
├─ Use: Foreground layer for all gameplay scenes
├─ Layer: Place over time-of-day backgrounds
└─ Contains: Desk, papers, office items

MainBGNoDesk.png
├─ Use: Main menu, character selection (no desk needed)
├─ Alternative: Clean background without foreground
└─ Mood: Professional, clean, focused
```

#### Background Layering System
```
Layer 1 (Back):   MainBGMorning/Afternoon/Night.png
Layer 2 (Front):  MainBGDesk.png
Layer 3 (UI):     Canvas with game UI

Result: Dynamic time-of-day system with consistent desk
```

---

### UI Icons (Complete! ✅)

#### Resource Icons (4 Core Resources)
```
PopularityIcon.png
├─ Represents: 👥 Public approval rating
├─ Use: Resource bar, card effects, HUD
├─ Color: Blue/Purple tones
└─ Size: 64x64 recommended

StabilityIcon.png
├─ Represents: 🏛️ Government/institutional stability
├─ Use: Resource bar, card effects, HUD
├─ Color: Gray/Stone tones
└─ Size: 64x64 recommended

MediaIcon.png
├─ Represents: 📺 Press relations and media trust
├─ Use: Resource bar, card effects, HUD
├─ Color: Red/TV tones
└─ Size: 64x64 recommended

EconomicIcon.png
├─ Represents: 💰 Economic health and prosperity
├─ Use: Resource bar, card effects, HUD
├─ Color: Gold/Green tones
└─ Size: 64x64 recommended

DecisionDelayIcon.png
├─ Represents: ⏰ Delayed consequences
├─ Use: Card effects, timeline indicators
└─ Size: 64x64 recommended
```

#### Directional Indicators
```
UpArrow.png / UpArrowClear.png
├─ Use: Positive resource changes (+5, +10, +15)
├─ Color: Green (solid) or white (clear)
└─ Animation: Float up with fade

DownArrow.png / DownArrowClear.png
├─ Use: Negative resource changes (-5, -10, -15)
├─ Color: Red (solid) or white (clear)
└─ Animation: Float down with fade
```

---

### UI Elements & Masks

#### Masking System
```
CircleMask.png
├─ Use: Character portraits (circular frame)
├─ Apply to: All character selection cards
├─ Size: 256x256 recommended
└─ Effect: Professional, consistent look

FullBodyMask.png
├─ Use: Full character sprites in dialogue
├─ Apply to: Character full-body images
└─ Effect: Consistent framing for all characters
```

#### UI Components
```
Clipboard.png
├─ Use: Decision card background
├─ Style: Official document aesthetic
└─ Layer: Behind card text and choices

Clock.png
├─ Use: Time/urgency indicator
├─ Placement: Top-right of crisis cards
└─ Animation: Pulse for urgent decisions

Warning.png
├─ Use: Crisis alerts, critical decisions
├─ Color: Red/Yellow warning colors
└─ Animation: Flash or pulse effect

folder.png
├─ Use: File/document icons
└─ Context: Archive, records, history

Incognito.png
├─ Use: Secret decisions, hidden content
└─ Context: Classified information, mysteries
```

#### Shapes & Utilities
```
Circle.png / WhiteCircle.png
├─ Use: UI backgrounds, buttons, indicators
└─ Versatile: Can be tinted any color

EmojiOne.png
├─ Use: Emoji reactions, feedback
└─ Context: Social media, public reactions
```

---

### News & Crisis Assets

```
CrisisNews.png
├─ Use: Crisis announcement overlay
├─ Placement: Full screen or panel
└─ Style: Breaking news aesthetic

NewsCrisisScreenHolder.png
├─ Use: News broadcast template
├─ Contains: TV frame, news desk layout
└─ Layer: Behind news text and graphics

FakeNewsPlaceHolder.png
├─ Use: Fake news mini-game
├─ Context: Media manipulation mechanics
└─ Style: Satirical news article

NationwideOutRage.png
├─ Use: Major crisis event graphic
├─ Context: Protests, public unrest
└─ Impact: High-stakes decision moment

PigeonUprisingImminent.png
├─ Use: Absurd crisis event (satirical)
├─ Context: Humorous/surreal scenarios
└─ Tone: Comedy relief moment

CrowdHappy.png
├─ Use: Positive public reaction
├─ Context: Victory, high popularity
└─ Animation: Celebration effects
```

---

### Placeholder Assets

```
placeholderpresi.png
├─ Use: Temporary character sprite
└─ Replace: With final character art

PlaceholderccButterfly.png
├─ Use: Temporary character sprite
└─ Replace: With final character art

Ironfist presido.png
├─ Use: Military character concept
└─ Potential: General James 'Ironside' Steel
```

---

## 🎵 Audio Assets by Category

### Background Music (3 tracks)

```
relaxing-guitar-loop-v5-245859.mp3
├─ Use: Character selection, main menu
├─ Mood: Calm, contemplative, welcoming
├─ Loop: Yes
└─ Volume: 0.6-0.8

newsreportmusic-6242.mp3
├─ Use: Crisis mode, news segments
├─ Mood: Urgent, professional, tense
├─ Loop: Yes
└─ Volume: 0.7-0.9

park-6026.mp3
├─ Use: Peaceful moments, low-stress gameplay
├─ Mood: Relaxing, ambient, nature
├─ Loop: Yes
└─ Volume: 0.5-0.7
```

### UI Sound Effects (5 sounds)

```
stamp-81635.mp3
├─ Use: Decision approval, character selection
├─ Context: Official action, confirmation
└─ Volume: 0.8

correct-6033.mp3
├─ Use: Correct choice, positive feedback
├─ Context: Good decision, success
└─ Volume: 0.7

wrong-answer-126515.mp3
├─ Use: Bad choice, negative feedback
├─ Context: Poor decision, failure
└─ Volume: 0.7

menu-change-89197.mp3
├─ Use: Menu navigation, card flip
├─ Context: UI transitions
└─ Volume: 0.5

party-balloon-pop-323588.mp3
├─ Use: Hover sound, light interaction
├─ Context: Subtle feedback
└─ Volume: 0.3
```

### Crowd Reactions (3 sounds)

```
applause-236785.mp3
├─ Use: Positive public reaction
├─ Context: High popularity, good decision
└─ Volume: 0.8

boo-36556.mp3
├─ Use: Negative public reaction
├─ Context: Low popularity, bad decision
└─ Volume: 0.8

crowd-noise-284490.mp3
├─ Use: General public ambience
├─ Context: Background atmosphere
└─ Volume: 0.4 (ambient)
```

### Crisis/Alert Sounds (4 sounds)

```
hong-kong-eas-alarm-391340.mp3
├─ Use: Major crisis alert
├─ Context: Emergency broadcast system
└─ Volume: 0.9 (attention-grabbing)

dirty-siren-40635.mp3
├─ Use: Police/emergency siren
├─ Context: Law enforcement crisis
└─ Volume: 0.8

alien-alert-noise-287332.mp3
├─ Use: Absurd/sci-fi crisis
├─ Context: POTUS-9000 events, alien scenarios
└─ Volume: 0.7

alarm-301729.mp3
├─ Use: General alarm/warning
├─ Context: Time running out, urgent decision
└─ Volume: 0.8
```

### Ambient Sounds (3 sounds)

```
busy-town-square-48608.mp3
├─ Use: Urban ambience, high activity
├─ Context: Busy gameplay moments
├─ Loop: Yes
└─ Volume: 0.4

park-6026.mp3
├─ Use: Peaceful ambience
├─ Context: Calm gameplay moments
├─ Loop: Yes
└─ Volume: 0.3

relaxing-guitar-loop-v5-245859.mp3
├─ Use: Background music/ambience
├─ Context: Character selection, menus
├─ Loop: Yes
└─ Volume: 0.6
```

### Media Sounds (3 sounds)

```
tv-opening-news-logo-154230.mp3
├─ Use: News segment intro
├─ Context: Media events, news ticker
└─ Volume: 0.7

tv-shutdown-386167.mp3
├─ Use: TV power off, end of broadcast
├─ Context: Scene transitions, media blackout
└─ Volume: 0.6

newsreportmusic-6242.mp3
├─ Use: News broadcast background
├─ Context: Crisis coverage, media focus
└─ Volume: 0.7
```

### Celebration Sounds (2 sounds)

```
party-horn-68443.mp3
├─ Use: Victory, positive ending
├─ Context: Win condition, celebration
└─ Volume: 0.8

party-balloon-pop-323588.mp3
├─ Use: Small victories, hover feedback
├─ Context: Subtle positive feedback
└─ Volume: 0.3-0.5
```

### Misc Effects (3 sounds)

```
walking-up-stairs-86304.mp3
├─ Use: Scene transitions, progression
├─ Context: Moving to next day/phase
└─ Volume: 0.6

cuckoo-clock-359874.mp3
├─ Use: Time passing, day change
├─ Context: Clock ticking, deadline
└─ Volume: 0.5

glitch-sfx-312910.mp3
├─ Use: Error, system failure, bad ending
├─ Context: POTUS-9000 malfunction, chaos
└─ Volume: 0.7
```

---

## 🎯 Asset Usage by Scene

### Main Menu Scene
```
Visual:
- MainBGNoDesk.png (background)
- zzExecutive Disorder Initial logo.png (logo)

Audio:
- relaxing-guitar-loop-v5-245859.mp3 (music)
- menu-change-89197.mp3 (navigation)
```

### Character Selection Scene
```
Visual:
- MainBGMorning.png (background, blurred)
- CircleMask.png (portrait frames)
- All character portraits
- Resource icons (for stat display)

Audio:
- relaxing-guitar-loop-v5-245859.mp3 (music)
- party-balloon-pop-323588.mp3 (hover)
- stamp-81635.mp3 (selection)
- correct-6033.mp3 (confirmation)
```

### Gameplay Scene (Morning)
```
Visual:
- MainBGMorning.png + MainBGDesk.png (layered)
- All resource icons (HUD)
- Clipboard.png (card background)
- UpArrow/DownArrow (resource changes)

Audio:
- park-6026.mp3 or busy-town-square-48608.mp3 (ambience)
- stamp-81635.mp3 (decisions)
- applause-236785.mp3 / boo-36556.mp3 (reactions)
```

### Gameplay Scene (Crisis Mode)
```
Visual:
- CrisisNews.png (overlay)
- Warning.png (alert icon)
- NewsCrisisScreenHolder.png (news frame)

Audio:
- hong-kong-eas-alarm-391340.mp3 (alert)
- newsreportmusic-6242.mp3 (background)
- dirty-siren-40635.mp3 (emergency)
```

### Ending Scene
```
Visual:
- MainBGNight.png (dramatic background)
- CrowdHappy.png (victory) or NationwideOutRage.png (defeat)

Audio:
- party-horn-68443.mp3 (victory)
- glitch-sfx-312910.mp3 (defeat)
```

---

## ✅ Asset Readiness Checklist

### Ready to Use Immediately
- [x] 5 background images (complete time-of-day system)
- [x] 5 resource icons (all 4 resources + delay)
- [x] 4 directional arrows (resource feedback)
- [x] 2 character portraits (can use for 2 characters)
- [x] 22 audio files (complete sound library)
- [x] UI masks and shapes
- [x] News/crisis graphics

### Need to Generate (AI)
- [ ] 8 character portraits (via DALL-E 3)
- [ ] 10 character voice lines (via ElevenLabs)
- [ ] Additional ending-specific backgrounds (optional)

### Recommended Organization
```
Current: unity/Assets/Art/ (all PNG files)
Better:  unity/Assets/Art/Characters/
         unity/Assets/Art/Backgrounds/
         unity/Assets/Art/UI/Icons/
         unity/Assets/Art/UI/Masks/
         unity/Assets/Art/News/

Current: unity/Assets/Audio/Sounds/ (all MP3 files)
Better:  unity/Assets/Audio/Music/
         unity/Assets/Audio/SFX/UI/
         unity/Assets/Audio/SFX/Reactions/
         unity/Assets/Audio/SFX/Alerts/
         unity/Assets/Audio/Ambience/
```

---

## 🎨 Asset Quality Notes

### Image Formats
✅ All PNG (perfect for Unity, supports transparency)
✅ Suitable for UI and 2D sprites
✅ Can be imported as Sprite (2D and UI)

### Audio Formats
✅ All MP3 (good compression, Unity compatible)
✅ Suitable for both music and SFX
✅ Can be imported as AudioClip

### Recommendations
- Character portraits: Import as Sprite (2D and UI), 512x512 or higher
- Background images: Import as Sprite (2D and UI), original resolution
- UI icons: Import as Sprite (2D and UI), 64x64 or 128x128
- Music: Import as AudioClip, Load Type: Streaming
- SFX: Import as AudioClip, Load Type: Decompress On Load

---

## 🚀 Next Steps

1. **Organize assets** into subfolders (optional but recommended)
2. **Generate missing portraits** using AI backend
3. **Configure import settings** for optimal performance
4. **Create sprite atlases** for UI elements (optimization)
5. **Setup audio mixer** for volume control
6. **Test all assets** in Unity scenes

Your asset library is comprehensive and production-ready! 🎉
