using Gunfighter.Entity.Weapon;
using UnityEngine;

namespace Gunfighter.Entity.Character.Controller
{
    public class CharacterAttackController : MonoBehaviour
    {
        protected CharacterController characterController;
        
        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            Initialize();
        }

        public virtual void Initialize()
        {
        
        }

        public virtual void DoAttack(AttackType attackType)
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
