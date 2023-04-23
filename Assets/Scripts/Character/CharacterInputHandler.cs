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

    public Vector2 MovementInput { get; private set; }

    public bool RollInput { get; private set; }

    public bool ShootInput { get; private set; }

    public bool ReloadInput { get; private set; }

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

    private void Update()
    {
        //MOVEMENT
        if (MovementInput.magnitude >= 0)
        {
            OnMove?.Invoke(true);
        }
        else
        {
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
