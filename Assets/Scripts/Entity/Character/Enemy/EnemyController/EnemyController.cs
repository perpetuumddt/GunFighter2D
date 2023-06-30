using Gunfighter.Entity.Character.Enemy.States;
using Gunfighter.Entity.Character.StateMachine;
using CharacterController = Gunfighter.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Entity.Character.Enemy.EnemyController
{
    public class EnemyController : Controller.CharacterController
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
