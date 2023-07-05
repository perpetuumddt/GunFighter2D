using Gunfighter.Runtime.Entity.Obstacle.Controllers;
using Gunfighter.Runtime.Entity.StateMachine;
using Gunfighter.Runtime.Entity.States;

namespace Gunfighter.Runtime.Entity.Obstacle.States
{
    public class ObstacleDeadState : EntityDeathState<ObstacleController>
    {
        private string animParam = "isActive"; 
        public ObstacleDeadState(ObstacleController data, StateMachine<ObstacleController> machine) : base(data, machine)
        {
        }
        
        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
            
            Data.HealthController.DestroyOnDeath();
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void StopExecution()
        {
            base.StopExecution();
        }

        public override void SwitchAliveState()
        {
            StopExecution();
            StateMachine.CurrentState = new ObstacleDeadState(StateMachine.CurrentState.Data, StateMachine);
            StateMachine.CurrentState.Initialize();
            StateMachine.CurrentState.Execute();
        }
    }
}