using Managers;
using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartPanelUI : MonoBehaviour
    {
        [Header("Economy Info")]
        [SerializeField] private TextMeshProUGUI _totalGoldCountText;
        
        [Header("Power Up Buttons")]
        [SerializeField] private Button _addArmorButton;
        
        [Header("Game Settings")]
        [SerializeField] private Button _startButton;
        [SerializeField] private GameObject _childObject;

        #region Unity Methods

        private void Awake()
        {
            _addArmorButton.onClick.AddListener(AddArmorButtonClicked);
            _startButton.onClick.AddListener(StartButtonClicked);
        }

        private void OnEnable()
        {
            EventManager.OnTotalGoldChanged += OnTotalGoldChanged;
            EventManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            EventManager.OnTotalGoldChanged -= OnTotalGoldChanged;
            EventManager.OnGameStateChanged -= OnGameStateChanged;
        }

        #endregion

        private void AddArmorButtonClicked()
        {
            EventManager.OnAddArmorButtonClicked?.Invoke();
        }

        private void StartButtonClicked()
        {
            _childObject.SetActive(false);
            GameManager.Instance.ChangeGameState(GameState.Playing);
        }

        private void GameOver()
        {
            _childObject.SetActive(true);
        }
        
        private void OnTotalGoldChanged(int totalGold)
        {
            _totalGoldCountText.text = totalGold.ToString();
        }
        
        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.GameOver) GameOver();
        }
    }
}