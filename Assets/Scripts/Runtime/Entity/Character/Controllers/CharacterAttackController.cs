using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Controllers
{
    public class CharacterAttackController : MonoBehaviour
    {
        protected CharacterController characterController;
        
        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        public virtual void DoAttack()
        {
        
        }
        
        public virtual void StopAttacking()
        {
            
        }

        public virtual void Reload()
        {

        }

        public virtual void ChangeWeapon()
        {
        
        }

        public virtual void SwapWeapon()
        {
        
        }

        public virtual void SetWeaponVisible(bool isVisible)
        {
            
        }
    }
}
