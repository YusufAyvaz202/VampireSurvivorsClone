using System.Collections;
using Interfaces;
using UnityEngine;

namespace Abstract
{
    public abstract class BaseGun: MonoBehaviour, IAttacker
    {
        [Header("Gun Settings")]
        private float _attackCooldown = 2f;
        
        #region Base Methods
        
        public abstract void Attack();

        #endregion
        
        private IEnumerator WaitAttackCooldown()
        {
            while (true)
            {
                yield return new WaitForSeconds(_attackCooldown);
                Attack();
            }
            // ReSharper disable once IteratorNeverReturns
        }

        #region Helper Methods

        public void StartAttackCooldown()
        {
            StartCoroutine(WaitAttackCooldown());
        }

        #endregion
    }
}