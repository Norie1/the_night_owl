using UnityEngine;
using UnityEngine.UI;

public class Moonwalk_S : MonoBehaviour
{
    public Transform[] waypoints;

    public Transform player;
    public SpriteRenderer playerSprite;
    public Animator playerAnimator;
    public Text moonwalkText;

    private bool isInRange;

    [HideInInspector]
    public bool isDancing;

    private Transform target;
    private int destPoint;

    private float countdown = 0.3f;
    private bool startCountdown;

    private PlayerMovement_S playerMovement;

    public static Moonwalk_S instance;

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
        target = waypoints[0];
        destPoint = 0;

        playerMovement = PlayerMovement_S.instance;
    }


    void Update()
    {
        //Start moonwalking when near the trigger and when pressing E
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !isDancing)
        {
            
            isDancing = true;
            startCountdown = true;

            playerMovement.freezePlayerMovement = true;
            playerAnimator.Play("Moonwalk_S");
            playerSprite.flipX = false;

            moonwalkText.enabled = false;
        }
        //Stop dancing
        else if (Input.GetKeyDown(KeyCode.E) && isDancing)
        {
            isDancing = false;

            countdown = 0.3f;

            playerMovement.freezePlayerMovement = false;
            playerAnimator.Play("PlayerIdle");

            target = waypoints[0];
            destPoint = 0; 
        }

        if (isDancing)
        {
            if (startCountdown)
            {
                countdown -= Time.deltaTime;
            }

            if (countdown <= 0)
            {
                startCountdown = false;

                Vector3 direction = target.position - transform.position;

                //Player movement (normalization of the movement vector)
                player.Translate(direction.normalized * 2 * Time.deltaTime, Space.World);

                //When the player is close to the target
                if (Vector3.Distance(player.position, target.position) < 0.3f)
                {
                    //target = next target
                    destPoint = (destPoint + 1) % waypoints.Length;
                    target = waypoints[destPoint];

                    //Player flip
                    playerSprite.flipX = !playerSprite.flipX;
                }
            }    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInRange = true;

        if (!isDancing)
        {
            moonwalkText.enabled = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
        moonwalkText.enabled = false;
    }
}
