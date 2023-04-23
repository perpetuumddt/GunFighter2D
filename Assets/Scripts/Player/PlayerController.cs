using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    private StateMachine<CharacterController> _stateMachine;

    private void Awake()
    {
        _stateMachine = new StateMachine<CharacterController>(new CharacterIdleState(this,_stateMachine));
    }
}
