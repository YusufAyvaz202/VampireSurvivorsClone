using System.Collections.Generic;
using Abstract;
using Interfaces;
using Misc;
using UnityEngine;

namespace Guns
{
    // it doesn't work right because of list and bool _isAttacking. refactor it later
    public class Axe : BaseGun
    {
        [Header("Axe Settings")]
        private const int _attackDamage = 5;
        private List<IAttackable> _attackableList = new();
        private Animator _animator;
        private bool _isAttacking;
        
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
        
        public override void Attack(IAttackable _)
        {
            PlayAttackAnimation();
            
            if (_attackableList == null) return;

            _isAttacking = true;
            foreach (var attackable in _attackableList)
            {
                if(attackable == null) continue;
                attackable.TakeDamage(_attackDamage);
            }
            _isAttacking = false;
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
            if (_isAttacking) return;
            if (other.TryGetComponent(out IAttackable attackable))
            {
                if (_attackableList.Contains(attackable)) return;
                _attackableList.Add(attackable);
            }
        }
        
        private void ResetAttackable(Collider2D other)
        {
            if (_isAttacking) return;
            if (other.TryGetComponent(out IAttackable attackable))
            {
                if (!_attackableList.Contains(attackable)) return;
                _attackableList.Remove(attackable);
            }
        }

        #endregion
    }
}