using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Executive Disorder/Political Character")]
public class PoliticalCharacter : ScriptableObject
{
    [Header("Identity")]
    public string characterName;
    public string title;
    [TextArea(2, 3)]
    public string shortBio;
    [TextArea(4, 8)]
    public string fullBio;
    
    [Header("Starting Stats")]
    [Range(0, 100)] public int startingPopularity = 50;
    [Range(0, 100)] public int startingStability = 50;
    [Range(0, 100)] public int startingMedia = 50;
    [Range(0, 100)] public int startingEconomy = 50;
    
    [Header("Visuals")]
    public Sprite portrait;
    public Color themeColor = Color.white;
    
    [Header("Audio")]
    public AudioClip introVoiceLine;
    
    [Header("Unique Traits")]
    [TextArea(2, 4)]
    public string[] specialAbilities;
    
    [Header("AI Generation")]
    public bool useAIPortrait = false;
    [TextArea(2, 3)]
    public string aiPortraitPrompt;
}
