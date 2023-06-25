using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawedOffScript : WeaponRanged
{
    private IEnumerator Shoot()
    {
        _canShoot = false;
        
        for(int i=0;  i<((WeaponRangedData)_weaponData).AmmountOfBullets; i++)
        {
            int bulletAngleDeviation = ((WeaponRangedData)_weaponData).SpreadAngle / 2 - Random.Range(0, ((WeaponRangedData)_weaponData).SpreadAngle) ;
            CreateBullet(angleDeviation:bulletAngleDeviation);
        }
        _shootPS.Play();
        yield return new WaitForSeconds(_weaponData.AttackSpeed);
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
            _shootCoroutine = StartCoroutine(Shoot());
        }
    }
}
