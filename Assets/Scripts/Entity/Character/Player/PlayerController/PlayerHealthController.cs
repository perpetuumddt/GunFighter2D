using System;
using System.Collections;
using Entity.Character.Controller;
using ScriptableObjects.Data.Character.Player;
using UnityEngine;

namespace Entity.Character.Player.PlayerController
{
    public class PlayerHealthController : CharacterHealthController
    {
        private float invincibilityDurationSeconds = 1.5f;
        private float invincibilityDeltaTime = 0.15f;
        private bool isInvincible;
    
        [SerializeField]
        public PlayerData _playerData;


        private void Awake()
        {
            ChangeMaxHealth(_playerData.DefaultMaxHealth);
            CurrentHealth = _playerData.DefaultMaxHealth;
        }


        public override void UpdateHealth(int _currentHealth)
        {
            base.UpdateHealth(this.CurrentHealth);

            if(_currentHealth <= 0)
            {
                InvokeOnHealthZero();
            }
        }

        public override void TakeDamage(int damage)
        {
            if (damage < 0) throw new ArgumentOutOfRangeException();
            if (!isInvincible)
            {
                _currentHealth = CurrentHealth - damage;
                StartCoroutine(BecomeTemporarilyInvincible());
                UpdateHealth(CurrentHealth);
            }
        }

        public override void ReplenishHealth(int health)
        {
            if (health < 0) throw new ArgumentOutOfRangeException();
        
            _currentHealth = CurrentHealth + health;
            UpdateHealth(CurrentHealth);
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
}
 