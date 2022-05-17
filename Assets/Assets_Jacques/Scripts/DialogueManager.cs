using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public static DialogueManager instance;

    private void Awake() 
    {
        if(instance != null){
        Debug.LogWarning("il y a plus d'une instance d eDialogueManager dans la scene");
        return;
    }
    instance = this;
}

public void StartDialogue(Dialogue dialogue)
{
    nameText.text = dialogue.name;
}
}
