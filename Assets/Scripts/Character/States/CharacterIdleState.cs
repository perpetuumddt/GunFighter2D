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
            StateMachine.CurrentState.Data.CharacterInputHandler.OnAttack += Attack;
        }

        public override void Execute()
        {
            base.Execute();
            StateMachine.CurrentState.Data.CharacterMovementController.SetVelocity(0f, 0f);
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(_animParameter, true);
        }

        public override void StopExecution()
        {
            StateMachine.CurrentState.Data.CharacterInputHandler.OnMove -= SwitchState;
            StateMachine.CurrentState.Data.CharacterInputHandler.OnAttack -= Attack;
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(_animParameter, false);
            base.StopExecution();
        }

        private void SwitchState(bool isMove)
        {
            if (isMove && IsExecuted)
            {
                StopExecution();
                StateMachine.CurrentState = new CharacterWalkState(StateMachine.CurrentState.Data, StateMachine);
                StateMachine.CurrentState.Initialize();
                StateMachine.CurrentState.Execute();
            }
            else
            {
                Execute();
            }
        }

        public void Attack()
        {
            StateMachine.CurrentState.Data.CharacterAttackController.DoAttack();
        }

        public void Reload()
        {
            StateMachine.CurrentState.Data.CharacterAttackController.Reload();
        }

        public void ChangeWeapon()
        {
            StateMachine.CurrentState.Data.CharacterAttackController.ChangeWeapon();
        }
    }
}