using Gunfighter.Runtime.Entity.Controller;
using Gunfighter.Runtime.Entity.StateMachine;
using Gunfighter.Runtime.ScriptableObjects.Data.Entity.Character;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Controller
{
    public class CharacterController : EntityController
    {
        public new CharacterAnimationController AnimationController { get; private set; }
        public new CharacterAttackController AttackController { get; private set; }
        public new CharacterCollectorController CollectorController { get; private set; }
        public new CharacterCollisionController CollisionController { get; private set; }
        public new CharacterDropController DropController { get; private set; }
        public new CharacterHealthController HealthController { get; private set; }
        public new CharacterInputHandler InputHandler { get; private set; }
        public new CharacterMovementController MovementController { get; private set; }
        public new CharacterRotationController RotationController { get; private set; }

        public CharacterData CharacterData => EntityData as CharacterData;
        
        
        

        protected override void Awake()
        {
            SetControllerReferences();
        }

        private void SetControllerReferences()
        {
            AnimationController = GetComponent<CharacterAnimationController>();
            AttackController = GetComponent<CharacterAttackController>();
            CollectorController = GetComponent<CharacterCollectorController>();
            CollisionController = GetComponent<CharacterCollisionController>();
            DropController = GetComponent<CharacterDropController>();
            HealthController = GetComponent<CharacterHealthController>();
            InputHandler = GetComponent<CharacterInputHandler>();
            MovementController = GetComponent<CharacterMovementController>();
            RotationController = GetComponent<CharacterRotationController>();
        }
    }
}
