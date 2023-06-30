using Gunfighter.ScriptableObjects.Data.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [FormerlySerializedAs("_weaponData")] [SerializeField]
        protected WeaponData weaponData;

        public float Damage => weaponData.Damage;

        public WeaponData WeaponData => weaponData;

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
