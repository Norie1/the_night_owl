using UnityEngine;
using System.Collections;

public class PlayerMovement_S : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    public float dashSpeed;

    private float horizontalMovement;

    private bool isJumping;
    private bool isDancing;
    private bool activeDash;
    private bool isGrounded;
    private bool onTheWallR;
    private bool onTheWallL;
    private bool wallJumpR;
    private bool wallJumpL;
    private bool superJump;
    private bool doubleJump;

    private bool flipPlayer;

    [HideInInspector]
    public bool freezePlayerMovement;

    [HideInInspector]
    public bool playerFall;

    public LayerMask foundationLayer;
    public Transform groundCheck;

    public Transform WallCheckRUp;
    public Transform WallCheckRDown;
    public Transform WallCheckLUp;
    public Transform WallCheckLDown;

    [HideInInspector]
    public Vector3 respawnPoint;
    public GameObject deathZone;

    public Projectile_S projectilePrefab;
    public Transform launchOffsetR;
    public Transform launchOffsetL;
    private bool activeProjectile;

    private Rigidbody2D rigidBody;
    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;

    private PlayerHealth_S playerHealth;
    private Moonwalk_S moonwalk;

    private Transform[] waypoints;
    private Transform target;
    private int destPoint;

    public static PlayerMovement_S instance;


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

        activeDash = true;
        activeProjectile = true;

        //Import of public methods and attributes from PlayerHealth script
        playerHealth = PlayerHealth_S.instance;
        moonwalk = Moonwalk_S.instance;

        //Importing the rigid body, animator and the sprite renderer of the player
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        waypoints = Moonwalk_S.instance.waypoints;

        //Initialization of the first target/destination for the Moonwalk
        ReintializeMoonwalk();
    }

    // Update is called once per frame
    void Update()
    {
        isDancing = moonwalk.isDancing;

        if (isDancing)
        {
            Vector3 direction = target.position - transform.position;

            //Player movement (normalization of the movement vector)
            transform.Translate(direction.normalized * 2 * Time.deltaTime, Space.World);

            //When the player is close to the target
            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                //target = next target
                destPoint = (destPoint + 1) % waypoints.Length;
                target = waypoints[destPoint];

                //Player flip
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }

        //Move player if freezePlayer movement is inactive
        if (!freezePlayerMovement)
        {
            //Verification of the proximity with the ground
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.38f, foundationLayer);

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

            if (Input.GetKeyDown(KeyCode.X) && activeDash)
            {
                StartCoroutine(Dash(horizontalMovement));
                activeDash = false;
            }

            if (Input.GetKeyDown(KeyCode.F) && activeProjectile)
            {
                projectilePrefab.flipPlayer = flipPlayer;

                Vector3 launchOffset;
                if (!flipPlayer)
                {
                    launchOffset = new Vector3(launchOffsetR.position.x, launchOffsetR.position.y, launchOffsetR.position.z);
                    Instantiate(projectilePrefab, launchOffset, launchOffsetR.rotation);
                }
                else
                {
                    launchOffset = new Vector3(launchOffsetL.position.x, launchOffsetL.position.y, launchOffsetL.position.z);
                    Instantiate(projectilePrefab, launchOffset, launchOffsetL.rotation);
                }
            }
        }
    }

    void FixedUpdate()
    {
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
        Vector3 targetVelocity;
        if (!freezePlayerMovement)
        {
            targetVelocity = new Vector2(_horizontalMovement, rigidBody.velocity.y);
        }
        else
        {
            targetVelocity = new Vector2(0, 0);
        }

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
        playerAnimator.Play("PlayerDash_S");
        yield return new WaitForSeconds(1f);
        playerAnimator.Play("PlayerIdle");

        //2 second delay before reactivating the dash
        yield return new WaitForSeconds(1f);
        activeDash = true;
    }

    private void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
            flipPlayer = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
            flipPlayer = true;
        }
    }

    public void RespawnPlayer()
    {
        transform.position = respawnPoint;

        //Bool initialized in the Checkpoint inspector
        spriteRenderer.flipX = !RespawnManager_S.instance.facingRight;
    }

    public void ReintializeMoonwalk()
    {
        target = waypoints[0];
        destPoint = 0;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            playerHealth.TakeDamage(10);
            RespawnPlayer();

            playerFall = true;
            yield return new WaitForSeconds(3f);
            playerFall = false;
        }
        else if (collision.tag == "Lava")
        {
            playerHealth.TakeDamage(100);

        }
    }
}