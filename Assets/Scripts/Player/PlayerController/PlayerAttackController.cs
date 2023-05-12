using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : CharacterAttackController
{

    [SerializeField]
    protected WeaponManager _weaponManager;

    [SerializeField]
    private SpriptableObjectWeaponEvent _weaponEvent;

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
            //подписать на ивент о подборе оружия. если подбор успешен, забирать у weaponworldviewcontr дату и пробрасывать в метод SetActiveCurrentWeapon
            //_weaponManager.SetActiveCurrentWeapon
        }
        else
        {
            //отписка от ивента о подборе оружия ChangeWeapon
        }
    }
}
