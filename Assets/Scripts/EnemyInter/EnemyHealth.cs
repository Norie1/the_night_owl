using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 250;
    public int currentHealth;
    
    public GameObject objectToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
		currentHealth -= damage;
		
		if (currentHealth <= 0)
		{
			Die();
		}
	}
	
	public void Die()
	{
		Destroy(objectToDestroy);
	}
    
    
}
