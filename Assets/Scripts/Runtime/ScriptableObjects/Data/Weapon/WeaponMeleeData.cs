using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.ScriptableObjects.Data.Weapon
{
    [CreateAssetMenu(fileName = "WeaponMeleeData", menuName = "Data/Weapon Data/New Weapon Melee Data")]
    public class WeaponMeleeData : WeaponData
    {
        [SerializeField]
        private float range;
    }
}
