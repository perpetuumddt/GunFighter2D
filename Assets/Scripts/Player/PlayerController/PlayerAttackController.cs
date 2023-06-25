using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : CharacterAttackController
{

    [SerializeField]
    protected WeaponManager _weaponManager;

    [SerializeField]
    protected WeaponController _weaponController;

    [SerializeField]
    private SpriptableObjectWeaponEvent _weaponEvent;

    [SerializeField]
    private CharacterInputHandler _inputHandler;

    private WeaponWorldViewController _weaponWorldViewController;

    
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
        _weaponController.CurrentWeapon.DoAttack(attackType);
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
}
