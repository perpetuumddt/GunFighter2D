using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject _ammoUnitPrefab;

    [SerializeField]
    private WeaponController _weaponController;


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
        
    }

    private void SetupAmmo(int value)
    {
        Debug.Log("Current weapon has " + value + " clip size");
    }
}
