// 10/6/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CharacterCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image portraitImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI bioText;
    [SerializeField] private Button selectButton;
    [SerializeField] private Image backgroundImage;

    private PoliticalCharacter character;
    private Action<PoliticalCharacter> onSelectCallback;

    // Initialize the card with a character and a callback
    public void Initialize(PoliticalCharacter character, Action<PoliticalCharacter> onSelectCallback)
    {
        this.character = character;
        this.onSelectCallback = onSelectCallback;

        UpdateDisplay();

        // Set up the button click event
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => onSelectCallback?.Invoke(character));
    }

    // Update the display with the character's data
    private void UpdateDisplay()
    {
        if (character == null) return;

        nameText.text = character.characterName;
        titleText.text = character.title;
        bioText.text = character.shortBio;
        portraitImage.sprite = character.portrait;
        backgroundImage.color = character.themeColor;
    }

    // Handle hover effects when the pointer enters
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Example hover effect: slightly brighten the background
        backgroundImage.color = character.themeColor * 1.2f;
    }

    // Handle hover effects when the pointer exits
    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the background color
        backgroundImage.color = character.themeColor;
    }
}
