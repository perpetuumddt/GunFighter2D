using CharacterController = Gunfighter.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Entity.Character.StateMachine.States
{
    public class CharacterWalkState : State<Controller.CharacterController>
    {
        protected string AnimParameter = "isWalk";

        public CharacterWalkState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
        {

        }

        public override void Execute()
        {
            base.Execute();
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(AnimParameter, true);
        }
        public override void Initialize(params object[] param)
        {
            
        }

        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(AnimParameter, false);
        }
    }
}
