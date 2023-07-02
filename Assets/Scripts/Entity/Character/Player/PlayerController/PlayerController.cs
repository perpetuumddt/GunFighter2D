using Gunfighter.Entity.Character.Player.States;
using Gunfighter.Entity.Character.StateMachine;
using Gunfighter.ScriptableObjects.Event;
using UnityEngine;
using UnityEngine.Serialization;
using CharacterController = Gunfighter.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Entity.Character.Player.PlayerController
{
    public class PlayerController : Controller.CharacterController
    {
        public PlayerLevelController PlayerLevelController { get; private set; }


        [SerializeField]
        private SOVoidEvent playerDeathEvent;
        public SOVoidEvent PlayerDeathEvent => playerDeathEvent;

        protected override void Awake()
        {
            base.Awake();
            PlayerLevelController = GetComponent<PlayerLevelController>();
            StateMachine = new StateMachine<CharacterController>();
            StateMachine.CurrentState = new PlayerIdleState(this, StateMachine);
            StateMachine.CurrentState.Initialize();
        }

        
    }
}
