using Gunfighter.Runtime.Entity.Character.Player.Components;
using Gunfighter.Runtime.Entity.Character.Player.Controllers;
using Gunfighter.Runtime.Entity.Character.States;
using Gunfighter.Runtime.Entity.StateMachine;
using Gunfighter.Runtime.Entity.Weapon;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controllers.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Player.States
{
    public class PlayerWalkState : CharacterWalkState<PlayerController>
    {

        private float _velocity = 4f;
        public PlayerWalkState(PlayerController data, StateMachine<PlayerController> stateMachine) : base(data, stateMachine)
        {
        }

        public override void Execute()
        {
            base.Execute();
            Movement(StateMachine.CurrentState.Data.InputHandler as PlayerInputHandler);
        }

        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.InputHandler.OnMove -= SwichStateIdle;
            StateMachine.CurrentState.Data.InputHandler.OnRoll -= SwichStateRoll;
            StateMachine.CurrentState.Data.InputHandler.OnAttack -= Attack;
            StateMachine.CurrentState.Data.InputHandler.OnAttackCanceled -= AttackCanceled;
            StateMachine.CurrentState.Data.InputHandler.OnReload -= Reload;
            StateMachine.CurrentState.Data.InputHandler.OnSwapWeapon -= SwapWeapon;
            StateMachine.CurrentState.Data.HealthController.OnHealthZero -= SwichDeathState;
            StateMachine.CurrentState.Data.AttackController.StopAttacking();
        }

        public override void Initialize(params object[] param)
        {
            
            base.Initialize(param);
            StateMachine.CurrentState.Data.InputHandler.OnRoll += SwichStateRoll;
            StateMachine.CurrentState.Data.InputHandler.OnMove += SwichStateIdle;
            StateMachine.CurrentState.Data.InputHandler.OnAttack += Attack;
            StateMachine.CurrentState.Data.InputHandler.OnAttackCanceled += AttackCanceled;
            StateMachine.CurrentState.Data.InputHandler.OnReload += Reload;
            StateMachine.CurrentState.Data.InputHandler.OnSwapWeapon += SwapWeapon;
            StateMachine.CurrentState.Data.HealthController.OnHealthZero += SwichDeathState;
        }

        private void SwichDeathState()
        {
            StopExecution();
            StateMachine.CurrentState = new PlayerDeathState(StateMachine.CurrentState.Data, StateMachine);
            StateMachine.CurrentState.Initialize();
            StateMachine.CurrentState.Execute();
        }

        private void SwichStateIdle(bool isMove)
        {
            if (!isMove && IsExecuted)
            {
                StopExecution();
                StateMachine.CurrentState = new PlayerIdleState(StateMachine.CurrentState.Data, StateMachine);
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
            if ((Data.MovementController as PlayerMovementController).CanRoll)
            {
                StopExecution();
                StateMachine.CurrentState = new PlayerRollState(StateMachine.CurrentState.Data, StateMachine);
                StateMachine.CurrentState.Initialize();
                StateMachine.CurrentState.Execute();
            }

        }
        private void Movement(PlayerInputHandler inputHandler)
        {
            StateMachine.CurrentState.Data.RotationController.CheckLookingDirection();
            StateMachine.CurrentState.Data.MovementController.DoMove(inputHandler.MovementInputVector.x * _velocity, inputHandler.MovementInputVector.y * _velocity);
        }

        public void Attack()
        {
            StateMachine.CurrentState.Data.AttackController.DoAttack();
        }
        
        public void AttackCanceled()
        {
            StateMachine.CurrentState.Data.AttackController.StopAttacking();
        }

        public void Reload()
        {
            StateMachine.CurrentState.Data.AttackController.Reload();
        }

        public void ChangeWeapon()
        {
            StateMachine.CurrentState.Data.AttackController.ChangeWeapon();
        }

        private void SwapWeapon()
        {
            StateMachine.CurrentState.Data.AttackController.SwapWeapon();
        }
    }
}
