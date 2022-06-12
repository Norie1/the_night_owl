using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Key_J : MonoBehaviour
{
    public UnityEvent myEvents;
    private bool isInRange;

    void Update()
    {
       if(isInRange && Input.GetKeyDown(KeyCode.E))
       {
            Destroy(transform.parent.gameObject);
       }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
            isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
            isInRange = false;
    }
}
