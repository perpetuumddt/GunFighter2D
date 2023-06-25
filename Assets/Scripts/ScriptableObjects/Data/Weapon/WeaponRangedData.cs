using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponRangedData", menuName = "Data/Weapon Data/New Weapon Ranged Data")]
public class WeaponRangedData : WeaponData
{
    [SerializeField]
    private float _reloadTime;
    public float ReloadTime => _reloadTime;
    
    [SerializeField]
    private int _ammoCountInClip;
    public int AmmoCountInClip => _ammoCountInClip;


    [SerializeField]
    private int _ammoMaxCount;
    public int AmmoMaxCount => _ammoMaxCount;


    [SerializeField]
    private int _spreadAngle;
    public int SpreadAngle => _spreadAngle;


    [SerializeField] 
    private int _ammountOfBullets;
    public int AmmountOfBullets => _ammountOfBullets;

}
