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
            base.UpdateHealth(_currentHealth);
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                InvokeOnHealthZero();
            }
            UpdateHealth(_currentHealth);
        }

        public override void DestroyOnDeath()
        {
            base.DestroyOnDeath();
            Destroy(gameObject, 1f);
        }
    }
}