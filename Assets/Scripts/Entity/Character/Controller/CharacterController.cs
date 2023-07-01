using System;
using Gunfighter.Entity.Character.StateMachine;
using Gunfighter.ScriptableObjects.Data.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Character.Controller
{
    public class CharacterController : MonoBehaviour
    {
        public CharacterAnimationController CharacterAnimationController { get; private set; }
        public CharacterAttackController CharacterAttackController { get; private set; }
        public CharacterCollectorController CharacterCollectorController { get; private set; }
        public CharacterDropController CharacterDropController { get; private set; }
        public CharacterHealthController CharacterHealthController { get; private set; }
        public CharacterInputHandler CharacterInputHandler { get; private set; }
        public CharacterMovementController CharacterMovementController { get; private set; }
        public CharacterRotationController CharacterRotationController { get; private set; }


        [SerializeField] 
        private CharacterData characterData;

        public CharacterData CharacterData => characterData;
    
        protected StateMachine<CharacterController> StateMachine;

        protected virtual void Awake()
        {
            CharacterAnimationController = GetComponent<CharacterAnimationController>();
            CharacterAttackController = GetComponent<CharacterAttackController>();
            CharacterCollectorController = GetComponent<CharacterCollectorController>();
            CharacterDropController = GetComponent<CharacterDropController>();
            CharacterHealthController = GetComponent<CharacterHealthController>();
            CharacterInputHandler = GetComponent<CharacterInputHandler>();
            CharacterMovementController = GetComponent<CharacterMovementController>();
            CharacterRotationController = GetComponent<CharacterRotationController>();
        }
    }
}
