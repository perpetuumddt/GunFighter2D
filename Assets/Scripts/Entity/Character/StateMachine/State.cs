namespace Gunfighter.Entity.Character.StateMachine 
{
    public class State<T>
    {
        public virtual bool IsExecuted { get; set; }
        
        public virtual T Data { get; private set; }

        public virtual StateMachine<T> StateMachine { get; private set; }   

        public State(T data, StateMachine<T> machine)
        {
            StateMachine = machine;
            Data = data;
        }

        public virtual void Execute()
        {
            IsExecuted = true;
        }
        public virtual void StopExecution()
        {
            IsExecuted = false;
        }
        public virtual void Initialize(params object[] param)
        {
            
        }
    }
}

