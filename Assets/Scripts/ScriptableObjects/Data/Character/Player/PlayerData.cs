using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Character Data/New Player Data")]
public class PlayerData : CharacterData
{
    [SerializeField]
    private float _rollSpeed;
    public float RollSpeed => _rollSpeed;

    [SerializeField]
    private float _rollCooldown;
    public float RollCooldown => _rollCooldown;
}
