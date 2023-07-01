using System;
using Gunfighter.Entity.Character.Enemy.States;
using Gunfighter.Entity.Character.StateMachine;
using Gunfighter.Entity.Character.Controller;

namespace Gunfighter.Entity.Character.Enemy.EnemyController
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
