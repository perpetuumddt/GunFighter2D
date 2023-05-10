using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string _name;
    public string Name => _name;

    [SerializeField]
    private float _damage;
    public float Damage => _damage;

    [SerializeField]
    private float _attackSpeed;
    public float AttackSpeed => _attackSpeed;

    [SerializeField]
    private Sprite _sprite;
    public Sprite Sprite => _sprite;
}
