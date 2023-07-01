using System;
using UnityEngine;

namespace Gunfighter.Entity.Character.Controller
{
    public class CharacterMovementController : MonoBehaviour
    {
        protected CharacterController characterController;

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        public virtual void DoMove(params object[]param)
        {
        }
    
    }
}
