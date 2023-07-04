using Gunfighter.Runtime.Entity.State;
using Gunfighter.Runtime.Entity.StateMachine;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.States
{
    public abstract class CharacterIdleState<T> : EntityActiveState<T> where T: CharacterController
    {
        private string _animParameter = "isIdle";

        public CharacterIdleState(T data, StateMachine<T> stateMachine) : base(data, stateMachine)
        {
            
        }

        public override void Initialize(params object[] param)
        {
            
        }

        public override void Execute()
        {
            base.Execute();
            StateMachine.CurrentState.Data.AnimationController.SetActiveBoolAnim(_animParameter, true);
        }

        public override void StopExecution()
        {
            StateMachine.CurrentState.Data.AnimationController.SetActiveBoolAnim(_animParameter, false);
            base.StopExecution();
        }

        public override void SwitchDeathState()
        {
            
        }
    }
}