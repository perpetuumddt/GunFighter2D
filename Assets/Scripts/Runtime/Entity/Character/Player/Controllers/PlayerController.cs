using Gunfighter.Runtime.Entity.Character.Player.Components;
using Gunfighter.Runtime.Entity.Character.Player.States;
using Gunfighter.Runtime.Entity.StateMachine;
using Gunfighter.Runtime.ScriptableObjects.Event;
using UnityEngine;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Player.Controllers
{
    public class PlayerController : CharacterController
    {
        public new PlayerAnimationController AnimationController { get; private set; }
        public new PlayerAttackController AttackController { get; private set; }
        public new PlayerCollectorController CollectorController { get; private set; }
        
        // public new PlayerCollisionController CollisionController { get; private set; }
        public new PlayerDropController DropController { get; private set; }
        public new PlayerHealthController HealthController { get; private set; }
        public new CharacterInputHandler InputHandler { get; private set; }
        public new PlayerMovementController MovementController { get; private set; }
        public new PlayerRotationController RotationController { get; private set; }
        
        public PlayerLevelController LevelController { get; private set; }

        [SerializeField]
        private SOVoidEvent playerDeathEvent;
        public SOVoidEvent PlayerDeathEvent => playerDeathEvent;
        
        protected StateMachine<PlayerController> StateMachine;
        protected override void Awake()
        {
            base.Awake();
            
            SetControllerReferences();
            
            StateMachine = new StateMachine<PlayerController>();
            StateMachine.CurrentState = new PlayerIdleState(this, StateMachine);
            StateMachine.CurrentState.Initialize();
        }

        private void SetControllerReferences()
        {
            AnimationController = GetComponent<PlayerAnimationController>();
            AttackController = GetComponent<PlayerAttackController>();
            CollectorController = GetComponent<PlayerCollectorController>();
            DropController = GetComponent<PlayerDropController>();
            HealthController = GetComponent<PlayerHealthController>();
            InputHandler = GetComponent<PlayerInputHandler>();
            MovementController = GetComponent<PlayerMovementController>();
            RotationController = GetComponent<PlayerRotationController>();
            
            LevelController = GetComponent<PlayerLevelController>();
        }

        
    }
}
