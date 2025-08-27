using Abstract;
using Managers;
using Misc;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PrizeInteractionController : MonoBehaviour
    {
        [Header("References")]
        private PlayerAttackController _playerAttackController;
        private PlayerHealthController _playerHealthController;
        private PlayerMovementController _playerMovementController;

        #region Unity Methods

        private void Awake()
        {
            InitializeComponents();
        }

        private void OnEnable()
        {
            EventManager.OnPrizeCollected += CheckPrizeInteraction;
        }
        
        private void OnDisable()
        {
            EventManager.OnPrizeCollected -= CheckPrizeInteraction;
        }

        #endregion
        
        private void CheckPrizeInteraction(PrizeDataSO prizeDataSo)
        {
            switch (prizeDataSo.PrizeType)
            {
                case PrizeType.Gun:
                    BaseGun gun = prizeDataSo.PrizeObject.GetComponent<BaseGun>();
                    CollectGun(gun);
                    break;
                case PrizeType.Health:
                    // Increase player health
                    break;
            }
        }
        
        private void CollectGun(BaseGun gun)
        {
            _playerAttackController.CollectGun(gun);
        }
        
        #region Helper Methods

        private void InitializeComponents()
        {
            _playerAttackController = GetComponent<PlayerAttackController>();
            _playerHealthController = GetComponent<PlayerHealthController>();
            _playerMovementController = GetComponent<PlayerMovementController>();
        }

        #endregion
    }
}