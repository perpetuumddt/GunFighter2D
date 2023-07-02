using Gunfighter.Entity.Character.StateMachine;
using Gunfighter.Entity.Character.StateMachine.States;
using CharacterController = Gunfighter.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Entity.Character.Player.States
{
    public class PlayerDeathState : CharacterDeathState
    {
        public PlayerDeathState(CharacterController data, StateMachine<CharacterController> machine) : base(data, machine)
        {
            
        }
    }
}
