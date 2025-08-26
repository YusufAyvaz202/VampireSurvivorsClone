using Abstract;
using Interfaces;
using Misc;
using UnityEngine;

namespace Guns
{
    public class Axe : BaseGun
    {
        [Header("Axe Settings")]
        private const int _attackDamage = 5;
        private IAttackable _attackable;
        private Animator _animator;

        #region Unity Methods

        private void Awake()
        {
            InitializeComponents();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            SetAttackable(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            ResetAttackable(other);
        }

        #endregion
        
        //TODO: Convert to the area attack with _attackable list.
        public override void Attack(IAttackable attackable)
        {
            PlayAttackAnimation();
            
            if (_attackable == null) return;
            _attackable.TakeDamage(_attackDamage);
        }

        #region Helper Methods

        private void InitializeComponents()
        {
            _animator = GetComponentInChildren<Animator>();
        }
        
        private void PlayAttackAnimation()
        {
            _animator.SetTrigger(Const.OtherAnimation.ATTACK_ANIMATION);
        }
        
        private void SetAttackable(Collider2D other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                _attackable = attackable;
            }
        }
        
        private void ResetAttackable(Collider2D other)
        {
            if (other.TryGetComponent(out IAttackable _))
            {
                _attackable = null;
            }
        }

        #endregion
    }
}