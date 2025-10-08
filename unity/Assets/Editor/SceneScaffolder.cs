using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ExecutiveDisorder.EditorTools
{
    public static class SceneScaffolder
    {
        private const string ScenesDir = "Assets/Game/Scenes";

        [MenuItem("Codex/Setup/Create Boot & MainMenu Scenes")]
        public static void SetupAll()
        {
            Directory.CreateDirectory(ScenesDir);
            var boot = CreateBootScene();
            var menu = CreateMainMenuScene();
            var gameplay = CreateGameplayScene();

            var scenes = new[]
            {
                new EditorBuildSettingsScene(boot, true),
                new EditorBuildSettingsScene(menu, true),
                new EditorBuildSettingsScene(gameplay, true)
            };
            EditorBuildSettings.scenes = scenes;
            Debug.Log($"✅ Scaffolding complete. Scenes saved to {ScenesDir} and added to Build Settings.");
        }

        public static string CreateBootScene()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var go = new GameObject("Boot");
            go.AddComponent<ExecutiveDisorder.Game.BootLoader>();
            EditorSceneManager.MoveGameObjectToScene(go, scene);

            var path = Path.Combine(ScenesDir, "Boot.unity");
            EditorSceneManager.SaveScene(scene, path, true);
            return path;
        }

        public static string CreateMainMenuScene()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            var canvas = CreateCanvas(scene);
            CreateEventSystem(scene);

            var controllerGO = new GameObject("MainMenuController", typeof(ExecutiveDisorder.Game.MainMenuController));
            controllerGO.transform.SetParent(canvas.transform, false);
            var controller = controllerGO.GetComponent<ExecutiveDisorder.Game.MainMenuController>();

            // Leader list panel (left)
            var leadersScroll = CreateRectTransform("LeadersScroll", canvas.transform);
            leadersScroll.anchorMin = new Vector2(0f, 0f);
            leadersScroll.anchorMax = new Vector2(0.28f, 1f);
            leadersScroll.offsetMin = new Vector2(20f, 20f);
            leadersScroll.offsetMax = new Vector2(-12f, -20f);
            var leadersBg = leadersScroll.gameObject.AddComponent<Image>();
            leadersBg.color = new Color(0.07f, 0.09f, 0.12f, 0.95f);

            var leadersScrollRect = leadersScroll.gameObject.AddComponent<ScrollRect>();
            leadersScrollRect.horizontal = false;
            leadersScrollRect.movementType = ScrollRect.MovementType.Clamped;

            var leadersViewport = CreateRectTransform("Viewport", leadersScroll);
            leadersViewport.anchorMin = Vector2.zero;
            leadersViewport.anchorMax = Vector2.one;
            leadersViewport.offsetMin = Vector2.zero;
            leadersViewport.offsetMax = Vector2.zero;
            var leadersViewportImg = leadersViewport.gameObject.AddComponent<Image>();
            leadersViewportImg.color = new Color(0f, 0f, 0f, 0.1f);
            var leadersMask = leadersViewport.gameObject.AddComponent<Mask>();
            leadersMask.showMaskGraphic = false;
            leadersScrollRect.viewport = leadersViewport;

            var leadersContent = CreateRectTransform("Content", leadersViewport);
            leadersContent.anchorMin = new Vector2(0f, 1f);
            leadersContent.anchorMax = new Vector2(1f, 1f);
            leadersContent.pivot = new Vector2(0.5f, 1f);
            leadersContent.offsetMin = Vector2.zero;
            leadersContent.offsetMax = Vector2.zero;
            var leaderLayout = leadersContent.gameObject.AddComponent<VerticalLayoutGroup>();
            leaderLayout.spacing = 8f;
            leaderLayout.padding = new RectOffset(12, 12, 12, 12);
            leadersContent.gameObject.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            leadersScrollRect.content = leadersContent;

            // Details panel (right)
            var detailsPanel = CreateRectTransform("DetailsPanel", canvas.transform);
            detailsPanel.anchorMin = new Vector2(0.3f, 0f);
            detailsPanel.anchorMax = new Vector2(1f, 1f);
            detailsPanel.offsetMin = new Vector2(12f, 20f);
            detailsPanel.offsetMax = new Vector2(-20f, -20f);
            var detailsBg = detailsPanel.gameObject.AddComponent<Image>();
            detailsBg.color = new Color(0.12f, 0.14f, 0.2f, 0.9f);

            var portrait = CreateRectTransform("Portrait", detailsPanel);
            portrait.anchorMin = new Vector2(0f, 0.55f);
            portrait.anchorMax = new Vector2(0.25f, 0.95f);
            portrait.offsetMin = new Vector2(20f, 20f);
            portrait.offsetMax = new Vector2(-20f, -20f);
            var portraitImg = portrait.gameObject.AddComponent<Image>();
            portraitImg.color = new Color(0.22f, 0.25f, 0.35f, 1f);

            var nameText = CreateText("LeaderName", detailsPanel, "Select a Leader", 32, TextAnchor.MiddleLeft);
            var nameRect = nameText.GetComponent<RectTransform>();
            nameRect.anchorMin = new Vector2(0.27f, 0.78f);
            nameRect.anchorMax = new Vector2(1f, 0.95f);
            nameRect.offsetMin = new Vector2(10f, -10f);
            nameRect.offsetMax = new Vector2(-10f, -10f);

            var bioText = CreateText("LeaderBio", detailsPanel, "", 18, TextAnchor.UpperLeft);
            bioText.alignment = TextAnchor.UpperLeft;
            bioText.horizontalOverflow = HorizontalWrapMode.Wrap;
            bioText.verticalOverflow = VerticalWrapMode.Truncate;
            var bioRect = bioText.GetComponent<RectTransform>();
            bioRect.anchorMin = new Vector2(0.27f, 0.55f);
            bioRect.anchorMax = new Vector2(1f, 0.78f);
            bioRect.offsetMin = new Vector2(10f, 0f);
            bioRect.offsetMax = new Vector2(-10f, -10f);

            var buttonsRow = CreateRectTransform("ButtonsRow", detailsPanel);
            buttonsRow.anchorMin = new Vector2(0.27f, 0.48f);
            buttonsRow.anchorMax = new Vector2(0.7f, 0.55f);
            buttonsRow.offsetMin = new Vector2(10f, 0f);
            buttonsRow.offsetMax = new Vector2(-10f, 0f);
            var buttonsLayout = buttonsRow.gameObject.AddComponent<HorizontalLayoutGroup>();
            buttonsLayout.spacing = 12f;
            buttonsLayout.childForceExpandWidth = false;
            buttonsLayout.childControlWidth = true;

            var startButton = CreateButton("StartButton", buttonsRow, "Start Game");
            var quitButton = CreateButton("QuitButton", buttonsRow, "Quit");
            quitButton.GetComponent<Image>().color = new Color(0.55f, 0.16f, 0.2f, 0.9f);

            var cardsScroll = CreateRectTransform("CardsScroll", detailsPanel);
            cardsScroll.anchorMin = new Vector2(0f, 0f);
            cardsScroll.anchorMax = new Vector2(1f, 0.48f);
            cardsScroll.offsetMin = new Vector2(20f, 20f);
            cardsScroll.offsetMax = new Vector2(-20f, 20f);
            var cardsBg = cardsScroll.gameObject.AddComponent<Image>();
            cardsBg.color = new Color(0.1f, 0.12f, 0.18f, 0.9f);

            var cardScroll = cardsScroll.gameObject.AddComponent<ScrollRect>();
            cardScroll.horizontal = false;
            cardScroll.movementType = ScrollRect.MovementType.Clamped;

            var cardsViewport = CreateRectTransform("Viewport", cardsScroll);
            cardsViewport.anchorMin = Vector2.zero;
            cardsViewport.anchorMax = Vector2.one;
            cardsViewport.offsetMin = Vector2.zero;
            cardsViewport.offsetMax = Vector2.zero;
            var cardsViewportImg = cardsViewport.gameObject.AddComponent<Image>();
            cardsViewportImg.color = new Color(0f, 0f, 0f, 0.1f);
            var cardsMask = cardsViewport.gameObject.AddComponent<Mask>();
            cardsMask.showMaskGraphic = false;
            cardScroll.viewport = cardsViewport;

            var cardsContent = CreateRectTransform("Content", cardsViewport);
            cardsContent.anchorMin = new Vector2(0f, 1f);
            cardsContent.anchorMax = new Vector2(1f, 1f);
            cardsContent.pivot = new Vector2(0.5f, 1f);
            cardsContent.offsetMin = Vector2.zero;
            cardsContent.offsetMax = Vector2.zero;
            var grid = cardsContent.gameObject.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(220f, 120f);
            grid.spacing = new Vector2(16f, 16f);
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 2;
            cardsContent.gameObject.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            cardScroll.content = cardsContent;

            // Wire references
            controller.leaderListRoot = leadersContent;
            controller.cardGridRoot = cardsContent;
            controller.leaderPortraitImage = portraitImg;
            controller.leaderNameText = nameText;
            controller.leaderBioText = bioText;
            controller.startGameButton = startButton;
            controller.quitButton = quitButton;
            controller.cardScroll = cardScroll;

            var path = Path.Combine(ScenesDir, "MainMenu.unity");
            EditorSceneManager.SaveScene(scene, path, true);
            return path;
        }

        public static string CreateGameplayScene()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            var canvas = CreateCanvas(scene);
            CreateEventSystem(scene);

            var controllerGO = new GameObject("GameplayController", typeof(ExecutiveDisorder.Game.GameplayController));
            controllerGO.transform.SetParent(canvas.transform, false);
            var controller = controllerGO.GetComponent<ExecutiveDisorder.Game.GameplayController>();

            var header = CreateText("Header", canvas.transform, "Sandbox Simulation", 32, TextAnchor.MiddleLeft);
            var headerRect = header.GetComponent<RectTransform>();
            headerRect.anchorMin = new Vector2(0f, 0.85f);
            headerRect.anchorMax = new Vector2(0.6f, 1f);
            headerRect.offsetMin = new Vector2(20f, -10f);
            headerRect.offsetMax = new Vector2(-20f, -10f);

            var stats = CreateText("Stats", canvas.transform, "Approval: 50", 22, TextAnchor.UpperLeft);
            var statsRect = stats.GetComponent<RectTransform>();
            statsRect.anchorMin = new Vector2(0f, 0.45f);
            statsRect.anchorMax = new Vector2(0.3f, 0.85f);
            statsRect.offsetMin = new Vector2(20f, 0f);
            statsRect.offsetMax = new Vector2(-20f, 0f);

            var logScroll = CreateRectTransform("LogScroll", canvas.transform);
            logScroll.anchorMin = new Vector2(0.32f, 0.1f);
            logScroll.anchorMax = new Vector2(1f, 0.82f);
            logScroll.offsetMin = new Vector2(20f, 20f);
            logScroll.offsetMax = new Vector2(-20f, -20f);
            var logBg = logScroll.gameObject.AddComponent<Image>();
            logBg.color = new Color(0.08f, 0.09f, 0.14f, 0.95f);

            var logScrollRect = logScroll.gameObject.AddComponent<ScrollRect>();
            logScrollRect.horizontal = false;
            logScrollRect.movementType = ScrollRect.MovementType.Clamped;

            var logViewport = CreateRectTransform("Viewport", logScroll);
            logViewport.anchorMin = Vector2.zero;
            logViewport.anchorMax = Vector2.one;
            logViewport.offsetMin = Vector2.zero;
            logViewport.offsetMax = Vector2.zero;
            var logViewportImg = logViewport.gameObject.AddComponent<Image>();
            logViewportImg.color = new Color(0f, 0f, 0f, 0.05f);
            var logMask = logViewport.gameObject.AddComponent<Mask>();
            logMask.showMaskGraphic = false;
            logScrollRect.viewport = logViewport;

            var logContent = CreateRectTransform("Content", logViewport);
            logContent.anchorMin = new Vector2(0f, 1f);
            logContent.anchorMax = new Vector2(1f, 1f);
            logContent.pivot = new Vector2(0.5f, 1f);
            logContent.offsetMin = Vector2.zero;
            logContent.offsetMax = Vector2.zero;
            var logText = CreateText("LogText", logContent, "Session log...", 18, TextAnchor.UpperLeft);
            var logTextRect = logText.GetComponent<RectTransform>();
            logTextRect.anchorMin = new Vector2(0f, 0f);
            logTextRect.anchorMax = new Vector2(1f, 1f);
            logTextRect.offsetMin = new Vector2(10f, 10f);
            logTextRect.offsetMax = new Vector2(-10f, -10f);
            logScrollRect.content = logContent;

            logContent.gameObject.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            var buttonsRow = CreateRectTransform("Buttons", canvas.transform);
            buttonsRow.anchorMin = new Vector2(0f, 0f);
            buttonsRow.anchorMax = new Vector2(1f, 0.1f);
            buttonsRow.offsetMin = new Vector2(20f, 20f);
            buttonsRow.offsetMax = new Vector2(-20f, -10f);
            var buttonsLayout = buttonsRow.gameObject.AddComponent<HorizontalLayoutGroup>();
            buttonsLayout.spacing = 16f;
            buttonsLayout.childAlignment = TextAnchor.MiddleLeft;
            buttonsLayout.childForceExpandWidth = false;

            var executeButton = CreateButton("ExecuteTurn", buttonsRow, "Execute Turn");
            var backButton = CreateButton("ReturnToMenu", buttonsRow, "Return to Menu");

            controller.headerText = header;
            controller.statsText = stats;
            controller.logText = logText;
            controller.executeTurnButton = executeButton;
            controller.backButton = backButton;

            var path = Path.Combine(ScenesDir, "Gameplay.unity");
            EditorSceneManager.SaveScene(scene, path, true);
            return path;
        }

        private static GameObject CreateCanvas(Scene scene)
        {
            var canvasGO = new GameObject("Canvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            var canvas = canvasGO.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvasGO.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            EditorSceneManager.MoveGameObjectToScene(canvasGO, scene);
            return canvasGO;
        }

        private static void CreateEventSystem(Scene scene)
        {
            var es = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            EditorSceneManager.MoveGameObjectToScene(es, scene);
        }

        private static RectTransform CreateRectTransform(string name, Transform parent)
        {
            var go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent, false);
            return go.GetComponent<RectTransform>();
        }

        private static Text CreateText(string name, Transform parent, string defaultText, int fontSize, TextAnchor anchor)
        {
            var rect = CreateRectTransform(name, parent);
            var text = rect.gameObject.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.text = defaultText;
            text.fontSize = fontSize;
            text.color = Color.white;
            text.alignment = anchor;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow;

            var rect = text.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            return text;
        }

        private static Button CreateButton(string name, Transform parent, string label)
        {
            var go = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button), typeof(LayoutElement));
            go.transform.SetParent(parent, false);
            var image = go.GetComponent<Image>();
            image.color = new Color(0.2f, 0.3f, 0.45f, 0.95f);
            var layout = go.GetComponent<LayoutElement>();
            layout.preferredWidth = 200f;
            layout.preferredHeight = 60f;

            var button = go.GetComponent<Button>();
            button.targetGraphic = image;

            var text = CreateText("Label", go.transform, label, 24, TextAnchor.MiddleCenter);
            var textRect = text.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            return button;
        }
    }
}
