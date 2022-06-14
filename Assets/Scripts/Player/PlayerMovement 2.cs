using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    public float dashSpeed;

    private float horizontalMovement;

    public bool isJumping;
    public bool activeDash;
    public bool isGrounded;
    public bool onTheWallR;
    public bool onTheWallL;
    public bool wallJumpR;
    public bool wallJumpL;
    public bool superJump;
    public bool doubleJump;

    public Transform groundCheck;
    public LayerMask collisionLayer;

    public Transform WallCheckRUp;
    public Transform WallCheckRDown;
    public Transform WallCheckLUp;
    public Transform WallCheckLDown;

    [HideInInspector]
    public Vector3 respawnPoint;
    public GameObject fallDetector;

    
    private Rigidbody2D rigidBody;
    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;

    private PlayerHealth playerHealth;
    private Inventory inventory;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Player movement already initialized.");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        //respawnPoint intialization to the initial spawn position of the player
        respawnPoint = transform.position;
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.38f, collisionLayer);

        activeDash = true;

        //Import of public methods and attributes from PlayerHealth script
        playerHealth = PlayerHealth.instance;

        //Importing the rigid body, animator and the sprite renderer of the player
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Verification of proximity with a wall
        onTheWallR = Physics2D.OverlapArea(WallCheckRUp.position, WallCheckRDown.position);
        onTheWallL = Physics2D.OverlapArea(WallCheckLUp.position, WallCheckLDown.position);
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.38f, collisionLayer);

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
        if (Input.GetKeyDown(KeyCode.X) && activeDash)
        {
            StartCoroutine(Dash(horizontalMovement));
            activeDash = false;
        }

        //Fall detector following the player
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    void FixedUpdate()
    {
        //Verification of proximity with the floor
        //groundCheckRadius = 0.38f, to be adapted to the Player size if necessary (use OnDrawGizmos to test the radius)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.38f, collisionLayer);

        //Reinitialization of Double/Wall jump when grounded
        if (isGrounded)
        {
            doubleJump = true;
            wallJumpL = true;
            wallJumpR = true;
        }

        horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        MovePlayer(horizontalMovement);

        Flip(rigidBody.velocity.x);

        //Movement animation
        float playerVelocity = Mathf.Abs(rigidBody.velocity.x);
        playerAnimator.SetFloat("PlayerSpeed", playerVelocity);
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

    private IEnumerator Dash(float _horizontalMovement)
    {
        //Dash destination
        Vector3 destination;
        

        //Move player to the target
        if (spriteRenderer.flipX == true)
        {
            //Right dash
            destination = new Vector3(Mathf.Abs(_horizontalMovement) - dashSpeed, 0, rigidBody.velocity.y);
        }
        else
        {
            //Left dash
            destination = new Vector3(Mathf.Abs(_horizontalMovement) + dashSpeed, 0, rigidBody.velocity.y);
        }

        rigidBody.MovePosition(transform.position + destination * Time.deltaTime * dashSpeed);
        
        //Play animation of Dash
        playerAnimator.Play("PlayerDashing");
        
        //2 second delay before reactivating the dash
        yield return new WaitForSeconds(2f);
        activeDash = true;
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
            //To shoot the fire ball to the left, we have to rotate the transform of the fire ball movement
            transform.Rotate(0f,180f,0f);
            
        }
    }

    [HideInInspector]
    public void RespawnPlayer()
    {
        transform.position = respawnPoint;
        spriteRenderer.flipX = false;
    }

    //Respawn when falling into a hole
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            playerHealth.TakeDamage(10);
            RespawnPlayer();
        }
    }
}
