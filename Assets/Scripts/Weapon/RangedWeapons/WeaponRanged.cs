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
    [SerializeField]
    protected int _clipSize;
    public int AmmoLeftInClip { get => _ammoLeftInClip; private set { _ammoLeftInClip = value; InvokeOnAmmoLeftChanged(value); } }
    public int ClipSize { get => _clipSize; private set { _clipSize = value; InvokeOnWeaponSetup(value); } }
    public bool ReloadPerforming { get => _reloading; private set { _reloading = value; InvokeOnReloadPerforming(value); } }

    public event Action<int> OnAmmoLeftChanged;
    public event Action<int> OnWeaponSetup;
    public event Action<bool> OnReloadPerforming;

    protected WeaponRangedData _weaponRangedData;
    

    private void Awake()
    {
        this._weaponRangedData = (WeaponRangedData)_weaponData;
        AmmoLeftInClip = _weaponRangedData.AmmoCountInClip;
        ClipSize = _weaponRangedData.AmmoCountInClip;
    }
    public void Start()
    {
        _bulletPool = GameObject.FindGameObjectWithTag("BulletPool").transform;
        _shotPoint.rotation = Quaternion.Euler(0, 0, 90);
        this.pool = new PoolMono<BulletScript>(this.bulletPrefab, this.poolCount, this._bulletPool);
        this.pool.autoExpand = this.autoExpand;
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
        ReloadPerforming = false;
        //_reloading = false;
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

        AmmoLeftInClip--;
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
        //_reloading = true;
        ReloadPerforming = true;
        yield return new WaitForSeconds(_weaponRangedData.ReloadTime);
        AmmoLeftInClip = _weaponRangedData.AmmoCountInClip;
        //_reloading = false;
        ReloadPerforming = false;
        
    }
    public virtual void HandleReload(bool manual = false)
    {
        if ((AmmoLeftInClip == 0 || manual) && !ReloadPerforming)
        {
            _reloadCoroutine = StartCoroutine(Reload());
        }
    }

    public void InvokeOnAmmoLeftChanged(int ammoLeft)
    {
        OnAmmoLeftChanged?.Invoke(ammoLeft);
    }

    public void InvokeOnWeaponSetup(int value)
    {
        OnWeaponSetup?.Invoke(value);
    }

    public void InvokeOnReloadPerforming(bool value)
    {
        OnReloadPerforming?.Invoke(value);
    }
}
