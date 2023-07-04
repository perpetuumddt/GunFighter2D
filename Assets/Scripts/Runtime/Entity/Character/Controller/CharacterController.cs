using Gunfighter.Runtime.Entity.Character.StateMachine;
using Gunfighter.Runtime.Entity.Controller;
using Gunfighter.Runtime.ScriptableObjects.Data.Entity.Character;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Controller
{
    public class CharacterController : EntityController
    {
        public CharacterAnimationController CharacterAnimationController { get; private set; }
        public CharacterAttackController CharacterAttackController { get; private set; }
        public CharacterCollectorController CharacterCollectorController { get; private set; }
        public CharacterDropController CharacterDropController { get; private set; }
        public CharacterHealthController CharacterHealthController { get; private set; }
        public CharacterInputHandler CharacterInputHandler { get; private set; }
        public CharacterMovementController CharacterMovementController { get; private set; }
        public CharacterRotationController CharacterRotationController { get; private set; }

        public CharacterData CharacterData => EntityData as CharacterData;
        
        
        protected StateMachine<CharacterController> StateMachine;

        protected override void Awake()
        {
            base.Awake();
            CharacterAnimationController = GetComponent<CharacterAnimationController>();
            CharacterAttackController = GetComponent<CharacterAttackController>();
            CharacterCollectorController = GetComponent<CharacterCollectorController>();
            CharacterDropController = GetComponent<CharacterDropController>();
            CharacterHealthController = GetComponent<CharacterHealthController>();
            CharacterInputHandler = GetComponent<CharacterInputHandler>();
            CharacterMovementController = GetComponent<CharacterMovementController>();
            CharacterRotationController = GetComponent<CharacterRotationController>();
        }
    }
}
