using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class LevelIntro_S : MonoBehaviour
{
    [SerializeField]
    private Text levelIntroText;

    public Dialog_S dialog;

    private bool isInRange;

    private PlayerMovement_S playerMovement;
    private DialogManager_S dialogManager;

    void Start()
    {
        //Import of public methods and attributes from PlayerMovement_S and DialogManager_S scripts
        playerMovement = PlayerMovement_S.instance;
        dialogManager = DialogManager_S.instance;

        playerMovement.freezePlayerMovement = true;

        StartCoroutine(LevelIntro());
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogManager.DisplayNextSentence();
        }
    }

    private IEnumerator LevelIntro()
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
