using Gunfighter.Entity.Character.Controller;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Character.Player.PlayerController
{
    public class PlayerMovementController : CharacterMovementController
    {
        [FormerlySerializedAs("_rigidbody")] [SerializeField]
        private Rigidbody2D rigidbody;
        private Vector2 _currentVelocity;

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
    }
}
