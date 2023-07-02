using System.Threading.Tasks;
using Gunfighter.Runtime.Entity.Character.Player.Input;
using Gunfighter.Runtime.Entity.Character.Player.PlayerController;
using Gunfighter.Runtime.Entity.Character.StateMachine;
using Gunfighter.Runtime.Entity.Character.StateMachine.States;
using Gunfighter.Runtime.ScriptableObjects.Data.Character.Player;
using UnityEngine;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Player.States
{
    public class PlayerRollState : CharacterRollState
    {
        private float _durationLeft;

        private PlayerData _playerData;

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
            _playerData = (Data.CharacterData as PlayerData);
            _durationLeft = _playerData.RollDuration;
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
            while (_durationLeft > 0f)
            {
                Vector2 moveVector = new Vector2(rollDirection.x * _playerData.RollSpeed,
                    rollDirection.y * _playerData.RollSpeed);
                StateMachine.CurrentState.Data.CharacterMovementController.DoMove(moveVector.x,moveVector.y);
                _durationLeft -= Time.deltaTime;
                (Data.CharacterMovementController as PlayerMovementController).StartCooldownTimer(_playerData
                    .RollCooldown);
                await Task.Delay(1);
            }
            SwichState();
        }
    }
}

