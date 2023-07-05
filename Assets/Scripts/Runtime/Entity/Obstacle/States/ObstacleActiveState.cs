using Gunfighter.Runtime.Entity.Obstacle.Controllers;
using Gunfighter.Runtime.Entity.StateMachine;
using Gunfighter.Runtime.Entity.States;

namespace Gunfighter.Runtime.Entity.Obstacle.States
{
    public class ObstacleActiveState : EntityActiveState<ObstacleController>
    {
        private string animParam = "isActive"; 
        public ObstacleActiveState(ObstacleController data, StateMachine<ObstacleController> machine) : base(data, machine)
        {
        }
        
        
        
        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
            
            Data.HealthController.OnHealthZero += SwitchDeathState;
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void StopExecution()
        {
            base.StopExecution();
            Data.HealthController.OnHealthZero -= SwitchDeathState;
        }

        public override void SwitchDeathState()
        {
            StopExecution();
            StateMachine.CurrentState = new ObstacleDeadState(StateMachine.CurrentState.Data, StateMachine);
            StateMachine.CurrentState.Initialize();
            StateMachine.CurrentState.Execute();
        }
    }
}
