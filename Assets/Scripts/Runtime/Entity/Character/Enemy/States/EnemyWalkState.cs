using Gunfighter.Runtime.Entity.Character.Enemy.Controllers;
using Gunfighter.Runtime.Entity.Character.States;
using Gunfighter.Runtime.Entity.StateMachine;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Enemy.States
{
    public class EnemyWalkState : CharacterWalkState<EnemyController>
    {
        public EnemyWalkState(EnemyController data, StateMachine<EnemyController> stateMachine) : base(data, stateMachine)
        {
        }
        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
            StateMachine.CurrentState.Data.MovementController.DoMove(true);
            StateMachine.CurrentState.Data.HealthController.OnHealthZero += SwitchStateDeath;
        }
        public override void Execute()
        {
            base.Execute();
            StateMachine.CurrentState.Data.RotationController.CheckLookingDirection();
        }

        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.HealthController.OnHealthZero -= SwitchStateDeath;
        }

        private void SwitchStateDeath()
        {
            StopExecution();
            StateMachine.CurrentState = new EnemyDeathState(StateMachine.CurrentState.Data, StateMachine);
            StateMachine.CurrentState.Initialize();
            StateMachine.CurrentState.Execute();
           
        }
    }
}
