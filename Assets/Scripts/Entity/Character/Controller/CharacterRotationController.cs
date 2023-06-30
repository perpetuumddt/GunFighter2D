using Entity.Character.Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Character.Controller
{
    public class CharacterRotationController : MonoBehaviour
    {
        protected bool _lookLeft, _rollLeft;
        private Camera mainCam;
        private PlayerInputHandler _playerInputHandler;

        private void Start()
        {
            _playerInputHandler = GetComponent<PlayerInputHandler>();
            mainCam = FindObjectOfType<Camera>();
        }

        public void CheckLookingDirection()
        {
            Vector3 mousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 rotation = mousePos - transform.position;

            if (!_lookLeft && rotation.x > 0.1f)
            {
                Flip();
            }
            if (_lookLeft && rotation.x < -0.1f)
            {
                Flip();
            }
        }

        public Vector2 CheckMovementDirection()
        {
            return _playerInputHandler.movementInputVector;
        }

        public void CheckRollingDirection()
        {
            if (!_rollLeft && CheckMovementDirection().x > 0.1f)
            {
                Flip();
            }
            if (_rollLeft && CheckMovementDirection().x < -0.1f)
            {
                Flip();
            }
        }
        public void Flip()
        {
            _lookLeft = !_lookLeft;
            _rollLeft = !_rollLeft;
            transform.Rotate(0, 180, 0);
        }
    }
}
