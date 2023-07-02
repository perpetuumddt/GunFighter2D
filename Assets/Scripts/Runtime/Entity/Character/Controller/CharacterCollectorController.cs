using Gunfighter.Runtime.Interface.Collect;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Controller
{
    public class CharacterCollectorController : MonoBehaviour ,ICollector
    {
        public virtual void Collect(ICollectable collectable)
        {
            collectable.DoCollect();
        }

    
        //продублировать как CharacterInteractorController
        //в он тригер ентер ActivateInteraction
        //в он ексит DeactivateInteraction
    }
}
