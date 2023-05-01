using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotationController : MonoBehaviour
{
    private Camera mainCam;
    private bool _lookLeft,_rollLeft;
    private PlayerInputHandler _playerInputHandler;
    private void CheckLookingDirection()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 rotation = mousePos - transform.position;

        if(_lookLeft && rotation.x > 0.1f)
        {
            Flip();
        }
        if(!_lookLeft && rotation.x < -0.1f)
        {
            Flip();
        }
    }
    private Vector2 CheckMovementDirection()
    {
        return _playerInputHandler.movementInputVector;
    }

    public void CheckRollingDirection()
    {
        if(!_rollLeft && CheckMovementDirection().x > 0.1f)
        {
            Flip();
        }
        if (_rollLeft && CheckMovementDirection().x < -0.1f)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _lookLeft = !_lookLeft;
        _rollLeft = !_rollLeft;
        transform.Rotate(0, 180, 0);
    }
}
