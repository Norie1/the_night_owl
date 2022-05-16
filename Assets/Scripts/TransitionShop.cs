using UnityEngine;
using UnityEngine.SceneManagement;

public class transition_Shop : MonoBehaviour
{
     public string sceneName;
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
