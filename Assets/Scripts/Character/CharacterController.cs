using System.Collections;
using System.Collections.Generic;
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
}
