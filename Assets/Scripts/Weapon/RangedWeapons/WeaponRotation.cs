using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCam;
    [SerializeField]
    private Transform _shotPoint;
    [SerializeField]
    private SpriteRenderer _weaponSprite;

    private void Update()
    {
        Vector3 mousepos = _mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousepos.z = 0f;
        Vector2 rotation = mousepos - transform.position;
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x)*Mathf.Rad2Deg; //rotation in degrees

        RotationLogic(rotation,rotationZ);
    }

    private void RotationLogic(Vector2 rotation, float rotationZ)
    {
        _shotPoint.transform.rotation = Quaternion.Euler(0, 0, rotationZ + 270);
        _weaponSprite.transform.rotation = Quaternion.Euler(0, 0, rotationZ + 180);

        if (rotation.x < 0.1)
        {
            WeaponFlip();
        }
    }

    private void WeaponFlip()
    {
        _weaponSprite.transform.Rotate(180, 0, 0);
    }
}
