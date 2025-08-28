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

        public void CollectGun(BaseGun gun)
        {
            var incomingGunType = gun.GetGunType();

            foreach (var existingGun in _guns)
            {
                if (existingGun.GetGunType() == incomingGunType)
                {
                    existingGun.IncreaseAttackDamage();
                    Debug.Log($"Improved gun: {existingGun.GetType().Name}");
                    return;
                }
            }

            Debug.Log("Adding new gun...");
            var newGun = Instantiate(gun, _gunParentTransform);
            newGun.transform.localPosition = Vector3.zero;
            newGun.transform.localRotation = Quaternion.identity;
            newGun.transform.localScale = Vector3.one;

            _guns.Add(newGun);
            StartLastGunCoolDown();
        }

        #endregion
    }
}