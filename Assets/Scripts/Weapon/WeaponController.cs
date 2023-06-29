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

    public event Action<int> OnWeaponSetup;

    public event Action OnReload;

    public void SetupWeapon(Weapon weapon, WeaponData weaponData)
    {
        if (CurrentWeapon is WeaponRanged)
        {
            ((WeaponRanged)CurrentWeapon).OnAmmoLeftChanged -= InvokeOnAmmoLeftChanged;
            ((WeaponRanged)CurrentWeapon).OnWeaponSetup -= InvokeOnWeaponSetup;
            ((WeaponRanged)CurrentWeapon).OnReloadPerforming -= InvokeOnReload;
        }
        _weapon = weapon;
        _weaponData = weaponData;
        _weapon.Initialize(_spriteRenderer);
        if (CurrentWeapon is WeaponRanged)
        {
            ((WeaponRanged)CurrentWeapon).OnWeaponSetup += InvokeOnWeaponSetup;
            ((WeaponRanged)CurrentWeapon).OnAmmoLeftChanged += InvokeOnAmmoLeftChanged;
            InvokeOnWeaponSetup(((WeaponRanged)CurrentWeapon).ClipSize);
            InvokeOnAmmoLeftChanged(((WeaponRanged)CurrentWeapon).AmmoLeftInClip);
        }
    }

    private void InvokeOnReload(bool value)
    {
        OnReload?.Invoke();
    }

    public void InvokeOnAmmoLeftChanged(int value)
    {
        OnAmmoLeftChanged?.Invoke(value);
    }

    public void InvokeOnWeaponSetup(int value)
    {
        OnWeaponSetup?.Invoke(value);
    }
}
