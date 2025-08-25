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
        [SerializeField] private GameObject _experiencePrefab;

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
        }

        private void OnEnable()
        {
            EventManager.OnEnemyDied += SpawnExperienceObject;
        }

        private void OnDisable()
        {
            EventManager.OnEnemyDied -= SpawnExperienceObject;
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

        private void SpawnExperienceObject(Vector3 spawnPosition)
        {
            var experience = Instantiate(_experiencePrefab);
            experience.transform.position = spawnPosition;
        }
    }
}