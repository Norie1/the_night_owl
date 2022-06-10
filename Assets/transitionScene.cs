using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class transitionScene : MonoBehaviour
{
public string nameScene;
void OnEnable()
{
 SceneManager.LoadScene(nameScene, LoadSceneMode.Additive);}
}
