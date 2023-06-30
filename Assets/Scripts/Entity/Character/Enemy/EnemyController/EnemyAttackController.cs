using Entity.Character.Controller;
using Interface.Damage;
using ScriptableObjects.Data.Character.Enemies;
using UnityEngine;

namespace Entity.Character.Enemy.EnemyController
{
    public class EnemyAttackController : CharacterAttackController
    {
        [SerializeField]
        private EnemyData _enemyData;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.CompareTag("Player"))
            {
                collision.transform.GetComponent<IDamageable>().TakeDamage(_enemyData.Damage);
            
            }
        
        }
    }
}
