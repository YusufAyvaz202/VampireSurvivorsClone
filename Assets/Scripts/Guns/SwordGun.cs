using System.Collections;
using Abstract;
using Ammo;
using Interfaces;
using UnityEngine;

namespace Guns
{
    public class SwordGun : BaseGun
    {
        [Header("Settings")]
        [SerializeField] private float _deactivateDelay = 3f;
        
        [Header("References")]
        private Coroutine _coroutine;
        private Sword _sword;
        
        #region Unity Methods
        
        protected override void Awake()
        {
            base.Awake();
            InitializeSword();
        }

        protected override void  OnDisable()
        {
            base.OnDisable();
            StopCoroutine();
        }
        
        #endregion
        
        public override void Attack(IAttackable attackable)
        {
            if (_coroutine != null) return;
            _coroutine = StartCoroutine(DeactivateSwordAfterDelay());
        }
        
        private IEnumerator DeactivateSwordAfterDelay()
        {
            while (true)
            {
                _sword.gameObject.SetActive(true);
                yield return new WaitForSeconds(_deactivateDelay);
            
                _sword.transform.SetParent(transform);
                _sword.transform.localPosition = Vector3.zero;
                _sword.gameObject.SetActive(false);
                
                yield return new WaitForSeconds(_attackCooldown);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        #region Helper Methods

        private void InitializeSword()
        {
            _sword = GetComponentInChildren<Sword>();
            _sword.SetDamage(_attackDamage);
        }

        private void StopCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
                _sword.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}