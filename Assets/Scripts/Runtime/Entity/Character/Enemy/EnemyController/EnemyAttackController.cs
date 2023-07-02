using Gunfighter.Runtime.Entity.Character.Controller;
using Gunfighter.Runtime.Interface.Damage;
using Gunfighter.Runtime.ScriptableObjects.Data.Character.Enemies;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Enemy.EnemyController
{
    public class EnemyAttackController : CharacterAttackController
    {
        
        private EnemyData _enemyData;

        protected override void Awake()
        {
            base.Awake();
            _enemyData = (EnemyData)characterController.CharacterData;
        }
        
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.CompareTag("Player"))
            {
                collision.transform.GetComponent<IDamageable>().TakeDamage(_enemyData.Damage);
            
            }
        
        }
    }
}
