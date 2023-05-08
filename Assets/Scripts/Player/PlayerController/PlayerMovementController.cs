using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : CharacterMovementController
{
    [SerializeField]
    private Rigidbody2D _rigidbody;
    private Vector2 _currentVelocity;

    public override void DoMove(params object[] param)
    {
        base.DoMove(param);
        SetVelocity((float)param[0], (float)param[1]);
    }
    private void SetVelocity(float velocityX, float velocityY)
    {
        _currentVelocity.Set(velocityX, velocityY);
        _rigidbody.velocity = _currentVelocity;
    }
}
