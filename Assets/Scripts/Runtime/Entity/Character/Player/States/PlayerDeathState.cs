using System.Collections;
using Gunfighter.Runtime.Entity.Character.Player.Controllers;
using Gunfighter.Runtime.Entity.Character.States;
using Gunfighter.Runtime.Entity.StateMachine;
using Gunfighter.Runtime.General.CustomYieldInstructions;
using UnityEngine;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controllers.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Player.States
{
    public class PlayerDeathState : CharacterDeathState<PlayerController>
    {
        public PlayerDeathState(PlayerController data, StateMachine<PlayerController> machine) : base(data, machine)
        {
            
        }

        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
            Data.HealthController.CanBeInvincible = false;
            Data.AttackController.SetWeaponVisible(false);
            StateMachine.CurrentState.Data.GetComponent<Rigidbody2D>().isKinematic = true;
            StateMachine.CurrentState.Data.MovementController.StopMovement();
            Data.StartCoroutine(WaitForDeathAnimation());
        }
        
        private IEnumerator WaitForDeathAnimation()
        {
            // Wait for previous animation to finish
            yield return new WaitForAnimationToFinish(Data.AnimationController.Animator); 
            // Wait for death animation to finish
            yield return new WaitForAnimationToFinish(Data.AnimationController.Animator);
            // Wait for a little time before exiting state, to see dead enemy
            //yield return new WaitForSeconds(0.3f);
            StateMachine.CurrentState.Data.PlayerDeathEvent.RaiseEvent();
        }

        public override void StopExecution()
        {
            Data.HealthController.CanBeInvincible = true;
            Data.AttackController.SetWeaponVisible(true);
            StateMachine.CurrentState.Data.GetComponent<Rigidbody2D>().isKinematic = false;
            base.StopExecution();
        }
    }
}
