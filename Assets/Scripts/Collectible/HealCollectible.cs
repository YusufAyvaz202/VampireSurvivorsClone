using Interfaces;
using UnityEngine;

namespace Collectible
{
    public class HealCollectible : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int _healAmount;

        #region Unity Methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IHealable healable))
            {
                healable.Heal(_healAmount);
                Destroy(gameObject);
            }
        }

        #endregion
        
    }
}