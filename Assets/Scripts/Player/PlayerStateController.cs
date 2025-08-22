using Managers;
using Misc;
using UnityEngine;

namespace Player
{
    public class PlayerStateController : MonoBehaviour
    {
        [Header("Singleton")]
        public static PlayerStateController Instance;
        
        [Header("References")]
        private PlayerState _playerState;

        #region Unity Methods

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            _playerState = PlayerState.Idle;
        }

        #endregion
        
        public void ChangeState(PlayerState newState)
        {
           if (_playerState == newState) return;
           
           _playerState = newState;
           EventManager.OnPlayerStateChange?.Invoke(_playerState);
        }
    }
}