using System;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Controller
{
    public abstract class EntityHealthController : MonoBehaviour
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
                InvokeUpdateHealth(_currentHealth);
            }
        }
        protected EntityController entityController;
        public int MaxHealth => _maxHealth;

        public event Action<bool> OnHealthZero;
        public event Action<int> OnUpdateHealth;
            
        protected virtual void Awake()
        {
            entityController = GetComponent<EntityController>();
        }
        
        
        public virtual void InvokeUpdateHealth(int currentHealth)
        {
            OnUpdateHealth?.Invoke(currentHealth);
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
