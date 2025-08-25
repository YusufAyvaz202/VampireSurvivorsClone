using UI;
using UnityEngine;

namespace GenericControllers
{
    [RequireComponent(typeof(HealthUI))]
    public class HealthController : MonoBehaviour
    {
        [Header("References")]
        private HealthUI _healthUI;
        
        [Header("Health Settings")]
        private float _maxHealth;
        private float _currentHealth;

        #region Unity Methods

        private void Awake()
        {
            _healthUI = GetComponent<HealthUI>();
            _healthUI.SetMaxHealth(_maxHealth);
        }

        #endregion
        
        public void TakeDamage(float damage)
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
            //TODO: Add death logic
        }
    }
}