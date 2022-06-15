using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerSave : MonoBehaviour
{
    private Player_Info info;

    private UnityEngine.SceneManagement.Scene scene;    

    public void SaveScene()
    {
        Debug.Log("Save");
        SaveSystem.SavePlayer(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene()
    {
        info = SaveSystem.LoadPlayer();
        SceneManager.LoadScene(info.scene-1,UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
