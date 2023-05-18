using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectorController : CharacterCollectorController, ICollector
{
    public static event Action<Vector2> OnPlayerPositionUpdate;

    void Update()
    {
        Vector2 playerPosition = transform.position;

        OnPlayerPositionUpdate?.Invoke(playerPosition);
    }

    public override void Collect(ICollectable collectable)
    {
        base.Collect(collectable);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable _collectable = collision.gameObject.GetComponent<ICollectable>();
        if (_collectable == null)
        {
            return;
        }
        Collect(_collectable);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
