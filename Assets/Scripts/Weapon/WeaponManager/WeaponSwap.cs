using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapController : MonoBehaviour
{
    [SerializeField]
    private WeaponManager _weaponManager;

    public Weapon _currentWeapon;
    public WeaponData _currentWeaponData;
    public Weapon _spareWeapon;
    public WeaponData _spareWeaponData;

    public virtual void SwapWeapon()
    {
        Weapon buffWeapon = _currentWeapon;
        WeaponData buffData = _currentWeaponData;

        _currentWeapon = _spareWeapon;
        _currentWeaponData = _spareWeaponData;

        //_weaponManager.SetupWeapon(_currentWeapon,_currentWeaponData);

        _spareWeapon = buffWeapon;
        _spareWeaponData = buffData;
    }
}
