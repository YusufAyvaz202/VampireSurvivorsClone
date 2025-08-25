using System.Collections.Generic;
using Abstract;
using GenericControllers;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerAttackController : MonoBehaviour, IAttackable
    {
        [Header("References")]
        [SerializeField] private List<BaseGun> _guns = new();
        private HealthController _healthController;

        #region Unity Methods

        private void Awake()
        {
            InitializeComponents();
        }

        private void Start()
        {
            Attack();
        }

        #endregion
        
        private void Attack()
        {
            _guns[^1].StartAttackCooldown();
        }
        
        private void AddGun(BaseGun gun)
        {
            _guns.Add(gun);
            Attack();
        }

        public void TakeDamage(int damage)
        {
            _healthController.TakeDamage(damage);
        }

        #region Helper Methods

        private void InitializeComponents()
        {
            _healthController = GetComponent<HealthController>();
        }

        #endregion
    }
}