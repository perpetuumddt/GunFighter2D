using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private Weapon[] _weapons;

    private Weapon _currentWeapon;

    public Weapon CurrentWeapon => _currentWeapon;

    public virtual void SetupWeapon(WeaponData weaponData)
    {
        foreach (var weapon in _weapons) 
        {
            if(weapon.WeaponData == weaponData)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }
}
