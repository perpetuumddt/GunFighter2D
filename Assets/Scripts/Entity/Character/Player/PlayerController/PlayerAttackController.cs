using System;
using Entity.Character.Controller;
using Entity.Weapon;
using Entity.Weapon.RangedWeapons;
using Entity.Weapon.WeaponManager;
using ScriptableObjects;
using ScriptableObjects.Data.Weapon;
using UnityEngine;

namespace Entity.Character.Player.PlayerController
{
    public class PlayerAttackController : CharacterAttackController
    {
        [SerializeField]
        private Animator _reloadUIAnimator;

        [SerializeField]
        protected WeaponManager _weaponManager;

        [SerializeField]
        protected WeaponController _weaponController;

        [SerializeField]
        private SpriptableObjectWeaponEvent _weaponEvent;

        [SerializeField]
        private CharacterInputHandler _inputHandler;

        private WeaponWorldViewController _weaponWorldViewController;

        public event Action<WeaponData> OnWeaponChanged;
        public event Action OnAttack;
    
        private void OnEnable()
        {
            _weaponEvent.OnSetActivePickupWeapon += SetActiveChangeWeapon;
            _weaponController.OnReload += PlayReloadUIAnimation;
        }

        private void OnDisable()
        {
            _weaponEvent.OnSetActivePickupWeapon -= SetActiveChangeWeapon;
            _weaponController.OnReload -= PlayReloadUIAnimation;
        }

        public override void DoAttack(AttackType attackType)
        {
            base.DoAttack(attackType);
            _weaponController.CurrentWeapon.DoAttack(attackType);
            InvokeOnAttack();
        }

        public override void Reload()
        {
            base.Reload();
            if(_weaponController.CurrentWeapon is WeaponRanged)
                ((WeaponRanged)_weaponController.CurrentWeapon).HandleReload(manual:true);
        }


        public override void ChangeWeapon()
        {
            base.ChangeWeapon();
            _weaponManager.ChangeWeapon(_weaponWorldViewController.GetWeapon(),_weaponWorldViewController.GetWeaponData());
        }

        public override void SwapWeapon()
        {
            base.SwapWeapon();
            _weaponManager.SwapWeapon();
            InvokeOnWeaponChanged(_weaponManager.CurrentWeapon.WeaponData);
        }

        public void SetActiveChangeWeapon(bool isActive, WeaponWorldViewController weaponWorldViewController)
        {
            if(isActive)
            {
                _weaponWorldViewController = weaponWorldViewController;
                _inputHandler.OnInteract += ChangeWeapon;
            }
            else
            {
                _inputHandler.OnInteract -= ChangeWeapon;
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
                _reloadUIAnimator.Play("ReloadUIAnimation");
            }
            else
            {
                _reloadUIAnimator.Play("None");
            }
        
        }    
    }
}
