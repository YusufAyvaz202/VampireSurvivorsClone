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
        [SerializeField] private float _currentHealth;

        [Header("Armor Settings")]
        [Range(0,1)][SerializeField] private float _armorPercentage;

        #region Unity Methods

        private void Awake()
        {
            _currentHealth = _maxHealth;
            _armorPercentage = PlayerPrefs.GetFloat("ArmorPercentage");
            _healthUI = GetComponent<HealthUI>();
            _healthUI.SetMaxHealth(_maxHealth);
        }

        private void OnEnable()
        {
            EventManager.OnAddArmorButtonClicked += IncreaseArmor;
        }

        private void OnDisable()
        {
            EventManager.OnAddArmorButtonClicked -= IncreaseArmor;
            PlayerPrefs.SetFloat("ArmorPercentage", _armorPercentage);
        }

        #endregion

        [ContextMenu("Armor test")]
        public void ArmorTest()
        {
            TakeDamage(50);
        }
        
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage * (1 - _armorPercentage);
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

        private void IncreaseArmor()
        {
            if(_armorPercentage >= 1) return;
            if (EconomyManager.Instance.CanSpend(50))
            {
                _armorPercentage += 0.1f;
            }
        }
    }
}