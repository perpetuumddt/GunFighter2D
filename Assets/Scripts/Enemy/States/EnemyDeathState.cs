using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : CharacterDeathState
{
    public EnemyDeathState(CharacterController data, StateMachine<CharacterController> machine) : base(data, machine)
    {
    }

    public override void Execute()
    {
        base.Execute();
        StopMovement();
    }

    public override void StopExecution()
    {
        base.StopExecution();
    }

    public override void Initialize(params object[] param)
    {
        base.Initialize(param);
    }

    private void StopMovement()
    {
        StateMachine.CurrentState.Data.CharacterMovementController.DoMove(false);
    }
}
