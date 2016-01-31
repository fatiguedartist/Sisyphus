
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
        public int Level { get; set; }
        public bool PlayerDiedRecently { get; set; }

        public event Action PlayerDied;
        public event Action<int> LevelChanged;

        private GameState()
        {
            Level = 6;
        }

        public static void ResetGamestate()
        {
            Instance.Level = 3;
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
            Instance.Level = Mathf.Min(Instance.Level + 1, MaxLevel);
            UnityEngine.Debug.Log("Level increased to [" + Instance.Level + "]!");
            SceneManager.LoadScene(0);
        }
    }
}
