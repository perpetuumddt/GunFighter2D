using Gunfighter.Runtime.Entity.Character.Controller;
using Gunfighter.Runtime.Entity.Character.Enemy.States;
using Gunfighter.Runtime.Entity.Character.StateMachine;

namespace Gunfighter.Runtime.Entity.Character.Enemy.EnemyController
{
    public class EnemyController : CharacterController
    {

        protected override void Awake()
        {
            base.Awake();
            StateMachine = new StateMachine<CharacterController>();
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
    }
}
