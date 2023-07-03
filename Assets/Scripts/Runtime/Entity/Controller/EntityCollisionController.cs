using System;
using Gunfighter.Runtime.Interface.Damage;
using Gunfighter.Runtime.ScriptableObjects.Data.Entity;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Controller
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
