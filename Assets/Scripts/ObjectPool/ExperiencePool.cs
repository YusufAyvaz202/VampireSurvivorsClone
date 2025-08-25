using System.Collections.Generic;
using Experiences;
using UnityEngine;

namespace ObjectPool
{
    public class ExperiencePool
    {
        [Header("Object Pool Settings")] private Queue<Experience> _experiencesQueue = new();
        private Experience _experiencePrefabPrefab;
        private Transform _parent;

        public ExperiencePool(Experience experiencePrefab, int initialSize, Transform parent = null)
        {
            _experiencePrefabPrefab = experiencePrefab;
            _parent = parent;
            for (int i = 0; i < initialSize; i++)
            {
                Experience experience = Object.Instantiate(_experiencePrefabPrefab, parent);
                experience.gameObject.SetActive(false);
                _experiencesQueue.Enqueue(experience);
            }
        }

        public Experience GetExperience()
        {
            Experience experience;
            if (_experiencesQueue.Count > 0)
            {
                experience = _experiencesQueue.Dequeue();
                experience.gameObject.SetActive(true);
                return experience;
            }

            experience = Object.Instantiate(_experiencePrefabPrefab, _parent);
            experience.gameObject.SetActive(false);
            return experience;
        }

        public void ReturnExperienceToPool(Experience experience)
        {
            experience.gameObject.SetActive(false);
            _experiencesQueue.Enqueue(experience);
        }
    }
}