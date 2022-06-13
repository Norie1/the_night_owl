using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_Health_A : MonoBehaviour
{
    public int health = 400;
    
    public string nameScene;
    
    public GameObject objectToDestroy;
    
    public bool isInvulnerable = false;
    
    public void TakeDamage(int damage)
    {
		if (isInvulnerable)
		{
			return;
		}
		
		health -= damage;
		
		if (health <= 200)
		{
			GetComponent<Animator>().SetBool("isEnraged", true);
		}
		
		if (health <= 0)
		{
			Die();
		}
	}
	
	public void Die()
	{
		SceneManager.LoadScene(nameScene);
		Destroy(objectToDestroy);
		
	}
}
