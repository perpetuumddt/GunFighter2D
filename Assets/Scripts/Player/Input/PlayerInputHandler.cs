using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : CharacterInputHandler
{
    [SerializeField]
    private PlayerInput m_PlayerInput;

    private PlayerInputAction playerInputAction;
    

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        
    }

    private void OnEnable()
    {
        playerInputAction.Enable();
    }

    private void OnDisable()
    {
        playerInputAction.Disable();
    }

    private void Start()
    {
        
    }

    protected override void Update()
    {
        MovementInput = playerInputAction.Gameplay.Movement.ReadValue<Vector2>();
        base.Update();
        //Debug.Log(move);
    }
}
