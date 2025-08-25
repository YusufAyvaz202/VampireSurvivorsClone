using Experiences;
using ObjectPool;
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
        [SerializeField] private int _experienceToNextLevel;
        [SerializeField] private int _currentLevel;

        [Header("Spawn Settings")] 
        [SerializeField] private RectTransform _experienceTargetTransform;
        [SerializeField] private Experience _experiencePrefab;
        private ExperiencePool _experiencePool;

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

        private void Start()
        {
            EventManager.OnMaxExperienceChanged?.Invoke(Mathf.RoundToInt(_experienceCurve.Evaluate(_currentLevel + 1)));
            _experiencePool = new ExperiencePool(_experiencePrefab, 10);
        }

        private void OnEnable()
        {
            EventManager.OnEnemyDied += SpawnExperienceObject;
            EventManager.OnExperienceCollected += OnExperienceCollected;
        }

        private void OnDisable()
        {
            EventManager.OnEnemyDied -= SpawnExperienceObject;
            EventManager.OnExperienceCollected -= OnExperienceCollected;
        }

        #endregion

        #region Experience Methods

        public void AddExperience(int amount)
        {
            _currentExperience += amount;
            _experienceToNextLevel = Mathf.RoundToInt(_experienceCurve.Evaluate(_currentLevel + 1));
            if (_currentExperience >= _experienceToNextLevel)
            {
                LevelUp();
            }
            EventManager.OnExperienceChanged?.Invoke(_currentExperience);
        }
        
        private void LevelUp()
        {
            _currentLevel++;
            _currentExperience = 0;
            EventManager.OnMaxExperienceChanged?.Invoke(Mathf.RoundToInt(_experienceCurve.Evaluate(_currentLevel + 1)));
        }

        #endregion

        #region Helper Methods
        
        private void SpawnExperienceObject(Vector3 spawnPosition)
        {
            Experience experience = _experiencePool.GetExperience();
            experience.SetRectTransform(_experienceTargetTransform);
            experience.transform.position = spawnPosition;
        }

        private void OnExperienceCollected(Experience experience)
        {
            _experiencePool.ReturnExperienceToPool(experience);
        }
        

        #endregion
        
    }
}