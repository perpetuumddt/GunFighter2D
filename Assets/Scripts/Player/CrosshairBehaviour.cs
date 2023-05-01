using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class CrosshairBehaviour : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Camera _camera;

    private Vector2 _mousePos;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Cursor.visible = false;
    }

    private void Update()
    {
        _mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //_mousePos = Mouse.current.position.ReadValue();
        //Debug.Log(_mousePos);
        transform.position = _mousePos;
    }

    public void ChangeCrosshairVisibility()
    {
        if (_spriteRenderer.enabled == true) _spriteRenderer.enabled = false;
    }
}
