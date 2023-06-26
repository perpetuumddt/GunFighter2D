using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : CharacterHealthController, IDamageable
{
    private int _currentHealth;
    private float invincibilityDurationSeconds = 1.5f;
    private float invincibilityDeltaTime = 0.15f;
    private bool isInvincible;
    
    [SerializeField]
    public PlayerData _playerData;

    private void Start()
    {
        _currentHealth = (int)_playerData.Health;
        UpdateHealth(_currentHealth);
        
    }


    public override void UpdateHealth(int _currentHealth)
    {
        base.UpdateHealth(this._currentHealth);

        if(_currentHealth <= 0)
        {
            InvokeOnHealthZero();
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0) throw new ArgumentOutOfRangeException();
        if (!isInvincible)
        {
            _currentHealth -= damage;
            StartCoroutine(BecomeTemporarilyInvincible());
            UpdateHealth(_currentHealth);
        }
    }

    public void ReplenishHealth(int health)
    {
        if (health < 0) throw new ArgumentOutOfRangeException();
        
        _currentHealth += health;
        UpdateHealth(_currentHealth);
    }
    
    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;
        for (float i = 0; i < invincibilityDurationSeconds; i += invincibilityDeltaTime)
        {
            // 1.5/0.15 = 10 invulnerability frames 
            if (this.transform.localScale == Vector3.one)
            {
                ScaleModelTo(Vector3.zero);
            }
            else
            {
                ScaleModelTo(Vector3.one);
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }
        ScaleModelTo(Vector3.one);
        isInvincible = false;
    }

    private void ScaleModelTo(Vector3 scale)
    {
        transform.localScale = scale;
    }
}
 