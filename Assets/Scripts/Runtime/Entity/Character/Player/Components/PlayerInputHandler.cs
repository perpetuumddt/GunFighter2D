using Gunfighter.Runtime.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gunfighter.Runtime.Entity.Character.Player.Components
{
    public class PlayerInputHandler : CharacterInputHandler
    {
        [SerializeField]
        private InputReader inputReader;

        public Vector2 MovementInputVector { get; private set; }

        private void OnEnable()
        {
            inputReader.OnMoveEvent += SetMovementVector;
            inputReader.OnMoveCanceledEvent += SetMovementVector;

            inputReader.OnRollEvent += InvokeOnRoll;

            inputReader.OnAttackEvent += InvokeOnAttack;
            inputReader.OnAttackCanceledEvent += UnInvokeOnAttack;    

            inputReader.OnReloadEvent += InvokeOnReload;

            inputReader.OnSwitchWeaponEvent += InvokeOnSwapWeapon;

            inputReader.OnInteractEvent += InvokeOnInteract;
        }

        private void OnDisable()
        {
            inputReader.OnMoveEvent -= SetMovementVector;
            inputReader.OnMoveCanceledEvent -= SetMovementVector;

            inputReader.OnRollEvent -= InvokeOnRoll;

            inputReader.OnAttackEvent -= InvokeOnAttack;
            inputReader.OnAttackCanceledEvent -= UnInvokeOnAttack;    

            inputReader.OnReloadEvent -= InvokeOnReload;

            inputReader.OnSwitchWeaponEvent -= InvokeOnSwapWeapon;

            inputReader.OnInteractEvent -= InvokeOnInteract;
        }

        

        //public void UseRollInput() => RollInput = false;

        public void OnReloadInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InvokeOnReload();
            }
        }

        //public void UseReloadInput() => ReloadInput = false;

        public void OnAttackInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                InvokeOnAttack();
            }

            if (context.canceled)
            {
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

        public void SetMovementVector(Vector2 inputVector)
        {
            MovementInputVector = inputVector;
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
