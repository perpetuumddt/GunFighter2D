using System;
using System.Collections;
using Gunfighter.Entity.Character.Controller;
using Gunfighter.ScriptableObjects.Data.Character.Player;
using UnityEngine;
using UnityEngine.Serialization;
using CharacterController = Gunfighter.Entity.Character.Controller.CharacterController;

namespace Gunfighter.Entity.Character.Player.PlayerController
{
    public class PlayerHealthController : CharacterHealthController
    {
        private float _invincibilityDurationSeconds = 1.5f;
        private float _invincibilityDeltaTime = 0.15f;
        private bool _isInvincible;
        private bool _canBeInvincible = true;

        public bool CanBeInvincible
        {
            get => _canBeInvincible;
            set => _canBeInvincible = value;
        }


        private void Start()
        {
            ChangeMaxHealth(characterController.CharacterData.DefaultMaxHealth);
            CurrentHealth = MaxHealth;
        }


        public override void UpdateHealth(int currentHealth)
        {
            base.UpdateHealth(this.CurrentHealth);

            if(currentHealth <= 0)
            {
                InvokeOnHealthZero();
            }
        }

        public override void TakeDamage(int damage)
        {
            if (damage < 0) throw new ArgumentOutOfRangeException();
            if (!_isInvincible)
            {
                _currentHealth = CurrentHealth - damage;
                if (CanBeInvincible && _currentHealth != 0)
                {
                    StartCoroutine(BecomeTemporarilyInvincible());
                }
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
            _isInvincible = true;
            for (float i = 0; i < _invincibilityDurationSeconds; i += _invincibilityDeltaTime)
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
                yield return new WaitForSeconds(_invincibilityDeltaTime);
            }
            ScaleModelTo(Vector3.one);
            _isInvincible = false;
        }

        private void ScaleModelTo(Vector3 scale)
        {
            transform.localScale = scale;
        }
    }
}
 