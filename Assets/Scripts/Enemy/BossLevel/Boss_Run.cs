using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : MonoBehaviour
{
    public Transform pos;
    public LayerMask collisionLayer;

    private Rigidbody2D rigidBody;
    private Animator bossAnimator;
    private SpriteRenderer spriteRenderer;
    private Vector3 velocity = Vector3.zero;

    public Boss boss;
    public Boss_Health bossHealth;
    public static Boss_Health instance;
    
   

    private void Start()
    {

        //Import of public methods and attributes from BossHealth script
        
		bossHealth = gameObject.GetComponent<Boss_Health>();

        //Importing the rigid body, animator and the sprite renderer of the player
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        bossAnimator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    //void Update()
    //{
       
    //}

    void FixedUpdate()
    {
        //Verification of proximity with the floor
        //groundCheckRadius = 0.38f, to be adapted to the Player size if necessary (use OnDrawGizmos to test the radius)

        boss.LookAtPlayer();    
        
        MoveBoss(pos);

        //Movement animation
        float bossVelocity = Mathf.Abs(rigidBody.velocity.x);
        bossAnimator.SetFloat("BossSpeed", bossVelocity);
    }

    private void MoveBoss(Transform _position)
    {
        Vector3 targetVelocity = new Vector2(_position.position.x, rigidBody.velocity.y);
        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, .05f);

        bossAnimator.Play("Run");
   }
}

