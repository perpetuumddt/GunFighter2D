using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    private int poolCount = 10;
    [SerializeField]
    private bool autoExpand = false;
    [SerializeField]
    private BulletScript bulletPrefab;

    private PoolMono<BulletScript> pool;

    private void Start()
    {
        this.pool = new PoolMono<BulletScript>(this.bulletPrefab, this.poolCount, this.transform);
        this.pool.autoExpand = this.autoExpand;
    }
}
