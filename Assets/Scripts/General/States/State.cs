using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine 
{
    public class State<T>
    {
        public virtual T Data { get; private set; }

        public State(T data, StateMachine<T> machine)
        {
            Data = data;
        }

        public virtual void Execute(StateMachine<T> stateMachine)
        {

        }
    }
}

