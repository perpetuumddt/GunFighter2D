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
    }

    private void OnDisable()
    {
        _weaponController.OnAmmoLeftChanged -= UpdateAmmo;
    }

    private void UpdateAmmo(int value)
    {
        
    }
}
