using Gunfighter.Runtime.Entity.Character.Controller;
using Gunfighter.Runtime.Entity.Character.Enemy.States;
using Gunfighter.Runtime.Entity.StateMachine;

namespace Gunfighter.Runtime.Entity.Character.Enemy.Controllers
{
    public class EnemyController : CharacterController
    {
        public new EnemyAnimationController AnimationController { get; private set; }
        public new EnemyAttackController AttackController { get; private set; }
        public new EnemyCollisionController CollisionController { get; private set; }
        public new EnemyDropController DropController { get; private set; }
        public new EnemyHealthController HealthController { get; private set; }
        public new EnemyMovementController MovementController { get; private set; }
        public new EnemyRotationController RotationController { get; private set; }
        
        
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
            AnimationController = GetComponent<EnemyAnimationController>();
            AttackController = GetComponent<EnemyAttackController>();
            CollisionController = GetComponent<EnemyCollisionController>();
            DropController = GetComponent<EnemyDropController>();
            HealthController = GetComponent<EnemyHealthController>();
            MovementController = GetComponent<EnemyMovementController>();
            RotationController = GetComponent<EnemyRotationController>();
            
        }
    }
}
