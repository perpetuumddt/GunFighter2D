namespace Entity.Character.StateMachine
{
    public class StateMachine<T>
    {
        public virtual State<T> CurrentState { get; set; }

        public StateMachine()
        {
            
        }

        public StateMachine(State<T> state) 
        {
            CurrentState = state;
        }
    }
}
