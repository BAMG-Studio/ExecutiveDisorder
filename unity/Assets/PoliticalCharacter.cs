// 10/6/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Executive Disorder/Political Character")]
public class PoliticalCharacter : ScriptableObject
{
    public string characterName;
    public string title;

    [TextArea]
    public string shortBio;

    [TextArea]
    public string fullBio;

    [Range(0, 100)]
    public int startingPopularity;

    [Range(0, 100)]
    public int startingStability;

    [Range(0, 100)]
    public int startingMedia;

    [Range(0, 100)]
    public int startingEconomy;

    public Sprite portrait;
    public Color themeColor;
    public AudioClip introVoiceLine;

    public string[] specialAbilities;

    public bool useAIPortrait;

    [TextArea]
    public string aiPortraitPrompt;
}
