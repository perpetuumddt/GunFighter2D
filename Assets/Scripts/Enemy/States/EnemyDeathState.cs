using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : CharacterDeathState
{
    public EnemyDeathState(CharacterController data, StateMachine<CharacterController> machine) : base(data, machine)
    {
    }

    public override void Initialize(params object[] param)
    {
        base.Initialize(param);
    }
    public override void Execute()
    {
        base.Execute();
        StateMachine.CurrentState.Data.CharacterMovementController.DoMove(false);
        StateMachine.CurrentState.Data.GetComponent<Rigidbody2D>().isKinematic = true;
        StateMachine.CurrentState.Data.CharacterHealthController.DestroyOnDeath();
    }

    public override void StopExecution()
    {
        base.StopExecution();
    }
}
