using Gunfighter.Entity.Weapon;
using Gunfighter.Entity.Weapon.RangedWeapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.UI.Ammo
{
    public class AmmoDisplayUIController : MonoBehaviour
    {
        [FormerlySerializedAs("_ammoUnitPrefab")] [SerializeField]
        private GameObject ammoUnitPrefab;

        [FormerlySerializedAs("_containerElement")] [SerializeField]
        private GameObject containerElement;

        [FormerlySerializedAs("_weaponController")] [SerializeField]
        private WeaponController weaponController;

        private AmmoDisplayController _ammoDisplayController;

        private void Awake()
        {
            _ammoDisplayController = new AmmoDisplayController(ammoUnitPrefab, this.gameObject, containerElement);
            SetupAmmo(((WeaponRanged)weaponController.CurrentWeapon).AmmoLeftInClip);
        }

        private void OnEnable()
        {
            weaponController.OnAmmoLeftChanged += UpdateAmmo;
            weaponController.OnWeaponSetup += SetupAmmo;
        }

        private void OnDisable()
        {
            weaponController.OnAmmoLeftChanged -= UpdateAmmo;
            weaponController.OnWeaponSetup -= SetupAmmo;
        }

        private void UpdateAmmo(int value)
        {
            _ammoDisplayController.UpdateDisplay(value);
        }

        private void SetupAmmo(int value)
        {
            _ammoDisplayController.SetupDisplay(value);
        }
    }
}
