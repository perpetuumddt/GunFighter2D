using System;
using Gunfighter.Runtime.Entity.Controllers;

namespace Gunfighter.Runtime.Entity.Obstacle.Controllers
{
    public class ObstacleHealthController : EntityHealthController
    {
        public override void TakeDamage(int damage)
        {
            if (damage < 0) throw new ArgumentOutOfRangeException();
            if(CurrentHealth >0)
            {
                CurrentHealth -= damage;
                if (CurrentHealth <= 0)
                {
                    InvokeOnHealthZero();
                }
            }
        }

        public override void DestroyOnDeath()
        {
            base.DestroyOnDeath();
            this.gameObject.SetActive(false);
            
        }

        protected override void Awake()
        {
            base.Awake();
            MaxHealth = entityController.EntityData.DefaultMaxHealth;
            CurrentHealth = MaxHealth;
        }
    }
}
