using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : CharacterIdleState
{
    public PlayerIdleState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
    {
    }

    public override void Initialize(params object[] param)
    {
        base.Initialize(param);
        StateMachine.CurrentState.Data.CharacterInputHandler.OnMove += SwitchState;
        StateMachine.CurrentState.Data.CharacterInputHandler.OnAttack += Attack;
        StateMachine.CurrentState.Data.CharacterInputHandler.OnSwapWeapon += SwapWeapon;
    }

    public override void Execute()
    {
        base.Execute();
        StateMachine.CurrentState.Data.CharacterRotationController.CheckMovementDirection();
        StateMachine.CurrentState.Data.CharacterRotationController.CheckLookingDirection();
        StateMachine.CurrentState.Data.CharacterMovementController.DoMove(0f, 0f);
    }

    public override void StopExecution()
    {
        base.StopExecution();
        StateMachine.CurrentState.Data.CharacterInputHandler.OnMove -= SwitchState;
        StateMachine.CurrentState.Data.CharacterInputHandler.OnAttack -= Attack;
        StateMachine.CurrentState.Data.CharacterInputHandler.OnSwapWeapon -= SwapWeapon;
    }

    public void Attack(bool isAttacking)
    {
        StateMachine.CurrentState.Data.CharacterAttackController.DoAttack(AttackType.Single);
    }

    public void Reload()
    {
        StateMachine.CurrentState.Data.CharacterAttackController.Reload();
    }

    public void ChangeWeapon()
    {
        StateMachine.CurrentState.Data.CharacterAttackController.ChangeWeapon();
    }

    private void SwitchState(bool isMove)
    {
        if (isMove && IsExecuted)
        {
            StopExecution();
            StateMachine.CurrentState = new PlayerWalkState(StateMachine.CurrentState.Data, StateMachine);
            StateMachine.CurrentState.Initialize();
            StateMachine.CurrentState.Execute();
        }
        else
        {
            Execute();
        }
    }

    private void SwapWeapon()
    {
        StateMachine.CurrentState.Data.CharacterAttackController.SwapWeapon();
    }
}