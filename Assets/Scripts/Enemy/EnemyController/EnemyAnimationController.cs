using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : CharacterAnimationController
{
    [SerializeField]
    private Animator _animator;

    public override void SetActiveBoolAnim(string parameter, bool isActive)
    {
        base.SetActiveBoolAnim(parameter, isActive);
        _animator.SetBool(parameter, isActive);
    }
}
