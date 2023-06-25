using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponRanged : Weapon
{
    protected Coroutine _shootCoroutine;
    protected Coroutine _reloadCoroutine;

    [SerializeField]
    private int poolCount = 0;
    [SerializeField]
    private bool autoExpand = true;
    [SerializeField]
    private BulletScript bulletPrefab;
    [SerializeField]
    private Transform _shotPoint;
    [SerializeField]
    protected ParticleSystem _shootPS;

    private PoolMono<BulletScript> pool;

    protected bool _canShoot;

    private void OnEnable()
    {
        _canShoot = true;
    }

    public void Start()
    {
        _shotPoint.rotation = Quaternion.Euler(0, 0, 90);

        this.pool = new PoolMono<BulletScript>(this.bulletPrefab, this.poolCount, this.transform);
        this.pool.autoExpand = this.autoExpand;
    }
    
    public virtual void CreateBullet(int angleDeviation = 0)
    {
        var bullet = this.pool.GetFreeElement();
        bullet.transform.position = _shotPoint.position;
        bullet.transform.rotation = _shotPoint.rotation * Quaternion.Euler(Vector3.forward * angleDeviation);
        bullet.SetBulletDamage(WeaponData.Damage);
    }
    
    private IEnumerator Shoot(WeaponData weaponData)
    {
        _canShoot = false;
        _shootPS.Play();
        for(int i=0;  i<((WeaponRangedData)_weaponData).AmmountOfBullets; i++)
        {
            int bulletAngleDeviation = ((WeaponRangedData)_weaponData).SpreadAngle / 2 - Random.Range(0, ((WeaponRangedData)_weaponData).SpreadAngle) ;
            CreateBullet(angleDeviation:bulletAngleDeviation);
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
