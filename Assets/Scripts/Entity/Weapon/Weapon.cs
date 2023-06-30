using ScriptableObjects.Data.Weapon;
using UnityEngine;

namespace Entity.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        protected WeaponData _weaponData;

        public float Damage => _weaponData.Damage;

        public WeaponData WeaponData => _weaponData;

        public virtual void DoAttack(AttackType attackType)
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
