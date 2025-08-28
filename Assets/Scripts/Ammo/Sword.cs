using Interfaces;
using UnityEngine;

namespace Ammo
{
    public class Sword : MonoBehaviour
    {
        [Header("References")] 
        private Rigidbody2D _rigidbody2D;

        [Header("Settings")]
        [SerializeField] private float _forceAmount;
        private int _damage = 25;

        #region Unity Methods

        private void Awake()
        {
            InitializeComponents();
        }

        private void OnEnable()
        {
            AddForce();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Attack(other);
        }

        #endregion

        private void AddForce()
        {
            transform.SetParent(null);
            _rigidbody2D.AddForce(Vector2.up * _forceAmount, ForceMode2D.Impulse);
        }

        private void Attack(Collider2D other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                attackable.TakeDamage(_damage);
            }
        }

        #region Helper Methods

        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        private void InitializeComponents()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        #endregion
    }
}