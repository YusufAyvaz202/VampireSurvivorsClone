using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPool<T> where T : Component
    {
        [Header("Object Pool Settings")] private Queue<T> _queue = new();
        private Transform _parent;
        private T _prefab;

        public ObjectPool(T prefab, int initialSize, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            for (int i = 0; i < initialSize; i++)
            {
                T item = Object.Instantiate(_prefab, parent);
                item.gameObject.SetActive(false);
                _queue.Enqueue(item);
            }
        }

        public T GetObject()
        {
            T item;
            if (_queue.Count > 0)
            {
                item = _queue.Dequeue();
                item.gameObject.SetActive(true);
                return item;
            }

            item = Object.Instantiate(_prefab, _parent);
            item.gameObject.SetActive(true);
            return item;
        }

        public void ReturnToPool(T item)
        {
            item.gameObject.SetActive(false);
            _queue.Enqueue(item);
        }
    }
}