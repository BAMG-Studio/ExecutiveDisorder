// 10/6/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionSceneSetup : MonoBehaviour
{
    [MenuItem("Tools/Setup Character Selection Scene")]
    public static void SetupScene()
    {
        // Create Canvas
        GameObject canvas = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        Canvas canvasComponent = canvas.GetComponent<Canvas>();
        canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        // Create Background Image
        GameObject background = new GameObject("Background", typeof(Image));
        background.transform.SetParent(canvas.transform, false);
        RectTransform bgRect = background.GetComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;
        Image bgImage = background.GetComponent<Image>();
        bgImage.color = Color.gray; // Replace this with your MainBGMorning.png sprite
        // bgImage.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/PathTo/MainBGMorning.png");

        // Create Title
        GameObject title = new GameObject("Title", typeof(TextMeshProUGUI));
        title.transform.SetParent(canvas.transform, false);
        RectTransform titleRect = title.GetComponent<RectTransform>();
        titleRect.anchorMin = new Vector2(0.5f, 1);
        titleRect.anchorMax = new Vector2(0.5f, 1);
        titleRect.pivot = new Vector2(0.5f, 1);
        titleRect.anchoredPosition = new Vector2(0, -50);
        titleRect.sizeDelta = new Vector2(800, 100);
        TextMeshProUGUI titleText = title.GetComponent<TextMeshProUGUI>();
        titleText.text = "SELECT YOUR LEADER";
        titleText.fontSize = 48;
        titleText.alignment = TextAlignmentOptions.Center;

        // Create Scroll View
        GameObject scrollView = new GameObject("CharacterScrollView", typeof(ScrollRect));
        scrollView.transform.SetParent(canvas.transform, false);
        RectTransform scrollRect = scrollView.GetComponent<RectTransform>();
        scrollRect.anchorMin = new Vector2(0, 0.5f);
        scrollRect.anchorMax = new Vector2(0.6f, 0.5f);
        scrollRect.pivot = new Vector2(0, 0.5f);
        scrollRect.anchoredPosition = new Vector2(50, 0);
        scrollRect.sizeDelta = new Vector2(1100, 800);

        // Scroll View Content
        GameObject content = new GameObject("Content", typeof(RectTransform), typeof(GridLayoutGroup), typeof(ContentSizeFitter));
        content.transform.SetParent(scrollView.transform, false);
        RectTransform contentRect = content.GetComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0, 1);
        contentRect.anchorMax = new Vector2(0, 1);
        contentRect.pivot = new Vector2(0, 1);
        contentRect.anchoredPosition = Vector2.zero;
        contentRect.sizeDelta = new Vector2(1100, 800);
        GridLayoutGroup grid = content.GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(220, 320);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 5;
        ContentSizeFitter fitter = content.GetComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        // Assign Content to ScrollRect
        ScrollRect scroll = scrollView.GetComponent<ScrollRect>();
        scroll.content = contentRect;

        // Create Selected Character Panel
        GameObject selectedPanel = new GameObject("SelectedCharacterPanel", typeof(Image));
        selectedPanel.transform.SetParent(canvas.transform, false);
        RectTransform panelRect = selectedPanel.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.7f, 0.5f);
        panelRect.anchorMax = new Vector2(1, 0.5f);
        panelRect.pivot = new Vector2(0.5f, 0.5f);
        panelRect.anchoredPosition = new Vector2(0, 0);
        panelRect.sizeDelta = new Vector2(600, 800);
        Image panelImage = selectedPanel.GetComponent<Image>();
        panelImage.color = Color.white;

        // Add Portrait to Panel
        GameObject portrait = new GameObject("SelectedPortrait", typeof(Image));
        portrait.transform.SetParent(selectedPanel.transform, false);
        RectTransform portraitRect = portrait.GetComponent<RectTransform>();
        portraitRect.anchorMin = new Vector2(0.5f, 1);
        portraitRect.anchorMax = new Vector2(0.5f, 1);
        portraitRect.pivot = new Vector2(0.5f, 1);
        portraitRect.anchoredPosition = new Vector2(0, -100);
        portraitRect.sizeDelta = new Vector2(300, 300);

        // Add Name to Panel
        GameObject nameText = new GameObject("SelectedName", typeof(TextMeshProUGUI));
        nameText.transform.SetParent(selectedPanel.transform, false);
        RectTransform nameRect = nameText.GetComponent<RectTransform>();
        nameRect.anchorMin = new Vector2(0.5f, 1);
        nameRect.anchorMax = new Vector2(0.5f, 1);
        nameRect.pivot = new Vector2(0.5f, 1);
        nameRect.anchoredPosition = new Vector2(0, -450);
        nameRect.sizeDelta = new Vector2(500, 50);
        TextMeshProUGUI nameTMP = nameText.GetComponent<TextMeshProUGUI>();
        nameTMP.text = "Character Name";
        nameTMP.fontSize = 36;
        nameTMP.alignment = TextAlignmentOptions.Center;

        // Add Bio to Panel
        GameObject bioText = new GameObject("SelectedBio", typeof(TextMeshProUGUI));
        bioText.transform.SetParent(selectedPanel.transform, false);
        RectTransform bioRect = bioText.GetComponent<RectTransform>();
        bioRect.anchorMin = new Vector2(0.5f, 1);
        bioRect.anchorMax = new Vector2(0.5f, 1);
        bioRect.pivot = new Vector2(0.5f, 1);
        bioRect.anchoredPosition = new Vector2(0, -520);
        bioRect.sizeDelta = new Vector2(500, 200);
        TextMeshProUGUI bioTMP = bioText.GetComponent<TextMeshProUGUI>();
        bioTMP.text = "Character Bio";
        bioTMP.fontSize = 24;
        bioTMP.alignment = TextAlignmentOptions.TopLeft;

        // Add Buttons
        GameObject startButton = new GameObject("StartCampaignButton", typeof(Button), typeof(Image));
        startButton.transform.SetParent(canvas.transform, false);
        RectTransform startRect = startButton.GetComponent<RectTransform>();
        startRect.anchorMin = new Vector2(0.5f, 0);
        startRect.anchorMax = new Vector2(0.5f, 0);
        startRect.pivot = new Vector2(0.5f, 0);
        startRect.anchoredPosition = new Vector2(-200, 50);
        startRect.sizeDelta = new Vector2(300, 100);
        TextMeshProUGUI startText = new GameObject("Text", typeof(TextMeshProUGUI)).AddComponent<TextMeshProUGUI>();
        startText.transform.SetParent(startButton.transform, false);
        startText.text = "START CAMPAIGN";
        startText.fontSize = 24;
        startText.alignment = TextAlignmentOptions.Center;

        GameObject randomButton = new GameObject("RandomButton", typeof(Button), typeof(Image));
        randomButton.transform.SetParent(canvas.transform, false);
        RectTransform randomRect = randomButton.GetComponent<RectTransform>();
        randomRect.anchorMin = new Vector2(0.5f, 0);
        randomRect.anchorMax = new Vector2(0.5f, 0);
        randomRect.pivot = new Vector2(0.5f, 0);
        randomRect.anchoredPosition = new Vector2(200, 50);
        randomRect.sizeDelta = new Vector2(300, 100);
        TextMeshProUGUI randomText = new GameObject("Text", typeof(TextMeshProUGUI)).AddComponent<TextMeshProUGUI>();
        randomText.transform.SetParent(randomButton.transform, false);
        randomText.text = "RANDOM";
        randomText.fontSize = 24;
        randomText.alignment = TextAlignmentOptions.Center;

        Debug.Log("Character Selection Scene setup complete!");
    }
}
