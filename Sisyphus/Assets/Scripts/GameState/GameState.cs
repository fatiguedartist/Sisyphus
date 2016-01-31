
using System;
using UnityEngine;

namespace Sisyphus
{
    public class GameState
    {
        private static GameState _gameState = new GameState();
        public static GameState Instance { get { return _gameState; } }

        const int MaxLevel = 11;
        public int Level { get; set; }
        public bool PlayerIsDead { get; set; }

        public event Action PlayerDied;
        public event Action<int> LevelChanged;

        private GameState()
        {
            Level = 3;
            PlayerIsDead = false;
        }

        public static void ResetGamestate()
        {
            _gameState = new GameState();
        }

        public static void KillPlayer()
        {
            UnityEngine.Debug.Log("I should be ded");
            Instance.PlayerIsDead = true;
            Instance.PlayerDied.Raise();
            ResetGamestate();

            Application.LoadLevel(0);
        }

        public static void IncreaseLevel()
        {
            Instance.Level = Mathf.Min(Instance.Level + 1, MaxLevel);
            UnityEngine.Debug.Log("Level increased to [" + Instance.Level + "]!");

            Application.LoadLevel(0);
        }
    }
}
