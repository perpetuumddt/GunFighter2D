using CharacterController = Entity.Character.Controller.CharacterController;

namespace Entity.Character.StateMachine.States
{
    public class CharacterDeathState : State<CharacterController>
    {
        protected string _animParameter = "isDeath";
    
    

        public CharacterDeathState(CharacterController data, StateMachine<CharacterController> machine) : base(data, machine)
        {
        }

        public override void Execute()
        {
            base.Execute();
            Data.CharacterData.InvokeOnDeath(Data.CharacterData);
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
