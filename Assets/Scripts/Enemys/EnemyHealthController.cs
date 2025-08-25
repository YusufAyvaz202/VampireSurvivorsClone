using Interfaces;
using UI;
using UnityEngine;

namespace Enemys
{
    [RequireComponent(typeof(HealthUI))]
    public class EnemyHealthController : MonoBehaviour, IAttackable
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
            //TODO: Add death logic with EventManager and object Pooling.
            Debug.Log($"{gameObject.name} died.");
        }
    }
}