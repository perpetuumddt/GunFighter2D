using Gunfighter.Runtime.Entity.Character.StateMachine;

namespace Gunfighter.Runtime.Entity.State
{
    public abstract class EntityActiveState<T> : State<T>
    {
        public EntityActiveState(T data, StateMachine<T> machine) : base(data, machine)
        {
        }
        
        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void StopExecution()
        {
            base.StopExecution();
        }

        public abstract void SwitchDeathState();
    }
}
