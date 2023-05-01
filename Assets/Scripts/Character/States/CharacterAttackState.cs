using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace StateMachine
{
    public class CharacterAttackState : State<CharacterController>
    {
        private string _animParameter = "isWalk";

        private float _velocity = 4f; //TODO: перенести в PlayerData

        public CharacterAttackState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
        {

        }

        public override void Execute()
        {
            base.Execute();
            Movement(StateMachine.CurrentState.Data.CharacterInputHandler as PlayerInputHandler);
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(_animParameter, true);
        }
        public override void Initialize(params object[] param)
        {
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
            //StateMachine.CurrentState.Data.CharacterInputHandler.OnRoll -= SwichStateRoll; 
            StateMachine.CurrentState = new CharacterRollState(StateMachine.CurrentState.Data, StateMachine);
            StateMachine.CurrentState.Initialize();
            StateMachine.CurrentState.Execute();

        }

        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(_animParameter, false);
            StateMachine.CurrentState.Data.CharacterInputHandler.OnMove -= SwichStateIdle;
            StateMachine.CurrentState.Data.CharacterInputHandler.OnRoll -= SwichStateRoll;
        }

        private  void Movement(PlayerInputHandler inputHandler)
        {
            StateMachine.CurrentState.Data.CharacterMovementController.SetVelocity(inputHandler.movementInputVector.x * _velocity, inputHandler.movementInputVector.y * _velocity);
        }

    }
}
