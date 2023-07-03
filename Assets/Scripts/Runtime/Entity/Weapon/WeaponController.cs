using System;
using Gunfighter.Runtime.Entity.Weapon.RangedWeapons;
using Gunfighter.Runtime.General;
using Gunfighter.Runtime.ScriptableObjects.Data.Weapon;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private Weapon weapon;
        [SerializeField, ReadOnly]
        private WeaponData weaponData;
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public Weapon CurrentWeapon => weapon;
        public SpriteRenderer SpriteRenderer => spriteRenderer;

        public event Action<int> OnAmmoLeftChanged;

        public event Action<int> OnWeaponSetup;

        public event Action<bool> OnReload;

        public event Action OnShootingCooldownOver;

        public void SetupWeapon(Weapon weapon, WeaponData weaponData)
        {
            if (CurrentWeapon is WeaponRanged rangedPrev)
            {
                rangedPrev.OnAmmoLeftChanged -= InvokeOnAmmoLeftChanged;
                rangedPrev.OnWeaponSetup -= InvokeOnWeaponSetup;
                rangedPrev.OnReloadPerforming -= InvokeOnReload;
                rangedPrev.OnShootingCooldownOver -= InvokeOnShootingCooldownOver;
            }
            this.weapon = weapon;
            this.weaponData = weaponData;
            this.weapon.Initialize(spriteRenderer);
            if (CurrentWeapon is WeaponRanged rangedCurrent)
            {
                rangedCurrent.OnWeaponSetup += InvokeOnWeaponSetup;
                rangedCurrent.OnAmmoLeftChanged += InvokeOnAmmoLeftChanged;
                rangedCurrent.OnReloadPerforming += InvokeOnReload;
                rangedCurrent.OnShootingCooldownOver += InvokeOnShootingCooldownOver;
                InvokeOnWeaponSetup(((WeaponRanged)CurrentWeapon).ClipSize);
                InvokeOnAmmoLeftChanged(((WeaponRanged)CurrentWeapon).AmmoLeftInClip);
            }
        }

        private void InvokeOnShootingCooldownOver()
        {
            OnShootingCooldownOver?.Invoke();
        }

        private void InvokeOnReload(bool value)
        {
            OnReload?.Invoke(value);
        }

        public void InvokeOnAmmoLeftChanged(int value)
        {
            OnAmmoLeftChanged?.Invoke(value);
        }

        public void InvokeOnWeaponSetup(int value)
        {
            OnWeaponSetup?.Invoke(value);
        }
    }
}
