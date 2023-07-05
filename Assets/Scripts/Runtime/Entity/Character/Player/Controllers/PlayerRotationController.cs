using Gunfighter.Runtime.Entity.Character.Controllers;
using Gunfighter.Runtime.Entity.Character.Player.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gunfighter.Runtime.Entity.Character.Player.Controllers
{
    public class PlayerRotationController : CharacterRotationController
    {
        protected bool LookLeft, RollLeft;
        private Camera _mainCam;
        private PlayerInputHandler _playerInputHandler;
        
        private void Start()
        {
            _playerInputHandler = GetComponent<PlayerInputHandler>();
            _mainCam = FindObjectOfType<Camera>();
        }

        public override void CheckLookingDirection()
        {
            Vector3 mousePos = _mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 rotation = mousePos - transform.position;

            if (!LookLeft && rotation.x > 0.1f)
            {
                Flip();
            }
            if (LookLeft && rotation.x < -0.1f)
            {
                Flip();
            }
        }

        public override Vector2 CheckMovementDirection()
        {
            return _playerInputHandler.MovementInputVector;
        }
        
        public override void CheckRollingDirection()
        {
            if (!RollLeft && CheckMovementDirection().x > 0.1f)
            {
                Flip();
            }
            if (RollLeft && CheckMovementDirection().x < -0.1f)
            {
                Flip();
            }
        }

        protected override void Flip()
        {
            LookLeft = !LookLeft;
            RollLeft = !RollLeft;
            transform.Rotate(0, 180, 0);
        }
    }
}
