using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject _ammoUnitPrefab;

    [SerializeField]
    private GameObject _containerElement;

    [SerializeField]
    private WeaponController _weaponController;

    private AmmoController _ammoController;

    private void Awake()
    {
        _ammoController = new AmmoController(_ammoUnitPrefab, _containerElement);
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
        _ammoController.UpdateAmmoController(value);
    }

    private void SetupAmmo(int value)
    {
        _ammoController.SetupAmmoController(value, this.gameObject);
    }
}
