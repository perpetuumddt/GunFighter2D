using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private Weapon[] _weapons;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    protected WeaponData _currentWeaponData;
    [SerializeField]
    protected WeaponData _spareWeaponData;

    [SerializeField]
    private Weapon _currentWeapon;

    [SerializeField]
    private Weapon _spareWeapon;

    public Weapon CurrentWeapon => _currentWeapon;
    public Weapon SpareWeapon => _spareWeapon;

    private void Awake()
    {
        SetupCurrentWeapon(_currentWeaponData);
        SetupSpareWeapon(_spareWeaponData);
    }

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
    public virtual void SetupCurrentWeapon(WeaponData weaponData)
    {
        SetupWeapon(_currentWeapon, weaponData);
    }

    public virtual void SetupSpareWeapon(WeaponData weaponData)
    {
        SetupWeapon(_spareWeapon, weaponData);
    }

    public virtual void SwapWeapon()
    {
        Weapon buffWeapon = _currentWeapon;
        _currentWeapon = _spareWeapon;
        _spareWeapon = buffWeapon;
    }
}
