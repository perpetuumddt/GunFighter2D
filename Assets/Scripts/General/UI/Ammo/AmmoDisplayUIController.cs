using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _ammoDisplayController = new AmmoDisplayController(_ammoUnitPrefab, _containerElement);
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
        _ammoDisplayController.SetupDisplay(value, this.gameObject);
    }
}
