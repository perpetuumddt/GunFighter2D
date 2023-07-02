using Gunfighter.Runtime.ScriptableObjects.Data.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.Entity.Weapon.WeaponManager
{
    public class WeaponManager : MonoBehaviour
    {
        [FormerlySerializedAs("_weaponController")]
        [Header("Weapon Controller")]
        [SerializeField]
        private WeaponController weaponController;

        [FormerlySerializedAs("_currentWeapon")]
        [Header("Current Weapon")]
        [SerializeField]
        private Weapon currentWeapon;
        [FormerlySerializedAs("_currentWeaponData")] [SerializeField]
        protected WeaponData currentWeaponData;

        [FormerlySerializedAs("_spareWeapon")]
        [Header("Spare Weapon")]
        [SerializeField]
        private Weapon spareWeapon;
        [FormerlySerializedAs("_spareWeaponData")] [SerializeField]
        protected WeaponData spareWeaponData;

        [FormerlySerializedAs("_weapons")]
        [Header("Weapons")]
        [SerializeField]
        private Weapon[] weapons;

        public Weapon CurrentWeapon => currentWeapon;
        public Weapon SpareWeapon => spareWeapon;

        private void Awake()
        {
            weaponController.SetupWeapon(currentWeapon,currentWeaponData);
        }

        public virtual void SwapWeapon()
        {
            currentWeapon.Finilize();
            Weapon buffWeapon = currentWeapon;
            WeaponData buffData = currentWeaponData;

            currentWeapon = spareWeapon;
            currentWeaponData = spareWeaponData;

            weaponController.SetupWeapon(currentWeapon, currentWeaponData);

            spareWeapon = buffWeapon;
            spareWeaponData = buffData;    
        }

        public void ChangeWeapon(Weapon weapon,WeaponData weaponData)
        {
            currentWeapon.Finilize();
            currentWeapon = weapon;
            currentWeaponData = weaponData;
            weaponController.SetupWeapon(currentWeapon, currentWeaponData);
        }
    }
}
