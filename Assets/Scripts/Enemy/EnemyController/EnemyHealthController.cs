using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy.EnemyController
{
    public class EnemyHealthController : CharacterHealthController
    {
        [SerializeField]
        private EnemyData _enemyData;

        

        
        private void Start()
        {
            _currentHealth = _enemyData.Health;
            UpdateHealth(_currentHealth);
        }

        

        public override void TakeDamage(int damage)
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
            this.gameObject.SetActive(false);
        }
    }
}