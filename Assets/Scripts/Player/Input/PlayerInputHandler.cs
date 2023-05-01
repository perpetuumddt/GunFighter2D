using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : CharacterInputHandler
{


    PlayerInputAction playerInputAction; //"Player Input Action" is a name of Input Action Asset

    public Vector2 movementInputVector { get; private set; }

    public bool RollInput { get; protected set; }

    public bool ShootInput { get; protected set; }

    public bool ReloadInput { get; protected set; }



    private void OnEnable()
    {
        playerInputAction = new PlayerInputAction(); //Creates instance of Input Action Asset
        playerInputAction.Gameplay.Enable(); //Enables Gameplay Action Map

        playerInputAction.Gameplay.Movement.performed += SetMovementVector;
        playerInputAction.Gameplay.Movement.canceled += SetMovementVector;

        playerInputAction.Gameplay.Roll.started += OnRollInput;
    }

    private void OnDisable()
    {
        playerInputAction.Gameplay.Movement.performed -= SetMovementVector;
        playerInputAction.Gameplay.Movement.canceled -= SetMovementVector;

        playerInputAction.Gameplay.Roll.started -= OnRollInput;

        playerInputAction.Gameplay.Disable(); //Disables Gameplay Action Map 
        //(any time after enabling smth or subscribing to using C# events it`s important to disable them/unsubscribe)
    }

    public void SetMovementVector(InputAction.CallbackContext context)
    {
        movementInputVector = context.ReadValue<Vector2>();
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
        }
    }

    public void UseReloadInput() => ReloadInput = false;

    public void OnShootInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ShootInput = true;
        }

        if (context.canceled)
        {
            ShootInput = false;
        }
    }

    protected virtual void Update()
    {
        //MOVEMENT
        //Debug.Log(RollInput);
        if (movementInputVector.magnitude > 0)
        {
            InvokeOnMove();
        }
        else
        {
            UnInvokeOnMove();
        }
    }
    //Подвязать инпуты сюда.
}
