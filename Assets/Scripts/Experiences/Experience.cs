using DG.Tweening;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Experiences
{
    public class Experience : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform _experienceBar;
        private Camera _camera;
        
        [Header("Experience Settings")]
        private int _minExperience = 1;
        private int _maxExperience = 3;
        
        #region Unity Methods

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayUIAnimation();
        }

        #endregion

        private void PlayUIAnimation()
        {
            if (_camera != null)
            {
                var worldPosition = _camera.ScreenToWorldPoint(_experienceBar.transform.position);
                transform.DOMove(worldPosition, 1f).SetEase(Ease.InCirc).OnComplete(AddExperienceToPlayer);
            }
        }

        private void AddExperienceToPlayer()
        {
            ExperienceManager.Instance.AddExperience(Random.Range(_minExperience, _maxExperience));

            EventManager.OnExperienceCollected(this);
        }

        #region Helper Methods

        public void SetRectTransform(RectTransform rectTransform)
        {
            _experienceBar = rectTransform;
        }

        #endregion
    }
}