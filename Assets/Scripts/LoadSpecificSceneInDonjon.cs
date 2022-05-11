using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificSceneInDonjon : MonoBehaviour
{
    private void OnTriggerEnter2D ( Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //Scene2 -> premier donjon Jacques
            SceneManager.LoadScene("Level_Donjon1-Jacques");
        }
    }
}
