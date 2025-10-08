using System.Collections.Generic;
using UnityEngine;

namespace ExecutiveDisorder.Game.Data
{
    [CreateAssetMenu(fileName = "LeaderDef", menuName = "ExecutiveDisorder/Leader", order = 1)]
    public class LeaderDef : ScriptableObject
    {
        public string id;
        public string displayName;
        [TextArea] public string bio;

        public List<string> traitTags = new();
        public List<string> startingDeck = new(); // card IDs

        // Portrait/art addressable key or Resources path
        public string portraitKey;
    }
}

