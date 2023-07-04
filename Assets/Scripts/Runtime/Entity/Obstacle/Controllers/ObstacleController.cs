using System;
using Gunfighter.Runtime.Entity.Controller;
using Gunfighter.Runtime.Entity.Obstacle.State;
using Gunfighter.Runtime.Entity.StateMachine;
using Serilog.Debugging;

namespace Gunfighter.Runtime.Entity.Obstacle.Controllers
{
    public class ObstacleController : EntityController
    {
        public new ObstacleAnimationController AnimationController => 
            base.AnimationController as ObstacleAnimationController;
        public new ObstacleCollisionController CollisionController => 
            base.CollisionController as ObstacleCollisionController;
        public new ObstacleDropController DropController =>
            base.DropController as ObstacleDropController;
        public new ObstacleHealthController HealthController => 
            base.HealthController as ObstacleHealthController;
        
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
