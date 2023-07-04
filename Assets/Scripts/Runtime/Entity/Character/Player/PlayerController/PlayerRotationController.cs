using Gunfighter.Runtime.Entity.Character.Controller;
using Gunfighter.Runtime.Entity.Character.Player.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gunfighter.Runtime.Entity.Character.Player.PlayerController
{
    public class PlayerRotationController : CharacterRotationController
    {
        protected bool LookLeft, RollLeft;
        private Camera _mainCam;
        private PlayerInputHandler _playerInputHandler;

        private bool isFlipped = false;
        
        private SpriteRenderer _playerSprite;
        
        private void Start()
        {
            _playerInputHandler = GetComponent<PlayerInputHandler>();
            _playerSprite = GetComponent<SpriteRenderer>();
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
            isFlipped = !isFlipped;
            LookLeft = !LookLeft;
            RollLeft = !RollLeft;
            _playerSprite.flipX = isFlipped;
        }
    }
}
