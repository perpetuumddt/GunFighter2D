using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropController : CharacterDropController
{
    [SerializeField]
    private CoinController _coinToDrop;

    private PoolMono<CoinController> _pool;
    private bool _autoExpand = true;
    private int _poolCount = 1;

    private void Start()
    {
        this._pool = new PoolMono<CoinController> (this._coinToDrop,this._poolCount,this.transform);
        this._pool.autoExpand = this._autoExpand;
    }

    public override void DropItem()
    {
        base.DropItem();
        var coin = this._pool.GetFreeElement();
        //GameObject drop = Instantiate(_itemToDrop, transform.position, transform.rotation);
        DropDirection(coin);
    }

    private void DropDirection(CoinController drop)
    {
        Vector2 dropDirection = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
        drop.GetComponent<Rigidbody2D>().AddForce(dropDirection * 100, ForceMode2D.Impulse);
    }
}
