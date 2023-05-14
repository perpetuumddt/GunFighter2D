using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollectorController : MonoBehaviour ,ICollector
{
    public void Collect(ICollectable collectable)
    {
        collectable.DoCollect();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable _collectable = collision.gameObject.GetComponent<ICollectable>();
        if(_collectable==null)
        {
            return;
        }
        Collect(_collectable);    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    //продублировать как CharacterInteractorController
    //в он тригер ентер ActivateInteraction
    //в он ексит DeactivateInteraction
}
