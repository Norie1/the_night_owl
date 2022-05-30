using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger_S : MonoBehaviour
{
    public Text interactMessage;

    public Dialog_S dialog;

    private bool isInRange;

    DialogManager_S dialogManager;

    void Start()
    {
        //Importing public methods and attributes of the DialogManager_S script
        dialogManager = DialogManager_S.instance;
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogManager.dialogStarted)
            {
                dialogManager.StartDialogue(dialog);
            }
            else
            {
                dialogManager.DisplayNextSentece();
            }
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
