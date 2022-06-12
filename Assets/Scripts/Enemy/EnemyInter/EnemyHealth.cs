using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 250;
    public int currentHealth;
    
    public SpriteRenderer enemySprite;
    public GameObject objectToDestroy;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
		currentHealth -= damage;
		StartCoroutine(InvincibilityFlash());
		if (currentHealth <= 0)
		{
			Die();
		}
	}
	
	public void Die()
	{
		Destroy(objectToDestroy);
	}

     //Apply a flash effect on the player (graphics only) when invincible (after taking damage)
    public IEnumerator InvincibilityFlash()
    {
            //Make player transparent
            enemySprite.color = new Color(1f, 1f, 1f, 0f);
            //Wait 0.3 seconds
            yield return new WaitForSeconds(0.16f);
            //Make player visible
            enemySprite.color = new Color(1f, 1f, 1f, 1f);
            //Wait 0.3 seconds
            yield return new WaitForSeconds(0.16f);
    }
    
    
}
