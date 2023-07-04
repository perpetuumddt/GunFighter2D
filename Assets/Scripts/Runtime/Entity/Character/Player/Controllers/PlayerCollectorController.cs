using System;
using Gunfighter.Runtime.Entity.Character.Controller;
using Gunfighter.Runtime.Interface.Collect;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Player.Controllers
{
    public class PlayerCollectorController : CharacterCollectorController, ICollector
    {
        public static event Action<Vector2> OnPlayerPositionUpdate;

        void Update()
        {
            Vector2 playerPosition = transform.position;

            OnPlayerPositionUpdate?.Invoke(playerPosition);
        }

        public override void Collect(ICollectable collectable)
        {
            base.Collect(collectable);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ICollectable collectable = collision.gameObject.GetComponent<ICollectable>();
            if (collectable == null)
            {
                return;
            }
            Collect(collectable);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {

        }
    }
}
