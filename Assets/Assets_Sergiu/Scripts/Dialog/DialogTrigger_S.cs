using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger_S : MonoBehaviour
{
    public Text interactMessage;

    public Dialog_S dialog;

    private bool isInRange;

    private PlayerHealth_S playerHealth;

    DialogManager_S dialogManager;

    void Start()
    {
        //Importing public methods and attributes of the DialogManager_S and PlayerHealth_S scripts
        dialogManager = DialogManager_S.instance;
        playerHealth = PlayerHealth_S.instance;
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogManager.dialogStarted)
            {
                dialogManager.StartDialog(dialog);
                interactMessage.enabled = false;
            }
            else
            {
                if (!dialogManager.DisplayNextSentence())
                {
                    interactMessage.enabled = true;
                }
            }
        }
        //If postmortem dialog is started
        else if (playerHealth.postmortemDialog && Input.GetKeyDown(KeyCode.E))
        {
            dialogManager.DisplayNextSentence();
            playerHealth.postmortemDialog = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
        interactMessage.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
        interactMessage.enabled = false;
    }
}
