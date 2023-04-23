using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : CharacterInputHandler
{
    [SerializeField]
    private PlayerInput m_PlayerInput;

    private void OnEnable()
    {
        //m_PlayerInput.actionEvents.
    }

    private void OnDisable()
    {
        
    }
}
