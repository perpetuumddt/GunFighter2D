using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.ScriptableObjects.Data.Weapon
{
    public class WeaponData : ScriptableObject
    {
        [FormerlySerializedAs("_name")] [SerializeField]
        private string name;
        public string Name => name;

        [FormerlySerializedAs("_damage")] [SerializeField]
        private int damage;
        public int Damage => damage;

        [FormerlySerializedAs("_attackSpeed")] [SerializeField]
        private float attackSpeed;
        public float AttackSpeed => attackSpeed;

        [FormerlySerializedAs("_sprite")] [SerializeField]
        private Sprite sprite;
        public Sprite Sprite => sprite;
    }
}
