using UnityEngine;
using UnityEngine.UI;

public class Moonwalk_S : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    public SpriteRenderer playerSprite;
    public Animator playerAnimator;
    public Text moonwalkText;

    //private Transform target;
    //private int destPoint;
    private bool isInRange;

    [HideInInspector]
    public bool isDancing;

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
    /*
    void Start()
    {
        //Initialization of the first target/destination
        target = waypoints[0];
        destPoint = 0;
    }
    */

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !isDancing)
        {
            isDancing = true;
            playerAnimator.Play("Moonwalk_S");
            playerSprite.flipX = false;
            moonwalkText.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && isDancing)
        {
            isDancing = false;
            playerAnimator.Play("PlayerIdle");
        }
        /*
        if (isDancing)
        {
            Vector3 direction = target.position - transform.position;

            //Player movement (normalization of the movement vector)
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            //When the enemy is close to the target
            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                //target = next target
                destPoint = (destPoint + 1) % waypoints.Length;
                target = waypoints[destPoint];

                //Player flip
                playerSprite.flipX = !playerSprite.flipX;
            }
        }
        */
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
