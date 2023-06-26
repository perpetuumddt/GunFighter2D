using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthController : MonoBehaviour
{
    public event Action<bool> OnHealthZero;
    public virtual void UpdateHealth(int _currentHealth)
    {

    }
    

    public virtual void DestroyOnDeath()
    {

    }

    protected void InvokeOnHealthZero()
    {
        OnHealthZero?.Invoke(true);
    }
}
