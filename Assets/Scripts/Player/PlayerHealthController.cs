using Interfaces;
using Managers;
using Misc;
using UI;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(HealthUI))]
    public class PlayerHealthController : MonoBehaviour, IAttackable ,IHealable
    {
        [Header("References")]
        private HealthUI _healthUI;
        
        [Header("Health Settings")]
        [SerializeField] private float _maxHealth;
        private float _currentHealth;

        #region Unity Methods

        private void Awake()
        {
            _currentHealth = _maxHealth;
            
            _healthUI = GetComponent<HealthUI>();
            _healthUI.SetMaxHealth(_maxHealth);
        }

        #endregion
        
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _healthUI.UpdateHealthBar(_currentHealth);
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            GameManager.Instance.ChangeGameState(GameState.GameOver);
        }

        public void Heal(int healAmount)
        {
            if (_currentHealth + healAmount > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            else
            {
                _currentHealth += healAmount;
            }
            _healthUI.UpdateHealthBar(_currentHealth);
        }
    }
}