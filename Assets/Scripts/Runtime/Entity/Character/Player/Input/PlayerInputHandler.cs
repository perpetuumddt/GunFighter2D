using UnityEngine;
using UnityEngine.InputSystem;

namespace Gunfighter.Runtime.Entity.Character.Player.Input
{
    public class PlayerInputHandler : CharacterInputHandler
    {
        PlayerInputAction _playerInputAction; //"Player Input Action" is a name of Input Action Asset

        public Vector2 MovementInputVector { get; private set; }

        public bool RollInput { get; protected set; }

        public bool AttackInput { get; protected set; }

        public bool ReloadInput { get; protected set; }



        private void OnEnable()
        {
            _playerInputAction = new PlayerInputAction(); //Creates instance of Input Action Asset
            _playerInputAction.Gameplay.Enable(); //Enables Gameplay Action Map

            _playerInputAction.Gameplay.Movement.performed += SetMovementVector;
            _playerInputAction.Gameplay.Movement.canceled += SetMovementVector;

            _playerInputAction.Gameplay.Roll.started += OnRollInput;

            _playerInputAction.Gameplay.Attack.started += OnAttackInput;
            _playerInputAction.Gameplay.Attack.canceled += OnAttackInput;    

            _playerInputAction.Gameplay.Reload.started += OnReloadInput;

            _playerInputAction.Gameplay.SwitchWeapon.started += OnSwitchWeaponInput;

            _playerInputAction.Gameplay.Interact.started += OnInteractInput;
        }

        private void OnDisable()
        {
            _playerInputAction.Gameplay.Movement.performed -= SetMovementVector;
            _playerInputAction.Gameplay.Movement.canceled -= SetMovementVector;

            _playerInputAction.Gameplay.Roll.started -= OnRollInput;

            _playerInputAction.Gameplay.Attack.started -= OnAttackInput;
            _playerInputAction.Gameplay.Attack.canceled-= OnAttackInput;

            _playerInputAction.Gameplay.Reload.started -= OnReloadInput;

            _playerInputAction.Gameplay.SwitchWeapon.started -= OnSwitchWeaponInput;

            _playerInputAction.Gameplay.Interact.started -= OnInteractInput;

            _playerInputAction.Gameplay.Disable(); //Disables Gameplay Action Map 
            //(any time after enabling smth or subscribing to using C# events it`s important to disable them/unsubscribe)
        }

        public void OnRollInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                RollInput = true;
                InvokeOnRoll();
            }
        }

        //public void UseRollInput() => RollInput = false;

        public void OnReloadInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                ReloadInput = true;
                InvokeOnReload();
            }
        }

        //public void UseReloadInput() => ReloadInput = false;

        public void OnAttackInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                AttackInput = true;
                InvokeOnAttack();
            }

            if (context.canceled)
            {
                AttackInput = false;
                UnInvokeOnAttack();
            }
        }

        public void OnSwitchWeaponInput(InputAction.CallbackContext context)
        {
            if(context.started) 
            {
                InvokeOnSwapWeapon();
            }
        }

        public void OnInteractInput(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                InvokeOnInteract();
            }
        }

        public void SetMovementVector(InputAction.CallbackContext context)
        {
            MovementInputVector = context.ReadValue<Vector2>();
        }

        protected virtual void Update()
        {
            if (MovementInputVector.magnitude > 0)
            {
                InvokeOnMove();
            }
            else
            {
                UnInvokeOnMove();
            }
        }
    }
}
