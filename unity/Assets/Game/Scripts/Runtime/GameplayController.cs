using System.Collections.Generic;
using System.Linq;
using ExecutiveDisorder.Game.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExecutiveDisorder.Game
{
    public class GameplayController : MonoBehaviour
    {
        [Header("UI References")]
        public Text headerText;
        public Text statsText;
        public Text logText;
        public Button executeTurnButton;
        public Button backButton;

        private GameDatabase _database;
        private LeaderDef _leader;
        private readonly Dictionary<string, float> _stats = new()
        {
            {"Approval", 50f},
            {"Economy", 50f},
            {"Absurdity", 50f},
            {"Reputation", 50f},
            {"Panic", 20f},
        };
        private readonly List<string> _log = new();
        private System.Random _random;

        private void Awake()
        {
            _random = new System.Random();
            _database = GameContext.Database ?? Resources.Load<GameDatabase>("Generated/GameDatabase");
            if (_database == null)
            {
                AppendLog("No database loaded. Returning to menu.");
                backButton?.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
                return;
            }

            _leader = ResolveLeader();

            if (executeTurnButton != null)
            {
                executeTurnButton.onClick.AddListener(RunFakeTurn);
            }

            if (backButton != null)
            {
                backButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
            }

            InitializeStats();
            UpdateUI();
        }

        private void OnDestroy()
        {
            executeTurnButton?.onClick.RemoveListener(RunFakeTurn);
            backButton?.onClick.RemoveAllListeners();
        }

        private LeaderDef ResolveLeader()
        {
            if (_database.leaders == null || _database.leaders.Count == 0)
            {
                AppendLog("No leaders available.");
                return null;
            }

            var id = GameContext.SelectedLeaderId;
            if (!string.IsNullOrEmpty(id))
            {
                var leader = _database.leaders.FirstOrDefault(l => l.id == id);
                if (leader != null)
                {
                    return leader;
                }
            }

            return _database.leaders[0];
        }

        private void InitializeStats()
        {
            if (_leader != null)
            {
                _stats["Approval"] = _leader.traitTags != null && _leader.traitTags.Contains("approval") ? 60f : 50f;
            }

            AppendLog(_leader != null
                ? $"Beginning simulation with {_leader.displayName}."
                : "Beginning simulation without selected leader.");
        }

        private void RunFakeTurn()
        {
            if (_database.cards == null || _database.cards.Count == 0)
            {
                AppendLog("No cards available.");
                return;
            }

            var deck = _leader?.startingDeck ?? new List<string>();
            CardDef card;
            if (deck.Count > 0)
            {
                var options = _database.cards.Where(c => deck.Contains(c.id)).ToList();
                card = options.Count > 0 ? options[_random.Next(options.Count)] : _database.cards[_random.Next(_database.cards.Count)];
            }
            else
            {
                card = _database.cards[_random.Next(_database.cards.Count)];
            }

            ApplyCard(card);
            AppendLog($"Played card: {card.displayName}");

            if (_database.crises != null && _database.crises.Count > 0 && _random.NextDouble() < 0.35)
            {
                var crisis = _database.crises[_random.Next(_database.crises.Count)];
                AppendLog($"Crisis triggered: {crisis.displayName}");
                foreach (var effect in crisis.effects ?? new List<EffectSpec>())
                {
                    ApplyEffect(effect);
                }
            }

            UpdateUI();
        }

        private void ApplyCard(CardDef card)
        {
            if (card.effects == null) return;
            foreach (var effect in card.effects)
            {
                ApplyEffect(effect);
            }
        }

        private void ApplyEffect(EffectSpec effect)
        {
            if (effect == null || string.IsNullOrEmpty(effect.type)) return;

            string key = effect.type.Replace("Delta", string.Empty);
            if (!_stats.ContainsKey(key))
            {
                _stats[key] = 0f;
            }

            _stats[key] += effect.value;
            _stats[key] = Mathf.Clamp(_stats[key], -100f, 150f);
        }

        private void UpdateUI()
        {
            if (headerText != null)
            {
                headerText.text = _leader != null
                    ? $"{_leader.displayName} — Tactical Desk"
                    : "Sandbox Simulation";
            }

            if (statsText != null)
            {
                statsText.text = string.Join("\n", _stats.Select(kvp => $"{kvp.Key}: {kvp.Value:0}"));
            }

            if (logText != null)
            {
                logText.text = string.Join("\n", _log);
            }
        }

        private void AppendLog(string message)
        {
            _log.Add(message);
            if (_log.Count > 12)
            {
                _log.RemoveAt(0);
            }
            UpdateUI();
        }
    }
}
