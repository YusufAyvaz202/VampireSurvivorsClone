using System.Collections;
using System.Collections.Generic;
using Abstract;
using Misc;
using ObjectPooling;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [Header("Singleton")]
        public static EnemySpawnManager Instance;
        
        [Header("Settings")]
        private Dictionary<EnemyType, ObjectPool<BaseEnemy>> _enemyPools = new();
        [SerializeField] private List<BaseEnemy> _enemyPrefabs; 
        [SerializeField] private float _spawnInterval = 2f;
        private int _initialSpawnCount = 10;
        private float _spawnRadius = 20f;
        private bool _isPlaying;
        
        [Header("References")]
        [SerializeField] private Transform _playerTransform;

        #region Unity Methods

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemyRoutine());
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
            StopAllCoroutines();
        }

        #endregion
        
        private IEnumerator SpawnEnemyRoutine()
        {
            while (true)
            {
                while (!_isPlaying)
                {
                    yield return new WaitForEndOfFrame();
                }
                
                SpawnEnemy(EnemyType.FlyingEye, Random.insideUnitCircle * _spawnRadius);
                yield return new WaitForSeconds(_spawnInterval);
            }
            // ReSharper disable once IteratorNeverReturns
        }
        
        private void SpawnEnemy(EnemyType enemyType, Vector3 position)
        {
            if (_enemyPools.TryGetValue(enemyType, out var pool))
            {
                var enemy = pool.GetObject();
                enemy.transform.position = position;
                enemy.SetTarget(_playerTransform);
                enemy.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"No pool found for enemy type: {enemyType}");
            }
        }

        #region Helper Methods

        private void ReturnEnemyToPool(BaseEnemy enemy)
        {
            if (_enemyPools.TryGetValue(enemy.EnemyType, out var pool))
            {
                pool.ReturnToPool(enemy);
            }
            else
            {
                Debug.LogWarning($"No pool found for enemy type: {enemy.EnemyType}");
            }
        }
        
        private void Initialize()
        {
            foreach (var enemy in _enemyPrefabs)
            {
                var pool = new ObjectPool<BaseEnemy>(enemy, _initialSpawnCount, transform);
                _enemyPools.Add(enemy.EnemyType, pool);
            }
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            _isPlaying = gameState == GameState.Playing;
        }
        
        private void SubscribeEvents()
        {
            EventManager.OnEnemyDied += ReturnEnemyToPool;
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void UnsubscribeEvents()
        {
            EventManager.OnEnemyDied -= ReturnEnemyToPool;
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        #endregion
    }
}