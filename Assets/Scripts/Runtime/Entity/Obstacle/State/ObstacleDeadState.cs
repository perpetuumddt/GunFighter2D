using Gunfighter.Runtime.Entity.Character.StateMachine;
using Gunfighter.Runtime.Entity.Obstacle.Controllers;
using Gunfighter.Runtime.Entity.State;

namespace Gunfighter.Runtime.Entity.Obstacle.State
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
            
            Data.EntityHealthController.DestroyOnDeath();
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