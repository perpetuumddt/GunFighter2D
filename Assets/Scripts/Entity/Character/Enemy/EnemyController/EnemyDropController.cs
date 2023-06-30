using Gunfighter.Entity.Character.Controller;
using Gunfighter.General.Drop;
using Gunfighter.General.Objects_Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Entity.Character.Enemy.EnemyController
{
    public class EnemyDropController : CharacterDropController
    {
        [FormerlySerializedAs("_coinToDrop")] [SerializeField]
        private CoinController coinToDrop;

        private Transform _coinPool;

        private PoolMono<CoinController> _pool;
        private bool _autoExpand = true;
        private int _poolCount = 0;

        private void Start()
        {
            _coinPool = GameObject.FindGameObjectWithTag("CoinPool").transform;
            this._pool = new PoolMono<CoinController> (this.coinToDrop,this._poolCount,this._coinPool);
            this._pool.AutoExpand = this._autoExpand;
        }

        public override void DropItem()
        {
            base.DropItem();
            var coin = this._pool.GetFreeElement();
            coin.transform.position = this.transform.position;
            DropDirection(coin);
        }

        private void DropDirection(CoinController drop)
        {
            Vector2 dropDirection = new Vector2(Random.Range(-1, 1),Random.Range(-1, 1));
            drop.GetComponent<Rigidbody2D>().AddForce(dropDirection * 10, ForceMode2D.Impulse);
        }
    }
}
