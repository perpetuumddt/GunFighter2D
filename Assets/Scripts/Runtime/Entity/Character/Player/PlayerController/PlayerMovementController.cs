using System.Collections;
using System.Collections.Generic;
using Gunfighter.Runtime.Entity.Character.Controller;
using Gunfighter.Runtime.General;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.Entity.Character.Player.PlayerController
{
    public class PlayerMovementController : CharacterMovementController
    {
        [SerializeField]
        private Rigidbody2D rigidbody;
        private Vector2 _currentVelocity;
        
        [SerializeField, ReadOnly]
        private bool canRoll;
        public bool CanRoll => canRoll;

        protected override void Awake()
        {
            base.Awake();
            canRoll = true;
        }

        public override void DoMove(params object[] param)
        {
            base.DoMove(param);
            SetVelocity((float)param[0], (float)param[1]);
        }
        private void SetVelocity(float velocityX, float velocityY)
        {
            _currentVelocity.Set(velocityX, velocityY);
            rigidbody.velocity = _currentVelocity;
        }

        public void StopMovement()
        {
            _currentVelocity.Set(0,0);
            rigidbody.velocity = _currentVelocity;
        }

        public void StartCooldownTimer(float seconds)
        {
            StartCoroutine(CooldownTimer(seconds));
        }
        
        private IEnumerator CooldownTimer(float seconds)
        {
            canRoll = false;
            yield return new WaitForSeconds(seconds);
            canRoll = true;
        }
    }
}
