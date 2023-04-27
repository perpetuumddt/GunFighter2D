using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputHandler : MonoBehaviour
{
    public event Action<bool> OnMove;
    public event Action OnRoll;
    public event Action OnReload;
    public event Action OnShoot;


    PlayerInputAction playerInputAction; //"Player Input Action" is a name of Input Action Asset
    
    public Vector2 movementInputVector { get; private set; }
    public bool onMove;

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
            OnRoll?.Invoke();
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
            OnMove?.Invoke(true);
        }
        else
        {
            OnMove?.Invoke(false);
        }
    }
}
