using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : CharacterHealthController, IDamageable
{
    private int _currentHealth;

    [SerializeField]
    private PlayerStats _playerStats;

    [SerializeField]
    private HealthBarController _healthBarController;

    private void Start()
    {
        _currentHealth = _playerStats.GetMaxHealth();
        UpdateHealth(_currentHealth);
    }


    public override void UpdateHealth(int _currentHealth)
    {
        _healthBarController.UpdateHealthBar(_currentHealth);

        if(_currentHealth <= 0)
        {
            InvokeOnHealthZero();
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= (int)damage;
        UpdateHealth(_currentHealth);    
    }
}
 