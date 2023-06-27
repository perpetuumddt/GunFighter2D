using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;
using UnityEngine.TextCore.Text;

public class PlayerController : CharacterController
{
    private void Awake()
    {
        _stateMachine = new StateMachine<CharacterController>();
        _stateMachine.CurrentState = new PlayerIdleState(this, _stateMachine);
        _stateMachine.CurrentState.Initialize();
    }
}
