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
        //Import of public methods and attributes from PlayerMovement_S and DialogManager_S scripts
        playerMovement = PlayerMovement.instance;
        dialogManager = DialogManager.instance;

        //playerMovement.freezePlayerMovement = true;

        StartCoroutine(StartLevelIntro());
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogManager.DisplayNextSentence();
        }
    }

    private IEnumerator StartLevelIntro()
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
