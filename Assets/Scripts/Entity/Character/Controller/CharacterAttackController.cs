using Gunfighter.Entity.Weapon;
using UnityEngine;

namespace Gunfighter.Entity.Character.Controller
{
    public class CharacterAttackController : MonoBehaviour
    {
        private void Awake()
        {
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
    }
}
