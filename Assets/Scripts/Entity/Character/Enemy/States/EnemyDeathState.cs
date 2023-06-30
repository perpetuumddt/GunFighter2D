using Gunfighter.Entity.Character.StateMachine;
using Gunfighter.Entity.Character.StateMachine.States;
using UnityEngine;
using CharacterController = Gunfighter.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Entity.Character.Enemy.States
{
    public class EnemyDeathState : CharacterDeathState
    {
        public EnemyDeathState(CharacterController data, StateMachine<CharacterController> machine) : base(data, machine)
        {
        }

        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
        }
        public override void Execute()
        {
            base.Execute();
            StateMachine.CurrentState.Data.CharacterMovementController.DoMove(false);
            StateMachine.CurrentState.Data.GetComponent<Rigidbody2D>().isKinematic = true;
            StateMachine.CurrentState.Data.CharacterDropController.DropItem();
            StateMachine.CurrentState.Data.CharacterHealthController.DestroyOnDeath();
        }

        public override void StopExecution()
        {
            base.StopExecution();
        }
    }
}
