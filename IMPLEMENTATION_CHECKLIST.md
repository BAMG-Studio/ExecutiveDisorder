# ✅ Implementation Checklist - Character Selection System

## 📋 Overview

This checklist guides you through implementing the complete character selection system. Check off items as you complete them.

**Estimated Total Time**: 2-3 hours

---

## Phase 1: Understanding (30 minutes)

### Documentation Review
- [ ] Read `QUICKSTART.md` (5 min)
- [ ] Read `docs/ImplementationSummary.md` (10 min)
- [ ] Review `docs/AssetInventoryAnalysis.md` (15 min)

### Asset Verification
- [ ] Verify 40 PNG images exist in `unity/Assets/Art/`
- [ ] Verify 22 MP3 files exist in `unity/Assets/Audio/Sounds/`
- [ ] Confirm 2 character portraits present (MadamCash, TheTechnocrat)
- [ ] Confirm 5 background images present (time-of-day system)
- [ ] Confirm 5 resource icons present

**Phase 1 Complete**: ☐ You understand the scope and have verified assets

---

## Phase 2: Character Setup (30 minutes)

### Create Character ScriptableObjects
- [ ] Open Unity Editor
- [ ] Create folder: `Assets/ScriptableObjects/Characters/`
- [ ] Right-click → Create → Executive Disorder → Political Character

### Character 1: Rex Scaleston III
- [ ] Create asset "RexScalestonIII"
- [ ] Fill in: Name, Title, Bios
- [ ] Set stats: Pop 70, Stab 40, Media 60, Econ 50
- [ ] Set theme color: #228B22 (Forest Green)
- [ ] Add special abilities (3)
- [ ] Set AI portrait prompt
- [ ] Enable useAIPortrait

### Character 2: Donald J. Executive
- [ ] Create asset "DonaldJExecutive"
- [ ] Fill in: Name, Title, Bios
- [ ] Set stats: Pop 60, Stab 30, Media 20, Econ 80
- [ ] Set theme color: #DAA520 (Goldenrod)
- [ ] Add special abilities (3)
- [ ] Set AI portrait prompt
- [ ] Enable useAIPortrait

### Character 3: POTUS-9000
- [ ] Create asset "POTUS9000"
- [ ] Fill in: Name, Title, Bios
- [ ] Set stats: Pop 50, Stab 90, Media 70, Econ 60
- [ ] Set theme color: #4682B4 (Steel Blue)
- [ ] Add special abilities (3)
- [ ] Set AI portrait prompt
- [ ] Enable useAIPortrait

### Character 4: Alexandria Sanders-Warren
- [ ] Create asset "AlexandriaSandersWarren"
- [ ] Fill in: Name, Title, Bios
- [ ] Set stats: Pop 65, Stab 55, Media 80, Econ 40
- [ ] Set theme color: #8A2BE2 (Blue Violet)
- [ ] Add special abilities (3)
- [ ] Set AI portrait prompt
- [ ] Enable useAIPortrait

### Character 5: Richard M. Moneybags III
- [ ] Create asset "RichardMoneybags"
- [ ] Fill in: Name, Title, Bios
- [ ] Set stats: Pop 30, Stab 60, Media 40, Econ 95
- [ ] Set theme color: #B8860B (Dark Goldenrod)
- [ ] Add special abilities (3)
- [ ] Set AI portrait prompt
- [ ] Enable useAIPortrait

### Character 6: General James 'Ironside' Steel
- [ ] Create asset "GeneralSteel"
- [ ] Fill in: Name, Title, Bios
- [ ] Set stats: Pop 45, Stab 85, Media 50, Econ 55
- [ ] Set theme color: #696969 (Dim Gray)
- [ ] Add special abilities (3)
- [ ] Set AI portrait prompt
- [ ] Enable useAIPortrait

### Character 7: Diana Newsworthy
- [ ] Create asset "DianaNewsworthy"
- [ ] Fill in: Name, Title, Bios
- [ ] Set stats: Pop 55, Stab 50, Media 95, Econ 60
- [ ] Set theme color: #DC143C (Crimson)
- [ ] Add special abilities (3)
- [ ] Set AI portrait prompt
- [ ] Enable useAIPortrait

### Character 8: Johnny Q. Public
- [ ] Create asset "JohnnyQPublic"
- [ ] Fill in: Name, Title, Bios
- [ ] Set stats: Pop 85, Stab 35, Media 45, Econ 50
- [ ] Set theme color: #B22222 (Firebrick)
- [ ] Add special abilities (3)
- [ ] Set AI portrait prompt
- [ ] Enable useAIPortrait

### Character 9: Dr. Evelyn Technocrat
- [ ] Create asset "DrTechnocrat"
- [ ] Fill in: Name, Title, Bios
- [ ] Set stats: Pop 40, Stab 70, Media 60, Econ 75
- [ ] Set theme color: #483D8B (Dark Slate Blue)
- [ ] Add special abilities (3)
- [ ] Assign existing portrait: TheTechnocratPortrait.png
- [ ] Disable useAIPortrait (already have portrait)

### Character 10: Senator Marcus Tradition
- [ ] Create asset "SenatorTradition"
- [ ] Fill in: Name, Title, Bios
- [ ] Set stats: Pop 50, Stab 80, Media 55, Econ 65
- [ ] Set theme color: #191970 (Midnight Blue)
- [ ] Add special abilities (3)
- [ ] Assign existing portrait: MadamCashPortrait.png (or generate new)
- [ ] Set useAIPortrait based on choice

### Create Character Database
- [ ] Create folder: `Assets/ScriptableObjects/`
- [ ] Right-click → Create → Executive Disorder → Character Database
- [ ] Name it "CharacterDatabase"
- [ ] Drag all 10 character assets into "All Characters" list
- [ ] Verify all 10 characters are in the list

**Phase 2 Complete**: ☐ All 10 characters created and added to database

---

## Phase 3: AI Backend Testing (15 minutes)

### Setup Test Scene
- [ ] Create new scene: "AIBackendTest"
- [ ] Create empty GameObject: "AIBackendTester"
- [ ] Add component: AIBackendTester.cs
- [ ] Update API Base URL in Inspector

### Create Test UI
- [ ] Add Canvas to scene
- [ ] Add Button: "Test Health"
- [ ] Add Button: "Test Portrait"
- [ ] Add Button: "Test Voice"
- [ ] Add TextMeshPro: "Status Text"
- [ ] Add TextMeshPro: "Results Text"
- [ ] Add Image: "Test Image" (for portrait display)

### Assign References
- [ ] Assign Status Text to AIBackendTester
- [ ] Assign Results Text to AIBackendTester
- [ ] Assign Test Image to AIBackendTester
- [ ] Assign buttons to respective test methods

### Run Tests
- [ ] Press Play
- [ ] Click "Test Health" → Verify backend is running
- [ ] Click "Test Portrait" → Verify image generates
- [ ] Click "Test Voice" → Verify audio plays
- [ ] Check Console for any errors
- [ ] Note: Each test may take 10-30 seconds

**Phase 3 Complete**: ☐ AI backend tested and working

---

## Phase 4: Scene Building (45 minutes)

### Create Scene
- [ ] File → New Scene
- [ ] Save as: `Assets/Scenes/CharacterSelection.unity`
- [ ] Delete default Main Camera (if using UI camera)

### Setup Canvas
- [ ] GameObject → UI → Canvas
- [ ] Canvas: Render Mode = Screen Space - Overlay
- [ ] Add Canvas Scaler component
- [ ] Canvas Scaler: UI Scale Mode = Scale With Screen Size
- [ ] Canvas Scaler: Reference Resolution = 1920 x 1080
- [ ] Canvas Scaler: Match = 0.5

### Create Background
- [ ] Right-click Canvas → UI → Image
- [ ] Rename to "Background"
- [ ] RectTransform: Stretch to fill canvas (Anchor: Stretch/Stretch)
- [ ] Image: Source = MainBGMorning.png
- [ ] Image: Color = White (or tinted for blur effect)

### Create Header
- [ ] Right-click Canvas → Create Empty → "Header"
- [ ] RectTransform: Top anchor, Height 150
- [ ] Add child: TextMeshPro → "Title"
- [ ] Title: Text = "SELECT YOUR LEADER"
- [ ] Title: Font Size = 72, Alignment = Center, Color = White
- [ ] Add child: TextMeshPro → "Subtitle"
- [ ] Subtitle: Text = "Each character has unique strengths and weaknesses"
- [ ] Subtitle: Font Size = 24, Alignment = Center, Color = Light Gray

### Create Character Scroll View
- [ ] Right-click Canvas → UI → Scroll View
- [ ] Rename to "CharacterScrollView"
- [ ] RectTransform: Center, Width 1400, Height 700
- [ ] Delete Horizontal Scrollbar
- [ ] Scroll Rect: Horizontal = false, Vertical = true

### Configure Content
- [ ] Select Content (child of Viewport)
- [ ] RectTransform: Top anchor, Width 1400
- [ ] Add component: Grid Layout Group
- [ ] Grid: Cell Size = 220 x 320
- [ ] Grid: Spacing = 20 x 20
- [ ] Grid: Start Corner = Upper Left
- [ ] Grid: Start Axis = Horizontal
- [ ] Grid: Constraint = Fixed Column Count = 5
- [ ] Add component: Content Size Fitter
- [ ] Content Size Fitter: Vertical Fit = Preferred Size

### Create CharacterCard Prefab
- [ ] Create empty GameObject in scene: "CharacterCard"
- [ ] RectTransform: Width 200, Height 300
- [ ] Add component: CharacterCard.cs
- [ ] Add component: Layout Element (Min Width 200, Min Height 300)

#### CharacterCard Children
- [ ] Add child: Image → "Background"
- [ ] Background: Stretch to fill, Color = White
- [ ] Add child: Image → "PortraitMask"
- [ ] PortraitMask: Source = CircleMask.png, Width 150, Height 150
- [ ] Add child of PortraitMask: Image → "Portrait"
- [ ] Portrait: Masked by parent
- [ ] Add child: TextMeshPro → "NameText"
- [ ] NameText: Font Size = 24, Alignment = Center
- [ ] Add child: TextMeshPro → "TitleText"
- [ ] TitleText: Font Size = 18, Alignment = Center
- [ ] Add child: TextMeshPro → "BioText"
- [ ] BioText: Font Size = 14, Alignment = Left, Height 80
- [ ] Add child: Button → "SelectButton"
- [ ] SelectButton: Bottom anchor, Width 180, Height 40
- [ ] SelectButton child: TextMeshPro → "SELECT"

#### Assign CharacterCard References
- [ ] CharacterCard.cs: Assign Portrait Image
- [ ] CharacterCard.cs: Assign NameText
- [ ] CharacterCard.cs: Assign TitleText
- [ ] CharacterCard.cs: Assign BioText
- [ ] CharacterCard.cs: Assign SelectButton
- [ ] CharacterCard.cs: Assign Background Image

#### Save Prefab
- [ ] Drag CharacterCard to `Assets/Prefabs/UI/`
- [ ] Delete CharacterCard from scene

### Create Selected Character Panel
- [ ] Right-click Canvas → UI → Panel
- [ ] Rename to "SelectedCharacterPanel"
- [ ] RectTransform: Right anchor, Width 500, Height 900
- [ ] Image: Color = Semi-transparent black (0, 0, 0, 200)
- [ ] Add component: Canvas Group
- [ ] Canvas Group: Alpha = 1, Interactable = true
- [ ] Inspector: SetActive = false (initially hidden)

#### Selected Panel Children
- [ ] Add child: Image → "Portrait"
- [ ] Portrait: Top center, Width 300, Height 300
- [ ] Portrait: Mask = CircleMask.png
- [ ] Add child: TextMeshPro → "NameTitle"
- [ ] NameTitle: Font Size = 48, Alignment = Center
- [ ] Add child: TextMeshPro → "FullBio"
- [ ] FullBio: Font Size = 18, Width 450, Height 200
- [ ] Add child: TextMeshPro → "StatsDisplay"
- [ ] StatsDisplay: Font Size = 24, Color = Yellow
- [ ] Add child: Button → "StartCampaignButton"
- [ ] StartCampaignButton: Bottom center, Width 400, Height 60
- [ ] StartCampaignButton: Color = Green tint
- [ ] Button child: TextMeshPro → "START CAMPAIGN"

### Create Footer
- [ ] Right-click Canvas → Create Empty → "Footer"
- [ ] RectTransform: Bottom anchor, Height 80
- [ ] Add component: Horizontal Layout Group
- [ ] Horizontal Layout: Spacing = 20, Child Alignment = Middle Center
- [ ] Add child: Button → "BackButton"
- [ ] BackButton: Width 200, Height 60, Color = Red tint
- [ ] BackButton child: TextMeshPro → "← BACK"
- [ ] Add child: Button → "RandomButton"
- [ ] RandomButton: Width 200, Height 60, Color = Blue tint
- [ ] RandomButton child: TextMeshPro → "🎲 RANDOM"

### Create Managers
- [ ] Create empty GameObject: "Managers"
- [ ] Add component: CharacterSelectionManager.cs
- [ ] Add component: AICharacterGenerator.cs

### Assign Manager References
- [ ] CharacterSelectionManager: Character Grid Parent = Content
- [ ] CharacterSelectionManager: Character Card Prefab = CharacterCard prefab
- [ ] CharacterSelectionManager: Selected Character Panel = SelectedCharacterPanel
- [ ] CharacterSelectionManager: Start Campaign Button = StartCampaignButton
- [ ] CharacterSelectionManager: Random Button = RandomButton
- [ ] CharacterSelectionManager: Back Button = BackButton
- [ ] CharacterSelectionManager: Selected Portrait = Portrait in panel
- [ ] CharacterSelectionManager: Selected Name = NameTitle
- [ ] CharacterSelectionManager: Selected Bio = FullBio
- [ ] CharacterSelectionManager: Selected Stats = StatsDisplay
- [ ] CharacterSelectionManager: Character Database = CharacterDatabase asset
- [ ] CharacterSelectionManager: AI Generator = AICharacterGenerator component

### Configure AI Generator
- [ ] AICharacterGenerator: API Base URL = Your Replit URL

### Add Event System
- [ ] Verify EventSystem exists in scene (auto-created with Canvas)
- [ ] If missing: GameObject → UI → Event System

**Phase 4 Complete**: ☐ Character Selection scene built and configured

---

## Phase 5: Testing (15 minutes)

### Initial Test
- [ ] Press Play in Unity Editor
- [ ] Verify 10 character cards appear
- [ ] Verify cards are in 5x2 grid layout
- [ ] Verify scroll view works
- [ ] Check Console for errors

### Interaction Test
- [ ] Hover over character card → Verify pop sound plays
- [ ] Hover over character card → Verify card scales up
- [ ] Click character card → Verify stamp sound plays
- [ ] Click character card → Verify detail panel appears
- [ ] Verify character name, bio, and stats display correctly
- [ ] Verify star ratings show correctly

### Button Test
- [ ] Click "Random" button → Verify random character selected
- [ ] Click "Start Campaign" button → Verify scene transition (or log message)
- [ ] Click "Back" button → Verify scene transition (or log message)

### Audio Test
- [ ] Verify background music plays on scene start
- [ ] Verify music crossfades smoothly
- [ ] Verify all sound effects play correctly
- [ ] Verify volume levels are appropriate

**Phase 5 Complete**: ☐ Scene tested and working in Play mode

---

## Phase 6: AI Portrait Generation (30 minutes)

### Prepare for Generation
- [ ] Ensure Flask backend is running
- [ ] Verify API URL is correct in AICharacterGenerator
- [ ] Verify internet connection is stable
- [ ] Have patience: Each portrait takes 10-30 seconds

### Generate Portraits
- [ ] Press Play in Character Selection scene
- [ ] Wait for AI generation to complete (watch Console)
- [ ] Verify portraits appear on character cards
- [ ] Check quality of generated portraits

### Save Generated Portraits
- [ ] For each generated portrait:
  - [ ] Right-click texture in Project window
  - [ ] Export as PNG
  - [ ] Save to `Assets/Art/Characters/`
  - [ ] Rename appropriately (e.g., "RexScalestonPortrait.png")
  - [ ] Assign saved sprite to character asset
  - [ ] Disable useAIPortrait for that character

### Verify All Portraits
- [ ] Rex Scaleston III has portrait
- [ ] Donald J. Executive has portrait
- [ ] POTUS-9000 has portrait
- [ ] Alexandria Sanders-Warren has portrait
- [ ] Richard M. Moneybags III has portrait
- [ ] General James 'Ironside' Steel has portrait
- [ ] Diana Newsworthy has portrait
- [ ] Johnny Q. Public has portrait
- [ ] Dr. Evelyn Technocrat has portrait
- [ ] Senator Marcus Tradition has portrait

**Phase 6 Complete**: ☐ All 10 characters have portraits

---

## Phase 7: System Integration (30 minutes)

### StateManager Integration
- [ ] Open StateManager.cs
- [ ] Add "Characters" to AppState enum
- [ ] Save StateManager.cs

### Create State Controller (Optional)
- [ ] Create CharactersStateController.cs (if using state pattern)
- [ ] Implement Enter() and Exit() methods
- [ ] Register with StateManager

### Menu Transitions
- [ ] Open MainMenu scene
- [ ] Find "Start Game" or "New Game" button
- [ ] Update button to call: StateManager.Instance.SwitchState(AppState.Characters)
- [ ] Test transition from MainMenu to CharacterSelection

### Character to Gameplay Transition
- [ ] Verify StartCampaignButton calls StateManager.Instance.SwitchState(AppState.GameStart)
- [ ] Test transition from CharacterSelection to GameStart

### GameStateManager Integration (If exists)
- [ ] Create method: SetSelectedCharacter(PoliticalCharacter character)
- [ ] Store character name, starting stats
- [ ] Call from CharacterSelectionManager.OnStartCampaignClicked()

**Phase 7 Complete**: ☐ Character selection integrated with game flow

---

## Phase 8: Polish & Optimization (Optional)

### Visual Polish
- [ ] Add card hover animation (scale + glow)
- [ ] Add panel slide-in animation
- [ ] Add particle effects on selection
- [ ] Add background blur effect
- [ ] Add character theme color highlights

### Audio Polish
- [ ] Generate character voice lines (ElevenLabs)
- [ ] Add voice playback on character selection
- [ ] Fine-tune volume levels
- [ ] Add audio ducking for voice lines

### Performance Optimization
- [ ] Create sprite atlas for UI elements
- [ ] Optimize portrait texture sizes
- [ ] Pool character cards (if needed)
- [ ] Profile scene performance

### UX Improvements
- [ ] Add character ability tooltips
- [ ] Add stat comparison view
- [ ] Add character preview animations
- [ ] Add loading indicator for AI generation

**Phase 8 Complete**: ☐ Scene polished and optimized

---

## Final Verification

### Functionality
- [ ] All 10 characters display correctly
- [ ] Character selection works smoothly
- [ ] Audio feedback is satisfying
- [ ] Scene transitions work
- [ ] No console errors
- [ ] Performance is smooth (60 FPS)

### Assets
- [ ] All portraits are high quality
- [ ] All audio files play correctly
- [ ] All UI elements display properly
- [ ] Background system works

### Integration
- [ ] StateManager transitions work
- [ ] GameStateManager stores character data
- [ ] Full game flow tested (Menu → Select → Gameplay)

### Documentation
- [ ] Code is commented
- [ ] Scene is organized
- [ ] Prefabs are properly named
- [ ] Assets are organized

---

## 🎉 Implementation Complete!

### What You've Built
✅ Complete character selection system
✅ 10 unique political characters
✅ AI-generated portraits
✅ Full audio integration
✅ Smooth scene transitions
✅ Production-ready code

### Next Steps
1. Build additional game scenes (Gameplay, Endings)
2. Implement decision card system
3. Create resource management UI
4. Add save/load functionality
5. Polish and playtest

---

## 📊 Time Tracking

| Phase | Estimated | Actual | Notes |
|-------|-----------|--------|-------|
| Phase 1: Understanding | 30 min | ___ min | |
| Phase 2: Character Setup | 30 min | ___ min | |
| Phase 3: AI Testing | 15 min | ___ min | |
| Phase 4: Scene Building | 45 min | ___ min | |
| Phase 5: Testing | 15 min | ___ min | |
| Phase 6: AI Generation | 30 min | ___ min | |
| Phase 7: Integration | 30 min | ___ min | |
| Phase 8: Polish | Optional | ___ min | |
| **Total** | **2-3 hours** | **___ hours** | |

---

## 🐛 Troubleshooting

### Issue: Characters don't appear
- [ ] Verify CharacterDatabase is assigned
- [ ] Check CharacterCard prefab is assigned
- [ ] Verify Content has Grid Layout Group

### Issue: AI generation fails
- [ ] Test with AIBackendTester first
- [ ] Verify API URL is correct
- [ ] Check backend is running
- [ ] Check internet connection

### Issue: Sounds don't play
- [ ] Verify AudioManager exists
- [ ] Check sounds are configured in AudioManager
- [ ] Verify AudioManager.Instance is not null

### Issue: Scene looks wrong
- [ ] Check Canvas Scaler settings
- [ ] Verify RectTransform anchors
- [ ] Check Grid Layout Group settings

---

**Congratulations on completing the implementation! 🎉**
