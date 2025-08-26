using System.Collections.Generic;
using Abstract;
using UnityEngine;

namespace Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _gunParentTransform;
        [SerializeField] private List<BaseGun> _guns = new();

        #region Unity Methods

        private void Start()
        {
            StartLastGunCoolDown();
        }

        #endregion
        
        private void StartLastGunCoolDown()
        {
            _guns[^1].StartAttackCooldown();
        }
        
        #region Helper Methods

        private void AddGun(BaseGun gun)
        {
            gun.transform.SetParent(_gunParentTransform);
            
            _guns.Add(gun);
            StartLastGunCoolDown();
        }

        #endregion
    }
}