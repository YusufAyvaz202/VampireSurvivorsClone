using System.Collections;
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
        private HealthController _healthController;
        private EnemyType _enemyType;
        private Animator _animator;

        [Header("AI Settings")] 
        [SerializeField] private Transform _target;
        private NavMeshAgent _navMeshAgent;
        
        [Header("Attack Settings")]
        private float _attackCooldown = 1f;
        private Coroutine _attackCoroutine;
        
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isPlaying) return;
            CheckTriggerEnters(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_isPlaying) return;
            CheckTriggerExits(other);
        }

        private void OnDisable()
        {
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        #endregion

        #region Base Methods

        private void CheckTriggerEnters(Collider2D other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                _attackCoroutine = StartCoroutine(StartAttackCooldown(attackable));
            }
        }
        
        private void CheckTriggerExits(Collider2D other)
        {
            if (other.TryGetComponent(out IAttackable _))
            {
                StopCoroutine(_attackCoroutine);
            }
        }
        
        private IEnumerator StartAttackCooldown(IAttackable attackable)
        {
            while (true)
            {
                while (!_isPlaying)
                {
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(_attackCooldown);
                Attack(attackable);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        public void Attack(IAttackable attackable)
        {
            if (!_isPlaying) return;
            
            PlayAttackAnimation();
            attackable.TakeDamage(10);
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
            _animator = GetComponentInChildren<Animator>();
        }

        private void PlayAttackAnimation()
        {
            _animator.SetTrigger(Const.OtherAnimation.ATTACK_ANIMATION);
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