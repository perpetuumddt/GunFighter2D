using Gunfighter.Entity.Character.Controller;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Character.Player.PlayerController
{
    public class PlayerAnimationController : CharacterAnimationController
    {

        public override void SetActiveBoolAnim(string parameter, bool isActive)
        {
            base.SetActiveBoolAnim(parameter, isActive);
            animator.SetBool(parameter, isActive);
        }
    }
}
