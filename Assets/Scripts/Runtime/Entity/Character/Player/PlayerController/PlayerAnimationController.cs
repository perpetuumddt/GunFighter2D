using Gunfighter.Runtime.Entity.Character.Controller;

namespace Gunfighter.Runtime.Entity.Character.Player.PlayerController
{
    public class PlayerAnimationController : CharacterAnimationController
    {

        public override void SetActiveBoolAnim(string parameter, bool isActive)
        {
            base.SetActiveBoolAnim(parameter, isActive);
            _animator.SetBool(parameter, isActive);
        }
    }
}
