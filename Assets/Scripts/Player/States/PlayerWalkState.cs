using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : CharacterWalkState
{

    private float _velocity = 4f;
    public PlayerWalkState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
    {
    }

    public override void Execute()
    {
        base.Execute();
        Movement(StateMachine.CurrentState.Data.CharacterInputHandler as PlayerInputHandler);
    }

    public override void StopExecution()
    {
        base.StopExecution();
        StateMachine.CurrentState.Data.CharacterInputHandler.OnMove -= SwichStateIdle;
        StateMachine.CurrentState.Data.CharacterInputHandler.OnRoll -= SwichStateRoll;
        StateMachine.CurrentState.Data.CharacterInputHandler.OnAttack -= Attack;
        StateMachine.CurrentState.Data.CharacterInputHandler.OnReload -= Reload;
        StateMachine.CurrentState.Data.CharacterInputHandler.OnSwapWeapon -= SwapWeapon;
    }

    public override void Initialize(params object[] param)
    {
        base.Initialize(param);
        StateMachine.CurrentState.Data.CharacterInputHandler.OnRoll += SwichStateRoll;
        StateMachine.CurrentState.Data.CharacterInputHandler.OnMove += SwichStateIdle;
        StateMachine.CurrentState.Data.CharacterInputHandler.OnAttack += Attack;
        StateMachine.CurrentState.Data.CharacterInputHandler.OnReload += Reload;
        StateMachine.CurrentState.Data.CharacterInputHandler.OnSwapWeapon += SwapWeapon;
    }

    private void SwichStateIdle(bool isMove)
    {
        if (!isMove && IsExecuted)
        {
            StopExecution();
            StateMachine.CurrentState = new PlayerIdleState(StateMachine.CurrentState.Data, StateMachine);
            StateMachine.CurrentState.Initialize();
            StateMachine.CurrentState.Execute();
        }
        else
        {
            Execute();
        }
    }

    private void SwichStateRoll()
    {
        StopExecution();
        StateMachine.CurrentState = new PlayerRollState(StateMachine.CurrentState.Data, StateMachine);
        StateMachine.CurrentState.Initialize();
        StateMachine.CurrentState.Execute();

    }
    private void Movement(PlayerInputHandler inputHandler)
    {
        StateMachine.CurrentState.Data.CharacterRotationController.CheckLookingDirection();
        StateMachine.CurrentState.Data.CharacterMovementController.DoMove(inputHandler.movementInputVector.x * _velocity, inputHandler.movementInputVector.y * _velocity);
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

    private void SwapWeapon()
    {
        StateMachine.CurrentState.Data.CharacterAttackController.SwapWeapon();
    }
}
