using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExecutiveDisorder.Game.Data
{
    [Serializable]
    public class EffectSpec
    {
        public string type;
        public float value;
        public string withTag; // optional, for synergies/conditional effects
        public string target;  // optional, e.g., "self", "opponent", "global"
    }

    [CreateAssetMenu(fileName = "CardDef", menuName = "ExecutiveDisorder/Card", order = 0)]
    public class CardDef : ScriptableObject
    {
        public string id;
        public string displayName;
        [TextArea] public string description;

        public int cost;
        public string rarity; // common, uncommon, rare, legendary
        public List<string> tags = new();
        public List<EffectSpec> effects = new();

        // Optional art hook (Addressables label or Resources path if needed later)
        public string artKey;
    }
}

