using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager_S : MonoBehaviour
{
    public Image dialogBox;
    public Text nameText;
    public Text dialogText;

    private Queue<string> queue;

    //[HideInInspector]
    public bool dialogStarted;

    public static DialogManager_S instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    void Start()
    {
        queue = new Queue<string>();

        dialogBox.enabled = false;
        nameText.enabled = false;
        dialogText.enabled = false;
    }

    //Method called by the DialogueTrigger_S script
    public void StartDialogue(Dialog_S dialog)
    {
        nameText.text = dialog.name;

        queue.Clear();

        foreach (string sentence in dialog.sentences)
        {
            queue.Enqueue(sentence);
        }

        //Activating the graphic representation of the dialog box
        dialogBox.enabled = true;
        nameText.enabled = true;
        dialogText.enabled = true;

        dialogStarted = true;

        //Blocking player movement while dialog is active
        PlayerMovement_S.instance.freezePlayerMovement = true;

        DisplayNextSentece();
    }

    public void DisplayNextSentece()
    {
        if (queue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = queue.Dequeue();
        dialogText.text = sentence;
    }

    public void EndDialogue()
    {
        //Disactivating the graphic representation of the dialog box
        dialogBox.enabled = false;
        nameText.enabled = false;
        dialogText.enabled = false;

        //Unblocking player movement
        PlayerMovement_S.instance.freezePlayerMovement = false;

        dialogStarted = false;
    }
}
