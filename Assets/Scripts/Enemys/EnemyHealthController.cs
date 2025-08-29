using Abstract;
using Interfaces;
using Managers;
using Misc;
using UI;
using UnityEngine;

namespace Enemys
{
    [RequireComponent(typeof(HealthUI))]
    public class EnemyHealthController : MonoBehaviour, IAttackable
    {
        [Header("References")]
        private HealthUI _healthUI;
        private BaseEnemy _baseEnemy;
        
        [Header("Health Settings")]
        [SerializeField] private float _maxHealth;
        private float _currentHealth;

        #region Unity Methods

        private void Awake()
        {
            _currentHealth = _maxHealth;
            
            _healthUI = GetComponent<HealthUI>();
            _baseEnemy = GetComponent<BaseEnemy>();
            
            _healthUI.SetMaxHealth(_maxHealth);
        }

        private void OnEnable()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        #endregion
        
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _healthUI.UpdateHealthBar(_currentHealth);
            if (_currentHealth <= 0)
            {
                ResetHealth();
                Die();
            }
        }

        private void Die()
        {
            EventManager.OnEnemyDiePosition?.Invoke(transform.position);
            EventManager.OnEnemyDied?.Invoke(_baseEnemy);
        }

        private void ResetHealth()
        {
            _currentHealth = _maxHealth;
            _healthUI.UpdateHealthBar(_currentHealth);
        }

        #region Helper Methods
        
        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.GameOver)
            {
                ResetHealth();
                EventManager.OnEnemyDied?.Invoke(_baseEnemy);
            }
        }

        #endregion
    }
}