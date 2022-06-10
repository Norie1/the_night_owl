using UnityEngine;
using UnityEngine.UI;

public class FireStarter_S : MonoBehaviour
{
    [SerializeField]
    private Text firestarterText;

    public Dialog_S dialog;

    private bool isInRange;

    private PlayerMovement_S playerMovement;
    private DialogManager_S dialogManager;

    void Start()
    {
        //Import of public methods and attributes from PlayerMovement_S and DialogManager_S scripts
        playerMovement = PlayerMovement_S.instance;
        dialogManager = DialogManager_S.instance;
    }

    private void Update()
    {
        bool dialogFinished = false;
        //Starting FireStarter dialog when entering the trigger area
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogFinished = !dialogManager.DisplayNextSentence();
        }

        //Activating the on-screen FireStarterText when the dialog is finished
        if (isInRange && dialogFinished)
        {
            firestarterText.enabled = true;
        }

        //Disabling the on-screen FireStarterText when pushing the F button
        if (isInRange && Input.GetKeyDown(KeyCode.F))
        {
            firestarterText.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogManager.StartDialog(dialog);
            playerMovement.freezePlayerMovement = true;
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
