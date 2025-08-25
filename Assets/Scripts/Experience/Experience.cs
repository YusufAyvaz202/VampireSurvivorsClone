using DG.Tweening;
using Managers;
using UnityEngine;


namespace Experience
{
    public class Experience : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform _experienceBar;
        
        [Header("Experience Settings")]
        private int _minExperience = 1;
        private int _maxExperience = 3;
        
        #region Unity Methods

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayUIAnimation();
        }

        #endregion

        private void PlayUIAnimation()
        {
            if (Camera.main != null)
            {
                var worldPosition = Camera.main.ScreenToWorldPoint(_experienceBar.transform.position);
                transform.DOMove(worldPosition, 0.5f).OnComplete(AddExperienceToPlayer);
            }
        }

        private void AddExperienceToPlayer()
        {
            ExperienceManager.Instance.AddExperience(Random.Range(_minExperience, _maxExperience));
            
            //TODO: Return pool with object pooling.
            Destroy(gameObject);
        }
    }
}