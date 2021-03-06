using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 20f;
    public GameObject impactEffect; 
    public bool isFlipped = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Player");
        SpriteRenderer sprite = player.GetComponent<SpriteRenderer>(); 
        if(sprite.flipX == false)
        {
            rb.velocity = transform.right * projectileSpeed;    
        }
        else 
        {
            SpriteRenderer fireBall = GetComponent<SpriteRenderer>();
            fireBall.flipX = true;
            rb.velocity = (transform.right * -1) * projectileSpeed;    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if(enemy != null )
        {
        enemy.TakeDamage(100);
        Instantiate(impactEffect, transform.position, transform.rotation);
        }
        Boss_Health_J boss = collision.GetComponent<Boss_Health_J>();
        if(boss != null )
        {
        boss.TakeDamage(100);
        Instantiate(impactEffect, transform.position, transform.rotation);
        }
        Boss_Health_LastMob miniBoss = collision.GetComponent<Boss_Health_LastMob>();
        if (miniBoss != null )
        {
            miniBoss.TakeDamage(100);
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
        Boss_Health bossH = collision.GetComponent<Boss_Health>();
        if(bossH != null)
        {
            bossH.TakeDamage(100);
            Instantiate(impactEffect, transform.position, transform.rotation);
        } 
        Destroy(gameObject);
    }

    public void flip()
    {
        isFlipped = !isFlipped;
    }
}
