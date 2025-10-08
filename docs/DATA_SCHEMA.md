# Data Schema

Defines the authoring format (JSON) imported into ScriptableObjects under `Assets/Game/Generated`.

Files live in `unity/Assets/Game/Data/` and are optional; importer skips missing files.

## cards.json

{
  "cards": [
    {
      "id": "string (unique)",
      "name": "string",
      "description": "string",
      "cost": 0-10,
      "rarity": "common|uncommon|rare|legendary",
      "tags": ["string"],
      "effects": [
        { "type": "ApprovalDelta|EconomyDelta|PanicDelta|AbsurdityDelta|ReputationDelta", "value": number, "withTag": "string?", "target": "string?" }
      ],
      "artKey": "string (addressable/resources key)"
    }
  ]
}

## leaders.json

{
  "leaders": [
    {
      "id": "string (unique)",
      "name": "string",
      "bio": "string",
      "traitTags": ["string"],
      "startingDeck": ["card-id"],
      "portraitKey": "string"
    }
  ]
}

## factions.json

{
  "factions": [
    {
      "id": "string (unique)",
      "name": "string",
      "description": "string",
      "color": "#RRGGBB or #RRGGBBAA",
      "tags": ["string"]
    }
  ]
}

## crises.json

{
  "crises": [
    {
      "id": "string (unique)",
      "name": "string",
      "description": "string",
      "severity": 1-5,
      "tags": ["string"],
      "effects": [ { "type": "...", "value": number } ],
      "next": ["crisis-id"]
    }
  ]
}

## Import Process

- JSON → ScriptableObjects saved to `Assets/Game/Generated/{Cards|Leaders|Factions|Crises}`
- Aggregated `GameDatabase.asset` references all generated assets.
- Run via menu: `Codex/Data/Import All` or CLI: `-executeMethod CodexDataImporter.ImportAll`
- Build automatically calls a safe check: `CodexDataImporter.RunImportIfNeeded()`

## Generation (Stub)

- `tools/gen_content.py` creates deterministic starter content.
- Wrappers: `scripts/gen-content.sh` and `scripts/gen-content.ps1`
- Typical usage:
  - `bash scripts/gen-content.sh --seed 123`
  - `powershell -File scripts/gen-content.ps1 -Seed 123`

