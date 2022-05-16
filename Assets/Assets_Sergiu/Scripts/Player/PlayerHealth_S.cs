using UnityEngine;
using System.Collections;

public class PlayerHealth_S : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private bool isInvincible;

    //Import of public methods from PlayerMovement_S script
    public PlayerMovement_S playerMovement;

    public SpriteRenderer playerSprite;

    public HealthBar_S healthBar;

    void Start()
    {
        reinitializePlayerHealth();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            takeDamage(20);
        }
    }

    [HideInInspector]
    public void takeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.setHealth(currentHealth);

            //Respawn
            if (currentHealth <= 0)
            {
                //Use of respawn() method from the PlayerMovement_S script
                playerMovement.respawnPlayer();
            }
            else
            {
                isInvincible = true;
                StartCoroutine(invincibilityFlash());
                StartCoroutine(invincible());
            }            
        }
        
    }

    [HideInInspector]
    public void reinitializePlayerHealth()
    {
        currentHealth = maxHealth;
        healthBar.setHealth(currentHealth);
    }

    [HideInInspector]
    public void killPlayer()
    {
        currentHealth = 0;
        healthBar.setHealth(currentHealth);
    }

    //Apply a flash effect on the player (graphics only) when invincible - after taking damage
    public IEnumerator invincibilityFlash()
    {
        while (isInvincible)
        {
            //Make player transparent
            playerSprite.color = new Color(1f, 1f, 1f, 0f);
            //Wait 0.3 seconds
            yield return new WaitForSeconds(0.16f);
            //Make player visible
            playerSprite.color = new Color(1f, 1f, 1f, 1f);
            //Wait 0.3 seconds
            yield return new WaitForSeconds(0.16f);
        }
    }

    //Dsiable player invicibility after 3 seconds
    public IEnumerator invincible()
    {
        yield return new WaitForSeconds(3f);
        isInvincible = false;
    }
}
