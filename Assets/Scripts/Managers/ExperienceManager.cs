using System.Collections;
using UnityEngine;

namespace Managers
{
    public class ExperienceManager : MonoBehaviour
    {
        [Header("Singleton")]
        public static ExperienceManager Instance;
        
        [Header("Experience Settings")]
        [SerializeField] private AnimationCurve _experienceCurve;
        [SerializeField] private int _currentExperience;
        [SerializeField] private int _currentLevel;

        #region Unity Methods

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        #endregion

        #region Experience Methods

        private void AddExperience(int amount)
        {
            _currentExperience += amount;
            int experienceToNextLevel = Mathf.RoundToInt(_experienceCurve.Evaluate(_currentLevel));
            if (_currentExperience >= experienceToNextLevel)
            {
                LevelUp();
            }
            EventManager.OnExperienceChanged?.Invoke(_currentExperience);
        }
        
        private void LevelUp()
        {
            _currentLevel++;
            _currentExperience = 0;
            EventManager.OnMaxExperienceChanged?.Invoke(Mathf.RoundToInt(_experienceCurve.Evaluate(_currentLevel)));
        }

        #endregion
        
        // This is just for testing purposes
        private IEnumerator IncreaseExperienceOverTime()
        {
            while (true)
            {
                AddExperience(1);
                yield return new WaitForSeconds(0.1f);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}