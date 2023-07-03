using Gunfighter.Runtime.Entity.Character.Player.States;
using Gunfighter.Runtime.Entity.Character.StateMachine;
using Gunfighter.Runtime.ScriptableObjects.Event;
using UnityEngine;
using CharacterController = Gunfighter.Runtime.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Runtime.Entity.Character.Player.PlayerController
{
    public class PlayerController : Controller.CharacterController
    {
        public PlayerLevelController PlayerLevelController { get; private set; }
        
        public PlayerRotationController PlayerRotationController { get; private set; }

        [SerializeField]
        private SOVoidEvent playerDeathEvent;
        public SOVoidEvent PlayerDeathEvent => playerDeathEvent;

        protected override void Awake()
        {
            base.Awake();
            PlayerLevelController = GetComponent<PlayerLevelController>();
            PlayerRotationController = GetComponent<PlayerRotationController>();
            StateMachine = new StateMachine<CharacterController>();
            StateMachine.CurrentState = new PlayerIdleState(this, StateMachine);
            StateMachine.CurrentState.Initialize();
        }

        
    }
}
