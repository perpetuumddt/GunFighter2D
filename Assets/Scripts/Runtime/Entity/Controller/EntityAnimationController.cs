using UnityEngine;

namespace Gunfighter.Runtime.Entity.Controller
{
    public abstract class EntityAnimationController : MonoBehaviour
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
