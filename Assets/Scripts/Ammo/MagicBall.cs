using Interfaces;
using Managers;
using Misc;
using UnityEngine;

namespace Ammo
{
    public class MagicBall : MonoBehaviour
    {
        [Header("References")] 
        private Transform _targetTransform;
        private Rigidbody2D _rigidbody2D;
        
        [Header("Settings")]
        [SerializeField] private float _movementSpeed = 5f;
        private int _damage = 25;
        private bool _isPlaying;
        
        #region Unity Methods

        private void Awake()
        {
            InitializeComponents();
        }
        
        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageToTarget(other);
        }

        private void FixedUpdate()
        {
            if (!_isPlaying) return;
            MoveToTarget();
        }
        
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        #endregion

        // Move the magic ball towards the target
        private void MoveToTarget()
        {
            var direction = (_targetTransform.position - transform.position).normalized;
            _rigidbody2D.MovePosition(_rigidbody2D.position + (Vector2)direction * (Time.fixedDeltaTime * _movementSpeed));
        }

        // Deal damage to the target if it is attackable
        private void DamageToTarget(Collider2D other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                attackable.TakeDamage(_damage);
                EventManager.OnMagicBallAchieve?.Invoke(this);
            }
        }

        #region Helper Methods

        // Set target for the magic ball
        public void SetTarget(Transform target)
        {
            _targetTransform = target;
        }
        
        public void SetDamage(int damage)
        {
            _damage = damage;
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            _isPlaying = gameState == GameState.Playing;
        }
        
        private void InitializeComponents()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        private void SubscribeToEvents()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }
        
        private void UnsubscribeFromEvents()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        #endregion
    }
}