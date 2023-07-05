using Gunfighter.Runtime.Entity.Character.Controllers;

namespace Gunfighter.Runtime.Entity.Character.Player.Controllers
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
