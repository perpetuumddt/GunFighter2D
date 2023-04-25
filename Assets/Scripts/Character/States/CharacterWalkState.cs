using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace StateMachine
{
    public class CharacterWalkState : State<CharacterController>
    {
        private string _animParameter = "isMove";

        private float _velocity = 5f;

        public CharacterWalkState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
        {
            
        }

        public override void Execute()
        {
            base.Execute();
            Movement(StateMachine.CurrentState.Data.CharacterInputHandler as PlayerInputHandler);
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(true, _animParameter);
        }
        public override void Initialize(params object[] param)
        {
            StateMachine.CurrentState.Data.CharacterInputHandler.OnMove += (isMoving) => SwitchState(isMoving);
        }

        private void SwitchState(bool isMove)
        {
            if (!isMove)
            {
                IsExecuted = false;
                StateMachine.CurrentState = new CharacterIdleState(StateMachine.CurrentState.Data, StateMachine);
                StateMachine.CurrentState.Initialize();
                StateMachine.CurrentState.Execute();
            }
            else
            {
                Execute();
            }
        }

        private  void Movement(PlayerInputHandler inputHandler)
        {
                StateMachine.CurrentState.Data.CharacterMovementController.SetVelocity(inputHandler.MovementInput.x * _velocity, inputHandler.MovementInput.y * _velocity);
                Debug.Log(inputHandler.MovementInput.x + " " + inputHandler.MovementInput.y);

            
        }
    }
}
