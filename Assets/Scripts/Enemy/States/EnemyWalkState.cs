using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : CharacterWalkState
{
    public EnemyWalkState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
    {
    }
    public override void Initialize(params object[] param)
    {
        base.Initialize(param);
        StateMachine.CurrentState.Data.CharacterHealthController.OnHealthZero += SwitchStateDeath;
    }
    public override void Execute()
    {
        base.Execute();
        StateMachine.CurrentState.Data.CharacterMovementController.DoMove(true);
    }

    public override void StopExecution()
    {
        base.StopExecution();
        StateMachine.CurrentState.Data.CharacterHealthController.OnHealthZero -= SwitchStateDeath;
    }

    private void SwitchStateDeath(bool _onHealthZero)
    {
        if(_onHealthZero)
        {
            StopExecution();
            StateMachine.CurrentState = new EnemyDeathState(StateMachine.CurrentState.Data, StateMachine);
            StateMachine.CurrentState.Initialize();
            StateMachine.CurrentState.Execute();
        }
        else
        {
            Execute();
        }
    }
}
