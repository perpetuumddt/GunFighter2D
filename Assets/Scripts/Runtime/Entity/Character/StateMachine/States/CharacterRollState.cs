using CharacterController = Gunfighter.Runtime.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.StateMachine.States
{
    public class CharacterRollState : State<Controller.CharacterController>
    {
        private string _animParameter = "isRoll";

        public CharacterRollState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
        {

        }

        public override void Execute()
        {
            base.Execute();
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(_animParameter, true);
        }

        public override void StopExecution()
        {
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(_animParameter, false);
            base.StopExecution();
        }

        public override void Initialize(params object[] param)
        {

        }
    }
}
