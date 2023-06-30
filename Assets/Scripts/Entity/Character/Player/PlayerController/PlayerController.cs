using Gunfighter.Entity.Character.Player.States;
using Gunfighter.Entity.Character.StateMachine;
using Gunfighter.ScriptableObjects.Data.Character.Player;
using Gunfighter.ScriptableObjects.Event;
using UnityEngine;
using UnityEngine.Serialization;
using CharacterController = Gunfighter.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Entity.Character.Player.PlayerController
{
    public class PlayerController : Controller.CharacterController
    {
        private PlayerLevelController _playerLevelController;
        public PlayerLevelController PlayerLevelController => _playerLevelController;
        [FormerlySerializedAs("_expIncomingChannel")] [SerializeField] private ScriptableObjectExpEvent expIncomingChannel;
        [FormerlySerializedAs("_onExpChangedChannel")] [SerializeField] private ScriptableObjectTwoIntEvent onExpChangedChannel; 
        [FormerlySerializedAs("_onLevelUpChannel")] [SerializeField] private ScriptableObjectIntEvent onLevelUpChannel; 
        private void Awake()
        {
            _playerLevelController = new PlayerLevelController((PlayerData)this.CharacterData, 1, 0);
            StateMachine = new StateMachine<CharacterController>();
            StateMachine.CurrentState = new PlayerIdleState(this, StateMachine);
            StateMachine.CurrentState.Initialize();
        }

        private void OnEnable()
        {
            expIncomingChannel.EventRaised += _playerLevelController.AddExperience;
            _playerLevelController.OnExperienceChange += onExpChangedChannel.RaiseEvent;
            _playerLevelController.OnLevelUp += onLevelUpChannel.RaiseEvent;
        }

        private void OnDisable()
        {
            expIncomingChannel.EventRaised -= _playerLevelController.AddExperience;
            _playerLevelController.OnExperienceChange -= onExpChangedChannel.RaiseEvent;
            _playerLevelController.OnLevelUp -= onLevelUpChannel.RaiseEvent;
        }
    }
}
