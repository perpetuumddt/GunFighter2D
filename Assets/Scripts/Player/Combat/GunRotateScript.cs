using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class GunRotateScript : MonoBehaviour
{
    private Camera _mainCam;
    private SpriteRenderer _sprite;

    [SerializeField]
    private Transform _shotPoint;

    [SerializeField]
    private Transform _weaponTransform;

    private bool flip = false;

    private void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 mousePos = _mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;
        Vector2 rotation = mousePos - transform.position; //Vector
        
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x)* Mathf.Rad2Deg; //Rotation in degrees
        
       RotationLogic(rotationZ);
       WeaponSpriteLogic(rotation, rotationZ);
       
    }
    
    //Rotating weapon depending on cursor pos
    private void RotationLogic(float rotationZ)
    {
        transform.rotation = Quaternion.Euler(0,0,rotationZ+180);
        
    }

    private void WeaponSpriteLogic(Vector2 rotation, float rotationZ)
    {
        
        //Flipping weapon sprite
        //_sprite.flipY = rotation.x > 0;
        
        //Flipping shotpoing transform
        if(rotation.x > 0)
        {
            ShotPointFlip();
        }    
        

        //Changing sprite sorting order 
        _sprite.sortingOrder = rotationZ >= 40 && rotationZ <= 130 ? -1 : 0;

    }

    private void ShotPointFlip()
    {
        transform.Rotate(180, 0, 0);
    }
}