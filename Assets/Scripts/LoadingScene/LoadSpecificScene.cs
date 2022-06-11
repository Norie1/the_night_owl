using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    private void OnTriggerEnter2D ( Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" )
        {
            //Scene2 -> premier donjon Jacques
            SceneManager.LoadScene(sceneName);
        }
    }
}
