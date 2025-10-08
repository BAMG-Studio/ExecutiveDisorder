using System.Collections.Generic;
using UnityEngine;

namespace ExecutiveDisorder.Game.Data
{
    [CreateAssetMenu(fileName = "GameDatabase", menuName = "ExecutiveDisorder/Game Database", order = 10)]
    public class GameDatabase : ScriptableObject
    {
        public List<CardDef> cards = new();
        public List<LeaderDef> leaders = new();
        public List<FactionDef> factions = new();
        public List<CrisisDef> crises = new();
    }
}

