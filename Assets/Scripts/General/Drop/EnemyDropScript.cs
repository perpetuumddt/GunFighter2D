using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _coin;

    public void dropCoin()
    {
        GameObject coin = Instantiate(_coin, transform.position, transform.rotation) as GameObject;
    }
}
