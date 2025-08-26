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

        #region Unity Methods

        void OnEnable()
        {
            _objectPool = new ObjectPool<MagicBall>(_magicBallPrefab, _initialPoolSize);
            EventManager.OnMagicBallAchieve += MagicBallReturnedToPool;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _magicBallTargetTransform = other.transform;
        }

        void OnDisable()
        {
            EventManager.OnMagicBallAchieve -= MagicBallReturnedToPool;
        }

        #endregion
        
        public override void Attack(IAttackable attackable)
        {
            if (_magicBallTargetTransform == null) return;
            MagicBall magicBall = _objectPool.GetObject();
            magicBall.transform.position = transform.position;
            magicBall.SetTarget(_magicBallTargetTransform);
        }
        
        private void MagicBallReturnedToPool(MagicBall magicBall)
        {
            _objectPool.ReturnToPool(magicBall);
        }
    }
}