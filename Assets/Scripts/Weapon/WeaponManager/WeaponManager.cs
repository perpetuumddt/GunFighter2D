using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Controller")]
    [SerializeField]
    private WeaponController _weaponController;

    [Header("Current Weapon")]
    [SerializeField]
    private Weapon _currentWeapon;
    [SerializeField]
    protected WeaponData _currentWeaponData;

    [Header("Spare Weapon")]
    [SerializeField]
    private Weapon _spareWeapon;
    [SerializeField]
    protected WeaponData _spareWeaponData;

    [Header("Weapons")]
    [SerializeField]
    private Weapon[] _weapons;

    public Weapon CurrentWeapon => _currentWeapon;
    public Weapon SpareWeapon => _spareWeapon;

    private void Awake()
    {
        _weaponController.SetupWeapon(_currentWeapon,_currentWeaponData);
    }

    /*
    private void SetupWeapon(Weapon weapon, WeaponData weaponData)
    {
        foreach (var curWeapon in _weapons) 
        {
            if(curWeapon.WeaponData == weaponData)
            {
                weapon = curWeapon;
                curWeapon.gameObject.SetActive(true);
                curWeapon.Initialize(_spriteRenderer);
            }
            else
            {
                curWeapon.gameObject.SetActive(false);
            }
        }
    }
    */

    public virtual void SwapWeapon()
    {
        _currentWeapon.Finilize();
        Weapon buffWeapon = _currentWeapon;
        WeaponData buffData = _currentWeaponData;

        _currentWeapon = _spareWeapon;
        _currentWeaponData = _spareWeaponData;

        _weaponController.SetupWeapon(_currentWeapon, _currentWeaponData);

        _spareWeapon = buffWeapon;
        _spareWeaponData = buffData;    
    }

    public void ChangeWeapon(Weapon weapon,WeaponData weaponData)
    {
        _currentWeapon.Finilize();
        _currentWeapon = weapon;
        _currentWeaponData = weaponData;
        _weaponController.SetupWeapon(_currentWeapon, _currentWeaponData);
    }
}
