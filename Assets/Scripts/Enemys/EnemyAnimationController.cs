using Misc;
using UnityEngine;

namespace Enemys
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [Header("References")]
        private Animator _animator;

        #region Unity Methods

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        #endregion
        
        public void PlayAttackAnimation()
        {
            _animator.SetTrigger(Const.OtherAnimation.ATTACK_ANIMATION);
        }
    }
}