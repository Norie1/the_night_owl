using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBall : MonoBehaviour
{
    public Transform fireProjectileLeft;
    public Transform fireProjectileRight;
    public GameObject projectile;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            SpriteRenderer player = GetComponent<SpriteRenderer>();
           // if(player.flipX == false){
            Instantiate(projectile, fireProjectileLeft.position, fireProjectileLeft.rotation);
           //
            /*}
            else {
            Instantiate(projectile, fireProjectileRight.position, fireProjectileRight.rotation);
            }*/
       }
    }
}
