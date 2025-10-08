using System.Collections.Generic;
using UnityEngine;

namespace ExecutiveDisorder.Game.Data
{
    [CreateAssetMenu(fileName = "FactionDef", menuName = "ExecutiveDisorder/Faction", order = 2)]
    public class FactionDef : ScriptableObject
    {
        public string id;
        public string displayName;
        [TextArea] public string description;

        public Color color = Color.white; // primary theme color
        public List<string> tags = new();
    }
}

