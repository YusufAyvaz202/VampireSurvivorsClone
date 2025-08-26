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
        [SerializeField] private float _attackCooldown;

        [Header("Game Settings")] 
        private bool _isPlaying;

        #region Unity Methods

        private void Awake()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }
        #endregion
        
        #region Base Methods
        
        public abstract void Attack(IAttackable attackable);
        
        private IEnumerator WaitAttackCooldown()
        {
            while (true)
            {
                while (!_isPlaying)
                {
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(_attackCooldown);
                Attack(null);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        #endregion
        
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