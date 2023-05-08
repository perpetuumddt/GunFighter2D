using Assets.Scripts.Enemy.EnemyController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 3;

    public float GetMaxHealth()
    {
        return _maxHealth;
    }
}
