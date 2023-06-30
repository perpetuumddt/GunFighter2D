using Entity.Character.Enemy.States;
using Entity.Character.StateMachine;
using CharacterController = Entity.Character.Controller.CharacterController;

namespace Entity.Character.Enemy.EnemyController
{
    public class EnemyController : CharacterController
    {

        private void Awake()
        {
            _stateMachine = new StateMachine<CharacterController>();
            _stateMachine.CurrentState = new EnemyWalkState(this, _stateMachine);
            _stateMachine.CurrentState.Initialize();
            _stateMachine.CurrentState.Execute();
        }
    }
}
