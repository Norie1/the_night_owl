using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassageBossLevel : MonoBehaviour
{
	public string NameOfScene;
	
	public void GoToTheLevel()
	{
		SceneManager.LoadScene(NameOfScene);
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		GoToTheLevel();
	}
}
