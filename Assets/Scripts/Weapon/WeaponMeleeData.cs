using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponMeleeData", menuName = "Data/WeaponMeleeData")]
public class WeaponMeleeData : WeaponData
{
    [SerializeField]
    private float _range;
}
