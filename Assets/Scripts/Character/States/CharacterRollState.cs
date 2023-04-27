using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class CharacterRollState : State<CharacterController>
    {
        private string _animParameter = "isRoll";

        private float _velocity = 8f; //TODO: перенести в PlayerData

        public CharacterRollState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
        {
            
        }

        public override void Execute()
        {
            base.Execute();
            Roll(StateMachine.CurrentState.Data.CharacterInputHandler as PlayerInputHandler);
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(true, _animParameter);
        }

        public override void Initialize(params object[] param)
        {
            StateMachine.CurrentState.Data.CharacterInputHandler.OnMove += (isMoving) => SwitchState(isMoving);
        }

        private void SwitchState(bool isMove)
        {
            if(isMove) //Continue moving
            {
                StateMachine.CurrentState = new CharacterIdleState(StateMachine.CurrentState.Data, StateMachine);
                StateMachine.CurrentState.Initialize();
                StateMachine.CurrentState.Execute();
            }
            else //Go idle
            {
                StateMachine.CurrentState = new CharacterIdleState(StateMachine.CurrentState.Data, StateMachine);
                StateMachine.CurrentState.Initialize();
                StateMachine.CurrentState.Execute();
            }
        }

        private void Roll(PlayerInputHandler inputHandler)
        {
            StateMachine.CurrentState.Data.CharacterMovementController.SetVelocity(inputHandler.movementInputVector.x * _velocity, inputHandler.movementInputVector.y * _velocity);
        }
    }
}
