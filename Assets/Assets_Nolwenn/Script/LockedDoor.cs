using UnityEngine;
using UnityEngine.UI;

public class LockedDoor : MonoBehaviour
{
    public bool isInRange;
    public bool openable;
    private Text interactUI;

    private void Start() {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    private void Update() {
        if(isInRange && Input.GetKeyDown(KeyCode.E) && openable)
        {
            interactUI.enabled = false;
            Destroy(transform.parent.gameObject);
        }
    }

    public void makeOpenable()
    {
        openable = true;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            isInRange = false;
            interactUI.enabled = false;
        }
    }
}
