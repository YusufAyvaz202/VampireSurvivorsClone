using Interfaces;
using Managers;
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
        [SerializeField] private int _damage = 25;
        

        #region Unity Methods

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageToTarget(other);
        }

        private void FixedUpdate()
        {
            MoveToTarget();
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

        #endregion
    }
}