using System.Threading.Tasks;
using Gunfighter.Entity.Character.Player.Input;
using Gunfighter.Entity.Character.Player.PlayerController;
using Gunfighter.Entity.Character.StateMachine;
using Gunfighter.Entity.Character.StateMachine.States;
using UnityEngine;
using CharacterController = Gunfighter.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Entity.Character.Player.States
{
    public class PlayerRollState : CharacterRollState
    {
        private float _velocity = 6f;
        private float _duration = 0.4f;

        private bool _rollLeft;

        public PlayerRollState(CharacterController data, StateMachine<CharacterController> stateMachine) : base(data, stateMachine)
        {
        }

        public override void Execute()
        {
            base.Execute();
            Roll(StateMachine.CurrentState.Data.CharacterInputHandler as PlayerInputHandler);
        }

        public override void StopExecution()
        {
            ((PlayerAttackController)Data.CharacterAttackController).SetWeaponVisible(true);
            base.StopExecution();
        }

        public override void Initialize(params object[] param)
        {
            ((PlayerAttackController)Data.CharacterAttackController).SetWeaponVisible(false);
            base.Initialize(param);
        }

        private void SwichState()
        {
            if (IsExecuted)
            {
                StopExecution();
                StateMachine.CurrentState = new PlayerIdleState(StateMachine.CurrentState.Data, StateMachine);
                StateMachine.CurrentState.Initialize();
                StateMachine.CurrentState.Execute();
            }
        }

        private async void Roll(PlayerInputHandler inputHandler)
        {
            var rollDirection = inputHandler.MovementInputVector;
            StateMachine.CurrentState.Data.CharacterRotationController.CheckRollingDirection();
            while (_duration > 0f)
            {
                StateMachine.CurrentState.Data.CharacterMovementController.DoMove(rollDirection.x * _velocity, rollDirection.y * _velocity);
                _duration -= Time.deltaTime;
                await Task.Delay(1);
            }
            SwichState();
        }
    }
}

