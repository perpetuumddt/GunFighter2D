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
    public event Action OnAttack;
    public event Action OnChangeWeapon;

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
}
