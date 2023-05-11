using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawedOffScript : Weapon
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private Transform _shotPoint;

    [SerializeField]
    ParticleSystem _shootPs;

    private Coroutine _shootCoroutine;

    private Coroutine _reloadCoroutine;

    private bool _canShoot;

    private void Start()
    {
        _shotPoint.rotation = Quaternion.Euler(0, 0, 90);
    }

    private void OnEnable()
    {
        _canShoot = true;
    }

    private IEnumerator Shoot(WeaponData weaponData)
    {
        _canShoot = false;
        for(int i=0;  i<5; i++)
        {
            var _spreadOffset = Random.Range(-10f,10f);
            var _spread = Quaternion.Euler(0,0,_shotPoint.rotation.z+_spreadOffset);
            Debug.Log(_shotPoint.rotation);
            GameObject bulletClone = Instantiate(_bulletPrefab, _shotPoint.position, _shotPoint.rotation);
        }
        _shootPs.Play();
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
