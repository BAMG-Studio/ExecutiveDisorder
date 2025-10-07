// 10/6/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewCharacterDatabase", menuName = "Executive Disorder/Character Database")]
public class CharacterDatabase : ScriptableObject
{
    public List<PoliticalCharacter> allCharacters;

    public PoliticalCharacter GetCharacterByName(string name)
    {
        foreach (var character in allCharacters)
        {
            if (character.characterName == name)
            {
                return character;
            }
        }
        return null; // Return null if no character with the given name is found
    }
}
