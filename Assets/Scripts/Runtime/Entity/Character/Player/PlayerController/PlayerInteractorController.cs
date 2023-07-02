using Gunfighter.Runtime.Entity.Character.Controller;
using Gunfighter.Runtime.Interface.Interact;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Player.PlayerController
{
    public class PlayerInteractorController : CharacterInteractorController,IInteractor
    {
        public virtual void Interact(IInteractable interactable)
        {
            interactable.DoInteract();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
            if (interactable == null)
            {
                return;
            }
            interactable.ActivateInteraction();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
            if (interactable == null)
            {
                return;
            }
            interactable.DeactivateInteraction();
        }
    }
}
