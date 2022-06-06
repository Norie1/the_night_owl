using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager_S : MonoBehaviour
{
    public Image dialogBox;
    public Text nameText;
    public Text dialogText;

    private Queue<string> queue;

    [HideInInspector]
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

        //Disabling the graphic representation of the dialog box
        dialogBox.enabled = false;
        nameText.enabled = false;
        dialogText.enabled = false;
    }

    //Method called by the DialogueTrigger_S and LevelIntro_S scripts
    public void StartDialog(Dialog_S dialog)
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

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (queue.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = queue.Dequeue();
        dialogText.text = sentence;
    }

    public void EndDialog()
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
