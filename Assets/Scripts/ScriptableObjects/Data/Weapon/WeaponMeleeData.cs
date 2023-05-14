using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponMeleeData", menuName = "Data/Weapon Data/New Weapon Melee Data")]
public class WeaponMeleeData : WeaponData
{
    [SerializeField]
    private float _range;
}
