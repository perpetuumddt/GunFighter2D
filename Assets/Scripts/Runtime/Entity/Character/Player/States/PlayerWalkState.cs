using Gunfighter.Runtime.Entity.Character.Player.Components;
using Gunfighter.Runtime.Entity.Character.Player.PlayerController;
using Gunfighter.Runtime.Entity.Character.StateMachine;
using Gunfighter.Runtime.Entity.Character.StateMachine.States;
using Gunfighter.Runtime.Entity.Weapon;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Player.States
{
    public class PlayerWalkState : CharacterWalkState
    {

        private float _velocity = 4f;
        public PlayerWalkState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
        {
        }

        public override void Execute()
        {
            base.Execute();
            Movement(StateMachine.CurrentState.Data.CharacterInputHandler as PlayerInputHandler);
        }

        public override void StopExecution()
        {
            base.StopExecution();
            StateMachine.CurrentState.Data.CharacterInputHandler.OnMove -= SwichStateIdle;
            StateMachine.CurrentState.Data.CharacterInputHandler.OnRoll -= SwichStateRoll;
            StateMachine.CurrentState.Data.CharacterInputHandler.OnAttack -= Attack;
            StateMachine.CurrentState.Data.CharacterInputHandler.OnAttackCanceled -= AttackCanceled;
            StateMachine.CurrentState.Data.CharacterInputHandler.OnReload -= Reload;
            StateMachine.CurrentState.Data.CharacterInputHandler.OnSwapWeapon -= SwapWeapon;
            StateMachine.CurrentState.Data.CharacterHealthController.OnHealthZero -= SwichDeathState;
            StateMachine.CurrentState.Data.CharacterAttackController.StopAttacking();
        }

        public override void Initialize(params object[] param)
        {
            
            base.Initialize(param);
            StateMachine.CurrentState.Data.CharacterInputHandler.OnRoll += SwichStateRoll;
            StateMachine.CurrentState.Data.CharacterInputHandler.OnMove += SwichStateIdle;
            StateMachine.CurrentState.Data.CharacterInputHandler.OnAttack += Attack;
            StateMachine.CurrentState.Data.CharacterInputHandler.OnAttackCanceled += AttackCanceled;
            StateMachine.CurrentState.Data.CharacterInputHandler.OnReload += Reload;
            StateMachine.CurrentState.Data.CharacterInputHandler.OnSwapWeapon += SwapWeapon;
            StateMachine.CurrentState.Data.CharacterHealthController.OnHealthZero += SwichDeathState;
        }

        private void SwichDeathState(bool isDead)
        {
            if (isDead)
            {
                StopExecution();
                StateMachine.CurrentState = new PlayerDeathState(StateMachine.CurrentState.Data, StateMachine);
                StateMachine.CurrentState.Initialize();
                StateMachine.CurrentState.Execute();
            }
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
            if ((Data.CharacterMovementController as PlayerMovementController).CanRoll)
            {
                StopExecution();
                StateMachine.CurrentState = new PlayerRollState(StateMachine.CurrentState.Data, StateMachine);
                StateMachine.CurrentState.Initialize();
                StateMachine.CurrentState.Execute();
            }

        }
        private void Movement(PlayerInputHandler inputHandler)
        {
            StateMachine.CurrentState.Data.CharacterRotationController.CheckLookingDirection();
            StateMachine.CurrentState.Data.CharacterMovementController.DoMove(inputHandler.MovementInputVector.x * _velocity, inputHandler.MovementInputVector.y * _velocity);
        }

        public void Attack()
        {
            StateMachine.CurrentState.Data.CharacterAttackController.DoAttack(AttackType.Single);
        }
        
        public void AttackCanceled()
        {
            StateMachine.CurrentState.Data.CharacterAttackController.StopAttacking();
        }

        public void Reload()
        {
            StateMachine.CurrentState.Data.CharacterAttackController.Reload();
        }

        public void ChangeWeapon()
        {
            StateMachine.CurrentState.Data.CharacterAttackController.ChangeWeapon();
        }

        private void SwapWeapon()
        {
            StateMachine.CurrentState.Data.CharacterAttackController.SwapWeapon();
        }
    }
}
