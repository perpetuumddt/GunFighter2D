using Gunfighter.Runtime.Entity.Character.Controllers;
using Gunfighter.Runtime.Entity.Character.Enemy.States;
using Gunfighter.Runtime.Entity.StateMachine;

namespace Gunfighter.Runtime.Entity.Character.Enemy.Controllers
{
    public class EnemyController : CharacterController
    {
        public new EnemyAnimationController AnimationController => 
            base.AnimationController as EnemyAnimationController;
        public new EnemyAttackController AttackController => 
            base.AttackController as EnemyAttackController;
        
        public new EnemyCollisionController CollisionController =>
            base.CollisionController as EnemyCollisionController;
        public new EnemyDropController DropController => 
            base.DropController as EnemyDropController;
        public new EnemyHealthController HealthController =>
            base.HealthController as EnemyHealthController;
        public new EnemyMovementController MovementController => 
            base.MovementController as EnemyMovementController;
        public new EnemyRotationController RotationController => 
            base.RotationController as EnemyRotationController;
        
        
        protected StateMachine<EnemyController> StateMachine;

        protected override void Awake()
        {
            base.Awake();
            SetControllerReferences();
            StateMachine = new StateMachine<EnemyController>();
            StateMachine.CurrentState = new EnemyWalkState(this, StateMachine);
            
        }

        private void Start()
        {
            StateMachine.CurrentState.Initialize();
        }

        private void Update()
        {
            
            StateMachine.CurrentState.Execute();
        }
        
        private void SetControllerReferences()
        {
            
        }
    }
}
