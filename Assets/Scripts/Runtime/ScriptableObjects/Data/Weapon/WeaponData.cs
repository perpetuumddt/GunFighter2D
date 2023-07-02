using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.ScriptableObjects.Data.Weapon
{
    public class WeaponData : ScriptableObject
    {
        [SerializeField]
        private string name;
        public string Name => name;

        [SerializeField]
        private int damage;
        public int Damage => damage;

        [SerializeField]
        private float attackSpeed;
        public float AttackSpeed => attackSpeed;

        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite => sprite;
    }
}
