using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Controllers
{
    public class CharacterMovementController : MonoBehaviour
    {
        protected CharacterController characterController;

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        public virtual void DoMove(params object[]param)
        {
        }
    
    }
}
