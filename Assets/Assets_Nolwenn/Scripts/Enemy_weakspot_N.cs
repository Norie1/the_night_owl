using UnityEngine;

public class Enemy_weakspot_N : MonoBehaviour
{
    public EnemyHealth_N enemyHealth;
    public int damageTaken = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyHealth.TakeDamage(damageTaken);
        }
    }
}
