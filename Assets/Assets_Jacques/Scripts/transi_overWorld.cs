using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class transi_overWorld : MonoBehaviour
{  
    void OnTriggerEnter2D(Collider2D collision)
    {          
        if(collision.gameObject.tag == "Player" )
        {
            SceneManager.LoadScene("Level_Jacques");
        }
    }
}
