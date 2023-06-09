using System;
using Gunfighter.Runtime.General;
using Gunfighter.Runtime.Interface.Damage;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Controllers
{
    public abstract class EntityHealthController : MonoBehaviour, IDamageable
    {
        [SerializeField, ReadOnly]
        private int currentHealth;
        [SerializeField, ReadOnly]
        private int maxHealth;
        public int CurrentHealth
        {
            get => currentHealth;
            set
            {
                if(value < 0) currentHealth = 0;
                else if (value > MaxHealth) currentHealth = MaxHealth;
                currentHealth = value;
                OnUpdateHealth?.Invoke(currentHealth);
            }
        }
        protected EntityController entityController;

        public int MaxHealth
        {
            get => maxHealth;
            protected set => maxHealth = value;
        }

        public event Action OnHealthZero;
        public event Action<int> OnUpdateHealth;
            
        protected virtual void Awake()
        {
            entityController = GetComponent<EntityController>();
        }
        
        public virtual void DestroyOnDeath()
        {

        }
    
        public virtual void ReplenishHealth(int health)
        {
        
        }

        protected void InvokeOnHealthZero()
        {
            OnHealthZero?.Invoke();
        }


        public virtual void TakeDamage(int damage)
        {
        }
    }
}
