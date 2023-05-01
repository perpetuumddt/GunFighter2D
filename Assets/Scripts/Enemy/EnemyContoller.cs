using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyContoller : CharacterController
{
    private StateMachine<CharacterController> _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine<CharacterController>();
        _stateMachine.CurrentState = new CharacterIdleState(this, _stateMachine);
        _stateMachine.CurrentState.Initialize();
    }
}
