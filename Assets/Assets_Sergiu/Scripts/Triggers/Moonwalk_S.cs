using UnityEngine;
using UnityEngine.UI;

public class Moonwalk_S : MonoBehaviour
{
    public Transform[] waypoints;
    public SpriteRenderer playerSprite;
    public Animator playerAnimator;
    public Text moonwalkText;

    private bool isInRange;

    [HideInInspector]
    public bool isDancing;

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

        playerMovement = PlayerMovement_S.instance;
    }
    

    void Update()
    {
        //Start moonwalking when near the trigger and when pressing E
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !isDancing)
        {
            playerMovement.freezePlayerMovement = true;
            isDancing = true;           
            playerAnimator.Play("Moonwalk_S");
            playerSprite.flipX = false;
            moonwalkText.enabled = false;
        }
        //Stop dancing
        else if (Input.GetKeyDown(KeyCode.E) && isDancing)
        {
            isDancing = false;
            playerMovement.freezePlayerMovement = false;
            playerMovement.ReintializeMoonwalk();
            playerAnimator.Play("PlayerIdle");
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
