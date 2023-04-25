using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class CharacterRollState : State<CharacterController>
    {
        private string _animParameter = "isRoll";

        public CharacterRollState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
        {
            stateMachine.CurrentState.Data.CharacterInputHandler.OnMove += (isMoving) => SwitchState(isMoving);
        }

        public override void Execute()
        {
            base.Execute();
            //stateMachine.CurrentState.Data.CharacterMovementController.SetVelocity(ROLL_VELOCITY*DIRECTION);
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(true, _animParameter);
        }

        public override void Initialize(params object[] param)
        {
            StateMachine.CurrentState.Data.CharacterInputHandler.OnMove += (isMoving) => SwitchState(isMoving);
        }

        private void SwitchState(bool isMove)
        {
            if(isMove) 
            {
                //CONTINUE MOVING
            }
            else //GO IDLE
            {
                StateMachine.CurrentState = new CharacterIdleState(StateMachine.CurrentState.Data, StateMachine);
                StateMachine.CurrentState.Initialize();
                StateMachine.CurrentState.Execute();
            }
        }
    }
}
