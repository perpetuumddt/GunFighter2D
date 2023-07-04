using System;
using Gunfighter.Runtime.Entity.Character.StateMachine;
using Gunfighter.Runtime.ScriptableObjects.Data.Entity;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Controller
{
    public abstract class EntityController : MonoBehaviour
    {
        public EntityAnimationController EntityAnimationController { get; private set; }
        public EntityCollisionController EntityCollisionController { get; private set; }
        public EntityDropController EntityDropController { get; private set; }
        public EntityHealthController EntityHealthController { get; private set; }
        
        [SerializeField] private EntityData entityData;
        public EntityData EntityData => entityData;


        protected virtual void Awake()
        {
            EntityAnimationController = GetComponent<EntityAnimationController>();
            EntityCollisionController = GetComponent<EntityCollisionController>();
            EntityDropController = GetComponent<EntityDropController>();
            EntityHealthController = GetComponent<EntityHealthController>();
        }
    }
}
