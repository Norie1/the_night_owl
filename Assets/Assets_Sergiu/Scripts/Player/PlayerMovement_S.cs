using UnityEngine;

public class PlayerMovement_S : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;
    private bool onTheWallR;
    private bool onTheWallL;
    private bool wallJumpR;
    private bool wallJumpL;
    private bool superJump;
    private bool doubleJump;

    public Transform groundCheck;
    public LayerMask collisionLayer;

    public Transform WallCheckRUp;
    public Transform WallCheckRDown;
    public Transform WallCheckLUp;
    public Transform WallCheckLDown;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    public Rigidbody2D rigidBody;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    //Import of public methods from PlayerHealth_S scripts
    public PlayerHealth_S playerHealth;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        //respawnPoint intialization to the initial spawn position of the player
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Verification of proximity with a wall
        onTheWallR = Physics2D.OverlapArea(WallCheckRUp.position, WallCheckRDown.position);
        onTheWallL = Physics2D.OverlapArea(WallCheckLUp.position, WallCheckLDown.position);

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

        //Fall detector following the player
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    void FixedUpdate()
    {
        //Verification of proximity with the floor
        //groundCheckRadius = 0.38f, to be adapted to Player size if necessary (use OnDrawGizmos to test the radius)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.38f, collisionLayer);

        //Reinitialization of Double/Wall jump when grounded
        if (isGrounded)
        {
            doubleJump = true;
            wallJumpL = true;
            wallJumpR = true;
        }

        float horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;        

        MovePlayer(horizontalMovement);

        Flip(rigidBody.velocity.x);

        //Movement animation
        float playerVelocity = Mathf.Abs(rigidBody.velocity.x);
        animator.SetFloat("PlayerSpeed", playerVelocity);
    }

    private void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rigidBody.velocity.y);
        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, .05f);

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
            rigidBody.AddForce(new Vector2(0f, impulse));
            isJumping = false;
        }
    }

    private void Flip(float _velocity)
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

    //Respawn
    [HideInInspector]
    public void respawnPlayer()
    {
        transform.position = respawnPoint;
        playerHealth.reinitializePlayerHealth();
        spriteRenderer.flipX = false;
        Inventory_S.instance.reinitializeWallet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            respawnPlayer();
        }
    }
}
