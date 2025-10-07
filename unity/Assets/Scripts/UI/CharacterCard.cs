using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class CharacterCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Elements")]
    [SerializeField] private Image portraitImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI bioText;
    [SerializeField] private Button selectButton;
    [SerializeField] private Image backgroundImage;
    
    private PoliticalCharacter character;
    private Action<PoliticalCharacter> onSelectCallback;
    
    public PoliticalCharacter Character => character;
    
    public void Initialize(PoliticalCharacter character, Action<PoliticalCharacter> onSelect)
    {
        this.character = character;
        this.onSelectCallback = onSelect;
        
        UpdateDisplay();
        selectButton.onClick.AddListener(OnSelectClicked);
    }
    
    void UpdateDisplay()
    {
        portraitImage.sprite = character.portrait;
        nameText.text = character.characterName;
        titleText.text = character.title;
        bioText.text = character.shortBio;
        if (backgroundImage) backgroundImage.color = character.themeColor;
    }
    
    public void UpdatePortrait(Sprite newPortrait)
    {
        portraitImage.sprite = newPortrait;
    }
    
    void OnSelectClicked()
    {
        onSelectCallback?.Invoke(character);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(AudioManager.SoundType.Pop, volume: 0.3f);
        transform.localScale = Vector3.one * 1.05f;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}
