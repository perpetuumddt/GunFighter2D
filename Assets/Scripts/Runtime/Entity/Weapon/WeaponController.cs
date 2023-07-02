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

        public void SetupWeapon(Weapon weapon, WeaponData weaponData)
        {
            if (CurrentWeapon is WeaponRanged)
            {
                ((WeaponRanged)CurrentWeapon).OnAmmoLeftChanged -= InvokeOnAmmoLeftChanged;
                ((WeaponRanged)CurrentWeapon).OnWeaponSetup -= InvokeOnWeaponSetup;
                ((WeaponRanged)CurrentWeapon).OnReloadPerforming -= InvokeOnReload;
            }
            this.weapon = weapon;
            this.weaponData = weaponData;
            this.weapon.Initialize(spriteRenderer);
            if (CurrentWeapon is WeaponRanged)
            {
                ((WeaponRanged)CurrentWeapon).OnWeaponSetup += InvokeOnWeaponSetup;
                ((WeaponRanged)CurrentWeapon).OnAmmoLeftChanged += InvokeOnAmmoLeftChanged;
                ((WeaponRanged)CurrentWeapon).OnReloadPerforming += InvokeOnReload;
                InvokeOnWeaponSetup(((WeaponRanged)CurrentWeapon).ClipSize);
                InvokeOnAmmoLeftChanged(((WeaponRanged)CurrentWeapon).AmmoLeftInClip);
            }
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
