using System.Collections;
using Gunfighter.Runtime.Entity.Character.Player.PlayerController;
using Gunfighter.Runtime.Entity.Character.StateMachine;
using Gunfighter.Runtime.Entity.Character.StateMachine.States;
using Gunfighter.Runtime.General.CustomYieldInstructions;
using UnityEngine;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Player.States
{
    public class PlayerDeathState : CharacterDeathState
    {
        public PlayerDeathState(CharacterController data, StateMachine<CharacterController> machine) : base(data, machine)
        {
            
        }

        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
            ((PlayerHealthController)Data.CharacterHealthController).CanBeInvincible = false;
            ((PlayerAttackController)Data.CharacterAttackController).SetWeaponVisible(false);
            StateMachine.CurrentState.Data.GetComponent<Rigidbody2D>().isKinematic = true;
            ((PlayerMovementController)StateMachine.CurrentState.Data.CharacterMovementController).StopMovement();
            Data.StartCoroutine(WaitForDeathAnimation());
        }
        
        private IEnumerator WaitForDeathAnimation()
        {
            // Wait for previous animation to finish
            yield return new WaitForAnimationToFinish(Data.CharacterAnimationController.Animator); 
            // Wait for death animation to finish
            yield return new WaitForAnimationToFinish(Data.CharacterAnimationController.Animator);
            // Wait for a little time before exiting state, to see dead enemy
            yield return new WaitForSeconds(0.3f);
            ((PlayerController.PlayerController)StateMachine.CurrentState.Data).PlayerDeathEvent.RaiseEvent();
        }

        public override void StopExecution()
        {
            ((PlayerHealthController)Data.CharacterHealthController).CanBeInvincible = true;
            ((PlayerAttackController)Data.CharacterAttackController).SetWeaponVisible(true);
            StateMachine.CurrentState.Data.GetComponent<Rigidbody2D>().isKinematic = false;
            base.StopExecution();
        }
    }
}
