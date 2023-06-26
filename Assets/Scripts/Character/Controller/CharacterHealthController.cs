using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthController : MonoBehaviour
{
    public event Action<bool> OnHealthZero;
    public event Action<int> OnUpdateHealth;
    public virtual void UpdateHealth(int _currentHealth)
    {
        OnUpdateHealth?.Invoke(_currentHealth);
    }
    

    public virtual void DestroyOnDeath()
    {

    }

    protected void InvokeOnHealthZero()
    {
        OnHealthZero?.Invoke(true);
    }
}
