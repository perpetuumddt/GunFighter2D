using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.Entity.Weapon.WeaponManager
{
    public class WeaponSwap : MonoBehaviour
    {
        [FormerlySerializedAs("_currentWeapon")] public Weapon currentWeapon;
        [FormerlySerializedAs("_spareWeapon")] public Weapon spareWeapon;


    }
}
