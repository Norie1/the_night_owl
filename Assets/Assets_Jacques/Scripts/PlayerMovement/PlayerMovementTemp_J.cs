using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementTemp_J : MonoBehaviour
{

    public float moveSpeed;
    public float climbSpeed;
    public float jumpForce;
    public float speedDashing;
    private bool isJumping;
    private bool isGrounded;
    private bool isDashing = false;
    private bool canDashing = false;
   // [HideInInspector] public bool isClimbing;

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

    public static PlayerMovementTemp_J instance;
	
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("yop");
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
        if(Input.GetKeyDown(KeyCode.R))
        {
            isDashing = true;
        }
        else if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
      //  animator.SetBool("isClimbing", isClimbing);
    }

    void FixedUpdate()
    {      

        isGrounded = Physics2D.OverlapArea(groundedCheckLeft.position, groundedCheckRight.position);
    if(isDashing){
        DashPlayer(horizontalMovement);
    }
        MovePlayer(horizontalMovement, verticalMovement);
    }

    void DashPlayer(float _horizontalMovement)
    {
        //target du Dash
         Vector3 target;
        if(spriteRenderer.flipX == true){
            //move Left
            target = new Vector3(Mathf.Abs(_horizontalMovement)-speedDashing,0,rb.velocity.y);
        }
        else {
            //move Right
            target = new Vector3(Mathf.Abs(_horizontalMovement)+speedDashing,0,rb.velocity.y);
        }
        //apply target to position player
        rb.MovePosition(transform.position + target * Time.deltaTime * speedDashing);
        isDashing = false;
        //play animation of Dash
        animator.Play("PlayerDashing");

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
        if (collision.gameObject.tag == "Door_Donjon1"){        
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
        if (collision.gameObject.tag == "Door_Donjon1_Exit"){        
            SceneManager.LoadScene("Level_Jacques");
        }
       }

 void OnTriggerExit2D(Collider2D collision){
    }
}
