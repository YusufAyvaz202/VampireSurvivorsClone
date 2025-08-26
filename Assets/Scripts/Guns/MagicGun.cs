using Abstract;
using Ammo;
using Interfaces;
using Managers;
using ObjectPooling;
using UnityEngine;

namespace Guns
{
    public class MagicGun : BaseGun
    {
        [Header("Object Pooling")]
        [SerializeField] private MagicBall _magicBallPrefab;
        private ObjectPool<MagicBall> _objectPool;
        private int _initialPoolSize = 10;
        
        [Header("Magic Ball Settings")]
        private Transform _magicBallTargetTransform;
        private int _powerCount = 1;

        #region Unity Methods

        void OnEnable()
        {
            InitializePool();
            SubscribeToEvents();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            SetTargetTransform(other.transform);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.transform == _magicBallTargetTransform)
            {
                SetTargetTransform();
            }
        }

        void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        #endregion
        
        public override void Attack(IAttackable attackable)
        {
            if (_magicBallTargetTransform == null) return;

            for (int i = 0; i < _powerCount; i++)
            {
                MagicBall magicBall = _objectPool.GetObject();
                magicBall.transform.position = transform.position;
                magicBall.SetTarget(_magicBallTargetTransform);
            }
        }

        #region Helper Methods
        
        private void SetTargetTransform(Transform target = null)
        {
            _magicBallTargetTransform = target;
        }

        private void MagicBallReturnedToPool(MagicBall magicBall)
        {
            _objectPool.ReturnToPool(magicBall);
        }
        
        private void InitializePool()
        {
            _objectPool = new ObjectPool<MagicBall>(_magicBallPrefab, _initialPoolSize);
        }
        
        private void SubscribeToEvents()
        {
            EventManager.OnMagicBallAchieve += MagicBallReturnedToPool;
        }
        
        private void UnsubscribeFromEvents()
        {
            EventManager.OnMagicBallAchieve -= MagicBallReturnedToPool;
        }

        #endregion
        
        
    }
}