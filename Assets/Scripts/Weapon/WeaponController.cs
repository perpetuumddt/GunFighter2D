using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Weapon _weapon;
    [SerializeField]
    private WeaponData _weaponData;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public Weapon CurrentWeapon => _weapon;

    public event Action<int> OnAmmoLeftChanged;

    public void SetupWeapon(Weapon weapon, WeaponData weaponData)
    {
        if (CurrentWeapon is WeaponRanged)
        {
            ((WeaponRanged)CurrentWeapon).OnAmmoLeftChanged -= InvokeOnAmmoLeftChanged;
        }
        _weapon = weapon;
        _weaponData = weaponData;
        _weapon.Initialize(_spriteRenderer);
        if (CurrentWeapon is WeaponRanged)
        {
            ((WeaponRanged)CurrentWeapon).OnAmmoLeftChanged += InvokeOnAmmoLeftChanged;
            InvokeOnAmmoLeftChanged(((WeaponRanged)CurrentWeapon).AmmoLeftInClip);
        }
    }

    public void InvokeOnAmmoLeftChanged(int value)
    {
        OnAmmoLeftChanged?.Invoke(value);
    }
}
