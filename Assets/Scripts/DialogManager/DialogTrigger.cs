using UnityEngine;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour
{
    public Text interactMessage;

    public Dialog dialog;

    private bool isInRange;

    DialogManager dialogManager;

    void Start()
    {
        // Importing public methods and attributes of the DialogManager script
        dialogManager = DialogManager.instance;
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
