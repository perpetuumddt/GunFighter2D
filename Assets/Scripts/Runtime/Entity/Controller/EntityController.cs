using System;
using Gunfighter.Runtime.ScriptableObjects.Data.Entity;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Controller
{
    public abstract class EntityController : MonoBehaviour
    {
        public EntityAnimationController AnimationController { get; private set; }
        public EntityCollisionController CollisionController { get; private set; }
        public EntityDropController DropController { get; private set; }
        public EntityHealthController HealthController { get; private set; }
        
        [SerializeField] private EntityData entityData;
        public EntityData EntityData => entityData;

        protected virtual void Awake()
        {
            SetControllerReferences();
        }

        private void SetControllerReferences()
        {
            AnimationController = GetComponent<EntityAnimationController>();
            CollisionController = GetComponent<EntityCollisionController>();
            DropController = GetComponent<EntityDropController>();
            HealthController = GetComponent<EntityHealthController>();
        }
    }
}
