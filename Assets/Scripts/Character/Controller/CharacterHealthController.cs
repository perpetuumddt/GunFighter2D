using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthController : MonoBehaviour, IDamageable, ICharacterHealthController
{
    
    protected int _currentHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if(value < 0)throw new ArgumentOutOfRangeException();
            _currentHealth = value;
            OnUpdateHealth?.Invoke(_currentHealth);
        }
    }
    public event Action<bool> OnHealthZero;
    public event Action<int> OnUpdateHealth;
    public virtual void UpdateHealth(int _currentHealth)
    {
        OnUpdateHealth?.Invoke(_currentHealth);
    }
    

    public virtual void DestroyOnDeath()
    {

    }
    
    public virtual void ReplenishHealth(int health)
    {
        
    }

    protected void InvokeOnHealthZero()
    {
        OnHealthZero?.Invoke(true);
    }


    public virtual void TakeDamage(int damage)
    {
    }
}
