using Gunfighter.Runtime.Entity.State;
using Gunfighter.Runtime.Entity.StateMachine;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.States
{
    public abstract class CharacterAttackState<T>  : EntityActiveState<T> where T: CharacterController
    {
        private string _animParameter = "isWalk";

        public CharacterAttackState(T data, StateMachine<T> stateMachine) : base(data, stateMachine)
        {

        }

        public override void Execute()
        {
            base.Execute();
        }
        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.AnimationController.SetActiveBoolAnim(_animParameter, false);
        }

        public override void SwitchDeathState()
        {
            throw new System.NotImplementedException();
        }

        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
            StateMachine.CurrentState.Data.AnimationController.SetActiveBoolAnim(_animParameter, true);
        }
    }
}
