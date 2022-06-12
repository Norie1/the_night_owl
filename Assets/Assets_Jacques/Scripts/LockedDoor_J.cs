using UnityEngine;
using UnityEngine.UI;

public class LockedDoor_J : MonoBehaviour
{
    public bool isInRange;
    public bool openable;

    private void Update() {
        if(isInRange && Input.GetKeyDown(KeyCode.E) && openable)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public void makeOpenable()
    {
        openable = true;
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
