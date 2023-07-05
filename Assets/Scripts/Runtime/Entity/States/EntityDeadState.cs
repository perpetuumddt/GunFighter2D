using Gunfighter.Runtime.Entity.StateMachine;

namespace Gunfighter.Runtime.Entity.States
{
    public abstract class EntityDeathState<T> : State<T>
    {
        public EntityDeathState(T data, StateMachine<T> machine) : base(data, machine)
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

        public abstract void SwitchAliveState();
    }
}