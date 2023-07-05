using System.Collections;
using Gunfighter.Runtime.Entity.Character.Enemy.Controllers;
using Gunfighter.Runtime.Entity.Character.States;
using Gunfighter.Runtime.Entity.StateMachine;
using Gunfighter.Runtime.General.CustomYieldInstructions;
using UnityEngine;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controllers.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Enemy.States
{
    public class EnemyDeathState : CharacterDeathState<EnemyController>
    {
        private readonly float _waitAfterAnimationFinishedTime = 0.3f;
        public EnemyDeathState(EnemyController data, StateMachine<EnemyController> machine) : base(data, machine)
        {
        }

        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
            StateMachine.CurrentState.Data.MovementController.DoMove(false);
            StateMachine.CurrentState.Data.GetComponent<Rigidbody2D>().isKinematic = true;
            StateMachine.CurrentState.Data.DropController.DropItem();
            Data.StartCoroutine(WaitForDeathAnimation());
        }
        public override void Execute()
        {
            base.Execute();

        }

        private IEnumerator WaitForDeathAnimation()
        {
            // Wait for previous animation to finish
            yield return new WaitForAnimationToFinish(Data.AnimationController.Animator); 
            // Wait for death animation to finish
            yield return new WaitForAnimationToFinish(Data.AnimationController.Animator);
            // Wait for a little time before exiting state, to see dead enemy
            yield return new WaitForSeconds(_waitAfterAnimationFinishedTime);
            StopExecution();
        }

        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.HealthController.DestroyOnDeath();
        }
    }
}
