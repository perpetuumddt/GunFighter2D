using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterAnimationController _characterAnimationController;
    public CharacterAnimationController CharacterAnimationController => _characterAnimationController;

    [SerializeField]
    private CharacterMovementController _characterMovementController;
    public CharacterMovementController CharacterMovementController => _characterMovementController;

    [SerializeField]
    private CharacterInputHandler _characterInputHandler;
    public CharacterInputHandler CharacterInputHandler => _characterInputHandler;

    [SerializeField]
    private CharacterAttackController _characterAttackController;
    public CharacterAttackController CharacterAttackController => _characterAttackController;

    [SerializeField]
    private CharacterRotationController _characterRotationController;
    public CharacterRotationController CharacterRotationController => _characterRotationController;

    [SerializeField]
    private CharacterHealthController _characterHealthController;
    public CharacterHealthController CharacterHealthController => _characterHealthController;

    [SerializeField]
    private CharacterDropController _characterDropController;
    public CharacterDropController CharacterDropController => _characterDropController;

    [SerializeField]
    private CharacterCollectorController _characterCollectorController;
    public CharacterCollectorController CharacterCollectorController => _characterCollectorController;


    [SerializeField] 
    private CharacterData _characterData;

    public CharacterData CharacterData => _characterData;
    
    protected StateMachine<CharacterController> _stateMachine;

    public event Action<CharacterData> OnDeath;

    public void InvokeOnDeath(CharacterData data)
    {
        OnDeath.Invoke(data);
    }
}
