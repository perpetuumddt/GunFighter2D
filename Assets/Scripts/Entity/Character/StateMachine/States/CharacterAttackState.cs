using CharacterController = Entity.Character.Controller.CharacterController;

namespace Entity.Character.StateMachine.States
{
    public class CharacterAttackState : State<CharacterController>
    {
        private string _animParameter = "isWalk";

        public CharacterAttackState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
        {

        }

        public override void Execute()
        {
            base.Execute();
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(_animParameter, true);
        }
        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(_animParameter, false);
        }
        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
        }
    }
}
