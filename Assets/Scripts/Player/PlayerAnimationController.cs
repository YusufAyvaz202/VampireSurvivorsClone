using System;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [Header("References")]
        private Animator _animator;

        #region Unity Methods

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private void OnEnable()
        {
            EventManager.OnPlayerStateChanged += OnPlayerStateChange;
        }

        private void OnDisable()
        {
            EventManager.OnPlayerStateChanged -= OnPlayerStateChange;
        }

        #endregion
        
        private void PlayRunAnimation()
        {
            _animator.SetBool(Misc.Const.PlayerAnimation.RUN_ANIMATION, true);
        }
        
        private void PlayIdleAnimation()
        {
            _animator.SetBool(Misc.Const.PlayerAnimation.RUN_ANIMATION, false);
        }

        #region Helper Methods

        private void OnPlayerStateChange(Misc.PlayerState newState)
        {
            switch (newState)
            {
                case Misc.PlayerState.Idle:
                    PlayIdleAnimation();
                    break;
                case Misc.PlayerState.Running:
                    PlayRunAnimation();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        #endregion
        
    }
}