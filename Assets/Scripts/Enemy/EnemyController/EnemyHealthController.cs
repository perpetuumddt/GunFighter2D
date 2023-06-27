using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy.EnemyController
{
    public class EnemyHealthController : CharacterHealthController
    {
        [SerializeField]
        private EnemyData _enemyData;

        private EnemyManager _enemyManager;

        
        private void Start()
        {
            _enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
            _currentHealth = _enemyData.Health;
            UpdateHealth(_currentHealth);
        }

        public override void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                InvokeOnHealthZero();
                _enemyManager.InvokeOnEnemyDied(this.gameObject);
                //_enemyManager.EnemyDied(this.gameObject);
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