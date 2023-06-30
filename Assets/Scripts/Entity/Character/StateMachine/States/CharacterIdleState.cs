using CharacterController = Gunfighter.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Entity.Character.StateMachine.States
{
    public class CharacterIdleState : State<Controller.CharacterController>
    {
        private string _animParameter = "isIdle";

        public CharacterIdleState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
        {
            
        }

        public override void Initialize(params object[] param)
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
    }
}