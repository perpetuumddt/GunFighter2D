﻿using Gunfighter.Runtime.Entity.StateMachine;
using Gunfighter.Runtime.Entity.States;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controllers.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.States
{
    public class CharacterRollState<T> : EntityActiveState<T> where T: CharacterController

    {
    private string _animParameter = "isRoll";

    public CharacterRollState(T data, StateMachine<T> stateMachine) : base(data,
        stateMachine)
    {

    }

    public override void Execute()
    {
        base.Execute();
        StateMachine.CurrentState.Data.AnimationController.SetActiveBoolAnim(_animParameter, true);
    }

    public override void StopExecution()
    {
        StateMachine.CurrentState.Data.AnimationController.SetActiveBoolAnim(_animParameter, false);
        base.StopExecution();
    }

    public override void SwitchDeathState()
    {
        
    }

    public override void Initialize(params object[] param)
    {

    }
    }
}
