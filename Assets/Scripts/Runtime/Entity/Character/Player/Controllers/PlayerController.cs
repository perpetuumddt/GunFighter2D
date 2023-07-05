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
        public new PlayerAnimationController AnimationController => 
            base.AnimationController as PlayerAnimationController;
        public new PlayerAttackController AttackController => 
            base.AttackController as PlayerAttackController;
        public new PlayerCollectorController CollectorController => 
            base.CollectorController as PlayerCollectorController;
        
        // public new PlayerCollisionController CollisionController { get; private set; }
        public new PlayerDropController DropController => 
            base.DropController as PlayerDropController;
        public new PlayerHealthController HealthController =>
            base.HealthController as PlayerHealthController;
        public new CharacterInputHandler InputHandler =>
            base.InputHandler as PlayerInputHandler;
        public new PlayerMovementController MovementController => 
            base.MovementController as PlayerMovementController;
        public new PlayerRotationController RotationController => 
            base.RotationController as PlayerRotationController;
        
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
            LevelController = GetComponent<PlayerLevelController>();
        }

        
    }
}
