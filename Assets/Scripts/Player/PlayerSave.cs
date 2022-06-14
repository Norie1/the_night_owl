using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSave : MonoBehaviour
{
    public Transform pTransform;
    public PlayerFireBall pfb;
    public PlayerHealth ph;
    private string titleScene;
    private UnityEngine.SceneManagement.Scene scene;

    public void SaveScene()
    {
        SaveSystem.SavePlayer(ph, pfb , titleScene , pTransform);
    }

    public void LoadScene()
    {
        Player_Info info = SaveSystem.LoadPlayer();

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(scene.name,LoadSceneMode.Additive);
        Vector3 pos;
        pos.x = info.transform[0];
        pos.y = info.transform[1];
        pos.z = info.transform[2];
        pTransform.position = pos;
        pfb.canThrowFireBall = info.canThrow;
        ph.currentHealth = info.health;
        scene = SceneManager.GetSceneByName(titleScene);
        while(!asyncOp.isDone)
        {
            if(asyncOp.progress >= 0.9f)
            {
                Debug.Log("do you want to go in Scene, press e to go");
                asyncOp.allowSceneActivation = true;
            }
        }
        SceneManager.SetActiveScene(scene);

    }

}
