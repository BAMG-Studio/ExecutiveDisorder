#!/usr/bin/env python3
"""Theme-driven content generator for Executive Disorder.

Reads a theme JSON file (optional), produces YAML authoring files under
`data/` and companion JSON aggregations for the Unity importer under
`unity/Assets/Game/Data/`.
"""

import argparse
import json
import random
import string
from dataclasses import dataclass
from pathlib import Path
from typing import Dict, List, Optional, Tuple

import yaml

ROOT = Path(__file__).resolve().parents[1]
YAML_ROOT = ROOT / "data"
JSON_ROOT = ROOT / "unity" / "Assets" / "Game" / "Data"


DEFAULT_THEME = {
    "season": "default",
    "tone": "satirical",
    "difficulty": "medium",
    "content_counts": {
        "cards": 20,
        "leaders": 4,
        "crises": 10,
        "factions": 4,
    },
    "themes": {
        "primary": "absurd_political_decisions",
        "secondary": ["media_spin", "financial_scandal", "public_panic"],
        "satire_level": "medium",
    },
    "art_direction": {
        "card_style": "satirical_poster_v1",
        "leader_style": "professional_portrait",
        "crisis_style": "dramatic_news_photo",
        "palette": ["#1A1A2E", "#16213E", "#0F3460", "#E94560", "#F1F1F1"],
    },
    "audio_direction": {
        "music_style": "dramatic_orchestral_satire",
        "sfx_style": "impactful_political",
        "voice_style": "authoritative_satirical",
    },
}


CARD_EFFECTS = [
    "ApprovalDelta",
    "EconomyDelta",
    "AbsurdityDelta",
    "ReputationDelta",
    "PanicDelta",
]

CRISIS_TYPES = [
    ("minor", "political"),
    ("major", "economic"),
    ("major", "social"),
    ("catastrophic", "international"),
    ("minor", "media"),
]

LEADER_ARCHETYPES = [
    ("Spinmaster", ["spin", "media"]),
    ("Crisis Broker", ["crisis", "diplomacy"]),
    ("Meme Monarch", ["memes", "absurd"]),
    ("Iron Bureaucrat", ["order", "economy"]),
    ("Chaos Conductor", ["chaos", "panic"]),
    ("Budget Overlord", ["economy", "math"]),
]

FACTION_TYPES = [
    ("Deep Bureaucracy", ["order", "paperwork"]),
    ("Meme Syndicate", ["memes", "viral"]),
    ("Shadow Lobbies", ["economy", "influence"]),
    ("Crisis Cabal", ["crisis", "panic"]),
    ("Spin Doctors", ["media", "spin"]),
    ("International League", ["diplomacy", "reputation"]),
    ("Grassroots Choir", ["approval", "populism"]),
    ("Absurdity Cult", ["absurd", "spectacle"]),
    ("Algorithm Guild", ["tech", "memes"]),
    ("Satire Syndicate", ["satire", "media"]),
]


@dataclass
class Theme:
    counts: Dict[str, int]
    primary_theme: str
    secondary_themes: List[str]
    satire_level: str
    card_style: str
    leader_style: str
    crisis_style: str
    palette: List[str]
    music_style: str
    sfx_style: str
    voice_style: str
    tone: str


def load_theme(theme_path: Optional[Path]) -> Theme:
    data = DEFAULT_THEME
    if theme_path and theme_path.exists():
        with open(theme_path, "r", encoding="utf-8") as f:
            data = json.load(f)

    counts = data.get("content_counts", {})
    art = data.get("art_direction", {})
    audio = data.get("audio_direction", {})
    themes = data.get("themes", {})

    return Theme(
        counts={
            "cards": int(counts.get("cards", DEFAULT_THEME["content_counts"]["cards"])),
            "leaders": int(counts.get("leaders", DEFAULT_THEME["content_counts"]["leaders"])),
            "crises": int(counts.get("crises", DEFAULT_THEME["content_counts"]["crises"])),
            "factions": int(counts.get("factions", DEFAULT_THEME["content_counts"]["factions"])),
        },
        primary_theme=str(themes.get("primary", DEFAULT_THEME["themes"]["primary"])),
        secondary_themes=list(themes.get("secondary", DEFAULT_THEME["themes"]["secondary"])),
        satire_level=str(themes.get("satire_level", DEFAULT_THEME["themes"]["satire_level"])),
        card_style=str(art.get("card_style", DEFAULT_THEME["art_direction"]["card_style"])),
        leader_style=str(art.get("leader_style", DEFAULT_THEME["art_direction"]["leader_style"])),
        crisis_style=str(art.get("crisis_style", DEFAULT_THEME["art_direction"]["crisis_style"])),
        palette=list(art.get("palette", DEFAULT_THEME["art_direction"]["palette"])),
        music_style=str(audio.get("music_style", DEFAULT_THEME["audio_direction"]["music_style"])),
        sfx_style=str(audio.get("sfx_style", DEFAULT_THEME["audio_direction"]["sfx_style"])),
        voice_style=str(audio.get("voice_style", DEFAULT_THEME["audio_direction"]["voice_style"])),
        tone=str(data.get("tone", DEFAULT_THEME["tone"])),
    )


def slugify(name: str) -> str:
    slug = name.lower().replace("'", "")
    slug = "".join(ch if ch.isalnum() else "_" for ch in slug)
    while "__" in slug:
        slug = slug.replace("__", "_")
    return slug.strip("_")


def cleanup_generated_yaml():
    for category in ["cards", "leaders", "crises", "factions"]:
        directory = YAML_ROOT / category
        directory.mkdir(parents=True, exist_ok=True)
        for path in directory.glob("generated_*.yaml"):
            path.unlink()


def save_yaml(path: Path, payload: Dict):
    path.parent.mkdir(parents=True, exist_ok=True)
    with open(path, "w", encoding="utf-8") as f:
        yaml.safe_dump(payload, f, sort_keys=False, allow_unicode=False)


def write_json(path: Path, payload: Dict):
    path.parent.mkdir(parents=True, exist_ok=True)
    with open(path, "w", encoding="utf-8") as f:
        json.dump(payload, f, indent=2)


def choose_palette_color(theme: Theme, rng: random.Random, index: int) -> str:
    if not theme.palette:
        return "#CCCCCC"
    return theme.palette[index % len(theme.palette)]


def generate_card_name(theme: Theme, rng: random.Random, used: set) -> Tuple[str, str]:
    verbs = ["Ban", "Privatize", "Tax", "Mandate", "Gamify", "Streamline", "Crowdsource", "Nationalize", "Outlaw", "Celebrate"]
    subjects = [
        "Birds", "Oxygen", "Memes", "Reality", "Hands", "Dance Battles", "Pigeons", "Conspiracies", "Bureaucracy", "Inflation"
    ]
    modifiers = ["Forever", "for Votes", "for Clout", "with Jazz", "on Tuesdays", "Underwater", "via Polls", "at Scale", "for Charity"]

    while True:
        name = f"{rng.choice(verbs)} {rng.choice(subjects)} {rng.choice(modifiers)}"
        if name not in used:
            used.add(name)
            return name, slugify(f"card_{name}")


def generate_cards(theme: Theme, rng: random.Random) -> Tuple[List[Dict], List[Dict]]:
    count = theme.counts["cards"]
    used_names = set()
    yaml_cards = []
    json_cards = []

    for idx in range(count):
        name, slug = generate_card_name(theme, rng, used_names)
        rarity = rng.choices(["common", "uncommon", "rare", "legendary"], weights=[60, 25, 12, 3])[0]
        cost = rng.randint(0, 4)
        tag_pool = [theme.primary_theme] + theme.secondary_themes + ["absurd", "policy", "media", "economy", "panic"]
        tags = sorted(set(rng.sample(tag_pool, k=min(3, len(tag_pool)))))

        effects = []
        for effect_type in rng.sample(CARD_EFFECTS, k=rng.randint(1, 3)):
            magnitude = rng.randint(2, 9)
            sign = rng.choice([-1, 1])
            effects.append({"type": effect_type, "value": sign * magnitude})

        synergies = []
        if rng.random() < 0.35:
            synergy_tag = rng.choice(tags)
            synergies.append({"withTag": synergy_tag, "bonusAbsurdity": rng.randint(1, 4)})

        art_prompt = (
            f"{theme.tone} policy decree where {name.lower()}. "
            f"highlight chaotic bureaucracy, viral media reactions, palette {', '.join(theme.palette)}."
        )

        sfx_key = rng.choice(["gavel", "flashbulb", "megaphone", "drumroll"]) + "_hit"
        vo_line = f"Citizens, {name.lower()}!".replace("  ", " ")

        yaml_card = {
            "id": slug,
            "name": name,
            "description": f"A decree to {name.lower()}.",
            "cost": cost,
            "rarity": rarity,
            "tags": tags,
            "effects": effects,
            "synergies": synergies,
            "artStyle": theme.card_style,
            "artPrompt": art_prompt,
            "sfxKey": sfx_key,
            "voLine": vo_line,
        }
        yaml_cards.append(yaml_card)

        json_cards.append({
            "id": slug,
            "name": name,
            "description": yaml_card["description"],
            "cost": cost,
            "rarity": rarity,
            "tags": tags,
            "effects": effects,
            "artKey": f"art/cards/{slug}",
        })

        save_yaml(YAML_ROOT / "cards" / f"generated_card_{idx+1:03d}.yaml", yaml_card)

    return yaml_cards, json_cards


def generate_leaders(theme: Theme, rng: random.Random, card_ids: List[str]) -> Tuple[List[Dict], List[Dict]]:
    count = theme.counts["leaders"]
    yaml_leaders = []
    json_leaders = []

    for idx in range(count):
        archetype_title, trait_tags = rng.choice(LEADER_ARCHETYPES)
        honorifics = ["Supreme", "Grand", "Acting", "Virtual", "Provisional", "Executive"]
        roles = ["Commander", "Director", "Chancellor", "Highlord", "Consultant", "Strategist"]
        surnames = ["Magnifico", "Vector", "Flux", "Spin", "Memetrix", "Ledger", "Hyperion", "Cascade", "Quorum", "Vex"]
        role = rng.choice(roles)
        surname = rng.choice(surnames)
        name = f"{rng.choice(honorifics)} {role} {surname}"
        slug = slugify(f"leader_{idx+1:02d}_{surname.lower()}")

        base_stats = {
            "approval": rng.randint(35, 65),
            "economy": rng.randint(30, 70),
            "absurdity": rng.randint(45, 90),
            "reputation": rng.randint(20, 70),
            "panic": rng.randint(10, 55),
        }

        ability = {
            "name": f"{archetype_title} Protocol",
            "description": f"Leverages {theme.primary_theme.replace('_', ' ')} to sway factions.",
            "effects": [
                {"type": rng.choice(CARD_EFFECTS), "value": rng.randint(3, 8)},
                {"type": "AbsurdityDelta", "value": rng.randint(1, 5)},
            ],
        }

        starting_deck = rng.sample(card_ids, k=min(8, len(card_ids)))

        yaml_leader = {
            "id": slug,
            "name": name,
            "title": archetype_title,
            "archetype": theme.primary_theme,
            "baseStats": base_stats,
            "traitTags": list(dict.fromkeys(trait_tags + [theme.primary_theme])),
            "ability": ability,
            "startingDeck": starting_deck,
            "portraitStyle": theme.leader_style,
            "portraitPrompt": f"{theme.leader_style} portrait of {name}, embodying {archetype_title.lower()} vibes and {theme.tone} energy.",
            "voiceStyle": theme.voice_style,
            "theme_music": f"{slug}_theme",
        }
        yaml_leaders.append(yaml_leader)

        json_leaders.append({
            "id": slug,
            "name": name,
            "bio": f"{name} — {archetype_title} of {theme.primary_theme.replace('_', ' ')}.",
            "traitTags": list(dict.fromkeys(trait_tags + [theme.primary_theme])),
            "startingDeck": starting_deck,
            "portraitKey": f"art/portraits/{slug}",
        })

        save_yaml(YAML_ROOT / "leaders" / f"generated_leader_{idx+1:02d}.yaml", yaml_leader)

    return yaml_leaders, json_leaders


def severity_from_type(crisis_type: str) -> int:
    return {"minor": 2, "major": 3, "catastrophic": 5}.get(crisis_type, 2)


def generate_crises(theme: Theme, rng: random.Random, cards: List[Dict]) -> Tuple[List[Dict], List[Dict]]:
    count = theme.counts["crises"]
    yaml_crises = []
    json_crises = []

    crisis_ids: List[str] = []
    crisis_payloads: List[Tuple[Dict, Dict, Dict]] = []

    for idx in range(count):
        ctype, category = CRISIS_TYPES[idx % len(CRISIS_TYPES)]
        subject = rng.choice([
            "Pigeon", "Lobbyist", "Hashtag", "Inflation", "Algorithm", "Meme", "Diplomat", "Comedian", "Bureaucrat"
        ])
        headline = f"{subject} {rng.choice(['Riots', 'Walkout', 'Takeover', 'Summit', 'Inquest', 'Flash Mob'])} Rocks Nation"
        slug = slugify(f"crisis_{idx+1:03d}_{subject.lower()}")
        crisis_ids.append(slug)

        option_effect = [dict(effect) for effect in rng.choice(cards)["effects"]]

        options = [
            {
                "id": "press_conference",
                "text": "Hold emergency press conference",
                "effects": option_effect,
            },
            {
                "id": "meme_warfare",
                "text": "Deploy meme warfare unit",
                "effects": [{"type": "AbsurdityDelta", "value": rng.randint(4, 9)}],
            },
        ]

        yaml_crisis = {
            "id": slug,
            "name": f"{subject} {rng.choice(['Meltdown', 'Crisis', 'Saga', 'Spectacle'])}",
            "type": ctype,
            "category": category,
            "headline": headline,
            "triggerConditions": {
                "turns": {"min": rng.randint(2, 5), "max": rng.randint(8, 14)},
                "statThresholds": {"absurdity": {"min": rng.randint(40, 70)}},
            },
            "options": options,
            "chainEvents": [],
            "newsImage": f"crisis/{slug}",
            "imagePrompt": f"{theme.crisis_style} photo capturing {headline.lower()}, dramatic lighting, {theme.tone} satire.",
        }
        crisis_payloads.append((yaml_crisis, {
            "id": slug,
            "name": yaml_crisis["name"],
            "description": headline,
            "severity": severity_from_type(ctype),
            "tags": [category, theme.primary_theme],
            "effects": [{"type": "PanicDelta", "value": rng.randint(3, 8)}],
            "next": [],
        }, slug))

    # Link crises sequentially for a simple chain
    for idx, (yaml_crisis, json_crisis, crisis_id) in enumerate(crisis_payloads):
        if idx < len(crisis_payloads) - 1:
            next_id = crisis_payloads[idx + 1][2]
            yaml_crisis["chainEvents"].append({"triggeredBy": "press_conference", "nextCrisis": next_id, "delay": 1})
            json_crisis["next"].append(next_id)

        yaml_crises.append(yaml_crisis)
        json_crises.append(json_crisis)
        save_yaml(YAML_ROOT / "crises" / f"generated_crisis_{idx+1:03d}.yaml", yaml_crisis)

    return yaml_crises, json_crises


def generate_factions(theme: Theme, rng: random.Random, cards: List[Dict]) -> Tuple[List[Dict], List[Dict]]:
    count = theme.counts["factions"]
    yaml_factions = []
    json_factions = []

    for idx in range(count):
        faction_name, tag_options = FACTION_TYPES[idx % len(FACTION_TYPES)]
        slug = slugify(f"faction_{idx+1:02d}_{faction_name.split()[0].lower()}")
        primary_card = rng.choice(cards)

        yaml_faction = {
            "id": slug,
            "name": faction_name,
            "type": "influence_group",
            "baseInfluence": rng.randint(40, 70),
            "volatility": rng.randint(5, 15),
            "mechanics": {
                "reactions": [
                    {
                        "cardTag": rng.choice(primary_card["tags"]),
                        "influenceDelta": rng.randint(5, 15),
                    }
                ],
                "influenceEffects": [
                    {
                        "threshold": rng.randint(60, 90),
                        "effect": "faction_support",
                        "bonus": {"approvalMultiplier": round(rng.uniform(1.05, 1.3), 2)},
                    }
                ],
            },
            "unlockableCards": rng.sample([card["id"] for card in cards], k=min(5, len(cards))),
            "iconStyle": theme.card_style,
            "iconPrompt": f"Faction emblem for {faction_name}, colors {choose_palette_color(theme, rng, idx)}, satire level {theme.satire_level}.",
        }
        yaml_factions.append(yaml_faction)

        json_factions.append({
            "id": slug,
            "name": faction_name,
            "description": f"Influence bloc obsessed with {theme.primary_theme.replace('_', ' ')}.",
            "color": choose_palette_color(theme, rng, idx),
            "tags": tag_options,
        })

        save_yaml(YAML_ROOT / "factions" / f"generated_faction_{idx+1:02d}.yaml", yaml_faction)

    return yaml_factions, json_factions


def main():
    parser = argparse.ArgumentParser(description="Generate Executive Disorder content from a theme file")
    parser.add_argument("theme", nargs="?", help="Path to theme JSON (defaults to ./theme.json if present)")
    parser.add_argument("--seed", type=int, default=42)
    args = parser.parse_args()

    theme_path: Optional[Path] = None
    if args.theme:
        theme_path = Path(args.theme)
    else:
        default_candidate = ROOT / "theme.json"
        if default_candidate.exists():
            theme_path = default_candidate

    rng = random.Random(args.seed)
    theme = load_theme(theme_path)

    cleanup_generated_yaml()

    JSON_ROOT.mkdir(parents=True, exist_ok=True)

    yaml_cards, json_cards = generate_cards(theme, rng)
    yaml_leaders, json_leaders = generate_leaders(theme, rng, [card["id"] for card in json_cards])
    yaml_crises, json_crises = generate_crises(theme, rng, json_cards)
    yaml_factions, json_factions = generate_factions(theme, rng, json_cards)

    write_json(JSON_ROOT / "cards.json", {"cards": json_cards})
    write_json(JSON_ROOT / "leaders.json", {"leaders": json_leaders})
    write_json(JSON_ROOT / "crises.json", {"crises": json_crises})
    write_json(JSON_ROOT / "factions.json", {"factions": json_factions})

    print("=== Content Generation Complete ===")
    if theme_path:
        print(f"Theme: {theme_path}")
    print(f"Cards:   {len(json_cards)}")
    print(f"Leaders: {len(json_leaders)}")
    print(f"Crises:  {len(json_crises)}")
    print(f"Factions:{len(json_factions)}")


if __name__ == "__main__":
    main()
