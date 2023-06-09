using Gunfighter.Runtime.ScriptableObjects.Data.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.Entity.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        protected WeaponData weaponData;

        public float Damage => weaponData.Damage;

        public WeaponData WeaponData => weaponData;

        public virtual void DoAttack()
        {

        }
    
        public virtual void Initialize(SpriteRenderer spriteRenderer)
        {
            spriteRenderer.sprite = WeaponData.Sprite;
        }

        public virtual void Finilize()
        {
        
        }
    }
}
