using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : CharacterAttackController
{
    [SerializeField]
    private EnemyData _enemyData;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.transform.GetComponent<IDamageable>().TakeDamage(_enemyData.Damage);
            
        }
        
    }
}
