using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{

    [SerializeField]
    protected WeaponManager _weaponManager;

    [SerializeField]
    protected WeaponData _currentWeaponData;

    private void Awake()
    {
        Initialize();
        }
    public virtual void Initialize()
    {
        _weaponManager.SetupWeapon(_currentWeaponData);
    }

    public virtual void DoAttack(AttackType attackType)
    {
        _weaponManager.CurrentWeapon.DoAttack(attackType);
    }

    public virtual void Reload()
    {

    }

    public virtual void ChangeWeapon()
    {
        
    }
}
