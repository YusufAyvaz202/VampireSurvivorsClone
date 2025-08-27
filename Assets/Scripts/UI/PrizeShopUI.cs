using System.Collections.Generic;
using DG.Tweening;
using Managers;
using Misc;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PrizeShopUI : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private GameObject _blackBackgroundObject;

        [SerializeField] private GameObject _prizeShopPopup;
        [SerializeField] private Button[] _prizeButtons = new Button[3];

        [Header("Settings")] [SerializeField] private float _animationDuration = 0.3f;
        private RectTransform _prizeShopPopupTransform;
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

        private void ShowPrizeInfo(List<PrizeDataSO> prizeDataSo)
        {
            for (int i = 0; i < _prizeButtons.Length; i++)
            {
                var prizeData = prizeDataSo[i];
                _prizeButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = prizeData.PrizeName + " " + prizeData.PrizeDescription;
                _prizeButtons[i].onClick.AddListener(() => CollectPrizes(prizeData));
            }
        }

        private void CollectPrizes(PrizeDataSO prizeDataSo)
        {
            EventManager.OnPrizeCollected?.Invoke(prizeDataSo);
            ResumeGame();
        }

        private void OpenPrizeShop()
        {
            GameManager.Instance.ChangeGameState(GameState.Paused);
            OnGamePause();
        }

        private void OnGamePause()
        {
            _blackBackgroundObject.SetActive(true);
            _prizeShopPopup.SetActive(true);

            _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear).SetUpdate(true);
            _prizeShopPopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
        }

        private void ResumeGame()
        {
            GameManager.Instance.ChangeGameState(GameState.Playing);
            OnGameResume();
        }

        private void OnGameResume()
        {
            _blackBackgroundImage.DOFade(1f, _animationDuration).SetEase(Ease.Linear);
            _prizeShopPopupTransform.DOScale(1.0f, _animationDuration).SetEase(Ease.OutBack);

            _blackBackgroundObject.SetActive(false);
            _prizeShopPopup.SetActive(false);
        }

        #region Helper Methods

        private void InitializeComponents()
        {
            _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
            _prizeShopPopupTransform = _prizeShopPopup.GetComponent<RectTransform>();
        }

        private void SubscribeEvents()
        {
            EventManager.OnCurrentLevelChanged += OpenPrizeShop;
            EventManager.OnPrizeShowed += ShowPrizeInfo;
        }

        private void UnsubscribeEvents()
        {
            EventManager.OnCurrentLevelChanged -= OpenPrizeShop;
            EventManager.OnPrizeShowed -= ShowPrizeInfo;
        }

        #endregion
    }
}