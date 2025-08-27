using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class PrizeManager : MonoBehaviour
    {
        [Header("Singleton")]
        public List<PrizeDataSO> _prizes = new();
        public List<PrizeDataSO> _collectedPrized = new();

        #region Unity Methods

        private void OnEnable()
        {
            EventManager.OnCurrentLevelChanged += ShowRandomPrize;
            EventManager.OnPrizeCollected += AddCollectedPrize;
        }
        
        private void OnDisable()
        {
            EventManager.OnCurrentLevelChanged -= ShowRandomPrize;
            EventManager.OnPrizeCollected -= AddCollectedPrize;
        }

        #endregion
        
        private void ShowRandomPrize()
        {
            int randomIndex = Random.Range(0, _prizes.Count);
            var prize = _prizes[randomIndex];
            
            prize.PrizeDescription = _collectedPrized.Contains(prize) ? prize.PrizeCollectedDescription : prize.PrizeNotCollectedDescription;
            EventManager.OnPrizeShowed?.Invoke(prize);
        }
        
        private void AddCollectedPrize(PrizeDataSO prizeDataSo)
        {
            _collectedPrized.Add(prizeDataSo);
        }
    }
}