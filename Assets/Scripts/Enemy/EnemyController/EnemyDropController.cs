using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropController : CharacterDropController
{
    [SerializeField]
    private GameObject _itemToDrop;

    public override void DropItem()
    {
        base.DropItem();
        GameObject drop = Instantiate(_itemToDrop, transform.position, transform.rotation);
        DropDirection(drop);
    }

    private void DropDirection(GameObject drop)
    {
        Vector2 dropDirection = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        
        drop.GetComponent<Rigidbody2D>().AddForce(dropDirection * 100, ForceMode2D.Impulse);
    }
}
