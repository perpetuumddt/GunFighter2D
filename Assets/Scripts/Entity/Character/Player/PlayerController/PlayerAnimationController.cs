using Gunfighter.Entity.Character.Controller;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Character.Player.PlayerController
{
    public class PlayerAnimationController : CharacterAnimationController
    {
        [FormerlySerializedAs("_animator")] [SerializeField]
        private Animator animator;

        public override void SetActiveBoolAnim(string parameter, bool isActive)
        {
            base.SetActiveBoolAnim(parameter, isActive);
            animator.SetBool(parameter, isActive);
        }
    }
}
