using Managers;
using Misc;
using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [Header("References")]
        private PlayerInputController _playerInputController;
        private Rigidbody2D _rigidbody2D;

        [Header("Movement Setting")]
        [SerializeField] private float _moveSpeed;
        private Vector3 _moveInput;
        private Vector3 _lastMoveInput;
        
        [Header("Rotation Settings")]
        private Vector3 _currentLookDirection;
        private Vector3 _rightRotate = new(0, 0, 0);
        private Vector3 _leftRotate = new(0, 180, 0);
        
        [Header("Game Settings")]
        private bool _isPlaying;
        
        #region Unity Methods

        private void Awake()
        {
            _playerInputController = GetComponent<PlayerInputController>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        private void OnEnable()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void Update()
        {
            _moveInput = _playerInputController.MoveInput;
        }

        private void FixedUpdate()
        {
            if (!_isPlaying) return;
            
            Move();
            RotateFace();
        }
        
        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        #endregion

        private void Move()
        {
            Vector3 newPosition = _moveInput * (_moveSpeed * Time.fixedDeltaTime) ;
            _rigidbody2D.MovePosition(transform.position + newPosition);
            
            SetPlayerState();
        }

        private void RotateFace()
        {
            if (_moveInput.x == 0) return;
            Vector3 targetRotation = _moveInput.x < 0 ? _leftRotate : _rightRotate;

            if (_currentLookDirection != targetRotation)
            {
                _currentLookDirection = targetRotation;
                transform.rotation = Quaternion.Euler(_currentLookDirection);
            }
        }

        #region Helper Methods

        private void SetPlayerState()
        {
            if (_moveInput == Vector3.zero && _lastMoveInput != _moveInput)
            {
                PlayerStateController.Instance.ChangeState(PlayerState.Idle);
                _lastMoveInput = Vector2.zero;
            }
            else if (_moveInput != Vector3.zero && _lastMoveInput != _moveInput)
            {
                PlayerStateController.Instance.ChangeState(PlayerState.Running);
                _lastMoveInput = _moveInput;
            }
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            _isPlaying = gameState == GameState.Playing;
            if (!_isPlaying)
            {
                _moveInput = Vector2.zero;
                SetPlayerState();
            }
        }

        #endregion
    }
}