using Gunfighter.Runtime.Entity.Character.Controller;

namespace Gunfighter.Runtime.Entity.Character.StateMachine.States
{
    public class CharacterWalkState : State<CharacterController>
    {
        protected string AnimParameter = "isWalk";

        public CharacterWalkState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
        {

        }

        public override void Execute()
        {
            base.Execute();
        }
        public override void Initialize(params object[] param)
        {
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(AnimParameter, true);
            
        }

        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(AnimParameter, false);
        }
    }
}
