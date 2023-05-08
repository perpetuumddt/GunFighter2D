using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    //[SerializeField]
    //private ParticleSystem bulletDestroy;
    private void Start()
    {
        //bulletDestroy.Play();
        Destroy(gameObject, 0.1f);
    }
}
