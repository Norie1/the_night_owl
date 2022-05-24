using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Health : MonoBehaviour
{
    public int health = 400;
    
    public GameObject objectToDestroy;
    
    public bool isInvulnerable = false;
    
    public void TakeDamage(int damage)
    {
		if (isInvulnerable)
		{
			GetComponent<Animator>().SetBool("isAttack", false);
			return;
		}
		
		health -= damage;
		GetComponent<Animator>().SetBool("isAttack", true);
		
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
		Destroy(objectToDestroy);
	}
}
