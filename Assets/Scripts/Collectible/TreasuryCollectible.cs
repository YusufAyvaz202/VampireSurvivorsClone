using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Collectible
{
    public class TreasuryCollectible: MonoBehaviour
    {
        [SerializeField] private int _minGoldAmount;
        [SerializeField] private int _maxGoldAmount;


        #region Unity Methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            RewardGold();
        }

        #endregion

        private void RewardGold()
        {
            EconomyManager.Instance.CollectGold(Random.Range(_minGoldAmount, _maxGoldAmount));
            Destroy(gameObject);
        }
    }
}