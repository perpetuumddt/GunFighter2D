using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class CharacterIdleState : State<CharacterController>
    {
        private string _animParameter = "isIdle";

        public CharacterIdleState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
        {
            stateMachine.CurrentState.Data.CharacterInputHandler.OnMove += (isMoving) => SwitchState(isMoving, stateMachine);
        }

        public override void Execute(StateMachine<CharacterController> stateMachine)
        {
            base.Execute(stateMachine);
            stateMachine.CurrentState.Data.CharacterMovementController.SetVelocity(0f, 0f);
            stateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(true,_animParameter);
        }

        private void SwitchState(bool isMove, StateMachine<CharacterController> stateMachine)
        {
            if(isMove)
            {
                stateMachine.CurrentState = new CharacterWalkState(stateMachine.CurrentState.Data, stateMachine);
                stateMachine.CurrentState.Execute(stateMachine);
            }
        }
    }
}