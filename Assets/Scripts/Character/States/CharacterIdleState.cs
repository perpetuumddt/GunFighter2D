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
            
        }

        public override void Initialize(params object[] param)
        {
            StateMachine.CurrentState.Data.CharacterInputHandler.OnMove += SwitchState;
        }

        public override void Execute()
        {
            base.Execute();
            StateMachine.CurrentState.Data.CharacterMovementController.SetVelocity(0f, 0f);
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(true,_animParameter);
        }

        private void SwitchState(bool isMove)
        {
            if (isMove && IsExecuted)
            {
                StateMachine.CurrentState.Data.CharacterInputHandler.OnMove -= SwitchState;
                IsExecuted = false;
                StateMachine.CurrentState = new CharacterWalkState(StateMachine.CurrentState.Data, StateMachine);
                StateMachine.CurrentState.Initialize();
                StateMachine.CurrentState.Execute();
            }
            else
            {
                Execute();
            }
            //ROLL CAN`T BE PERFOMED IN IDLE STATE BECAUSE ROLL NEEDS DIRECTION
        }
    }
}