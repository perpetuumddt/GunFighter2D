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

    public void SetupWeapon(Weapon weapon, WeaponData weaponData)
    {
        _weapon = weapon;
        _weaponData = weaponData;
        //Instantiate(_weapon);
        _weapon.Initialize(_spriteRenderer);
    }
}
