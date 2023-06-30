using System;
using Gunfighter.Entity.Character.Controller;
using Gunfighter.General;
using Gunfighter.ScriptableObjects.Data.Character.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Character.Enemy.EnemyController
{
    public class EnemyHealthController : CharacterHealthController
    {
        [FormerlySerializedAs("_enemyData")] [SerializeField]
        private EnemyData enemyData;

        private EnemyManager _enemyManager;

        private void Awake()
        {
            ChangeMaxHealth(enemyData.DefaultMaxHealth);
            CurrentHealth = enemyData.DefaultMaxHealth;
        }

        private void Start()
        {
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