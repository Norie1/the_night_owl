using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FireBall : MonoBehaviour
{
    public Transform fireProjectile;
    public GameObject projectile;
     
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        yield return new WaitForSeconds(3f);
        Instantiate(projectile, fireProjectile.position, fireProjectile.rotation);    
    }



}
