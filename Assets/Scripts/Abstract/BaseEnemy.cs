using Interfaces;
using UnityEngine;
using Misc;
using UnityEngine.AI;

namespace Abstract
{
    public class BaseEnemy : MonoBehaviour, IAttacker, IAttackable
    {
        [Header("Enemy Settings")] private EnemyType _enemyType;

        [Header("AI Settings")] 
        [SerializeField] private Transform _target;
        private NavMeshAgent _navMeshAgent;
        
        [Header("Rotation Settings")]
        private Vector3 _rightRotate = new(0, 0, 0);
        private Vector3 _leftRotate = new(0, 180, 0);
        private Vector3 _faceDirection;

        #region Unity Methods

        private void Awake()
        {
            SetAISettings();
        }

        private void FixedUpdate()
        {
            MoveToTarget();
        }

        #endregion

        #region Base Methods

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Movement Methods

        private void MoveToTarget()
        {
            if (_target != null)
            {
                _navMeshAgent.SetDestination(_target.position);
                _faceDirection = (_target.position - transform.position).normalized;
                RotateFace();
            }
        }

        private void RotateFace()
        {
            if (_faceDirection.x == 0) return;
            Vector3 targetRotation = _faceDirection.x < 0 ? _leftRotate : _rightRotate;

            transform.rotation = Quaternion.Euler(targetRotation);
        }

        #endregion

        #region Helper Methods

        private void SetAISettings()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
        }

        #endregion
    }
}