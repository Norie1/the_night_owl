using UnityEngine;
using UnityEngine.UI;

public class Platforms_S : MonoBehaviour
{

    public Dialog_S dialog;

    public Checkpoint_S checkpoint;

    private bool isInRange;
    private bool dialogFinished;

    private DialogManager_S dialogManager;

    void Start()
    {
        //Import of public methods and attributes from DialogManager_S script
        dialogManager = DialogManager_S.instance;

        checkpoint.disableInteractMsg = true;
    }

    void Update()
    {
        //Starting FireStarter dialog when entering the trigger area
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogFinished = !dialogManager.DisplayNextSentence();
        }

        //Disabling the trigger when the dialog is finished
        if (dialogFinished)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogManager.StartDialog(dialog);
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
