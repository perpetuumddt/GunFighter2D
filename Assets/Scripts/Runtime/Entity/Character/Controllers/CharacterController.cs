using Gunfighter.Runtime.Entity.Controllers;
using Gunfighter.Runtime.ScriptableObjects.Data.Entity.Character;

namespace Gunfighter.Runtime.Entity.Character.Controllers
{
    public class CharacterController : EntityController
    {
        public new CharacterAnimationController AnimationController => base.AnimationController as CharacterAnimationController;
        public CharacterAttackController AttackController { get; private set; }
        public CharacterCollectorController CollectorController { get; private set; }
        public new CharacterCollisionController CollisionController => base.CollisionController as CharacterCollisionController;
        public new CharacterDropController DropController => base.DropController as CharacterDropController;
        public new CharacterHealthController HealthController => base.HealthController as CharacterHealthController;
        public CharacterInputHandler InputHandler { get; private set; }
        public CharacterMovementController MovementController { get; private set; }
        public CharacterRotationController RotationController { get; private set; }

        public CharacterData CharacterData => EntityData as CharacterData;
        
        
        

        protected override void Awake()
        {
            base.Awake();
            SetControllerReferences();
        }

        private void SetControllerReferences()
        {
            AttackController = GetComponent<CharacterAttackController>();
            CollectorController = GetComponent<CharacterCollectorController>();
            InputHandler = GetComponent<CharacterInputHandler>();
            MovementController = GetComponent<CharacterMovementController>();
            RotationController = GetComponent<CharacterRotationController>();
        }
    }
}
