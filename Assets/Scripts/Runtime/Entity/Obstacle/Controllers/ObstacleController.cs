using System;
using Gunfighter.Runtime.Entity.Character.StateMachine;
using Gunfighter.Runtime.Entity.Controller;
using Gunfighter.Runtime.Entity.Obstacle.State;
using Serilog.Debugging;

namespace Gunfighter.Runtime.Entity.Obstacle.Controllers
{
    public class ObstacleController : EntityController
    {
        protected StateMachine<ObstacleController> StateMachine;
        protected override void Awake()
        {
            base.Awake();
            StateMachine = new StateMachine<ObstacleController>();
            StateMachine.CurrentState = new ObstacleActiveState(this, StateMachine);
            StateMachine.CurrentState.Initialize();
        }

        private void Update()
        {
            StateMachine.CurrentState.Execute();
        }
    }
}
