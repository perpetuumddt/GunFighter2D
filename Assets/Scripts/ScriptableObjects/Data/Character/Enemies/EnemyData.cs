using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemtyData", menuName = "Data/Character Data/New Enemy Data")]
public class EnemyData : CharacterData
{
    [SerializeField] private int _damage;
    public int Damage => _damage;
}
