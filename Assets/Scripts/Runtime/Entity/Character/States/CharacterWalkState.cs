using Gunfighter.Runtime.Entity.Character.Controller;
using Gunfighter.Runtime.Entity.StateMachine;

namespace Gunfighter.Runtime.Entity.Character.States
{
    public class CharacterWalkState<T> : State<T> where T: CharacterController
    {
        protected string AnimParameter = "isWalk";

        public CharacterWalkState(T data, StateMachine<T> stateMachine) : base(data, stateMachine)
        {

        }

        public override void Execute()
        {
            base.Execute();
        }
        public override void Initialize(params object[] param)
        {
            StateMachine.CurrentState.Data.AnimationController.SetActiveBoolAnim(AnimParameter, true);
            
        }

        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.AnimationController.SetActiveBoolAnim(AnimParameter, false);
        }
    }
}
