# Data Pipeline Documentation - Executive Disorder

## 📋 Overview

The data pipeline transforms YAML content definitions into Unity assets through an automated generation workflow.

## 🏗️ Architecture

```
theme.json ──▶ gen_content.py ──▶ YAML (data/)
        │               │
        │               └──▶ JSON aggregates (unity/Assets/Game/Data/)
        │                                   │
        └───────────────────────────────────┘
                                            ▼
                                 CodexDataImporter (Unity)
                                            ▼
                                ScriptableObjects + GameDatabase
                                            ▼
                            Addressables (auto-labeled when available)
```

## 📁 Directory Structure

```
data/
├── cards/           # Policy card definitions
├── leaders/         # Leader archetype definitions
├── crises/          # Crisis event definitions
└── factions/        # Faction group definitions

tools/
├── gen_content.py   # Generate YAML from theme
├── gen_images.py    # Generate card art, portraits
└── gen_audio.py     # Generate music, SFX, VO

unity/Assets/
├── Art/Generated/   # AI-generated images
├── Audio/Generated/ # AI-generated audio
└── Data/           # ScriptableObjects
```

## 🎯 Content Schema

### Card Schema (`data/cards/*.yaml`)

```yaml
id: "unique_card_id"
name: "Display Name"
description: "Card description"
cost: 1-5
rarity: "common" | "uncommon" | "rare" | "legendary"
tags: ["policy", "absurd", ...]
effects:
  - type: "ApprovalDelta"
    value: -5
synergies:
  - withTag: "memes"
    bonusAbsurdity: 2
artStyle: "satirical_poster_v1"
artPrompt: "Detailed prompt for image generation"
sfxKey: "policy_slam"
voLine: "Voice line text"
```

### Leader Schema (`data/leaders/*.yaml`)

```yaml
id: "unique_leader_id"
name: "Leader Name"
title: "The Title"
archetype: "absurdist" | "authoritarian" | ...
baseStats:
  approval: 45
  economy: 40
  absurdity: 85
ability:
  name: "Ability Name"
  description: "What it does"
  effects: [...]
startingDeck: ["card_id_1", "card_id_2", ...]
portraitStyle: "professional_portrait"
portraitPrompt: "Detailed prompt"
voiceStyle: "deep_gravelly"
theme_music: "leader_theme_01"
```

### Crisis Schema (`data/crises/*.yaml`)

```yaml
id: "unique_crisis_id"
name: "Crisis Name"
type: "minor" | "major" | "catastrophic"
category: "economic" | "social" | ...
headline: "Breaking News Headline"
triggerConditions:
  turns: {min: 5, max: 15}
  requiredCards: ["card_id"]
  statThresholds:
    absurdity: {min: 60}
options:
  - id: "option_a"
    text: "Response A"
    effects: [...]
chainEvents:
  - triggeredBy: "option_a"
    nextCrisis: "follow_up_crisis"
    delay: 2
newsImage: "crisis_photo"
imagePrompt: "News photo prompt"
```

### Faction Schema (`data/factions/*.yaml`)

```yaml
id: "unique_faction_id"
name: "Faction Name"
type: "influence_group"
baseInfluence: 60
volatility: 8
mechanics:
  reactions:
    - cardTag: "scandal"
      influenceDelta: 15
  influenceEffects:
    - threshold: 80
      effect: "faction_support"
      bonus: {approvalMultiplier: 1.1}
unlockableCards: ["card_id_1", ...]
iconStyle: "icon_style"
iconPrompt: "Icon generation prompt"
```

## 🔄 Workflow

### 1. Generate Content Data

```bash
python3 tools/gen_content.py theme.json
```

Creates YAML files in `data/` based on `theme.json` configuration **and** JSON
aggregations under `unity/Assets/Game/Data/` for the Unity importer. Use
`--seed` to reproduce deterministic batches.

### 2. Generate Assets

```bash
# Images
export IMG_PROVIDER=mock  # or 'local' or 'api'
python3 tools/gen_images.py

# Audio
export AUDIO_PROVIDER=mock  # or 'local' or 'api'
python3 tools/gen_audio.py
```

### 3. Unity Import

Open Unity → `Codex/Data/Import All`

Or via CLI:
```bash
"/path/to/Unity" -batchmode -nographics \
  -projectPath unity \
  -executeMethod CodexDataImporter.ImportAll \
  -quit -logFile -
```

### 4. Build

```bash
bash scripts/codex-workflow.sh
```

Full pipeline: generate → import → build → commit → push

## 🎨 Art Generation

### Providers

- **mock**: Creates placeholder JSON (dev mode)
- **local**: ComfyUI or Stable Diffusion locally
- **api**: DALL-E, Midjourney, etc.

### Style Tokens

Defined in `theme.json`:
- `satirical_poster_v1`: 1950s propaganda style
- `professional_portrait`: Formal portrait photo
- `dramatic_news_photo`: Photojournalism style

### Prompts

Stored in YAML `artPrompt` field:
```yaml
artPrompt: "1950s propaganda poster style, 'NO BIRDS' sign with dramatic eagle silhouette crossed out, red and black color scheme"
```

Style suffix auto-appended from `theme.json`.

## 🎵 Audio Generation

### Providers

- **mock**: Creates placeholder JSON
- **local**: AudioCraft, MusicGen, Coqui TTS
- **api**: ElevenLabs, AIVA, Suno

### Types

1. **Music**: Theme loops (120s)
2. **SFX**: Short impacts (1-3s)
3. **Voice**: Leader/narrator lines (5-15s)

### Configuration

```yaml
# In leader/card YAML
theme_music: "leader_theme_01"
sfxKey: "policy_slam"
voLine: "No more birds!"
voiceStyle: "deep_gravelly"
```

## 🔧 Theme Configuration

`theme.json` controls all generation:

```json
{
  "content_counts": {
    "cards": 100,
    "leaders": 10,
    "crises": 30,
    "factions": 8
  },
  "art_direction": {
    "card_style": "satirical_poster_v1",
    "palette": ["#1a1a2e", "#e94560", ...]
  },
  "generation_config": {
    "providers": {
      "image": "mock",
      "audio": "mock"
    }
  }
}
```

## 🚀 Automation

### Via Codex Workflow

```bash
bash scripts/codex-workflow.sh
```

Steps:
1. Generate content (Python)
2. Generate assets (Python)
3. Import to Unity (auto-run during build)
4. Build WebGL
5. Commit & push

### CI/CD Integration

`.github/workflows/codex-build.yml` (TODO):
```yaml
- name: Generate Content
  run: python3 tools/gen_content.py theme.json

- name: Build Unity
  run: bash scripts/codex-build-webgl.sh
```

## 📊 Asset Manifest

Generated assets tracked in `artifacts/asset-manifest.json`:

```json
{
  "images": [
    {
      "id": "ban_all_birds",
      "path": "unity/Assets/Art/Generated/ban_all_birds.png",
      "style": "satirical_poster_v1",
      "prompt": "...",
      "provider": "local",
      "license": "CC-BY-4.0"
    }
  ],
  "audio": [...]
}
```

## 🔐 API Keys

Set via environment variables:

```bash
export IMG_API_KEY="your_dalle_key"
export AUDIO_API_KEY="your_elevenlabs_key"
export GITHUB_TOKEN="your_github_token"
```

Or in `theme.json` with placeholders:
```json
"api_keys": {
  "dalle": "${DALLE_API_KEY}"
}
```

## 📝 Examples

### Example: Generate Bird Crisis Card

1. Create `data/cards/ban_birds.yaml`:
```yaml
id: "ban_all_birds"
name: "Ban All Birds"
artPrompt: "1950s poster, NO BIRDS sign, dramatic eagle crossed out"
```

2. Generate assets:
```bash
python3 tools/gen_images.py
```

3. Import to Unity:
```bash
unity-editor -executeMethod ContentImporter.ImportCards
```

4. Card now available as ScriptableObject!

## 🐛 Troubleshooting

### Images not generating?
- Check `IMG_PROVIDER` env var
- Verify prompts in YAML
- Check `tools/gen_images.py` logs

### Audio missing?
- Check `AUDIO_PROVIDER` env var
- Verify audio fields in YAML
- Use `mock` mode for testing

### Unity import fails?
- Validate YAML/JSON schema via `dotnet run --project ExecutiveDisorder.Game`
- Check Unity console for exceptions
- Ensure `unity/Assets/Game/Data/*.json` files exist (run `scripts/gen-content.sh`)

## 🧱 Scene Scaffolding & Gameplay Stub

- `unity/Assets/Editor/SceneScaffolder.cs` provisions three scenes:
  - `Boot.unity` (loads `GameDatabase` from `Resources/Generated`).
  - `MainMenu.unity` (UGUI leader selection, card grid, start/quit buttons).
  - `Gameplay.unity` (minimal turn runner to sanity-check data effects).
- Generate or refresh scenes and Build Settings via:

  ```bash
  bash scripts/codex-init.sh
  ```

- Scenes are added to Build Settings automatically (Boot → MainMenu → Gameplay).

## 🧪 CLI Validation Harness

- `ExecutiveDisorder.Game/Program.cs` validates the JSON aggregates before Unity
  runs. Execute:

  ```bash
  dotnet run --project ExecutiveDisorder.Game
  ```

- Checks for duplicate IDs, missing card references, and prints content counts.

## 🎯 Next Steps

1. ✅ Schema defined
2. ✅ Theme-driven generators (mock mode)
3. ✅ Unity importer + Addressables labeling
4. ✅ Scene scaffolding + gameplay smoke test
5. ⏳ Hook real image/audio providers
6. ⏳ CI/CD automation (GitHub Actions)

---

**Codex is ready to generate content!** 🚀

Type **"update"** to trigger the full pipeline.
