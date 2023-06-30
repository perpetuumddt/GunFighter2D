using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthController : MonoBehaviour, IDamageable, ICharacterHealthController
{
    protected int _maxHealth;
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

    protected int MaxHealth => _maxHealth;

    public event Action<bool> OnHealthZero;
    public event Action<int> OnUpdateHealth;
    public event Action<int> OnMaxHealthChange;
    public virtual void UpdateHealth(int currentHealth)
    {
        OnUpdateHealth?.Invoke(currentHealth);
    }

    public virtual void ChangeMaxHealth(int newMaxHealth)
    {
        if (newMaxHealth < 0) throw new ArgumentOutOfRangeException();
        _maxHealth = newMaxHealth;
        if (CurrentHealth > _maxHealth) CurrentHealth = _maxHealth;
        OnMaxHealthChange?.Invoke(_maxHealth);
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
