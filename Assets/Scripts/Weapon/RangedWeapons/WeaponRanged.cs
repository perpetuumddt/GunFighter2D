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

    private Transform _bulletPool;

    protected bool CanShoot => !_reloading && !_isOnShootingCooldown && _ammoLeftInClip > 0;
    protected bool _reloading;
    protected bool _isOnShootingCooldown;
    [SerializeField]
    protected int _ammoLeftInClip;
    protected WeaponRangedData _weaponRangedData;

    public void Start()
    {
        _bulletPool = GameObject.FindGameObjectWithTag("BulletPool").transform;
        _shotPoint.rotation = Quaternion.Euler(0, 0, 90);
        print("start");
        this.pool = new PoolMono<BulletScript>(this.bulletPrefab, this.poolCount, this._bulletPool);
        this.pool.autoExpand = this.autoExpand;
        this._weaponRangedData = (WeaponRangedData)_weaponData;
        this._ammoLeftInClip = _weaponRangedData.AmmoCountInClip;
    }

    public override void Initialize(SpriteRenderer spriteRenderer)
    {
        base.Initialize(spriteRenderer);
    }

    public override void Finilize()
    {
        base.Finilize();
        
        if (_reloadCoroutine != null)
            StopCoroutine(_reloadCoroutine);
        _reloading = false;
    }

    public virtual void CreateBullet(int angleDeviation = 0)
    {
        var bullet = this.pool.GetFreeElement();
        bullet.transform.position = _shotPoint.position;
        bullet.transform.rotation = _shotPoint.rotation * Quaternion.Euler(Vector3.forward * angleDeviation);
        bullet.SetBulletDamage(WeaponData.Damage);
    }
    
    private IEnumerator Shoot()
    {
        _isOnShootingCooldown = true;
        _shootPS.Play();
        for(int i=0;  i<_weaponRangedData.BulletsInOneShot; i++)
        {
            int bulletAngleDeviation = _weaponRangedData.SpreadAngle / 2 - Random.Range(0, ((WeaponRangedData)_weaponData).SpreadAngle) ;
            CreateBullet(angleDeviation:bulletAngleDeviation);
        }

        _ammoLeftInClip--;
        yield return new WaitForSeconds(_weaponRangedData.AttackSpeed);
        _isOnShootingCooldown = false;
    }

    public override void DoAttack(AttackType attackType)
    {
        HandleReload();
        if (CanShoot)
        {
            base.DoAttack(attackType);
            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
            }
            _shootCoroutine = StartCoroutine(Shoot());
        }
    }

    private IEnumerator Reload()
    {
        print("Reload start");
        _reloading = true;
        yield return new WaitForSeconds(_weaponRangedData.ReloadTime);
        this._ammoLeftInClip = _weaponRangedData.AmmoCountInClip;
        _reloading = false;
        
        print("Reload end");
    }
    public virtual void HandleReload(bool manual = false)
    {
        if ((_ammoLeftInClip == 0 || manual) && !_reloading)
        {
            _reloadCoroutine = StartCoroutine(Reload());
        }
    }
    
    
}
