using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOutro_S : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
