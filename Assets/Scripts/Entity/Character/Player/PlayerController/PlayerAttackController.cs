using System;
using Gunfighter.Entity.Character.Controller;
using Gunfighter.Entity.Weapon;
using Gunfighter.Entity.Weapon.RangedWeapons;
using Gunfighter.Entity.Weapon.WeaponManager;
using Gunfighter.ScriptableObjects;
using Gunfighter.ScriptableObjects.Data.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Character.Player.PlayerController
{
    public class PlayerAttackController : CharacterAttackController
    {
        [FormerlySerializedAs("_reloadUIAnimator")] [SerializeField]
        private Animator reloadUIAnimator;

        [FormerlySerializedAs("_weaponManager")] [SerializeField]
        protected WeaponManager weaponManager;

        [FormerlySerializedAs("_weaponController")] [SerializeField]
        protected WeaponController weaponController;

        [FormerlySerializedAs("_weaponEvent")] [SerializeField]
        private SpriptableObjectWeaponEvent weaponEvent;

        [FormerlySerializedAs("_inputHandler")] [SerializeField]
        private CharacterInputHandler inputHandler;

        private WeaponWorldViewController _weaponWorldViewController;

        public event Action<WeaponData> OnWeaponChanged;
        public event Action OnAttack;
    
        private void OnEnable()
        {
            weaponEvent.OnSetActivePickupWeapon += SetActiveChangeWeapon;
            weaponController.OnReload += PlayReloadUIAnimation;
        }

        private void OnDisable()
        {
            weaponEvent.OnSetActivePickupWeapon -= SetActiveChangeWeapon;
            weaponController.OnReload -= PlayReloadUIAnimation;
        }

        public override void DoAttack(AttackType attackType)
        {
            base.DoAttack(attackType);
            weaponController.CurrentWeapon.DoAttack(attackType);
            InvokeOnAttack();
        }

        public override void Reload()
        {
            base.Reload();
            if(weaponController.CurrentWeapon is WeaponRanged)
                ((WeaponRanged)weaponController.CurrentWeapon).HandleReload(manual:true);
        }


        public override void ChangeWeapon()
        {
            base.ChangeWeapon();
            weaponManager.ChangeWeapon(_weaponWorldViewController.GetWeapon(),_weaponWorldViewController.GetWeaponData());
        }

        public override void SwapWeapon()
        {
            base.SwapWeapon();
            weaponManager.SwapWeapon();
            InvokeOnWeaponChanged(weaponManager.CurrentWeapon.WeaponData);
        }

        public void SetActiveChangeWeapon(bool isActive, WeaponWorldViewController weaponWorldViewController)
        {
            if(isActive)
            {
                _weaponWorldViewController = weaponWorldViewController;
                inputHandler.OnInteract += ChangeWeapon;
            }
            else
            {
                inputHandler.OnInteract -= ChangeWeapon;
                _weaponWorldViewController = null;
            }
        }

        public void InvokeOnWeaponChanged(WeaponData weaponData)
        {
            OnWeaponChanged?.Invoke(weaponData);
        }

        public void InvokeOnAttack()
        {
            OnAttack?.Invoke();
        }

        private void PlayReloadUIAnimation(bool value)
        {
            if (value)
            {
                reloadUIAnimator.Play("ReloadUIAnimation");
            }
            else
            {
                reloadUIAnimator.Play("None");
            }
        
        }    
    }
}
