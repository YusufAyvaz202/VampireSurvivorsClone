using Abstract;
using Interfaces;
using Misc;
using UnityEngine;

namespace Guns
{
    public class Axe : BaseGun
    {
        [Header("Axe Settings")]
        private IAttackable _attackable;
        private Animator _animator;

        #region Unity Methods

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        #endregion
        
        //TODO: Convert to the area attack with _attackable list.
        public override void Attack(IAttackable attackable)
        {
            _animator.SetTrigger(Const.OtherAnimation.ATTACK_ANIMATION);
            
            if (_attackable == null) return;
            _attackable.TakeDamage(_attackDamage);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                _attackable = attackable;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out IAttackable _))
            {
                _attackable = null;
            }
        }
    }
}