using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputHandler : MonoBehaviour
{
    public event Action<bool> OnMove;
    public event Action<bool> OnRoll;
    public event Action<bool> OnReload;
    public event Action<bool> OnShoot;

    public Vector2 MovementInput { get; protected set; }

    public bool RollInput { get; protected set; }

    public bool ShootInput { get; protected set; }

    public bool ReloadInput { get; protected set; }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnRollInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RollInput = true;
        }
    }

    public void UseRollInput() => RollInput = false;

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
        Debug.Log(MovementInput.magnitude);
        //MOVEMENT
        if (MovementInput.magnitude > 0)
        {
            Debug.Log("true");
            OnMove?.Invoke(true);
        }
        else
        {
            Debug.Log("false");
            OnMove?.Invoke(false);
        }
        //RELOAD
        if (RollInput)
        {
            OnRoll?.Invoke(true);
        }
        else
        {
            OnRoll?.Invoke(false);
        }
        //ROLL
        if (ReloadInput)
        {
            OnReload?.Invoke(true);
        }
        else
        {
            OnReload?.Invoke(false);
        }
        //SHOOT
        if (ShootInput)
        {
            OnShoot?.Invoke(true);
        }
        else
        {
            OnShoot?.Invoke(false);
        }
    }
}
