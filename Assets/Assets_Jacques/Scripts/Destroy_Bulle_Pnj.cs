using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Bulle_Pnj : MonoBehaviour
{
    public Animator pnjAnimator;
    public bool inRange;

    void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.E))
        {
            pnjAnimator.SetBool("isTalking",true);
        }
        else if(!inRange)
        {
            pnjAnimator.SetBool("isTalking",false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inRange = false;
        }
       
    }
}
