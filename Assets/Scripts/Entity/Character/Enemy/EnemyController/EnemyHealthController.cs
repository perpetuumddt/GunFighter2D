using System;
using Entity.Character.Controller;
using General;
using ScriptableObjects.Data.Character.Enemies;
using UnityEngine;

namespace Entity.Character.Enemy.EnemyController
{
    public class EnemyHealthController : CharacterHealthController
    {
        [SerializeField]
        private EnemyData _enemyData;

        private EnemyManager _enemyManager;

        private void Awake()
        {
            ChangeMaxHealth(_enemyData.DefaultMaxHealth);
            CurrentHealth = _enemyData.DefaultMaxHealth;
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