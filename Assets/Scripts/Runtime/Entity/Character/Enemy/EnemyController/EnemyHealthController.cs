using System;
using Gunfighter.Runtime.Entity.Character.Controller;
using Gunfighter.Runtime.General;
using Gunfighter.Runtime.ScriptableObjects.Data.Character.Enemies;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Enemy.EnemyController
{
    public class EnemyHealthController : CharacterHealthController
    {
        
        private EnemyData _enemyData;

        private EnemyManager _enemyManager;

        

        private void Start()
        {
            _enemyData = (EnemyData)characterController.CharacterData;
            ChangeMaxHealth(_enemyData.DefaultMaxHealth);
            CurrentHealth = _enemyData.DefaultMaxHealth;
            _enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
            
        }

        public override void TakeDamage(int damage)
        {
            if (damage < 0) throw new ArgumentOutOfRangeException();
            if(CurrentHealth >0)
            {
                CurrentHealth -= damage;
                if (_currentHealth <= 0)
                {
                    InvokeOnHealthZero();
                    _enemyManager.InvokeOnEnemyDied(this.gameObject);
                }
            }
        }

        public override void DestroyOnDeath()
        {
            base.DestroyOnDeath();
            this.gameObject.SetActive(false);
        }
    }
}