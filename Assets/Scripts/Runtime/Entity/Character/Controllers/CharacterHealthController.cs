using System;
using Gunfighter.Runtime.Entity.Controllers;

namespace Gunfighter.Runtime.Entity.Character.Controllers
{
    public abstract class CharacterHealthController : EntityHealthController
    {
        public event Action<int> OnMaxHealthChange;
        protected override void Awake()
        {
            base.Awake();
        }
        
        
        
        
        
        public override void DestroyOnDeath()
        {
            base.DestroyOnDeath();
        }
        
        public override void ReplenishHealth(int health)
        {   
            base.ReplenishHealth(health);
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        public virtual void ChangeMaxHealth(int newMaxHealth)
        {
            if (newMaxHealth < 0) throw new ArgumentOutOfRangeException();
            MaxHealth = newMaxHealth;
            OnMaxHealthChange?.Invoke(MaxHealth);
            if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        }

        
    }
}
