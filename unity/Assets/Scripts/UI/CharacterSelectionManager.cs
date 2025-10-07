using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelectionManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform characterGridParent;
    [SerializeField] private GameObject characterCardPrefab;
    [SerializeField] private GameObject selectedCharacterPanel;
    [SerializeField] private Button startCampaignButton;
    [SerializeField] private Button randomButton;
    [SerializeField] private Button backButton;
    
    [Header("Selected Character Display")]
    [SerializeField] private Image selectedPortrait;
    [SerializeField] private TextMeshProUGUI selectedName;
    [SerializeField] private TextMeshProUGUI selectedBio;
    [SerializeField] private TextMeshProUGUI selectedStats;
    
    [Header("Data")]
    [SerializeField] private CharacterDatabase characterDatabase;
    
    [Header("Components")]
    [SerializeField] private AICharacterGenerator aiGenerator;
    
    private PoliticalCharacter selectedCharacter;
    private List<CharacterCard> characterCards = new List<CharacterCard>();
    
    void Start()
    {
        GenerateCharacterCards();
        startCampaignButton.interactable = false;
        selectedCharacterPanel.SetActive(false);
        
        startCampaignButton.onClick.AddListener(OnStartCampaignClicked);
        randomButton.onClick.AddListener(OnRandomCharacterClicked);
        if (backButton) backButton.onClick.AddListener(OnBackClicked);
        
        AudioManager.Instance.PlayMusic(AudioManager.SoundType.RelaxingGuitar, fadeSeconds: 2f);
        
        StartCoroutine(GenerateAIPortraits());
    }
    
    void GenerateCharacterCards()
    {
        foreach (var character in characterDatabase.allCharacters)
        {
            GameObject cardObj = Instantiate(characterCardPrefab, characterGridParent);
            CharacterCard card = cardObj.GetComponent<CharacterCard>();
            card.Initialize(character, OnCharacterSelected);
            characterCards.Add(card);
        }
    }
    
    IEnumerator GenerateAIPortraits()
    {
        foreach (var character in characterDatabase.allCharacters)
        {
            if (character.useAIPortrait && character.portrait == null)
            {
                yield return aiGenerator.GenerateCharacterPortrait(
                    character.characterName,
                    character.aiPortraitPrompt,
                    texture =>
                    {
                        if (texture != null)
                        {
                            character.portrait = Sprite.Create(
                                texture,
                                new Rect(0, 0, texture.width, texture.height),
                                new Vector2(0.5f, 0.5f)
                            );
                            UpdateCharacterCard(character);
                        }
                    }
                );
                yield return new WaitForSeconds(1f);
            }
        }
    }
    
    void UpdateCharacterCard(PoliticalCharacter character)
    {
        var card = characterCards.Find(c => c.Character == character);
        card?.UpdatePortrait(character.portrait);
    }
    
    void OnCharacterSelected(PoliticalCharacter character)
    {
        selectedCharacter = character;
        AudioManager.Instance.PlaySFX(AudioManager.SoundType.Stamp, volume: 0.8f);
        
        UpdateSelectedCharacterPanel(character);
        startCampaignButton.interactable = true;
        selectedCharacterPanel.SetActive(true);
        
        if (character.introVoiceLine != null)
        {
            AudioManager.Instance.PlayClip(character.introVoiceLine);
        }
    }
    
    void UpdateSelectedCharacterPanel(PoliticalCharacter character)
    {
        selectedPortrait.sprite = character.portrait;
        selectedName.text = $"{character.characterName}\n<size=60%>{character.title}</size>";
        selectedBio.text = character.fullBio;
        
        selectedStats.text = $"Popularity: {GetStarRating(character.startingPopularity)}\n" +
                            $"Stability: {GetStarRating(character.startingStability)}\n" +
                            $"Media Trust: {GetStarRating(character.startingMedia)}\n" +
                            $"Economy: {GetStarRating(character.startingEconomy)}";
    }
    
    string GetStarRating(int value)
    {
        int stars = Mathf.RoundToInt(value / 20f);
        return new string('★', stars) + new string('☆', 5 - stars);
    }
    
    public void OnStartCampaignClicked()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SoundType.Correct);
        AudioManager.Instance.StopMusic(fadeSeconds: 1f);
        
        // TODO: Save to GameStateManager
        StateManager.Instance.SwitchState(AppState.GameStart);
    }
    
    public void OnRandomCharacterClicked()
    {
        int randomIndex = Random.Range(0, characterDatabase.allCharacters.Count);
        OnCharacterSelected(characterDatabase.allCharacters[randomIndex]);
    }
    
    void OnBackClicked()
    {
        AudioManager.Instance.StopMusic(fadeSeconds: 1f);
        StateManager.Instance.SwitchState(AppState.MainMenu);
    }
}
