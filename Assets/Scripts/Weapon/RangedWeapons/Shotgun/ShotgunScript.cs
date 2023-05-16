using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : WeaponRanged
{

    private IEnumerator Shoot(WeaponData weaponData)
    {
        _canShoot = false;
        _shootPS.Play();
        for (int i = 0; i < 4; i++)
        {
            CreateBullet();
        }
        yield return new WaitForSeconds(weaponData.AttackSpeed);
        _canShoot = true;
    }

    public override void DoAttack(AttackType attackType)
    {
        if (_canShoot)
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
