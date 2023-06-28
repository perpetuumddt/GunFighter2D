using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject _ammoUnitPrefab;

    [SerializeField]
    private PlayerAttackController _playerAttackController;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        _playerAttackController.OnAttack += UpdateAmmo;
        _playerAttackController.OnReload += UpdateAmmo;
        _playerAttackController.OnWeaponChanged += SetupAmmo;
    }

    private void OnDisable()
    {
        _playerAttackController.OnAttack -= UpdateAmmo;
        _playerAttackController.OnReload -= UpdateAmmo;
        _playerAttackController.OnWeaponChanged -= SetupAmmo;
    }

    private void UpdateAmmo()
    {
        
    }

    private void SetupAmmo(WeaponData weaponData)
    {

    }
}
