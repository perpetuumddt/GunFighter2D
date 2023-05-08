using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : CharacterInputHandler
{
    PlayerInputAction playerInputAction; //"Player Input Action" is a name of Input Action Asset

    public Vector2 movementInputVector { get; private set; }

    public bool RollInput { get; protected set; }

    public bool AttackInput { get; protected set; }

    public bool ReloadInput { get; protected set; }



    private void OnEnable()
    {
        playerInputAction = new PlayerInputAction(); //Creates instance of Input Action Asset
        playerInputAction.Gameplay.Enable(); //Enables Gameplay Action Map

        playerInputAction.Gameplay.Movement.performed += SetMovementVector;
        playerInputAction.Gameplay.Movement.canceled += SetMovementVector;

        playerInputAction.Gameplay.Roll.started += OnRollInput;

        playerInputAction.Gameplay.Attack.performed += OnAttackInput;
        playerInputAction.Gameplay.Attack.canceled += OnAttackInput;

        playerInputAction.Gameplay.Reload.started += OnReloadInput;

        playerInputAction.Gameplay.SwitchWeapon.started += OnSwitchWeaponInput;
    }

    private void OnDisable()
    {
        playerInputAction.Gameplay.Movement.performed -= SetMovementVector;
        playerInputAction.Gameplay.Movement.canceled -= SetMovementVector;

        playerInputAction.Gameplay.Roll.started -= OnRollInput;

        playerInputAction.Gameplay.Attack.performed-= OnAttackInput;
        playerInputAction.Gameplay.Attack.canceled-= OnAttackInput;

        playerInputAction.Gameplay.Reload.started -= OnReloadInput;

        playerInputAction.Gameplay.SwitchWeapon.started -= OnSwitchWeaponInput;

        playerInputAction.Gameplay.Disable(); //Disables Gameplay Action Map 
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
            InvokeOnSwitchWeapon();
        }
    }

    public void SetMovementVector(InputAction.CallbackContext context)
    {
        movementInputVector = context.ReadValue<Vector2>();
    }

    protected virtual void Update()
    {
        if (movementInputVector.magnitude > 0)
        {
            InvokeOnMove();
        }
        else
        {
            UnInvokeOnMove();
        }
    }
}
