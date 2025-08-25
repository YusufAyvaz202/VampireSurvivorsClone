using GenericControllers;
using Interfaces;
using Managers;
using UnityEngine;
using Misc;
using UnityEngine.AI;

namespace Abstract
{
    public class BaseEnemy : MonoBehaviour, IAttacker, IAttackable
    {
        [Header("Enemy Settings")] 
        private EnemyType _enemyType;
        private HealthController _healthController;

        [Header("AI Settings")] 
        [SerializeField] private Transform _target;
        private NavMeshAgent _navMeshAgent;
        
        [Header("Rotation Settings")]
        private Vector3 _rightRotate = new(0, 0, 0);
        private Vector3 _leftRotate = new(0, 180, 0);
        private Vector3 _faceDirection;

        [Header("Game Settings")] 
        private bool _isPlaying;

        #region Unity Methods

        private void Awake()
        {
            SetAISettings();
            InitializeComponents();
        }

        private void OnEnable()
        {
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }
        
        private void FixedUpdate()
        {
            if (!_isPlaying) return;
            MoveToTarget();
        }
        
        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        #endregion

        #region Base Methods

        public void Attack()
        {
            if (!_isPlaying) return;
            //TODO: Implement attack logic
        }

        public void TakeDamage(int damage)
        {
            if (!_isPlaying) return;
            _healthController.TakeDamage(damage);
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
        
        private void InitializeComponents()
        {
            _healthController = GetComponent<HealthController>();
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Playing)
            {
                _isPlaying = true;
                _navMeshAgent.isStopped = false;
            }
            else
            {
                _isPlaying = false;
                _navMeshAgent.isStopped = true;
            }
        }

        #endregion
    }
}