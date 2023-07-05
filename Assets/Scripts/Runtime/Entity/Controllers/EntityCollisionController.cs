using System;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Controllers
{
    public abstract class EntityCollisionController : MonoBehaviour
    {
        public Action<Collision2D> OnCollisionEnterEvent;

        protected abstract void InteractWithCollider(Collision2D collision);
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            InteractWithCollider(collision);
            OnCollisionEnterEvent?.Invoke(collision);
        }
    }
}
