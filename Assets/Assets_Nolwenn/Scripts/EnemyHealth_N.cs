using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth_N : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float invicibilityTime = 3f;
    [SerializeField] bool isInvincible;


    public SpriteRenderer enemySprite;
    public BoxCollider2D enemyCollider;
    public Rigidbody2D enemyRb;
    public Enemy_behaviour enemyScript;
    public EnemyFlying_behaviour flyingScript;

    [SerializeField]
    private int checkpointID; 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        
        //Verification of reached checkpoint
        bool activeRespawn = !RespawnManager.instance.checkpoints[checkpointID];

        //True if the player is dead
        bool playerDeath = PlayerHealth.instance.playerDeath;

        //Enemy restored on player death if already destroyed and checkpoint not yet reached
        if (playerDeath && activeRespawn)
        {
            RestoreEnemy();
        }
    }
    
    public void TakeDamage(int damage)
    {   
        if (!isInvincible){ 
            currentHealth -= damage;
		
		    if (currentHealth <= 0)
		    {
			Die();
		    }
            else
            {
                isInvincible = true;
                StartCoroutine(InvincibilityFlash());
                StartCoroutine(Invincibility());
            }
        }
		
	}
    
	
	public void Die()
	{
        enemySprite.enabled = false;
        enemyRb.bodyType = RigidbodyType2D.Static;
        enemyCollider.isTrigger = true;

        if(enemyScript != null) { enemyScript.enabled = false; }
        if(flyingScript != null) { flyingScript.enabled = false; }
        
        
	}

    public void RestoreEnemy()
    {
        enemySprite.enabled = true;
        enemyRb.bodyType = RigidbodyType2D.Dynamic;
        enemyCollider.isTrigger = false;

        if(enemyScript != null) { enemyScript.enabled = true; }
        if(flyingScript != null) { flyingScript.enabled = true; }
    }

    //Apply a flash effect on the enemy (graphics only) when invincible (after taking damage)
    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            //Make enemy transparent
            enemySprite.color = new Color(1f, 1f, 1f, 0f);
            //Wait 0.3 seconds
            yield return new WaitForSeconds(0.16f);
            //Make player visible
            enemySprite.color = new Color(1f, 1f, 1f, 1f);
            //Wait 0.3 seconds
            yield return new WaitForSeconds(0.16f);
        }
    }

    //Disable player invicibility after 3 seconds
    public IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(invicibilityTime);
        isInvincible = false;
    }
}
