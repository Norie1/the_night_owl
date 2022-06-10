using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    
public string nameScene;

   void OnEnable()
   {
    SceneManager.LoadScene(nameScene);
   }
}
