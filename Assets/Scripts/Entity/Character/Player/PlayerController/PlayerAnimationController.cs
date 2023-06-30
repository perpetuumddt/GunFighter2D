using Entity.Character.Controller;
using UnityEngine;

namespace Entity.Character.Player.PlayerController
{
    public class PlayerAnimationController : CharacterAnimationController
    {
        [SerializeField]
        private Animator _animator;

        public override void SetActiveBoolAnim(string parameter, bool isActive)
        {
            base.SetActiveBoolAnim(parameter, isActive);
            _animator.SetBool(parameter, isActive);
        }
    }
}
