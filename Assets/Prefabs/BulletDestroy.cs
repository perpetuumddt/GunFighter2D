using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _destroyPS;
    void Start()
    {
        _destroyPS.Play();
        Destroy(gameObject,1f);
    }
}
