using UnityEngine;

namespace Managers
{
    public class EconomyManager : MonoBehaviour
    {
        [Header("Singleton")]
        public static EconomyManager Instance;
        
        [Header("Settings")] 
        [SerializeField] private int _collectedGoldCount;
        [SerializeField] private int _totalGoldCount;

        #region Unity Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void OnEnable()
        {
            LoadTotalGoldCount();
        }

        private void OnDisable()
        {
            SaveTotalGoldCount();
        }

        #endregion

        public void CollectGold(int amount)
        {
            _collectedGoldCount += amount;
            EventManager.OnCurrentGoldChanged?.Invoke(_collectedGoldCount);
        }

        public bool CanSpend(int spendCost)
        {
            if (spendCost <= _totalGoldCount)
            {
                _totalGoldCount -= spendCost;
                return true;
            }
            return false;
        }

        private void SaveTotalGoldCount()
        {
            _totalGoldCount += _collectedGoldCount;
            PlayerPrefs.SetInt("TotalGoldCount", _totalGoldCount);
        }

        private void LoadTotalGoldCount()
        {
            _totalGoldCount = PlayerPrefs.GetInt("TotalGoldCount", _totalGoldCount);
            EventManager.OnTotalGoldChanged?.Invoke(_totalGoldCount);
        }
        
    }
}