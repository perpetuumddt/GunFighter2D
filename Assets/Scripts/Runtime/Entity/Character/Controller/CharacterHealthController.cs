using System;
using Gunfighter.Runtime.Interface.Damage;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Controller
{
    public class CharacterHealthController : MonoBehaviour, IDamageable
    {
      
        
        protected int _currentHealth;
        protected int _maxHealth;
        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                if(value < 0) _currentHealth = 0;
                else if (value > MaxHealth) _currentHealth = MaxHealth;
                _currentHealth = value;
                UpdateHealth(_currentHealth);
            }
        }
        protected CharacterController characterController;

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        public int MaxHealth => _maxHealth;

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
            OnMaxHealthChange?.Invoke(_maxHealth);
            if (CurrentHealth > _maxHealth) CurrentHealth = _maxHealth;
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
}
