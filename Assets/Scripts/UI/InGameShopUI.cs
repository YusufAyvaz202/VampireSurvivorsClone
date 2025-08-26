using DG.Tweening;
using Managers;
using Misc;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGameShopUI : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject _blackBackgroundObject;
        [SerializeField] private GameObject _InGameShopPopup;

        [Header("Settings")]
        [SerializeField] private float _animationDuration = 0.3f;
        private RectTransform _InGameShopPopupTransform;
        private Image _blackBackgroundImage;

        #region Unity Methods

        private void Awake()
        {
            InitializeComponents();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void ShowInGameShop()
        {
            GameManager.Instance.ChangeGameState(GameState.Paused);
            OnGamePause();
        }

        private void OnGamePause()
        {
            _blackBackgroundObject.SetActive(true);
            _InGameShopPopup.SetActive(true);

            _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
            _InGameShopPopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
        }

        private void ResumeGame()
        {
            GameManager.Instance.ChangeGameState(GameState.Playing);
            OnGameResume();
        }

        private void OnGameResume()
        {
            _blackBackgroundImage.DOFade(1f, _animationDuration).SetEase(Ease.Linear);
            _InGameShopPopupTransform.DOScale(1.0f, _animationDuration).SetEase(Ease.OutBack);

            _blackBackgroundObject.SetActive(false);
            _InGameShopPopup.SetActive(false);
        }

        #region Helper Methods

        private void InitializeComponents()
        {
            _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
            _InGameShopPopupTransform = _InGameShopPopup.GetComponent<RectTransform>();
        }

        private void SubscribeEvents()
        {
            EventManager.OnCurrentLevelChanged += ShowInGameShop;
        }

        private void UnsubscribeEvents()
        {
            EventManager.OnCurrentLevelChanged -= ShowInGameShop;
        }

        #endregion
    }
}