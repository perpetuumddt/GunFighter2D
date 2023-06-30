using CharacterController = Gunfighter.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Entity.Character.StateMachine.States
{
    public class CharacterDeathState : State<Controller.CharacterController>
    {
        protected string AnimParameter = "isDeath";
    
    

        public CharacterDeathState(CharacterController data, StateMachine<CharacterController> machine) : base(data, machine)
        {
        }

        public override void Execute()
        {
            base.Execute();
            Data.CharacterData.InvokeOnDeath(Data.CharacterData);
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(AnimParameter, true);
        }

        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(AnimParameter, false);
        }

        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
        }
    }
}
