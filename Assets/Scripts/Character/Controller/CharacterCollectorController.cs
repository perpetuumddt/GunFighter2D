using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
