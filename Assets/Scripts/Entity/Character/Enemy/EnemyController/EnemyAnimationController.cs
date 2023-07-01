using Gunfighter.Entity.Character.Controller;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Character.Enemy.EnemyController
{
    public class EnemyAnimationController : CharacterAnimationController
    {
        

        public override void SetActiveBoolAnim(string parameter, bool isActive)
        {
            base.SetActiveBoolAnim(parameter, isActive);
            _animator.SetBool(parameter, isActive);
        }
    }
}
