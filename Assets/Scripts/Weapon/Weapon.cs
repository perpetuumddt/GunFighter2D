using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponData _weaponData;
    public float Damage => _weaponData.Damage;

    public WeaponData WeaponData => _weaponData;
}
