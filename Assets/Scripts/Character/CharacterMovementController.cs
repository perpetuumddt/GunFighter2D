using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody;
    private Vector2 _currentVelocity;

    public void SetVelocity(float velocityX, float velocityY)
    {
        _currentVelocity.Set(velocityX, velocityY);
        _rigidbody.velocity = _currentVelocity;

    }
}
