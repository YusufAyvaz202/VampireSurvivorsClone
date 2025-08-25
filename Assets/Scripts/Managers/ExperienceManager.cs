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
        
        public void AddExperience(int amount)
        {
            _currentExperience += amount;
            int experienceToNextLevel = Mathf.RoundToInt(_experienceCurve.Evaluate(_currentLevel));
            if (_currentExperience >= experienceToNextLevel)
            {
                LevelUp();
            }
        }
        
        private void LevelUp()
        {
            _currentLevel++;
            _currentExperience = 0;
        }
    }
}