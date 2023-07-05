using Gunfighter.Runtime.Entity.Character.Player.Controllers;
using Gunfighter.Runtime.Entity.Character.States;
using Gunfighter.Runtime.Entity.StateMachine;
using Gunfighter.Runtime.Entity.Weapon;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controllers.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Player.States
{
    public class PlayerIdleState : CharacterIdleState<PlayerController>
    {
        public PlayerIdleState(PlayerController data, StateMachine<PlayerController> stateMachine) : base(data, stateMachine)
        {
        }

        public override void Initialize(params object[] param)
        {
            base.Initialize(param);
            StateMachine.CurrentState.Data.InputHandler.OnMove += SwitchState;
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

        public override void Execute()
        {
            base.Execute();
            StateMachine.CurrentState.Data.RotationController.CheckMovementDirection();
            StateMachine.CurrentState.Data.RotationController.CheckLookingDirection();
            StateMachine.CurrentState.Data.MovementController.DoMove(0f, 0f);
        }

        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.InputHandler.OnMove -= SwitchState;
            StateMachine.CurrentState.Data.InputHandler.OnAttack -= Attack;
            StateMachine.CurrentState.Data.InputHandler.OnAttackCanceled -= AttackCanceled;
            StateMachine.CurrentState.Data.InputHandler.OnReload -= Reload;
            StateMachine.CurrentState.Data.InputHandler.OnSwapWeapon -= SwapWeapon;
            StateMachine.CurrentState.Data.HealthController.OnHealthZero -= SwichDeathState;
            StateMachine.CurrentState.Data.AttackController.StopAttacking();
        }

        public void Attack()
        {
            StateMachine.CurrentState.Data.AttackController.DoAttack();
        }
        
        private void AttackCanceled()
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

        private void SwitchState(bool isMove)
        {
            if (isMove && IsExecuted)
            {
                StopExecution();
                StateMachine.CurrentState = new PlayerWalkState(StateMachine.CurrentState.Data, StateMachine);
                StateMachine.CurrentState.Initialize();
                StateMachine.CurrentState.Execute();
            }
            else
            {
                Execute();
            }
        }

        private void SwapWeapon()
        {
            StateMachine.CurrentState.Data.AttackController.SwapWeapon();
        }
    }
}