using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy.EnemyController
{
    public class EnemyHealthController : CharacterHealthController, IDamageable
    {
        [SerializeField]
        private EnemyStats _enemyStats;

        private float _currentHealth;

        private void Start()
        {
            _currentHealth = _enemyStats.GetMaxHealth();
            UpdateHealth(_currentHealth);
        }

        public override void UpdateHealth(float _currentHealth)
        {
            if(_currentHealth <= 0) 
            {
                InvokeOnHealthZero();
            }
            base.UpdateHealth(_currentHealth);
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            UpdateHealth(_currentHealth);
        }
    }
}