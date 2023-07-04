using Gunfighter.Runtime.Entity.State;
using Gunfighter.Runtime.Entity.StateMachine;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.States
{
    public class CharacterDeathState<T> : EntityDeathState<T> where T: CharacterController
    {
        protected string AnimParameter = "isDeath";

        public CharacterDeathState(T data, StateMachine<T> machine) : base(data, machine)
        {
        }

        public override void Execute()
        {
            base.Execute();
            Data.CharacterData.InvokeOnDeath(Data.CharacterData);
            StateMachine.CurrentState.Data.AnimationController.SetActiveBoolAnim(AnimParameter, true);
        }

        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.AnimationController.SetActiveBoolAnim(AnimParameter, false);
        }

        public override void SwitchAliveState()
        {
            
        }

        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
        }
    }
}
