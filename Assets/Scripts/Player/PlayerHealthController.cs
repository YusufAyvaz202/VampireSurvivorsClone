using Interfaces;
using Managers;
using Misc;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            
            // Load and validate armor percentage from PlayerPrefs
            float loadedArmor = PlayerPrefs.GetFloat("ArmorPercentage", 0f);
            _armorPercentage = Mathf.Clamp01(loadedArmor); // Ensure value is between 0 and 1
            
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
            SceneManager.LoadScene(Const.GameBalance.MAIN_SCENE_INDEX);
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
            if (EconomyManager.Instance.CanSpend(Const.GameBalance.ARMOR_UPGRADE_COST))
            {
                _armorPercentage += Const.GameBalance.ARMOR_INCREASE_AMOUNT;
                _armorPercentage = Mathf.Clamp01(_armorPercentage); // Ensure it doesn't exceed 100%
            }
        }

        private void ResetHealth()
        {
            _currentHealth = _maxHealth;
            _healthUI.SetMaxHealth(_maxHealth);
            _healthUI.UpdateHealthBar(_currentHealth);
        }
    }
}