using Gunfighter.Runtime.Entity.Character.States;
using Gunfighter.Runtime.Entity.Character.Player.Controllers;
using Gunfighter.Runtime.Entity.StateMachine;

namespace Gunfighter.Runtime.Entity.Character.Player.States
{
    public class PlayerAttackState : CharacterAttackState<PlayerController> // Should be used for special attack cases
    {
        /*
    private float _velocity = 4f;
    public PlayerAttackState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
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
    }

    public override void Initialize(params object[] param)
    {
        base.Initialize(param);
        StateMachine.CurrentState.Data.CharacterInputHandler.OnRoll += SwichStateRoll;
        StateMachine.CurrentState.Data.CharacterInputHandler.OnMove += SwichStateIdle;
    }

    private void SwichStateIdle(bool isMove)
    {
        if (!isMove && IsExecuted)
        {
            StopExecution();
            StateMachine.CurrentState = new CharacterIdleState(StateMachine.CurrentState.Data, StateMachine);
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
        StateMachine.CurrentState = new CharacterRollState(StateMachine.CurrentState.Data, StateMachine);
        StateMachine.CurrentState.Initialize();
        StateMachine.CurrentState.Execute();

    }

    private void Movement(PlayerInputHandler inputHandler)
    {
        StateMachine.CurrentState.Data.CharacterMovementController.DoMove(inputHandler.movementInputVector.x * _velocity, inputHandler.movementInputVector.y * _velocity);
    }*/
        public PlayerAttackState(PlayerController data, StateMachine<PlayerController> stateMachine) : base(data, stateMachine)
        {
        }
    }
}
