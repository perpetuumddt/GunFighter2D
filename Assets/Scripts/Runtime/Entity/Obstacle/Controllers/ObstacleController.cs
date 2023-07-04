using System;
using Gunfighter.Runtime.Entity.Controller;
using Gunfighter.Runtime.Entity.Obstacle.State;
using Gunfighter.Runtime.Entity.StateMachine;
using Serilog.Debugging;

namespace Gunfighter.Runtime.Entity.Obstacle.Controllers
{
    public class ObstacleController : EntityController
    {
        public new EntityAnimationController AnimationController { get; private set; }
        public new EntityCollisionController CollisionController { get; private set; }
        public new EntityDropController DropController { get; private set; }
        public new EntityHealthController HealthController { get; private set; }
        
        protected StateMachine<ObstacleController> StateMachine;
        protected void Awake()
        {
            //AnimationController = GetComponent<ObstacleAnimationController>();
            CollisionController = GetComponent<ObstacleCollisionController>();
            // DropController = GetComponent<ObstacleDropController>();
            HealthController = GetComponent<ObstacleHealthController>();
            
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
