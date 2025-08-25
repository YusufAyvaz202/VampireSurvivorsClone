using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ExperienceUI : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Image _experienceImage;
        private float _maxExperience;

        #region Unity Methods

        private void OnEnable()
        {
            EventManager.OnExperienceChanged += UpdateExperienceBar;
            EventManager.OnMaxExperienceChanged += SetMaxExperience;
        }
        
        private void OnDisable()
        {
            EventManager.OnExperienceChanged -= UpdateExperienceBar;
            EventManager.OnMaxExperienceChanged -= SetMaxExperience;
        }

        #endregion

        #region UI Methods

        private void UpdateExperienceBar(int experience)
        {
            _experienceImage.fillAmount = experience / _maxExperience;
        }

        private void SetMaxExperience(int maxExperience)
        {
            _maxExperience = maxExperience;
        }

        #endregion
       
    }
}