using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Controllers
{
    public abstract class CharacterRotationController : MonoBehaviour
    {
        public abstract void CheckLookingDirection();
        public abstract Vector2 CheckMovementDirection();

        public abstract void CheckRollingDirection();
        protected abstract void Flip();
    }
}
