using Gunfighter.Runtime.ScriptableObjects.Data.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.Entity.Weapon.WeaponManager
{
    public class WeaponSwapController : MonoBehaviour
    {
        [FormerlySerializedAs("_weaponManager")] [SerializeField]
        private WeaponManager weaponManager;

        [FormerlySerializedAs("_currentWeapon")] public Weapon currentWeapon;
        [FormerlySerializedAs("_currentWeaponData")] public WeaponData currentWeaponData;
        [FormerlySerializedAs("_spareWeapon")] public Weapon spareWeapon;
        [FormerlySerializedAs("_spareWeaponData")] public WeaponData spareWeaponData;

        public virtual void SwapWeapon()
        {
            Weapon buffWeapon = currentWeapon;
            WeaponData buffData = currentWeaponData;

            currentWeapon = spareWeapon;
            currentWeaponData = spareWeaponData;

            //_weaponManager.SetupWeapon(_currentWeapon,_currentWeaponData);

            spareWeapon = buffWeapon;
            spareWeaponData = buffData;
        }
    }
}
