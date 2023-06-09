using Gunfighter.Runtime.Entity.Character.Controllers;
using Gunfighter.Runtime.Interface.Damage;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Enemy.Controllers
{
    public class EnemyCollisionController : CharacterCollisionController
    {
        private EnemyController _enemyController;
        protected void Awake()
        {
            _enemyController = GetComponent<EnemyController>();
        }

        protected override void InteractWithCollider(Collision2D collision)
        {
            if(collision.collider.CompareTag("Player"))
            {
                collision.transform.GetComponent<IDamageable>()
                    .TakeDamage(_enemyController.CharacterData.CollisionDamage);
            
            }
        }
    }
}
