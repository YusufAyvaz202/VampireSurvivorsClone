using System;
using Misc;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Singleton")]
        public static GameManager Instance;
        
        [Header("Settings")]
        private GameState _gameState;

        #region Unity Methods

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            ChangeGameState(GameState.Playing);
        }

        #endregion

        public void ChangeGameState(GameState gameState)
        {
            if (_gameState == gameState) return;
            
            _gameState = gameState;
            EventManager.OnGameStateChanged?.Invoke(_gameState);
            Debug.Log($"GameState changed to {_gameState}");
        }
    }
}