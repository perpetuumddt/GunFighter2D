using System.Threading.Tasks;
using Gunfighter.Runtime.Entity.Character.Player.Components;
using Gunfighter.Runtime.Entity.Character.Player.Controllers;
using Gunfighter.Runtime.Entity.Character.States;
using Gunfighter.Runtime.Entity.StateMachine;
using Gunfighter.Runtime.ScriptableObjects.Data.Entity.Character.Player;
using UnityEngine;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controllers.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Player.States
{
    public class PlayerRollState : CharacterRollState<PlayerController>
    {
        private float _durationLeft;

        private PlayerData _playerData;

        public PlayerRollState(PlayerController data, StateMachine<PlayerController> stateMachine) : base(data, stateMachine)
        {
        }

        public override void Execute()
        {
            base.Execute();
            Roll(StateMachine.CurrentState.Data.InputHandler as PlayerInputHandler);
        }

        public override void StopExecution()
        {
            Data.AttackController.SetWeaponVisible(true);
            base.StopExecution();
        }

        public override void Initialize(params object[] param)
        {
            Data.AttackController.SetWeaponVisible(false);
            _playerData = (Data.CharacterData as PlayerData);
            _durationLeft = _playerData.RollDuration;
            base.Initialize(param);
        }

        private void SwichState()
        {
            if (!IsExecuted) return;
            StopExecution();
            StateMachine.CurrentState = new PlayerIdleState(StateMachine.CurrentState.Data, StateMachine);
            StateMachine.CurrentState.Initialize();
            StateMachine.CurrentState.Execute();
        }

        private async void Roll(PlayerInputHandler inputHandler)
        {
            var rollDirection = inputHandler.MovementInputVector;
            StateMachine.CurrentState.Data.RotationController.CheckRollingDirection();
            while (_durationLeft > 0f)
            {
                Vector2 moveVector = new Vector2(rollDirection.x * _playerData.RollSpeed,
                    rollDirection.y * _playerData.RollSpeed);
                StateMachine.CurrentState.Data.MovementController.DoMove(moveVector.x,moveVector.y);
                _durationLeft -= Time.deltaTime;
                Data.MovementController.StartCooldownTimer(_playerData.RollCooldown);
                await Task.Delay(1);
            }
            SwichState();
        }
    }
}

