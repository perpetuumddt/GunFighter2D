using Gunfighter.Runtime.Entity.Controller;
using Gunfighter.Runtime.Interface.Damage;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Obstacle.Controllers
{
    public class ObstacleCollisionController : EntityCollisionController
    {
        private ObstacleController _obstacleController;
        protected void Awake()
        {
            _obstacleController = GetComponent<ObstacleController>();
        }

        protected override void InteractWithCollider(Collision2D collision)
        {
            if(collision.collider.CompareTag("Player"))
            {
                collision.transform.GetComponent<IDamageable>()
                    .TakeDamage(_obstacleController.EntityData.CollisionDamage);
            
            }
        }
    }
}
