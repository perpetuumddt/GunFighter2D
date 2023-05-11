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
    public event Action<bool> OnAttack;
    public event Action OnSwapWeapon;

    protected void InvokeOnRoll()
    {
        OnRoll?.Invoke();
    }

    protected void InvokeOnMove()
    {
        OnMove?.Invoke(true);
    }
    protected void UnInvokeOnMove()
    {
        OnMove?.Invoke(false);
    }

    protected void InvokeOnAttack()
    {
        OnAttack?.Invoke(true);
    }

    protected void UnInvokeOnAttack()
    {
        OnAttack?.Invoke(false);
    }

    protected void InvokeOnReload()
    {
        OnReload?.Invoke();
    }

    protected void InvokeOnSwapWeapon()
    {
        OnSwapWeapon?.Invoke();
    }
}
