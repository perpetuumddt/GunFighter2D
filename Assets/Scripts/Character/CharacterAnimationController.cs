using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public void SetActiveBoolAnim(bool isActive,string parameter)
    {
        //_animator.SetBool(parameter,isActive);
    }
}
