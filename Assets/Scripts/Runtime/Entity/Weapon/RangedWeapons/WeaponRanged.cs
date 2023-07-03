using System;
using System.Collections;
using Gunfighter.Runtime.Entity.Projectile;
using Gunfighter.Runtime.General.Objects_Pool;
using Gunfighter.Runtime.ScriptableObjects.Data.Weapon;
using UnityEngine;
using UnityEngine.Serialization;
using Action = Unity.Plastic.Antlr3.Runtime.Misc.Action;
using Random = UnityEngine.Random;

namespace Gunfighter.Runtime.Entity.Weapon.RangedWeapons
{
    public class WeaponRanged : Weapon
    {
        protected Coroutine ShootCoroutine;
        protected Coroutine ReloadCoroutine;

        [SerializeField]
        private int poolCount = 0;
        [SerializeField]
        private bool autoExpand = true;
        [SerializeField]
        private BulletScript bulletPrefab;
        [FormerlySerializedAs("_shotPoint")] [SerializeField]
        private Transform shotPoint;
        [FormerlySerializedAs("_shootPS")] [SerializeField]
        protected ParticleSystem shootPS;

        private PoolMono<BulletScript> _pool;

        private Transform _bulletPool;

        protected bool CanShoot => !Reloading && !IsOnShootingCooldown && ammoLeftInClip > 0;
        protected bool Reloading;
        protected bool IsOnShootingCooldown;

        [FormerlySerializedAs("_ammoLeftInClip")] [SerializeField]
        protected int ammoLeftInClip;
        [FormerlySerializedAs("_clipSize")] [SerializeField]
        protected int clipSize;

        public int AmmoLeftInClip { get => ammoLeftInClip; private set { ammoLeftInClip = value; InvokeOnAmmoLeftChanged(value); } }
        public int ClipSize { get => clipSize; private set { clipSize = value; InvokeOnWeaponSetup(value); } }
        public bool ReloadPerforming { get => Reloading; private set { Reloading = value; InvokeOnReloadPerforming(value); } }
        
        
        public event Action<int> OnAmmoLeftChanged;
        public event Action<int> OnWeaponSetup;
        public event Action<bool> OnReloadPerforming;
        public event Action OnShootingCooldownOver;

        public WeaponRangedData WeaponRangedData { get; protected set; }
    
        
        private void Awake()
        {
            this.WeaponRangedData = (WeaponRangedData)weaponData;
            AmmoLeftInClip = WeaponRangedData.AmmoCountInClip;
            ClipSize = WeaponRangedData.AmmoCountInClip;
        }
        public void Start()
        {
            _bulletPool = GameObject.FindGameObjectWithTag("BulletPool").transform;
            shotPoint.rotation = Quaternion.Euler(0, 0, 90);
            this._pool = new PoolMono<BulletScript>(this.bulletPrefab, this.poolCount, this._bulletPool);
            this._pool.AutoExpand = this.autoExpand;
        }

        public override void Initialize(SpriteRenderer spriteRenderer)
        {
            base.Initialize(spriteRenderer);
        }

        public override void Finilize()
        {
            base.Finilize();
        
            if (ReloadCoroutine != null)
                StopCoroutine(ReloadCoroutine);
            ReloadPerforming = false;
        }

        public virtual void CreateBullet(int angleDeviation = 0)
        {
            var bullet = this._pool.GetFreeElement();
            bullet.transform.position = shotPoint.position;
            bullet.transform.rotation = shotPoint.rotation * Quaternion.Euler(Vector3.forward * angleDeviation);
            bullet.SetBulletDamage(WeaponData.Damage);
        }
    
        private IEnumerator Shoot()
        {
            IsOnShootingCooldown = true;
            shootPS.Play();
            for(int i=0;  i<WeaponRangedData.BulletsInOneShot; i++)
            {
                int bulletAngleDeviation = WeaponRangedData.SpreadAngle / 2 - Random.Range(0, ((WeaponRangedData)weaponData).SpreadAngle) ;
                CreateBullet(angleDeviation:bulletAngleDeviation);
            }

            AmmoLeftInClip--;
            yield return new WaitForSeconds(WeaponRangedData.AttackSpeed);
            ShootingCooldownOver();
        }

        private void ShootingCooldownOver()
        {
            IsOnShootingCooldown = false;
            OnShootingCooldownOver?.Invoke();
        }

        public override void DoAttack(AttackType attackType)
        {
            HandleReload();
            if (CanShoot)
            {
                base.DoAttack(attackType);
                if (ShootCoroutine != null)
                {
                    StopCoroutine(ShootCoroutine);
                }
                ShootCoroutine = StartCoroutine(Shoot());
            }
        }

        private IEnumerator Reload()
        {
            ReloadPerforming = true;
            yield return new WaitForSeconds(WeaponRangedData.ReloadTime);
            AmmoLeftInClip = WeaponRangedData.AmmoCountInClip;
            ReloadPerforming = false;
        
        }
        public virtual void HandleReload(bool manual = false)
        {
            if ((AmmoLeftInClip == 0 || manual) && !ReloadPerforming)
            {
                ReloadCoroutine = StartCoroutine(Reload());
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
}
