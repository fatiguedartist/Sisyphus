
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sisyphus
{
    public class GameState
    {
        private static GameState _gameState = new GameState();
        public static GameState Instance { get { return _gameState; } }

        const int MaxLevel = 11;
        const int MinLevel = 3;
        public int Level { get; set; }
        public bool PlayerDiedRecently { get; set; }

        public event Action PlayerDied;
        public event Action<int> LevelChanged;

        private GameState()
        {
            Level = MinLevel;
        }

        public static void ResetGamestate()
        {
            Instance.Level = MinLevel;
        }

        public static void KillPlayer()
        {
            UnityEngine.Debug.Log("I should be ded");
            Instance.PlayerDiedRecently = true;

            ResetGamestate();
            SceneManager.LoadScene(0);
        }

        public static void IncreaseLevel()
        {
            Instance.PlayerDiedRecently = false;
            Instance.Level = Mathf.Max((Instance.Level + 1) % (MaxLevel + 1), MinLevel);
            UnityEngine.Debug.Log("Level increased to [" + Instance.Level + "]!");
            SceneManager.LoadScene(0);
        }
    }
}
