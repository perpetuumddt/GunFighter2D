using Gunfighter.Entity.Character.Controller;
using Gunfighter.Interface.Damage;
using Gunfighter.ScriptableObjects.Data.Character.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Character.Enemy.EnemyController
{
    public class EnemyAttackController : CharacterAttackController
    {
        [FormerlySerializedAs("_enemyData")] [SerializeField]
        private EnemyData enemyData;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.CompareTag("Player"))
            {
                collision.transform.GetComponent<IDamageable>().TakeDamage(enemyData.Damage);
            
            }
        
        }
    }
}
