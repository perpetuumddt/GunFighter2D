﻿using System;
using Gunfighter.Runtime.Entity.Character.Controller;
using Gunfighter.Runtime.Entity.Weapon;
using Gunfighter.Runtime.Entity.Weapon.RangedWeapons;
using Gunfighter.Runtime.Entity.Weapon.WeaponManager;
using Gunfighter.Runtime.ScriptableObjects;
using Gunfighter.Runtime.ScriptableObjects.Data.Weapon;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Player.PlayerController
{
    public class PlayerAttackController : CharacterAttackController
    {
        [SerializeField]
        private Animator reloadUIAnimator;

        [SerializeField]
        protected WeaponManager weaponManager;

        [SerializeField]
        protected WeaponController weaponController;

        [SerializeField]
        private SpriptableObjectWeaponEvent weaponEvent;
        
        private CharacterInputHandler _inputHandler;

        private WeaponWorldViewController _weaponWorldViewController;

        public event Action<WeaponData> OnWeaponChanged;
        public event Action OnAttack;

        protected override void Awake()
        {
            base.Awake();
            _inputHandler = GetComponent<CharacterInputHandler>();
        }

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

        public override void SetWeaponVisible(bool isVisible)
        {
            weaponController.SpriteRenderer.gameObject.SetActive(isVisible);
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
                reloadUIAnimator.Play("ReloadUIAnimation");
            }
            else
            {
                reloadUIAnimator.Play("None");
            }
        
        }    
    }
}