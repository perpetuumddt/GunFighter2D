using Gunfighter.Entity.Character.Player.PlayerController;
using Gunfighter.Entity.Character.StateMachine;
using Gunfighter.Entity.Character.StateMachine.States;
using UnityEngine;
using CharacterController = Gunfighter.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Entity.Character.Player.States
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
