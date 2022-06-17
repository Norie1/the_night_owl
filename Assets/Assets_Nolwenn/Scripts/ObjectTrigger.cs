using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

abstract public class ObjectTrigger : MonoBehaviour
{
    public UnityEvent myEvents;
    protected bool isInRange;
    protected Text interactUI;
    public AudioClip soundEffect;

    // Start is called before the first frame update
    protected void Start()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    // Update is called once per frame
    abstract protected void Update();


    protected void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            isInRange = false;
            interactUI.enabled = false;
        }
    }
}
