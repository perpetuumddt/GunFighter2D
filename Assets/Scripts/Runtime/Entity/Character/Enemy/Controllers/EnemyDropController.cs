using Gunfighter.Runtime.Entity.Character.Controllers;
using Gunfighter.Runtime.General.Drop;
using Gunfighter.Runtime.General.Objects_Pool;
using UnityEngine;

namespace Gunfighter.Runtime.Entity.Character.Enemy.Controllers
{
    public class EnemyDropController : CharacterDropController
    {
        [SerializeField]
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
