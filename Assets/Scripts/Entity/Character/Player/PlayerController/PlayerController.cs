using Entity.Character.Player.States;
using Entity.Character.StateMachine;
using ScriptableObjects.Data.Character.Player;
using ScriptableObjects.Event;
using UnityEngine;
using CharacterController = Entity.Character.Controller.CharacterController;

namespace Entity.Character.Player.PlayerController
{
    public class PlayerController : CharacterController
    {
        private PlayerLevelController _playerLevelController;
        public PlayerLevelController PlayerLevelController => _playerLevelController;
        [SerializeField] private ScriptableObjectExpEvent _expIncomingChannel;
        [SerializeField] private ScriptableObjectTwoIntEvent _onExpChangedChannel; 
        [SerializeField] private ScriptableObjectIntEvent _onLevelUpChannel; 
        private void Awake()
        {
            _playerLevelController = new PlayerLevelController((PlayerData)this.CharacterData, 1, 0);
            _stateMachine = new StateMachine<CharacterController>();
            _stateMachine.CurrentState = new PlayerIdleState(this, _stateMachine);
            _stateMachine.CurrentState.Initialize();
        }

        private void OnEnable()
        {
            _expIncomingChannel.OnEventRaised += _playerLevelController.AddExperience;
            _playerLevelController.OnExperienceChange += _onExpChangedChannel.RaiseEvent;
            _playerLevelController.OnLevelUp += _onLevelUpChannel.RaiseEvent;
        }

        private void OnDisable()
        {
            _expIncomingChannel.OnEventRaised -= _playerLevelController.AddExperience;
            _playerLevelController.OnExperienceChange -= _onExpChangedChannel.RaiseEvent;
            _playerLevelController.OnLevelUp -= _onLevelUpChannel.RaiseEvent;
        }
    }
}
