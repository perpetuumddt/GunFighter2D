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
}
