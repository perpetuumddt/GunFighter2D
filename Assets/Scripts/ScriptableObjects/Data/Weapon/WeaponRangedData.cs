using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponRangedData", menuName = "Data/Weapon Data/New Weapon Ranged Data")]
public class WeaponRangedData : WeaponData
{
    [SerializeField]
    private float _reloadTime;

    [SerializeField]
    private int _ammoCountInClip;

    [SerializeField]
    private int _ammoMaxCount;
}
