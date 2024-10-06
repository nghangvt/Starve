using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DamageEnemy : MonoBehaviour
{
    Enemy enemyS;
    public int minDamage;
    public int maxDamage;
    public EnemyHealth enemyHealth;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyHealth = collision.GetComponent<EnemyHealth>();
        if (collision.CompareTag("Enemy"))
        {
            enemyS = collision.GetComponent<Enemy>();
            InvokeRepeating("takeDamageEnemy", 0, 0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyHealth = collision.GetComponent<EnemyHealth>();
        if (collision.CompareTag("Enemy"))
        {
            enemyS = null;
            CancelInvoke("takeDamageEnemy");
        }
    }

    void takeDamageEnemy()
    {
        int damage = UnityEngine.Random.Range(minDamage, maxDamage);

        enemyS.TakeDame(damage);
    }
}
