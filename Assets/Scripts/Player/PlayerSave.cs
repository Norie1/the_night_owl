using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSave : MonoBehaviour
{
    public Transform pTransform;
    public PlayerFireBall pfb;
    public PlayerHealth ph;
    private UnityEngine.SceneManagement.Scene scene;

    public void SaveScene()
    {
        Debug.Log("Save");
        SaveSystem.SavePlayer(ph, pfb , SceneManager.GetActiveScene().name , pTransform);
    }

    public void LoadScene()
    {
        Debug.Log("Load");
        Player_Info info = SaveSystem.LoadPlayer();

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(info.scene);

        asyncOp.allowSceneActivation = false;
        Vector3 pos;
        pos.x = info.transform[0];
        pos.y = info.transform[1];
        pos.z = info.transform[2];
        pTransform.position = pos;
        pfb.canThrowFireBall = info.canThrow;
        ph.currentHealth = info.health;
        while(!asyncOp.isDone)
        {
            if(asyncOp.progress >= 0.9f)
            {
                Debug.Log("do you want to go in Scene, press e to go");
             if(Input.GetKeyDown(KeyCode.Space))
             {
                asyncOp.allowSceneActivation = true;
            }
            }
        }
                
        SceneManager.SetActiveScene(scene);

    }

}
