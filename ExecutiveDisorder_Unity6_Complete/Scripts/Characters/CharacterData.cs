using UnityEngine;
using System.Collections.Generic;

namespace ExecutiveDisorder.Core
{
    /// <summary>
    /// ScriptableObject representing a political character
    /// </summary>
    [CreateAssetMenu(fileName = "New Character", menuName = "Executive Disorder/Character")]
    public class CharacterData : ScriptableObject
    {
        [Header("Identity")]
        public string characterId = "character_001";
        public string characterName = "Character Name";
        public string title = "Political Title";
        public CharacterArchetype archetype = CharacterArchetype.Populist;

        [Header("Biography")]
        [TextArea(3, 6)]
        public string biography = "Character background...";

        [Header("Personality")]
        public List<string> traits = new List<string>();
        public List<string> likes = new List<string>();
        public List<string> dislikes = new List<string>();

        [Header("Stats")]
        [Range(0, 100)]
        public int startingLoyalty = 50;
        [Range(0, 100)]
        public int competence = 50;
        [Range(0, 100)]
        public int chaosFactor = 50;
        public string specialAbility = "";

        [Header("Dialogue")]
        public List<DialogueLine> greetings = new List<DialogueLine>();
        public List<DialogueLine> happyLines = new List<DialogueLine>();
        public List<DialogueLine> angryLines = new List<DialogueLine>();
        public List<DialogueLine> crisisLines = new List<DialogueLine>();
        public List<DialogueLine> adviceLines = new List<DialogueLine>();
        public List<DialogueLine> farewellLines = new List<DialogueLine>();

        [Header("Relationships")]
        public List<string> alliedCharacterIds = new List<string>();
        public List<string> rivalCharacterIds = new List<string>();

        [Header("Visual")]
        public Sprite portraitNeutral;
        public Sprite portraitHappy;
        public Sprite portraitAngry;
        public Sprite portraitWorried;
        public Color characterColor = Color.white;
        public Sprite backgroundImage;

        [Header("Audio")]
        public AudioClip voiceClip;
        public AudioClip themeMusic;

        [Header("Game State")]
        [HideInInspector] public bool isActive = true;
        [HideInInspector] public int currentLoyalty;

        private void OnEnable()
        {
            currentLoyalty = startingLoyalty;
        }

        /// <summary>
        /// Get random dialogue line of specific type
        /// </summary>
        public string GetRandomDialogue(DialogueType type)
        {
            List<DialogueLine> lines = type switch
            {
                DialogueType.Greeting => greetings,
                DialogueType.Happy => happyLines,
                DialogueType.Angry => angryLines,
                DialogueType.Crisis => crisisLines,
                DialogueType.Advice => adviceLines,
                DialogueType.Farewell => farewellLines,
                _ => greetings
            };

            if (lines.Count == 0)
                return $"{characterName}: ...";

            return lines[Random.Range(0, lines.Count)].text;
        }

        /// <summary>
        /// Get portrait sprite based on emotion
        /// </summary>
        public Sprite GetPortrait(CharacterEmotion emotion)
        {
            return emotion switch
            {
                CharacterEmotion.Happy => portraitHappy != null ? portraitHappy : portraitNeutral,
                CharacterEmotion.Angry => portraitAngry != null ? portraitAngry : portraitNeutral,
                CharacterEmotion.Worried => portraitWorried != null ? portraitWorried : portraitNeutral,
                _ => portraitNeutral
            };
        }

        /// <summary>
        /// Modify character loyalty
        /// </summary>
        public void ModifyLoyalty(int amount)
        {
            currentLoyalty = Mathf.Clamp(currentLoyalty + amount, 0, 100);
        }

        /// <summary>
        /// Reset to starting state
        /// </summary>
        public void Reset()
        {
            currentLoyalty = startingLoyalty;
            isActive = true;
        }
    }

    /// <summary>
    /// Dialogue line with optional conditions
    /// </summary>
    [System.Serializable]
    public class DialogueLine
    {
        [TextArea(1, 3)]
        public string text;
        public int minLoyalty = 0;
        public int maxLoyalty = 100;
    }

    public enum CharacterArchetype
    {
        IguanaKing,        // Rex Scaleston III - Conspiracy theorist monarch
        Executive45,       // Donald J. Executive - Former everything
        MascotBot,         // POTUS-9000 - Sentient AI mascot
        Progressive,       // Alexandria Sanders-Warren - Idealistic reformer
        Corporate,         // Richard M. Moneybags III - Business lobbyist
        Military,          // General James 'Ironside' Steel - Military hawk
        MediaMogul,        // Diana Newsworthy - Press baron
        Populist           // Johnny Q. Public - Voice of the people
    }

    public enum DialogueType
    {
        Greeting,
        Happy,
        Angry,
        Crisis,
        Advice,
        Farewell
    }

    public enum CharacterEmotion
    {
        Neutral,
        Happy,
        Angry,
        Worried
    }
}
