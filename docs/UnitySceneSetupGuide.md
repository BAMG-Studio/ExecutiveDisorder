# Unity Scene Setup Guide - Character Selection

## 🎯 Complete Scene Hierarchy

### Scene: CharacterSelection.unity

```
CharacterSelection
├── Canvas (Screen Space - Overlay)
│   ├── Background
│   │   └── Image (MainBGMorning.png, blurred)
│   │
│   ├── Header
│   │   ├── Title (TextMeshPro)
│   │   │   └── Text: "SELECT YOUR LEADER"
│   │   └── Subtitle (TextMeshPro)
│   │       └── Text: "Each character has unique strengths and weaknesses"
│   │
│   ├── CharacterScrollView (Scroll Rect)
│   │   ├── Viewport
│   │   │   └── Content (Grid Layout Group)
│   │   │       └── [CharacterCard Prefabs instantiated here]
│   │   └── Scrollbar Vertical
│   │
│   ├── SelectedCharacterPanel
│   │   ├── Background (Panel)
│   │   ├── Portrait (Image - Large)
│   │   ├── NameTitle (TextMeshPro)
│   │   ├── FullBio (TextMeshPro - Scrollable)
│   │   ├── StatsDisplay (TextMeshPro)
│   │   └── StartCampaignButton
│   │       └── Text: "START CAMPAIGN"
│   │
│   └── Footer
│       ├── BackButton
│       │   └── Text: "← BACK"
│       └── RandomButton
│           └── Text: "🎲 RANDOM"
│
├── Managers
│   ├── CharacterSelectionManager
│   ├── AICharacterGenerator
│   └── EventSystem
│
└── Audio
    └── AudioManager (DontDestroyOnLoad)
```

---

## 📦 Prefab: CharacterCard.prefab

### Hierarchy
```
CharacterCard (200x300)
├── Background (Image)
│   └── Color: Character.themeColor
│
├── PortraitMask (Image - CircleMask.png)
│   └── Portrait (Image)
│       └── Sprite: Character.portrait
│
├── NameText (TextMeshPro)
│   └── Text: Character.characterName
│
├── TitleText (TextMeshPro)
│   └── Text: Character.title
│
├── BioText (TextMeshPro)
│   └── Text: Character.shortBio
│
└── SelectButton (Button)
    └── Text: "SELECT"
```

### Component Setup
```
CharacterCard GameObject:
- RectTransform: Width 200, Height 300
- CharacterCard.cs script
- Layout Element: Min Width 200, Min Height 300

Background:
- Image: Color = Character.themeColor
- RectTransform: Stretch to fill parent

PortraitMask:
- Image: CircleMask.png
- Image Type: Simple
- Preserve Aspect: true
- RectTransform: Width 150, Height 150, Top anchor

Portrait (child of PortraitMask):
- Image: Character.portrait
- Mask: true (from parent)

SelectButton:
- Button component
- RectTransform: Bottom anchor, Width 180, Height 40
```

---

## 🎨 Canvas Setup

### Canvas Settings
```
Render Mode: Screen Space - Overlay
Canvas Scaler:
  - UI Scale Mode: Scale With Screen Size
  - Reference Resolution: 1920 x 1080
  - Match: 0.5 (Width/Height)
Graphic Raycaster: Default
```

### Background Setup
```
GameObject: Background
- Image Component
  - Source Image: MainBGMorning.png
  - Image Type: Simple
  - Color: White (or tinted)
- RectTransform: Stretch to fill canvas
- Material: UI/Default (or blur shader)
```

### Header Setup
```
GameObject: Header
- RectTransform: Top anchor, Height 150

Title (TextMeshPro):
- Font Size: 72
- Alignment: Center
- Color: White
- Font: Bold
- Text: "SELECT YOUR LEADER"

Subtitle (TextMeshPro):
- Font Size: 24
- Alignment: Center
- Color: Light Gray
- Text: "Each character has unique strengths and weaknesses"
```

### Character Scroll View Setup
```
GameObject: CharacterScrollView
- RectTransform: Center, Width 1400, Height 700
- Scroll Rect Component:
  - Content: Content GameObject
  - Horizontal: false
  - Vertical: true
  - Movement Type: Elastic
  - Scrollbar: Scrollbar Vertical

Content (child):
- RectTransform: Top anchor, Width 1400
- Grid Layout Group:
  - Cell Size: 220 x 320
  - Spacing: 20 x 20
  - Start Corner: Upper Left
  - Start Axis: Horizontal
  - Child Alignment: Upper Center
  - Constraint: Fixed Column Count = 5
- Content Size Fitter:
  - Vertical Fit: Preferred Size
```

### Selected Character Panel Setup
```
GameObject: SelectedCharacterPanel
- RectTransform: Right anchor, Width 500, Height 900
- Canvas Group: Alpha 1, Interactable true
- Image: Semi-transparent black background
- Initially: SetActive(false)

Portrait:
- RectTransform: Top center, Width 300, Height 300
- Image: Character.portrait
- Mask: CircleMask.png

NameTitle:
- Font Size: 48
- Alignment: Center
- Color: White

FullBio:
- Font Size: 18
- Alignment: Left
- Color: Light Gray
- RectTransform: Width 450, Height 200
- Scroll Rect (optional)

StatsDisplay:
- Font Size: 24
- Alignment: Left
- Color: Yellow
- Text: "★★★★☆ Popularity\n..."

StartCampaignButton:
- RectTransform: Bottom center, Width 400, Height 60
- Button: Colors = Green highlight
- Text: "START CAMPAIGN" (Font Size 32)
```

### Footer Setup
```
GameObject: Footer
- RectTransform: Bottom anchor, Height 80
- Horizontal Layout Group: Spacing 20

BackButton:
- Width 200, Height 60
- Button: Colors = Red tint
- Text: "← BACK"

RandomButton:
- Width 200, Height 60
- Button: Colors = Blue tint
- Text: "🎲 RANDOM"
```

---

## 🔧 Manager Setup

### CharacterSelectionManager GameObject
```
GameObject: CharacterSelectionManager
- CharacterSelectionManager.cs script

Inspector Fields:
- Character Grid Parent: Content (from scroll view)
- Character Card Prefab: CharacterCard prefab
- Selected Character Panel: SelectedCharacterPanel GameObject
- Start Campaign Button: StartCampaignButton
- Random Button: RandomButton
- Back Button: BackButton
- Selected Portrait: Portrait Image in panel
- Selected Name: NameTitle TextMeshPro
- Selected Bio: FullBio TextMeshPro
- Selected Stats: StatsDisplay TextMeshPro
- Character Database: CharacterDatabase ScriptableObject
- AI Generator: AICharacterGenerator GameObject
```

### AICharacterGenerator GameObject
```
GameObject: AICharacterGenerator
- AICharacterGenerator.cs script

Inspector Fields:
- API Base URL: "https://your-replit-url.repl.co"
```

---

## 🎵 Audio Setup

### AudioManager Integration
```
AudioManager should already exist (DontDestroyOnLoad)

Sounds to configure in AudioManager:
- RelaxingGuitar: Background music
- Stamp: Character selection
- Correct: Confirm selection
- Pop: Hover sound
```

---

## 📋 Step-by-Step Creation

### Step 1: Create Scene
```
1. File → New Scene
2. Save as: Assets/Scenes/CharacterSelection.unity
3. Delete default Main Camera (if using UI camera)
```

### Step 2: Create Canvas
```
1. GameObject → UI → Canvas
2. Add Canvas Scaler (should auto-add)
3. Configure as specified above
```

### Step 3: Create Background
```
1. Right-click Canvas → UI → Image
2. Rename to "Background"
3. Stretch to fill canvas
4. Assign MainBGMorning.png
```

### Step 4: Create Header
```
1. Right-click Canvas → Create Empty → "Header"
2. Add Title (TextMeshPro)
3. Add Subtitle (TextMeshPro)
4. Configure as specified
```

### Step 5: Create Character Scroll View
```
1. Right-click Canvas → UI → Scroll View
2. Rename to "CharacterScrollView"
3. Delete Horizontal Scrollbar
4. Configure Content with Grid Layout Group
5. Add Content Size Fitter to Content
```

### Step 6: Create CharacterCard Prefab
```
1. Create empty GameObject in scene
2. Add all child components as specified
3. Add CharacterCard.cs script
4. Drag to Assets/Prefabs/UI/
5. Delete from scene
```

### Step 7: Create Selected Character Panel
```
1. Right-click Canvas → UI → Panel
2. Rename to "SelectedCharacterPanel"
3. Add all child components
4. Position on right side
5. SetActive(false) in Inspector
```

### Step 8: Create Footer
```
1. Right-click Canvas → Create Empty → "Footer"
2. Add Horizontal Layout Group
3. Add BackButton and RandomButton
4. Configure as specified
```

### Step 9: Create Managers
```
1. Create empty GameObject → "Managers"
2. Add CharacterSelectionManager.cs
3. Add AICharacterGenerator.cs
4. Assign all references in Inspector
```

### Step 10: Test
```
1. Create CharacterDatabase ScriptableObject
2. Create 2-3 test characters
3. Assign to CharacterSelectionManager
4. Press Play
5. Verify cards appear and selection works
```

---

## 🎨 Visual Polish

### Animations (Optional)
```
Card Hover: Scale 1.0 → 1.05 (0.2s)
Card Select: Punch scale effect
Panel Show: Slide in from right (0.3s)
Panel Hide: Slide out to right (0.3s)
```

### Particle Effects (Optional)
```
Selection: Confetti burst
Hover: Subtle glow
Confirm: Star burst
```

### Shaders (Optional)
```
Background: Blur shader
Cards: Rim lighting
Portrait: Circular mask with glow
```

---

## ✅ Testing Checklist

- [ ] Scene loads without errors
- [ ] Canvas scales properly on different resolutions
- [ ] Character cards instantiate correctly
- [ ] Scroll view works smoothly
- [ ] Card hover plays sound and scales
- [ ] Card selection updates panel
- [ ] Selected panel shows correct data
- [ ] Star ratings display correctly
- [ ] Start Campaign button works
- [ ] Random button selects random character
- [ ] Back button returns to main menu
- [ ] Background music plays
- [ ] All sounds trigger correctly
- [ ] AI portrait generation works (if enabled)

---

## 🐛 Common Issues

### Cards don't appear
- Check CharacterDatabase is assigned
- Verify CharacterCard prefab is assigned
- Check Content has Grid Layout Group

### Scroll view doesn't work
- Verify Content Size Fitter on Content
- Check Scroll Rect component settings
- Ensure Content is child of Viewport

### Sounds don't play
- Verify AudioManager exists in scene
- Check sounds are configured in AudioManager
- Ensure AudioManager.Instance is not null

### AI generation fails
- Check API URL is correct
- Verify backend is running
- Check Unity console for errors
- Test with AIBackendTester first
