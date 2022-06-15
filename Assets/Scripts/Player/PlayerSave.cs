using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerSave : MonoBehaviour
{
    public Transform pTransform;
    public PlayerFireBall pfb;
    public PlayerHealth ph;

    private Player_Info info;

    private UnityEngine.SceneManagement.Scene scene;    

    public void SaveScene()
    {
        Debug.Log("Save");
        SaveSystem.SavePlayer(ph, pfb , SceneManager.GetActiveScene().name , pTransform);
    }

    public void LoadScene()
    {
      if(SceneManager.GetActiveScene().name == "Menu")
      {
        GameObject player = GameObject.Find("Player");
        Destroy(player);
      }
        scene = SceneManager.GetActiveScene();
        info = SaveSystem.LoadPlayer();
        Debug.Log(info.scene);
        Vector3 pos;
        pos.x = info.transform[0];
        pos.y = info.transform[1];
        pos.z = info.transform[2];
        pTransform.position = pos;
        pfb.canThrowFireBall = info.canThrow;
        ph.currentHealth = info.health;
        //AsyncOperation asyncOp =        
        StartCoroutine(tempo());
        /*
        player = GameObject.Find("Player");
        PlayerFireBall pfbb = player.GetComponent<PlayerFireBall>();
        pfbb = pfb;
        PlayerHealth phh = player.GetComponent<PlayerHealth>();
        phh = ph;
        player.transform.position = pTransform.position;
        
        //targetedScene = SceneManager.GetActiveScene();
        Debug.Log("avant");
        SceneManager.MoveGameObjectToScene(player,SceneManager.GetSceneByName(info.scene));
        //SceneManager.SetActiveScene(targetedScene);
        
        Debug.Log("après");
      //  asyncOp.allowSceneActivation = false;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(info.scene));
        SceneManager.UnloadSceneAsync("Menu");
        */
      /*  while(!asyncOp.isDone)
        {
            loadText.text = "Loading progress : " + (asyncOp.progress * 100 + " %");
            if(asyncOp.progress >= 0.9f)
            {
                
            if(Input.GetKeyDown(KeyCode.Space))
            {
                asyncOp.allowSceneActivation = true;
            }
            }
        }*/
        //SceneManager.SetActiveScene(scene);
    }

    IEnumerator tempo()
    {

//a revoir

        SceneManager.LoadScene(info.scene,UnityEngine.SceneManagement.LoadSceneMode.Additive);
        yield return new WaitForSeconds(4f);
        scene = SceneManager.GetActiveScene();
        GameObject player = GameObject.Find("Player");
        PlayerFireBall pfbb = player.GetComponent<PlayerFireBall>();
        pfbb = pfb;
        PlayerHealth phh = player.GetComponent<PlayerHealth>();
        phh = ph;
        player.transform.position = pTransform.position;
        
        //targetedScene = SceneManager.GetActiveScene();
        Debug.Log("avant");
        SceneManager.MoveGameObjectToScene(player,SceneManager.GetSceneByName(info.scene));
        //SceneManager.SetActiveScene(targetedScene);
        
        Debug.Log("après");
      //  asyncOp.allowSceneActivation = false;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(info.scene));
        SceneManager.UnloadSceneAsync("Menu");
    }

}
