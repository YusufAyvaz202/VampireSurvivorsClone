using System.Collections;
using Interfaces;
using Managers;
using Misc;
using UnityEngine;

namespace Abstract
{
    public abstract class BaseGun: MonoBehaviour, IAttacker
    {
        [Header("Gun Settings")]
        private float _attackCooldown = 2f;
        
        [Header("Game Settings")] 
        private bool _isPlaying;

        #region Unity Methods

        private void OnEnable()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }
        #endregion
        
        #region Base Methods
        
        public abstract void Attack();

        #endregion
        
        private IEnumerator WaitAttackCooldown()
        {
            while (true)
            {
                while (!_isPlaying)
                {
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(_attackCooldown);
                Attack();
            }
            // ReSharper disable once IteratorNeverReturns
        }

        #region Helper Methods

        public void StartAttackCooldown()
        {
            StartCoroutine(WaitAttackCooldown());
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            _isPlaying = gameState == GameState.Playing;
        }

        #endregion
    }
}