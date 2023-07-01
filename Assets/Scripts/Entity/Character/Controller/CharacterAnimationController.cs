using UnityEngine;

namespace Gunfighter.Entity.Character.Controller
{
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField]
        protected Animator animator;

        public Animator Animator => animator;

        public virtual void SetActiveBoolAnim(string parameter, bool isActive)
        {
        
        }
    }
}
