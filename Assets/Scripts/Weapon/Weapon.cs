using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected WeaponData _weaponData;

    public float Damage => _weaponData.Damage;
    public WeaponData WeaponData => _weaponData;

    public virtual void DoAttack(AttackType attackType)
    {

    }
    
    public virtual void Initialize(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.sprite = WeaponData.Sprite;
    }
}
