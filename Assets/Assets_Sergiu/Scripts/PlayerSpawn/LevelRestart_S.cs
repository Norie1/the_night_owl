using UnityEngine;

//Necessary import for level restart
using UnityEngine.SceneManagement;

public class LevelRestart_S : MonoBehaviour
{
    private void Update()
    {
        //Restart level when "R" key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
