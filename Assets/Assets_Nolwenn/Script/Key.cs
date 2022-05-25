using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public GameObject door;
    public bool isInRange;
    private Text interactUI;

    private void Start() {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       if(isInRange && Input.GetKeyDown(KeyCode.E))
       {
           door.GetComponent<LockedDoor>().openable = true;
           interactUI.enabled = false;
           Destroy(gameObject);
       }
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
