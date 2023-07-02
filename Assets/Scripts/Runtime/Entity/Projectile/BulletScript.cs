using System.Collections;
using System.Threading.Tasks;
using Gunfighter.Runtime.Interface.Damage;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gunfighter.Runtime.Entity.Projectile
{
    public class BulletScript : MonoBehaviour
    {
        [SerializeField]
        private float bulletForce = 10f;

        [SerializeField]
        public int BulletDamage { get; set; }

        [SerializeField]
        private float bulletLifeTime = 2f;

        [FormerlySerializedAs("_destroyPS")] [SerializeField]
        private ParticleSystem destroyPS;


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
                collision.transform.GetComponent<IDamageable>().TakeDamage(BulletDamage);
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
            destroyPS.Play();
            await Task.Delay(1000);
            this.gameObject.SetActive(false);
        }

        public void SetBulletDamage(int bulletDamage)
        { this.BulletDamage = bulletDamage;}
    }
}
