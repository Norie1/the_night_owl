using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class LevelIntro : MonoBehaviour
{
    [SerializeField]
    private Text levelIntroText;

    public Dialog dialog;

    private bool isInRange;

    private PlayerMovement playerMovement;
    private DialogManager dialogManager;

    void Start()
    {
        // Import of public methods and attributes from PlayerMovement and DialogManager scripts
        playerMovement = PlayerMovement.instance;
        dialogManager = DialogManager.instance;

        //playerMovement.freezePlayerMovement = true;

        StartCoroutine(Intro());
    }

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.E))
        {
            dialogManager.DisplayNextSentence();
        }
    }

    private IEnumerator Intro()
    {
        levelIntroText.enabled = true;
        yield return new WaitForSeconds(3f);
        levelIntroText.enabled = false;
        dialogManager.StartDialog(dialog);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
