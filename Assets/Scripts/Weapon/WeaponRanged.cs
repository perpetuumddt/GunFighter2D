using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRanged : Weapon
{
    [SerializeField]
    private GameObject _bulletPrefab;

    protected Coroutine _shootCoroutine;
    protected Coroutine _reloadCoroutine;
}
