using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float bulletForce = 10f;
    
    private float bulletDamage = 1f;

    private bool isColliding;

    //[SerializeField]
    //private GameObject BulletDestroying;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * bulletForce * Time.fixedDeltaTime);
        Destroy(gameObject,3f);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<IDamageable>().TakeDamage(bulletDamage);
            Destroy(gameObject);
            //particles
        }
        if(collision.collider.CompareTag("Obstacle"))
        {
            //Instantiate(BulletDestroying, transform.position, Quaternion.identity);
            //damage obstacle if damageable
            Destroy(gameObject);
        }
    }
}
