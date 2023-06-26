using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float bulletForce = 10f;

    [SerializeField]
    public int bulletDamage { get; set; }

    [SerializeField]
    private float bulletLifeTime = 2f;

    [SerializeField]
    private ParticleSystem _destroyPS;


    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * bulletForce * Time.fixedDeltaTime);
        StartCoroutine(LifeRoutine());
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(bulletLifeTime);

        this.Deactivate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<IDamageable>().TakeDamage(bulletDamage);
            Deactivate();
            
        }
        if(collision.collider.CompareTag("Obstacle"))
        {
            Deactivate();
        }
    }
    private void Deactivate()
    {
        //_destroyPS.Play();
        this.gameObject.SetActive(false);
    }

    private async void BulletDestroy()
    {
        _destroyPS.Play();
        await Task.Delay(1000);
        this.gameObject.SetActive(false);
    }

    public void SetBulletDamage(int bulletDamage)
    { this.bulletDamage = bulletDamage;}
}
