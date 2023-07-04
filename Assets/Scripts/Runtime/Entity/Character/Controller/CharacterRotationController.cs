using Gunfighter.Runtime.Entity.Character.Player.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gunfighter.Runtime.Entity.Character.Controller
{
    public abstract class CharacterRotationController : MonoBehaviour
    {
        public abstract void CheckLookingDirection();
        public abstract Vector2 CheckMovementDirection();

        public abstract void CheckRollingDirection();
        protected abstract void Flip();
    }
}
