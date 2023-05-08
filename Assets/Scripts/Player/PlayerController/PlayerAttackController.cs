using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : CharacterAttackController
{

    public override void DoAttack(AttackType attackType)
    {
        base.DoAttack(attackType);
    }

    public override void Reload()
    {
        base.Reload();
    }

    public override void ChangeWeapon()
    {
        //setup weapon
        base.ChangeWeapon();
    }
}
