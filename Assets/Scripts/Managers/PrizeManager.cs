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
        
        private List<PrizeDataSO> _prizeDataSo = new();

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
            //TODO Make sure no duplicate prizes are shown
            _prizeDataSo.Clear();
            for (int i = 0; i < 3; i++)
            {
                int randomIndex = Random.Range(0, _prizes.Count);
                var prize = _prizes[randomIndex];
            
                prize.PrizeDescription = _collectedPrized.Contains(prize) ? prize.PrizeCollectedDescription : prize.PrizeNotCollectedDescription;
                _prizeDataSo.Add(prize);
            }
            
            EventManager.OnPrizeShowed?.Invoke(_prizeDataSo);
        }
        
        private void AddCollectedPrize(PrizeDataSO prizeDataSo)
        {
            _collectedPrized.Add(prizeDataSo);
        }
    }
}