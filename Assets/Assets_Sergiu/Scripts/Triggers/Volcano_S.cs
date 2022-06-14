using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Volcano_S : MonoBehaviour
{
    [SerializeField]
    private Text lavaText;

    public Dialog_S dialog;

    private bool isInRange;
    private bool dialogFinished;

    private DialogManager_S dialogManager;

    void Start()
    {
        //Import of public methods attributes from DialogManager_S script
        dialogManager = DialogManager_S.instance;
    }

    private void Update()
    {
        
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !dialogFinished)
        {
            dialogFinished = !dialogManager.DisplayNextSentence();

            if (dialogFinished)
            {
                StartCoroutine(DisplayMessage());
            }
        }

        if (PlayerHealth_S.instance.playerDeath)
        {
            lavaText.enabled = false;
        }
    }

    private IEnumerator DisplayMessage()
    {
        lavaText.enabled = true;
        yield return new WaitForSeconds(3f);
        lavaText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Starting Volcano dialog when entering the trigger area
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            dialogManager.StartDialog(dialog);
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
