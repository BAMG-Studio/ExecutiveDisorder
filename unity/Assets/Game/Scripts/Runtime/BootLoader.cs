using ExecutiveDisorder.Game.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExecutiveDisorder.Game
{
    public class BootLoader : MonoBehaviour
    {
        [SerializeField] private string databaseResourcePath = "Generated/GameDatabase";
        [SerializeField] private string nextScene = "MainMenu";

        private void Start()
        {
            var db = Resources.Load<GameDatabase>(databaseResourcePath);
            if (db == null)
            {
                Debug.LogError($"BootLoader: Could not load GameDatabase at Resources/{databaseResourcePath}. Did you run the importer?");
                return;
            }

            GameContext.Database = db;
            SceneManager.LoadScene(nextScene);
        }
    }
}

