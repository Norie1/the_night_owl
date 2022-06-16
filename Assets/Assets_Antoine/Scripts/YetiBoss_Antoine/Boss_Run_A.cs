using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run_A : MonoBehaviour
{
    public Transform pos;
    public LayerMask collisionLayer;

    private Rigidbody2D rigidBody;
    private Animator bossAnimator;
    private SpriteRenderer spriteRenderer;
    private Vector3 velocity = Vector3.zero;
    private float speed;
    private float bossVelocity;

    public Boss boss;
    public Boss_Health_A bossHealth;
    public static Boss_Health_A instance;
    
   

    private void Start()
    {

        //Import of public methods and attributes from BossHealth script
        
		bossHealth = gameObject.GetComponent<Boss_Health_A>();

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

        //Movement animation
        bossVelocity = Mathf.Abs(rigidBody.velocity.x);
        
        
		MoveBoss(pos);
    }

    private void MoveBoss(Transform _position)
    {
        Vector3 targetVelocity = new Vector2(_position.position.x, -5.35f);
        speed = (speed * Time.deltaTime) / 10;
        
        if (spriteRenderer.flipX == false)
        {
			rigidBody.velocity  *= -1;
		}

		rigidBody.velocity = Vector3.SmoothDamp(new Vector3(bossVelocity, -3.35f, 0f), targetVelocity, ref velocity, 0.02f);
	

        bossAnimator.Play("Run");
   }
}

