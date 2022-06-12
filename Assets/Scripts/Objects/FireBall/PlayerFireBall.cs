using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBall : MonoBehaviour
{
    public Animator playerAnimator;
    public Transform fireProjectileLeft;
    public Transform fireProjectileRight;
    public GameObject projectile;

    public bool canThrowFireBall = false;

    void Update()
    {
        if(canThrowFireBall == true)
        {
        if(Input.GetButtonDown("Fire1"))
        {
            SpriteRenderer player = GetComponent<SpriteRenderer>();
            if(player.flipX == true)
            {
                playerAnimator.Play("Attack2");
                Instantiate(projectile, fireProjectileLeft.position, fireProjectileLeft.rotation);
            }
            else 
            {
                playerAnimator.Play("Attack2");
                Instantiate(projectile, fireProjectileRight.position, fireProjectileRight.rotation);
            }
       }
    }
    }

}
