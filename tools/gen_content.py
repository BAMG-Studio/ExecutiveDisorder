#!/usr/bin/env python3
"""
Simple content generator stub for Executive Disorder.
Generates JSON files for cards, leaders, factions, and crises into
unity/Assets/Game/Data/.

No external dependencies. Deterministic via --seed.
"""
import argparse
import json
import os
import random
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
DATA_DIR = ROOT / "unity" / "Assets" / "Game" / "Data"

CARD_NAMES = [
    ("ban_birds", "Ban All Birds"),
    ("oxygen_tax", "Tax Oxygen"),
    ("dance_vote", "Dance Vote"),
    ("midnight_memes", "Midnight Memes"),
    ("press_spin", "Press Spin Cycle"),
]

FACTIONS = [
    ("bureaucrats", "Bureaucrats", "#4E9F3D", ["order", "economy"]),
    ("meme_syndicate", "Meme Syndicate", "#E84545", ["memes", "chaos"]),
]

LEADERS = [
    ("corrupt_executive", "The Corrupt Executive", ["greed", "spin"]),
    ("meme_politician", "The Meme Politician", ["memes", "charisma"]),
]

CRISES = [
    ("pigeon_uprising", "Pigeon Uprising", ["nature", "absurd"], 2),
    ("supply_seed_shortage", "Seed Supply Shortage", ["economy"], 3),
]


def write_json(path: Path, data: dict):
    path.parent.mkdir(parents=True, exist_ok=True)
    with open(path, "w", encoding="utf-8") as f:
        json.dump(data, f, indent=2)


def gen_cards(rng: random.Random):
    cards = []
    for cid, name in CARD_NAMES:
        cost = rng.randint(0, 3)
        rarity = rng.choice(["common", "common", "uncommon", "rare"])  # bias to common
        tags = [rng.choice(["policy", "economy", "memes", "culture", "absurd"])]
        effects = []
        # two simple effects
        effects.append({"type": rng.choice(["ApprovalDelta", "EconomyDelta", "PanicDelta", "AbsurdityDelta", "ReputationDelta"]),
                        "value": rng.choice([-4, -3, -2, 2, 3, 4])})
        effects.append({"type": rng.choice(["ApprovalDelta", "EconomyDelta", "PanicDelta", "AbsurdityDelta", "ReputationDelta"]),
                        "value": rng.choice([-3, -2, 2, 3, 5, 7])})
        cards.append({
            "id": cid,
            "name": name,
            "description": f"Auto-generated policy: {name}.",
            "cost": cost,
            "rarity": rarity,
            "tags": tags,
            "effects": effects,
            "artKey": f"art/cards/{cid}"
        })
    return {"cards": cards}


def gen_leaders(rng: random.Random):
    leaders = []
    for lid, name, traits in LEADERS:
        deck = [cid for cid, _ in rng.sample(CARD_NAMES, k=min(2, len(CARD_NAMES)))]
        leaders.append({
            "id": lid,
            "name": name,
            "bio": f"Auto-generated leader bio for {name}.",
            "traitTags": traits,
            "startingDeck": deck,
            "portraitKey": f"art/portraits/{lid}"
        })
    return {"leaders": leaders}


def gen_factions(rng: random.Random):
    factions = []
    for fid, name, color, tags in FACTIONS:
        factions.append({
            "id": fid,
            "name": name,
            "description": f"Auto-generated faction: {name}.",
            "color": color,
            "tags": tags
        })
    return {"factions": factions}


def gen_crises(rng: random.Random):
    crises = []
    for cid, name, tags, severity in CRISES:
        next_ids = []
        crises.append({
            "id": cid,
            "name": name,
            "description": f"Auto-generated crisis: {name}.",
            "severity": severity,
            "tags": tags,
            "effects": [
                {"type": rng.choice(["ApprovalDelta", "EconomyDelta", "PanicDelta", "ReputationDelta"]),
                 "value": rng.choice([-4, -2, 2, 3])}
            ],
            "next": next_ids
        })
    return {"crises": crises}


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--seed", type=int, default=42)
    args = parser.parse_args()

    rng = random.Random(args.seed)
    DATA_DIR.mkdir(parents=True, exist_ok=True)

    write_json(DATA_DIR / "cards.json", gen_cards(rng))
    write_json(DATA_DIR / "leaders.json", gen_leaders(rng))
    write_json(DATA_DIR / "factions.json", gen_factions(rng))
    write_json(DATA_DIR / "crises.json", gen_crises(rng))

    print(f"Generated content in {DATA_DIR}")


if __name__ == "__main__":
    main()

