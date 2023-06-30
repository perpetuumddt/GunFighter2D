using Entity.Weapon;
using Entity.Weapon.RangedWeapons;
using UnityEngine;

namespace UI.Ammo
{
    public class AmmoDisplayUIController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _ammoUnitPrefab;

        [SerializeField]
        private GameObject _containerElement;

        [SerializeField]
        private WeaponController _weaponController;

        private AmmoDisplayController _ammoDisplayController;

        private void Awake()
        {
            _ammoDisplayController = new AmmoDisplayController(_ammoUnitPrefab, this.gameObject, _containerElement);
            SetupAmmo(((WeaponRanged)_weaponController.CurrentWeapon).AmmoLeftInClip);
        }

        private void OnEnable()
        {
            _weaponController.OnAmmoLeftChanged += UpdateAmmo;
            _weaponController.OnWeaponSetup += SetupAmmo;
        }

        private void OnDisable()
        {
            _weaponController.OnAmmoLeftChanged -= UpdateAmmo;
            _weaponController.OnWeaponSetup -= SetupAmmo;
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
