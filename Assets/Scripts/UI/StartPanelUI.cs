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
        }

        private void OnDisable()
        {
            EventManager.OnTotalGoldChanged -= OnTotalGoldChanged;
        }

        #endregion

        private void AddArmorButtonClicked()
        {
            //TODO: ADD ARMOR AND SAVE TO JSON WİTH ANOTHER MANAGER
        }

        private void StartButtonClicked()
        {
            _childObject.SetActive(false);
            GameManager.Instance.ChangeGameState(GameState.Playing);
        }
        
        private void OnTotalGoldChanged(int totalGold)
        {
            _totalGoldCountText.text = totalGold.ToString();
        }
    }
}