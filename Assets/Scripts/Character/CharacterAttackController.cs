using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{

    [SerializeField]
    protected WeaponManager _weaponManager;

    [SerializeField]
    protected WeaponData _currentWeaponData;

    public virtual void Initialize()
    {
        _weaponManager.SetupWeapon(_currentWeaponData);
    }

    public virtual void DoAttack()
    {

    }

    public virtual void Reload()
    {

    }

    public virtual void ChangeWeapon()
    {
        
    }
}
