using Gunfighter.Runtime.Entity.Character.Player.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gunfighter.Runtime.Entity.Character.Controller
{
    public class CharacterRotationController : MonoBehaviour
    {
        protected bool LookLeft, RollLeft;
        private Camera _mainCam;
        private PlayerInputHandler _playerInputHandler;

        private void Start()
        {
            _playerInputHandler = GetComponent<PlayerInputHandler>();
            _mainCam = FindObjectOfType<Camera>();
        }

        public void CheckLookingDirection()
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

        public Vector2 CheckMovementDirection()
        {
            return _playerInputHandler.MovementInputVector;
        }

        public void CheckRollingDirection()
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
        public void Flip()
        {
            LookLeft = !LookLeft;
            RollLeft = !RollLeft;
            transform.Rotate(0, 180, 0);
        }
    }
}
