using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    
public class isTalking : MonoBehaviour
{

    public Animator animator;
    public bool past = false ;
    public bool test = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            test = true;
        } 
         if(test == true && past == true ) {
            animator.SetBool("isTrigger", test);
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player"){        
         past = true;
        }
    }
}
   
