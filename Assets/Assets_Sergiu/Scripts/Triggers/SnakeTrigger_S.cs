using UnityEngine;

public class SnakeTrigger_S : MonoBehaviour
{
    public Transform destination;

    public Dialog_S snakeDialog;
    public Dialog_S playerDialog;

    public Animator snakeAnimator;
    public SpriteRenderer snakeSprite;
    public SpriteRenderer playerSprite;

    private bool isInRange;
    private bool firstDialogFinished;
    private bool dialogFinished;
    private bool disableTrigger;
    private bool startSnakeMovement;
    private bool destinationReached;

    private float countdown = 1.2f;

    private DialogManager_S dialogManager;

    void Start()
    {
        //Import of public methods attributes from DialogManager_S script
        dialogManager = DialogManager_S.instance;

        snakeAnimator.Play("SnakeIdle_S");
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !dialogFinished)
        {
            if (!firstDialogFinished)
            {
                firstDialogFinished = !dialogManager.DisplayNextSentence();

                if (firstDialogFinished)
                {
                    startSnakeMovement = true;
                    snakeAnimator.Play("SnakeAnimation");
                }
            }

            if (destinationReached)
            {
                dialogFinished = !dialogManager.DisplayNextSentence();
            }
        }        

        if (startSnakeMovement)
        {
            PlayerMovement_S.instance.freezePlayerMovement = true;

            //Moving the snake until the destination is reached
            if (!destinationReached)
            {
                Vector3 direction = destination.position - transform.position;
                transform.Translate(direction.normalized * 4 * Time.deltaTime, Space.World);

                //Stopping the snake when destination is reached
                if (Vector2.Distance(transform.position, destination.position) < 0.03f)
                {
                    destinationReached = true;
                    dialogManager.StartDialog(playerDialog);
                    snakeSprite.enabled = false;
                    startSnakeMovement = false;
                }   
            }

            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                playerSprite.flipX = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Starting Snake dialog when entering the trigger area, if not already done
        if (collision.CompareTag("Player") && !disableTrigger)
        {
            dialogManager.StartDialog(snakeDialog);
            isInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
