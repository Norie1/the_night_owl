using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;

    [SerializeField] private bool isJumping;
    [SerializeField] private bool isGrounded;

    public bool onTheWallR;
    public bool onTheWallL;
    [SerializeField] private bool wallJumpR;
    [SerializeField] private bool wallJumpL;
    [SerializeField] private bool superJump;
    [SerializeField] private bool doubleJump;

    public Transform groundCheck;
    public float groundCheckRadius = 0.4f;
    public LayerMask layerMask;

    public CapsuleCollider2D playerCollider; //Collider du personnage
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform WallCheckRUp;
    [SerializeField] private Transform WallCheckRDown;
    [SerializeField] private Transform WallCheckLUp;
    [SerializeField] private Transform WallCheckLDown;

    //Rigidbody, animator, sprite renderer
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public static PlayerMov instance;

    //On définit PlayerMov comme une instance
    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance PlayerMov dans la scène");
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Verification of proximity with a wall
        onTheWallR = Physics2D.OverlapArea(WallCheckRUp.position, WallCheckRDown.position, layerMask);
        onTheWallL = Physics2D.OverlapArea(WallCheckLUp.position, WallCheckLDown.position, layerMask);

        if (Input.GetButtonDown("Jump"))
        {
            //Normal jump
            if (isGrounded)
            {
                isJumping = true;
            }
            //Wall jump right side
            else if (onTheWallR && wallJumpR)
            {
                isJumping = true;
                wallJumpR = false;
                wallJumpL = true;
                superJump = true;
            }
            //Wall jump left side
            else if (onTheWallL && wallJumpL)
            {
                isJumping = true;
                wallJumpL = false;
                wallJumpR = true;
                superJump = true;
            }
            //Double jump
            else if (doubleJump && !onTheWallL && !onTheWallR)
            {
                isJumping = true;
                doubleJump = false;
            }
            
        }
    }

    void FixedUpdate()
    {
        //Verification of proximity with the floor
        //Check if player is close to the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, layerMask);

        //Reinitialization of Double/Wall jump when grounded
        if (isGrounded)
        {
            doubleJump = true;
            wallJumpL = true;
            wallJumpR = true;
        }

        float horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;        

        MovePlayer(horizontalMovement);

        Flip(rb.velocity.x);

        //Movement animation
        float playerVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("PlayerSpeed", playerVelocity);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        //Jump////////
        if (isJumping)
        {
            //Wall jump
            float impulse;
            if (superJump)
            {
                //Additional force needed when jumping from a wall
                impulse = 450f;
                superJump = false;
            }
            //Normal jump
            else
            {
                impulse = jumpForce;
            }
            rb.AddForce(new Vector2(0f, impulse));
            isJumping = false;
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(WallCheckLDown.position, WallCheckLUp.position);
        Gizmos.DrawLine(WallCheckRDown.position, WallCheckRUp.position);
    }
}
