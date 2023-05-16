using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : CharacterAttackController
{

    [SerializeField]
    protected WeaponManager _weaponManager;

    [SerializeField]
    private SpriptableObjectWeaponEvent _weaponEvent;

    [SerializeField]
    private CharacterInputHandler _inputHandler;

    private WeaponWorldViewController _weaponWorldViewController;

    public override void Initialize()
    {
        base.Initialize();
    }
    private void OnEnable()
    {
        _weaponEvent.OnSetActivePickupWeapon += SetActiveChangeWeapon;
    }

    private void OnDisable()
    {
        _weaponEvent.OnSetActivePickupWeapon -= SetActiveChangeWeapon;
    }

    public override void DoAttack(AttackType attackType)
    {
        base.DoAttack(attackType);
        _weaponManager.CurrentWeapon.DoAttack(attackType);
    }

    public override void Reload()
    {
        base.Reload();
    }

    public override void ChangeWeapon()
    {
        base.ChangeWeapon();
        
    }

    public override void SwapWeapon()
    {
        base.SwapWeapon();
        _weaponManager.SwapWeapon();
    }

    public void SetActiveChangeWeapon(bool isActive, WeaponWorldViewController weaponWorldViewController)
    {
        if(isActive)
        {
            _weaponWorldViewController = weaponWorldViewController;
            _inputHandler.OnInteract += PickUpWeapon;
        }
        else
        {
            _inputHandler.OnInteract -= PickUpWeapon;
            _weaponWorldViewController = null;
        }
    }
    private void PickUpWeapon()
    {
        _weaponManager.SetupCurrentWeapon(_weaponWorldViewController.WeaponData);
    }
}
