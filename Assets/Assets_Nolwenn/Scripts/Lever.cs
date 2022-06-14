using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class Lever : ObjectTrigger
{   
    /*public UnityEvent myEvents;
    [SerializeField] private bool activated;
    private bool isInRange;
    private Text interactUI;

    private void Start() {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !activated && isInRange)
        {
            activated = true;
            interactUI.enabled = false;
            myEvents.Invoke();
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
    */

    [SerializeField] private bool activated;

    protected override void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !activated && isInRange)
        {
            activated = true;
            interactUI.enabled = false;
            myEvents.Invoke();
        }
    }
}
