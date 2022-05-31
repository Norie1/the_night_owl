using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject player;
    public float projectileSpeed = 20f;
    public GameObject impactEffect; 

    private Rigidbody2D rb;

    void Start()
    {
        SpriteRenderer sprite = player.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //if(sprite.flipX == false)
        //{
            rb.velocity = transform.right * projectileSpeed;    
        //}
       /* else 
        {
            transform.Rotate(0f,180f,0f);
            rb.velocity = transform.right * projectileSpeed;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if(enemy != null )
        {
        enemy.TakeDamage(100);
        Instantiate(impactEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
