using System;
using System.Collections.Generic;
using System.Linq;
using ExecutiveDisorder.Game.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExecutiveDisorder.Game
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("UI References")]
        public RectTransform leaderListRoot;
        public RectTransform cardGridRoot;
        public Image leaderPortraitImage;
        public Text leaderNameText;
        public Text leaderBioText;
        public Button startGameButton;
        public Button quitButton;
        public ScrollRect cardScroll;

        private readonly List<GameObject> _leaderButtons = new();
        private readonly List<GameObject> _cardTiles = new();
        private GameDatabase _database;
        private LeaderDef _selectedLeader;
        private readonly System.Random _random = new();

        private void Awake()
        {
            _database = GameContext.Database ?? Resources.Load<GameDatabase>("Generated/GameDatabase");
            if (_database == null)
            {
                Debug.LogError("MainMenuController: GameDatabase missing. Run codex-init or import data.");
                enabled = false;
                return;
            }

            BuildLeaderButtons();

            if (_database.leaders != null && _database.leaders.Count > 0)
            {
                SelectLeader(_database.leaders[0]);
            }

            if (startGameButton != null)
            {
                startGameButton.onClick.AddListener(OnStartGame);
                startGameButton.interactable = _selectedLeader != null;
            }

            if (quitButton != null)
            {
                quitButton.onClick.AddListener(Application.Quit);
            }
        }

        private void OnDestroy()
        {
            if (startGameButton != null)
            {
                startGameButton.onClick.RemoveListener(OnStartGame);
            }
            if (quitButton != null)
            {
                quitButton.onClick.RemoveListener(Application.Quit);
            }
        }

        private void BuildLeaderButtons()
        {
            foreach (var existing in _leaderButtons)
            {
                DestroyImmediate(existing);
            }
            _leaderButtons.Clear();

            if (leaderListRoot == null || _database.leaders == null)
            {
                return;
            }

            foreach (var leader in _database.leaders)
            {
                var button = CreateLeaderButton(leader);
                _leaderButtons.Add(button);
            }
        }

        private GameObject CreateLeaderButton(LeaderDef leader)
        {
            var buttonGO = new GameObject($"Leader_{leader.id}", typeof(RectTransform), typeof(Image), typeof(Button), typeof(LayoutElement));
            buttonGO.transform.SetParent(leaderListRoot, false);

            var image = buttonGO.GetComponent<Image>();
            image.color = new Color(0.15f, 0.18f, 0.26f, 0.9f);

            var layout = buttonGO.GetComponent<LayoutElement>();
            layout.preferredHeight = 60f;
            layout.minHeight = 50f;

            var button = buttonGO.GetComponent<Button>();
            button.targetGraphic = image;

            var textGO = new GameObject("Label", typeof(RectTransform), typeof(Text));
            textGO.transform.SetParent(buttonGO.transform, false);
            var text = textGO.GetComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.text = leader.displayName;
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleLeft;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 12;
            text.resizeTextMaxSize = 26;

            var rect = textGO.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = new Vector2(14f, 6f);
            rect.offsetMax = new Vector2(-14f, -6f);

            button.onClick.AddListener(() => SelectLeader(leader));

            return buttonGO;
        }

        private void SelectLeader(LeaderDef leader)
        {
            _selectedLeader = leader;
            GameContext.SelectedLeaderId = leader != null ? leader.id : null;
            UpdateLeaderDetails();
            PopulateCardGrid();

            if (startGameButton != null)
            {
                startGameButton.interactable = leader != null;
            }
        }

        private void UpdateLeaderDetails()
        {
            if (leaderNameText != null)
            {
                leaderNameText.text = _selectedLeader != null ? _selectedLeader.displayName : "Select a Leader";
            }

            if (leaderBioText != null)
            {
                leaderBioText.text = _selectedLeader != null ? _selectedLeader.bio : "";
            }

            if (leaderPortraitImage != null)
            {
                leaderPortraitImage.color = _selectedLeader != null ? ColorFromId(_selectedLeader.id) : new Color(0.2f, 0.2f, 0.2f, 1f);
            }
        }

        private void PopulateCardGrid()
        {
            foreach (var tile in _cardTiles)
            {
                DestroyImmediate(tile);
            }
            _cardTiles.Clear();

            if (cardGridRoot == null || _database.cards == null || _selectedLeader == null)
            {
                return;
            }

            var cardLookup = _database.cards.ToDictionary(card => card.id, card => card);

            foreach (var cardId in _selectedLeader.startingDeck)
            {
                if (!cardLookup.TryGetValue(cardId, out var card))
                {
                    continue;
                }

                var tile = CreateCardTile(card);
                _cardTiles.Add(tile);
            }

            if (cardScroll != null)
            {
                cardScroll.verticalNormalizedPosition = 1f;
            }
        }

        private GameObject CreateCardTile(CardDef card)
        {
            var tileGO = new GameObject($"Card_{card.id}", typeof(RectTransform), typeof(Image));
            tileGO.transform.SetParent(cardGridRoot, false);
            var image = tileGO.GetComponent<Image>();
            image.color = new Color(0.25f, 0.28f, 0.35f, 0.85f);

            var rect = tileGO.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(200f, 110f);

            var titleGO = new GameObject("Title", typeof(RectTransform), typeof(Text));
            titleGO.transform.SetParent(tileGO.transform, false);
            var title = titleGO.GetComponent<Text>();
            title.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            title.text = card.displayName;
            title.color = Color.white;
            title.alignment = TextAnchor.UpperLeft;
            title.resizeTextForBestFit = true;
            title.resizeTextMinSize = 12;
            title.resizeTextMaxSize = 26;

            var titleRect = titleGO.GetComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0f, 0.45f);
            titleRect.anchorMax = new Vector2(1f, 1f);
            titleRect.offsetMin = new Vector2(10f, 8f);
            titleRect.offsetMax = new Vector2(-10f, -6f);

            var bodyGO = new GameObject("Body", typeof(RectTransform), typeof(Text));
            bodyGO.transform.SetParent(tileGO.transform, false);
            var body = bodyGO.GetComponent<Text>();
            body.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            body.text = BuildCardDescription(card);
            body.color = new Color(0.85f, 0.9f, 1f, 1f);
            body.alignment = TextAnchor.UpperLeft;
            body.resizeTextForBestFit = true;
            body.resizeTextMinSize = 10;
            body.resizeTextMaxSize = 20;

            var bodyRect = bodyGO.GetComponent<RectTransform>();
            bodyRect.anchorMin = new Vector2(0f, 0f);
            bodyRect.anchorMax = new Vector2(1f, 0.55f);
            bodyRect.offsetMin = new Vector2(10f, 8f);
            bodyRect.offsetMax = new Vector2(-10f, -8f);

            return tileGO;
        }

        private static string BuildCardDescription(CardDef card)
        {
            if (card.effects == null || card.effects.Count == 0)
            {
                return "No effects listed.";
            }

            var parts = card.effects.Select(effect => $"{effect.type.Replace("Delta", "")}: {(effect.value >= 0 ? "+" : string.Empty)}{effect.value}");
            return string.Join(" / ", parts);
        }

        private void OnStartGame()
        {
            if (_selectedLeader == null)
            {
                Debug.LogWarning("MainMenuController: No leader selected.");
                return;
            }

            SceneManager.LoadScene("Gameplay");
        }

        private static Color ColorFromId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Color.gray;
            }

            int hash = id.GetHashCode();
            float r = ((hash >> 0) & 0xFF) / 255f;
            float g = ((hash >> 8) & 0xFF) / 255f;
            float b = ((hash >> 16) & 0xFF) / 255f;
            return new Color(r, g, b, 1f);
        }
    }
}

