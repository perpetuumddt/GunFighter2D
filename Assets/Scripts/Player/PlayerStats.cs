using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 6;

    public int GetMaxHealth()
    {
        return _maxHealth;
    }
}
