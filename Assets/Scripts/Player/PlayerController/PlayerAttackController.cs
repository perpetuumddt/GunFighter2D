using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : CharacterAttackController
{

    [SerializeField]
    protected WeaponManager _weaponManager;

    public override void Initialize()
    {
        base.Initialize();
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
}
