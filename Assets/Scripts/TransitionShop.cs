using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class transition_Shop : MonoBehaviour
{
    public bool trigger = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);           
        if(collision.gameObject.tag == "Player" )
        {
            trigger = true;
            SceneManager.LoadScene("Level_Shop");
        }
    }
}
