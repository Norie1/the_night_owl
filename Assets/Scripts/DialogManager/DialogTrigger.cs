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
            }
            else
            {
                if (!dialogManager.DisplayNextSentence())
                {
                    interactMessage.enabled = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInRange = true;
        }
      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInRange = false;
            interactMessage.enabled = false;
            dialogManager.EndDialog();
        }
       
    }
}
