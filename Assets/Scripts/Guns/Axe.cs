using System.Collections.Generic;
using Abstract;
using Interfaces;
using Misc;
using UnityEngine;

namespace Guns
{
    public class Axe : BaseGun
    {
        [Header("Axe Settings")] 
        private List<IAttackable> _attackableList = new();
        private Animator _animator;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
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

        public override void Attack(IAttackable _attackable)
        {
            PlayAttackAnimation();
            
            // Attack enemies directly without creating a copy of the list
            // Use reverse iteration to safely handle list modifications during iteration
            for (int i = _attackableList.Count - 1; i >= 0; i--)
            {
                if (_attackableList[i] == null)
                {
                    _attackableList.RemoveAt(i);
                    continue;
                }
                _attackableList[i].TakeDamage(_attackDamage);
            }
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
                if (_attackableList.Contains(attackable)) return;
                _attackableList.Add(attackable);
            }
        }

        private void ResetAttackable(Collider2D other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                if (!_attackableList.Contains(attackable)) return;
                _attackableList.Remove(attackable);
            }
        }

        #endregion
    }
}