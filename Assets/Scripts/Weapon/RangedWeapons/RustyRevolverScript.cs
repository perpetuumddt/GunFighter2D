using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RustyRevolverScript : Weapon
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private Transform _shotPoint;

    private Coroutine _shotCoroutine;

    private void Start()
    {
        WeaponData weaponData = new WeaponData();
        _shotPoint.rotation = Quaternion.Euler(0,0,90);
    }

    private IEnumerator Shoot(WeaponData weaponData)
    {
        GameObject bulletClone = Instantiate(_bulletPrefab, _shotPoint.position, _shotPoint.rotation);
        yield return new WaitForSeconds(weaponData.AttackSpeed);
    }

    public override void DoAttack(AttackType attackType)
    {
        base.DoAttack(attackType);
        if(_shotCoroutine != null) 
        {
            StopCoroutine(_shotCoroutine);
        }
        _shotCoroutine = StartCoroutine(Shoot(WeaponData));
    }
}
