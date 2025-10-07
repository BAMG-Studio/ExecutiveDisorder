using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Executive Disorder/Character Database")]
public class CharacterDatabase : ScriptableObject
{
    public List<PoliticalCharacter> allCharacters = new List<PoliticalCharacter>();
    
    public PoliticalCharacter GetCharacterByName(string name)
    {
        return allCharacters.Find(c => c.characterName == name);
    }
}
