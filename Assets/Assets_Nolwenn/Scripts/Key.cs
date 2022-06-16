using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Key : ObjectTrigger
{
    /*public UnityEvent myEvents;
    private bool isInRange;
    private Text interactUI;

    private void Start() {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       if(isInRange && Input.GetKeyDown(KeyCode.E))
       {
           myEvents.Invoke();
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
    } */

    override protected void Update()
    {
        if(isInRange && Input.GetKeyDown(KeyCode.E))
       {
        AudioManager.instance.PlayClipAt(soundEffect, transform.position);
        myEvents.Invoke();
        interactUI.enabled = false;
        Destroy(gameObject);
       }
    }
}
