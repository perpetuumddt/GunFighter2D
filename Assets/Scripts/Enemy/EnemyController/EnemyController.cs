using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : CharacterController
{
    private StateMachine<CharacterController> _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine<CharacterController>();
        _stateMachine.CurrentState = new EnemyWalkState(this, _stateMachine);
        _stateMachine.CurrentState.Initialize();
        _stateMachine.CurrentState.Execute();
    }
}
