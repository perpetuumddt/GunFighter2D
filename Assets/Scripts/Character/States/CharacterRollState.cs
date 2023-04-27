using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace StateMachine
{
    public class CharacterRollState : State<CharacterController>
    {
        private string _animParameter = "isRoll";

        private float _velocity = 8f; //TODO: перенести в PlayerData
        private float _duration = 1f;

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

        }

        private void SwichState()
        {
           if(IsExecuted)
            {
                IsExecuted = false;
                StateMachine.CurrentState = new CharacterIdleState(StateMachine.CurrentState.Data, StateMachine);
                StateMachine.CurrentState.Initialize();
                StateMachine.CurrentState.Execute();
            }
        }

        private async void Roll(PlayerInputHandler inputHandler)
        {
            var rollDirection = inputHandler.movementInputVector;
            while(_duration > 0f)
            {
                StateMachine.CurrentState.Data.CharacterMovementController.SetVelocity(rollDirection.x * _velocity, rollDirection.y * _velocity);
                _duration -= Time.deltaTime;
                await Task.Delay(1);
            }
            SwichState();
        }
    }
}
