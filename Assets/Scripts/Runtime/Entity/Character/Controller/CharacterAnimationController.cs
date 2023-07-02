using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Controller
{
    public class CharacterAnimationController : MonoBehaviour
    {
        protected Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public Animator Animator => _animator;

        public virtual void SetActiveBoolAnim(string parameter, bool isActive)
        {
        
        }
    }
}
