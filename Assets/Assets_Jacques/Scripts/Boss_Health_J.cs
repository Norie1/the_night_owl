using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Health_J : MonoBehaviour
{
    public int health = 500;
	
	public GameObject lootKey;
    public GameObject objectToDestroy;
    
    public void TakeDamage(int damage)
    {
		health -= damage;
		if (health <= 0)
		{			
			Transform t = GetComponent<Transform>();
			Instantiate(lootKey,t.position,t.rotation);
			Die();
		}
	}
	
	public void Die()
	{
		Destroy(objectToDestroy);
	}
}
