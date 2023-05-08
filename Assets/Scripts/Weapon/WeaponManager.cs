using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private Weapon[] _weapons;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private Weapon _currentWeapon;

    public Weapon CurrentWeapon => _currentWeapon;

    public virtual void SetupWeapon(WeaponData weaponData)
    {
        foreach (var weapon in _weapons) 
        {
            if(weapon.WeaponData == weaponData)
            {
                _currentWeapon = weapon;
                weapon.gameObject.SetActive(true);
                weapon.Initialize(_spriteRenderer);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }
}
