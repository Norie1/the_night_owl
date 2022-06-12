using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil_Enemy : MonoBehaviour
{
    public float projectileSpeed = 20f;
    public GameObject impactEffect; 

    private Rigidbody2D rb;

    void Start()
    {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * projectileSpeed;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
         if(collision.tag == "Foundation")
        {
            Destroy(gameObject);    
        }
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if(player != null )
        {
        player.TakeDamage(20);
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        }
       
        
    }
}
