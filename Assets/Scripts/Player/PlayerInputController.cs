using Misc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private InputActionAsset _inputActionAsset;

        [Header("Input Actions")] 
        private InputAction _moveAction;
        
        [Header("Movement")]
        private Vector2 _moveInput;
        public Vector2 MoveInput => _moveInput.normalized;

        #region Unity Methods

        private void Awake()
        {
            _moveAction = _inputActionAsset.FindAction(Const.InputString.MOVE);
            _moveAction.Enable();
        }

        private void OnEnable()
        {
            _moveAction.started += OnMovePerformed;
            _moveAction.performed += OnMovePerformed;
            _moveAction.canceled += OnMovePerformed;
        }

        private void OnDisable()
        {
            _moveAction.started -= OnMovePerformed;
            _moveAction.performed -= OnMovePerformed;
            _moveAction.canceled -= OnMovePerformed;
        }

        #endregion

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
    }
}
