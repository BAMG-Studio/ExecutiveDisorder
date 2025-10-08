using System.Collections.Generic;
using UnityEngine;

namespace ExecutiveDisorder.Game.Data
{
    [CreateAssetMenu(fileName = "CrisisDef", menuName = "ExecutiveDisorder/Crisis", order = 3)]
    public class CrisisDef : ScriptableObject
    {
        public string id;
        public string displayName;
        [TextArea] public string description;

        [Range(1,5)] public int severity = 1;
        public List<string> tags = new();

        // Effects applied on spawn or per turn while active
        public List<EffectSpec> effects = new();

        // Optional chaining to subsequent crises/events
        public List<string> nextCrisisIds = new();
    }
}

