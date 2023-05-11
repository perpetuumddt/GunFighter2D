using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawedOffScript : Weapon
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private Transform _shotPoint;

    private Coroutine _shootCoroutine;

    private Coroutine _reloadCoroutine;

    private bool _canShoot = true;

    private void Start()
    {
        _shotPoint.rotation = Quaternion.Euler(0, 0, 90);
    }

    private IEnumerator Shoot(WeaponData weaponData)
    {
        _canShoot = false;
        for(int i=0;  i<3; i++)
        {
            GameObject bulletClone = Instantiate(_bulletPrefab, _shotPoint.position, _shotPoint.rotation);
        }
        yield return new WaitForSeconds(weaponData.AttackSpeed);
        _canShoot = true;
    }

    public override void DoAttack(AttackType attackType)
    {
        if(_canShoot) 
        {
            base.DoAttack(attackType);
            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
            }
            _shootCoroutine = StartCoroutine(Shoot(_weaponData));
        }
    }
}
