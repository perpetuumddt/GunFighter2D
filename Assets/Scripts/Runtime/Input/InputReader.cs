using Gunfighter.Runtime.ScriptableObjects.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Gunfighter.Runtime.Input
{
    /// <summary>
    /// Centralised location where all input is read.
    /// Any object in need of reading input can reference
    /// this SO and just hook into its event delegates.
    /// </summary>
    [CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
    public class InputReader : DescriptionBaseSO, PlayerInputAction.IGameplayActions, PlayerInputAction.IMenusActions
    {
        // Gameplay input
        public event UnityAction OnRollEvent = delegate { };
        public event UnityAction OnReloadEvent = delegate { };
        public event UnityAction OnSwitchWeaponEvent = delegate { };
        public event UnityAction OnAttackEvent = delegate { };
        public event UnityAction OnAttackCanceledEvent = delegate { };
        public event UnityAction OnInteractEvent = delegate { }; // Used to talk, pickup objects
        public event UnityAction<Vector2> OnMoveEvent = delegate { };
        public event UnityAction<Vector2> OnMoveCanceledEvent = delegate { };
        public event UnityAction OnMenuPauseEvent = delegate { };
        
        // Menu input
        public event UnityAction OnMenuUnPauseEvent = delegate { };
        
        
        private PlayerInputAction _gameInput;

        #region Unity Events

        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new PlayerInputAction();

                _gameInput.Menus.SetCallbacks(this);
                _gameInput.Gameplay.SetCallbacks(this);
            }
            EnableGameplayInput();
        }

        private void OnDisable()
        {
            DisableAllInput();
        }

        #endregion

        #region InputMaps

        public void EnableGameplayInput()
        {
            _gameInput.Menus.Disable();
            _gameInput.Gameplay.Enable();
        }

        public void EnableMenuInput()
        {
            _gameInput.Gameplay.Disable();

            _gameInput.Menus.Enable();
        }

        public void DisableAllInput()
        {
            _gameInput.Gameplay.Disable();
            _gameInput.Menus.Disable();
        }

        #endregion
        
        
        
        public void OnMovement(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    OnMoveEvent.Invoke(context.ReadValue<Vector2>());
                    break;
                case InputActionPhase.Canceled:
                    OnMoveCanceledEvent.Invoke(context.ReadValue<Vector2>());
                    break;
            }
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            if(context.started)
                OnRollEvent.Invoke();
        }

        public void OnReload(InputAction.CallbackContext context)
        {
            if(context.started)
                OnReloadEvent.Invoke();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    OnAttackEvent.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    OnAttackCanceledEvent.Invoke();
                    break;
            }
        }

        public void OnSwitchWeapon(InputAction.CallbackContext context)
        {
            if(context.started)
                OnSwitchWeaponEvent.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if(context.started)
                OnInteractEvent.Invoke();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                OnMenuPauseEvent.Invoke();
        }

        public void OnMoveSelection(InputAction.CallbackContext context)
        {
            
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
            
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            
        }

        public void OnConfirm(InputAction.CallbackContext context)
        {
            
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            
        }

        public void OnMouseMove(InputAction.CallbackContext context)
        {
            
        }

        public void OnUnpause(InputAction.CallbackContext context)
        {
            OnMenuUnPauseEvent.Invoke();
        }

        public void OnChangeTab(InputAction.CallbackContext context)
        {
            
        }

        public void OnInventoryActionButton(InputAction.CallbackContext context)
        {
            
        }

        public void OnSaveActionButton(InputAction.CallbackContext context)
        {
            
        }

        public void OnResetActionButton(InputAction.CallbackContext context)
        {
            
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
            
        }

        public void OnCloseInventory(InputAction.CallbackContext context)
        {
            
        }
    }
}
