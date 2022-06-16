using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelOutro_S : MonoBehaviour
{

    public Transform player;
    public SpriteRenderer playerSprite;
    public Animator playerAnimator;

    public Dialog_S playerDialog;
    public Dialog_S computerDialog;   

    public Transform[] waypoints;

    private Transform target;
    private int destPoint;

    private DialogManager_S dialogManager;

    private bool firstDialogFinished;
    private bool startSecondDialog;
    private bool secondDialogFinished;
    private bool isInRange;
    private bool destinationReached;
    private bool animationStarted;

    private float countdown;

    private void Start()
    {
        dialogManager = DialogManager_S.instance;

        target = waypoints[0];
        destPoint = 0;

        countdown = 2;
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!firstDialogFinished)
            {
                firstDialogFinished = !dialogManager.DisplayNextSentence();
            }
            else if (!secondDialogFinished && destinationReached)
            {
                secondDialogFinished = !dialogManager.DisplayNextSentence();
            }
        }
        
        //Start moving to the destination
        if (firstDialogFinished && !destinationReached)
        {
            if (!animationStarted)
            {
                animationStarted = true;
                PlayerMovement_S.instance.freezePlayerMovement = true;
                playerAnimator.Play("PlayerRun2");
            }

            playerSprite.flipX = false;


            Vector3 direction = target.position - transform.position;

            player.transform.Translate(direction.normalized * 3 * Time.deltaTime, Space.World);

            if (Vector3.Distance(player.position, target.position) < 0.3f)
            {
                destPoint++;
                
                if (destPoint != 2)
                {
                    target = waypoints[destPoint];
                }
                else
                {
                    destinationReached = true;
                    animationStarted = false;
                }
            }
        }
        else if (destinationReached && !startSecondDialog)
        {
            if (!animationStarted)
            {
                playerAnimator.Play("Computer");
                animationStarted = true;
            }

            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {
                playerAnimator.speed = 0;
            }
            
            if (countdown <= -2)
            {
                startSecondDialog = true;
                dialogManager.StartDialog(computerDialog);
            }
        }
        else if (secondDialogFinished)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    private IEnumerator Wait(float amount)
    {
        yield return new WaitForSeconds(amount);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            dialogManager.StartDialog(playerDialog);
        }
    }
}
