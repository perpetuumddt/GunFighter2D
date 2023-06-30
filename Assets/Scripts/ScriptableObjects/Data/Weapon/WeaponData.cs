using UnityEngine;

namespace ScriptableObjects.Data.Weapon
{
    public class WeaponData : ScriptableObject
    {
        [SerializeField]
        private string _name;
        public string Name => _name;

        [SerializeField]
        private int _damage;
        public int Damage => _damage;

        [SerializeField]
        private float _attackSpeed;
        public float AttackSpeed => _attackSpeed;

        [SerializeField]
        private Sprite _sprite;
        public Sprite Sprite => _sprite;
    }
}
