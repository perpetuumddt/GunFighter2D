using UnityEngine;

namespace ScriptableObjects.Data.Weapon
{
    [CreateAssetMenu(fileName = "WeaponMeleeData", menuName = "Data/Weapon Data/New Weapon Melee Data")]
    public class WeaponMeleeData : WeaponData
    {
        [SerializeField]
        private float _range;
    }
}
