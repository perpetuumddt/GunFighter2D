using Gunfighter.Runtime.Entity.Character.StateMachine;
using Gunfighter.Runtime.Entity.Character.StateMachine.States;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Enemy.States
{
    public class EnemyWalkState : CharacterWalkState
    {
        public EnemyWalkState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
        {
        }
        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
            StateMachine.CurrentState.Data.CharacterMovementController.DoMove(true);
            StateMachine.CurrentState.Data.CharacterHealthController.OnHealthZero += SwitchStateDeath;
        }
        public override void Execute()
        {
            base.Execute();
        }

        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.CharacterHealthController.OnHealthZero -= SwitchStateDeath;
        }

        private void SwitchStateDeath(bool onHealthZero)
        {
            if(onHealthZero)
            {
                StopExecution();
                StateMachine.CurrentState = new EnemyDeathState(StateMachine.CurrentState.Data, StateMachine);
                StateMachine.CurrentState.Initialize();
                StateMachine.CurrentState.Execute();
            }
            else
            {
                Execute();
            }
        }
    }
}
