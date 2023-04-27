using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace StateMachine
{
    public class CharacterWalkState : State<CharacterController>
    {
        private string _animParameter = "isMove";

        private float _velocity = 4f; //TODO: перенести в PlayerData

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
             StateMachine.CurrentState.Data.CharacterInputHandler.OnMove += SwitchState;
        }

        private void SwitchState(bool isMove)
        {
            if (!isMove && IsExecuted)
            {
                StateMachine.CurrentState.Data.CharacterInputHandler.OnMove -= SwitchState;
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
            StateMachine.CurrentState.Data.CharacterMovementController.SetVelocity(inputHandler.movementInputVector.x * _velocity, inputHandler.movementInputVector.y * _velocity);
        }

    }
}
