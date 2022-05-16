using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject door;
    public bool isInRange;

    // Update is called once per frame
    void Update()
    {
       if(isInRange && Input.GetKeyDown(KeyCode.E))
       {
           door.GetComponent<LockedDoor>().openable = true;
           Destroy(gameObject);
       }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
