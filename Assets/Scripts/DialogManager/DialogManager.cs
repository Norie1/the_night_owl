using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Image dialogBox;
    public Text nameText;
    public Text dialogText;

    private Queue<string> queue;

    [HideInInspector]
    public bool dialogStarted;

    public static DialogManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("DialogManager already initiated.");
            return;
        }
        instance = this;
    }

    void Start()
    {
        queue = new Queue<string>();

        // Disabling the graphic representation of the dialog box
        dialogBox.enabled = false;
        nameText.enabled = false;
        dialogText.enabled = false;
    }

    // Method called by the DialogueTrigger and LevelIntro scripts
    public void StartDialog(Dialog dialog)
    {
        nameText.text = dialog.name;

        queue.Clear();

        foreach (string sentence in dialog.sentences)
        {
            queue.Enqueue(sentence);
        }

        // Activating the graphic representation of the dialog box
        dialogBox.enabled = true;
        nameText.enabled = true;
        dialogText.enabled = true;

        //Bool used by the DialogTrigger script
        dialogStarted = true;

        // Blocking player movement while dialog is active
        PlayerMovement.instance.freezePlayerMovement = true;

        DisplayNextSentence();
    }

    public bool DisplayNextSentence()
    {
        if (queue.Count == 0)
        {
            EndDialog();
            return false;
        }

        string sentence = queue.Dequeue();
        dialogText.text = sentence;
        return true;
    }

    public void EndDialog()
    {
        // Disactivating the graphic representation of the dialog box
        dialogBox.enabled = false;
        nameText.enabled = false;
        dialogText.enabled = false;

        // Unblocking player movement
        PlayerMovement.instance.freezePlayerMovement = false;

        //Bool used by the DialogTrigger script
        dialogStarted = false;
    }
}
