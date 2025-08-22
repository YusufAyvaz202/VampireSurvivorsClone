using System.Collections.Generic;
using Abstract;
using UnityEngine;

namespace Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private List<BaseGun> _guns = new();

        #region Unity Methods

        private void Start()
        {
            Attack();
        }

        #endregion
        
        private void Attack()
        {
            _guns[^1].StartAttackCooldown();
        }
        
        private void AddGun(BaseGun gun)
        {
            _guns.Add(gun);
            Attack();
        }
    }
}