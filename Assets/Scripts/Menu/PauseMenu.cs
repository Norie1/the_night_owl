using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsWindow;
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
                CloseSettingsWindow();
            }
            else{
                Pause();
            }
        }
    }

    void Pause()
    {
        //Disable PlayerMovement
       PlayerMov.instance.enabled = false;
       //Activate pauseMenu
       pauseMenuUI.SetActive(true);
       //Stop time
       Time.timeScale = 0;
       //Change game status
       gameIsPaused = true;
    }

    public void Resume()
    {
        PlayerMov.instance.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void OpenSettingsWindow()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene("Menu");
    }
}
