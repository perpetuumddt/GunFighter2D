using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float bulletForce = 10f;
    
    private float bulletDamage = 1f;

    [SerializeField]
    private GameObject _bulletDestroy;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * bulletForce * Time.fixedDeltaTime);
        Destroy(gameObject,3f);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<IDamageable>().TakeDamage(bulletDamage); //принцип подстановски лискофф
            DestroyBullet();
        }
        if(collision.collider.CompareTag("Obstacle"))
        {
            //damage obstacle if damageable
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        GameObject bulletDestroy = Instantiate(_bulletDestroy, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
