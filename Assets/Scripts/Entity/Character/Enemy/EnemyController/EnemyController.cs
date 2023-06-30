using System;
using Gunfighter.Entity.Character.Enemy.States;
using Gunfighter.Entity.Character.StateMachine;
using Gunfighter.Entity.Character.Controller;

namespace Gunfighter.Entity.Character.Enemy.EnemyController
{
    public class EnemyController : CharacterController
    {

        private void Awake()
        {
            StateMachine = new StateMachine<CharacterController>();
            StateMachine.CurrentState = new EnemyWalkState(this, StateMachine);
            StateMachine.CurrentState.Initialize();
            StateMachine.CurrentState.Execute();
        }
    }
}
