using UnityEngine;
using UnityEngine.UI;

public class FireStarter_S : MonoBehaviour
{
    [SerializeField]
    private Text firestarterText;

    public Dialog_S dialog;

    private bool isInRange;
    private bool dialogFinished;
    private bool disableTrigger;

    private DialogManager_S dialogManager;

    void Start()
    {
        //Import of public methods attributes from DialogManager_S script
        dialogManager = DialogManager_S.instance;
    }

    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogFinished = !dialogManager.DisplayNextSentence();
            }

            //Activating the on-screen FireStarterText when the dialog is finished
            if (isInRange && dialogFinished && !disableTrigger)
            {
                firestarterText.enabled = true;
            }

            //Disabling the on-screen FireStarterText when pushing the F button
            if (Input.GetKeyDown(KeyCode.F))
            {
                firestarterText.enabled = false;
                disableTrigger = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Starting FireStarter dialog when entering the trigger area
        if (collision.CompareTag("Player") && !disableTrigger)
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
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
