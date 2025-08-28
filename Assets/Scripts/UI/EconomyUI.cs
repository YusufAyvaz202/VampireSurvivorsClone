using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EconomyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentGoldCountText;

        #region Unity Methods

        private void OnEnable()
        {
            EventManager.OnCurrentGoldChanged += UpdateCurrentGoldCountText;
        }

        private void OnDisable()
        {
            EventManager.OnCurrentGoldChanged -= UpdateCurrentGoldCountText;
        }

        #endregion
        
        private void UpdateCurrentGoldCountText(int currentCGold)
        {
            _currentGoldCountText.text = currentCGold.ToString();   
        }
    }
}