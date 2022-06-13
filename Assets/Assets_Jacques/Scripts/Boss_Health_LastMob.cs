using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Health_LastMob : MonoBehaviour
{
    public int health = 500;
	public SpriteRenderer  enemySprite;
    public GameObject objectToDestroy;
    
    public void TakeDamage(int damage)
    {
		health -= damage;
		StartCoroutine(InvincibilityFlash());
		if (health <= 0)
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
