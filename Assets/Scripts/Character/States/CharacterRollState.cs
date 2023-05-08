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

        }

        public override void Execute()
        {
            base.Execute();
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(_animParameter, true);
        }

        public override void StopExecution()
        {
            StateMachine.CurrentState.Data.CharacterAnimationController.SetActiveBoolAnim(_animParameter, false);
            base.StopExecution();
        }

        public override void Initialize(params object[] param)
        {

        }
    }
}
