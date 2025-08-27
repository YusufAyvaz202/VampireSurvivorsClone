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
        [SerializeField] protected int _attackDamage;

        [Header("Game Settings")] 
        private bool _isPlaying;

        #region Unity Methods

        protected virtual void Awake()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
            StopAllCoroutines();
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
        
        public void IncreaseAttackDamage()
        {
            // Increase attack damage by 5%
            _attackDamage += _attackDamage / 20;
        }

        private void SubscribeToEvents()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void UnsubscribeToEvents()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            _isPlaying = gameState == GameState.Playing;
        }

        #endregion
    }
}