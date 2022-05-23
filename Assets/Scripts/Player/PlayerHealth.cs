using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool isInvincible;

    public HealthBar healthBar;
    public SpriteRenderer playerSprite;

    public PlayerMovement playerMovement;
    public Inventory inventory;

    public static PlayerHealth instance;

    //Attribute used by enemies and pickups to know when to respawn
    //[HideInInspector]
    public bool playerDeath;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("PlayerHealth already initialized");
            return;
        }
        instance = this;
    }

    void Start()
    {
        playerDeath = false;

        ResetPlayerHealth();

        //Import of public methods and attributes from PlayerMovement script
        playerMovement = PlayerMovement.instance;
        inventory = Inventory.instance;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(50);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            HealPlayer(50);
        }
    }

    //[HideInInspector]
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            //Respawn
            if (currentHealth <= 0)
            {
                inventory.ResetWallet();
                StartCoroutine(RespawnPlayer());
            }
            else
            {
                isInvincible = true;
                StartCoroutine(InvincibilityFlash());
                StartCoroutine(Invincibility());
            }
        }
    }

    //Restore player health
    [HideInInspector]
    public void HealPlayer(int amount)
    {
        if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }

        healthBar.SetHealth(currentHealth);
    }

    //[HideInInspector]
    public IEnumerator RespawnPlayer()
    {
        playerDeath = true;
        ResetPlayerHealth();

        //Use of respawn() method from the PlayerMovement_S script
        playerMovement.RespawnPlayer();
        yield return new WaitForSeconds(1f);
        playerDeath = false;
    }

    //Apply a flash effect on the player (graphics only) when invincible (after taking damage)
    public IEnumerator InvincibilityFlash()
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

    //Disable player invicibility after 3 seconds
    public IEnumerator Invincibility()
    {
        yield return new WaitForSeconds(3f);
        isInvincible = false;
    }

    //[HideInInspector]
    public void ResetPlayerHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
}
