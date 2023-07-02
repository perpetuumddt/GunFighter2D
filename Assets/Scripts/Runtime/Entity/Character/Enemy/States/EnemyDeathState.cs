using System.Collections;
using Gunfighter.Runtime.Entity.Character.StateMachine;
using Gunfighter.Runtime.Entity.Character.StateMachine.States;
using Gunfighter.Runtime.General.CustomYieldInstructions;
using UnityEngine;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Enemy.States
{
    public class EnemyDeathState : CharacterDeathState
    {
        private readonly float _waitAfterAnimationFinishedTime = 0.3f;
        public EnemyDeathState(CharacterController data, StateMachine<CharacterController> machine) : base(data, machine)
        {
        }

        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
            StateMachine.CurrentState.Data.CharacterMovementController.DoMove(false);
            StateMachine.CurrentState.Data.GetComponent<Rigidbody2D>().isKinematic = true;
            StateMachine.CurrentState.Data.CharacterDropController.DropItem();
            Data.StartCoroutine(WaitForDeathAnimation());
        }
        public override void Execute()
        {
            base.Execute();

        }

        private IEnumerator WaitForDeathAnimation()
        {
            // Wait for previous animation to finish
            yield return new WaitForAnimationToFinish(Data.CharacterAnimationController.Animator); 
            // Wait for death animation to finish
            yield return new WaitForAnimationToFinish(Data.CharacterAnimationController.Animator);
            // Wait for a little time before exiting state, to see dead enemy
            yield return new WaitForSeconds(_waitAfterAnimationFinishedTime);
            StopExecution();
        }

        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.CharacterHealthController.DestroyOnDeath();
        }
    }
}
