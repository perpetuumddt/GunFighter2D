using System;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character
{
    public class CharacterInputHandler : MonoBehaviour
    {
        public event Action<bool> OnMove;
        public event Action OnRoll;
        public event Action OnReload;
        public event Action OnAttack;
        public event Action OnAttackCanceled;
        public event Action OnSwapWeapon;
        public event Action OnInteract;

        protected void InvokeOnRoll()
        {
            OnRoll?.Invoke();
        }

        protected void InvokeOnMove()
        {
            OnMove?.Invoke(true);
        }
        protected void UnInvokeOnMove()
        {
            OnMove?.Invoke(false);
        }

        protected void InvokeOnAttack()
        {
            OnAttack?.Invoke();
        }

        protected void InvokeOnAttackCanceled()
        {
            OnAttackCanceled?.Invoke();
        }

        protected void InvokeOnReload()
        {
            OnReload?.Invoke();
        }

        protected void InvokeOnSwapWeapon()
        {
            OnSwapWeapon?.Invoke();
        }

        protected void InvokeOnInteract()
        {
            OnInteract?.Invoke();
        }
    }
}
