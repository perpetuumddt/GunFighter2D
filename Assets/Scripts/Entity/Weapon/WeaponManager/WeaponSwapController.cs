using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Weapon.WeaponManager
{
    public class WeaponSwap : MonoBehaviour
    {
        [FormerlySerializedAs("_currentWeapon")] public Weapon currentWeapon;
        [FormerlySerializedAs("_spareWeapon")] public Weapon spareWeapon;


    }
}
