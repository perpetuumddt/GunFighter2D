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
        GameObject drop = Instantiate(_itemToDrop,transform.position,transform.rotation);
    }
}
