using Gunfighter.Entity.Character.StateMachine;
using Gunfighter.ScriptableObjects.Data.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Character.Controller
{
    public class CharacterController : MonoBehaviour
    {
        [FormerlySerializedAs("_characterAnimationController")] [SerializeField]
        private CharacterAnimationController characterAnimationController;
        public CharacterAnimationController CharacterAnimationController => characterAnimationController;

        [FormerlySerializedAs("_characterMovementController")] [SerializeField]
        private CharacterMovementController characterMovementController;
        public CharacterMovementController CharacterMovementController => characterMovementController;

        [FormerlySerializedAs("_characterInputHandler")] [SerializeField]
        private CharacterInputHandler characterInputHandler;
        public CharacterInputHandler CharacterInputHandler => characterInputHandler;

        [FormerlySerializedAs("_characterAttackController")] [SerializeField]
        private CharacterAttackController characterAttackController;
        public CharacterAttackController CharacterAttackController => characterAttackController;

        [FormerlySerializedAs("_characterRotationController")] [SerializeField]
        private CharacterRotationController characterRotationController;
        public CharacterRotationController CharacterRotationController => characterRotationController;

        [FormerlySerializedAs("_characterHealthController")] [SerializeField]
        private CharacterHealthController characterHealthController;
        public CharacterHealthController CharacterHealthController => characterHealthController;

        [FormerlySerializedAs("_characterDropController")] [SerializeField]
        private CharacterDropController characterDropController;
        public CharacterDropController CharacterDropController => characterDropController;

        [FormerlySerializedAs("_characterCollectorController")] [SerializeField]
        private CharacterCollectorController characterCollectorController;
        public CharacterCollectorController CharacterCollectorController => characterCollectorController;


        [FormerlySerializedAs("_characterData")] [SerializeField] 
        private CharacterData characterData;

        public CharacterData CharacterData => characterData;
    
        protected StateMachine<CharacterController> StateMachine;

    
    }
}
