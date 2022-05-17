using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementTemp : MonoBehaviour
{

    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;
    private bool isJumping;
    private bool isGrounded;
    [HideInInspector] public bool isClimbing;

    public Rigidbody2D rb;
    public Animator animator;
    public Animator animatorPnjLog;
    public SpriteRenderer spriteRenderer;
   
    private Vector3 velocity = Vector3.zero;

    public Transform groundedCheckRight;
    public Transform groundedCheckLeft;
    public float groundCheckRadius;

    private float horizontalMovement;
    private float verticalMovement;

    public static PlayerMovementTemp instance;
	
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance PlayerMovement dans la scène");
            return;
        }

        instance = this;
    }

    void Update()
    {
        // On récupère mouvement horizontal
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        // On récupère mouvement vertical
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }

    void FixedUpdate()
    {      
        isGrounded = Physics2D.OverlapArea(groundedCheckLeft.position, groundedCheckRight.position);
        MovePlayer(horizontalMovement, verticalMovement);
    }

    void MovePlayer(float _horizontalMovement, float _verticalMovement)
    {
     

            //Déplacement horizontal
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
            if(isJumping && isGrounded){
                rb.AddForce(new Vector2(0f, jumpForce));
                isGrounded = false;
                isJumping = false;
            }

       // }
       // else
       // {
            //Déplacement vertical
           // Vector3 targetVelocity = new Vector2(0f, _verticalMovement);
           // rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
      //  }
    }

    void Flip(float _velocity){
        if(_velocity > 0.1f){
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f){ 
            spriteRenderer.flipX = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Door_Shop"){        
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                  }
       }

 void OnTriggerExit2D(Collider2D collision){
    }
	
/*	// Crée Gizmos pour le cercle de groundCheck
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }*/
}
