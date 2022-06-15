using UnityEngine;
using System.Collections;

public class PlayerHealth_S : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;

    private bool isInvincible;

    [SerializeField]
    private Dialog_S dialog;

    [HideInInspector]
    public bool postmortemDialog;

    public HealthBar_S healthBar;
    public SpriteRenderer playerSprite;

    private PlayerMovement_S playerMovement;

    public static PlayerHealth_S instance;

    //Attribute used by enemies, pickups and platforms to know when to respawn
    [HideInInspector]
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

        //Import of public methods and attributes from PlayerMovement_S script
        playerMovement = PlayerMovement_S.instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(50);
        }

        //Appuyer sur J ajoute 20HP au joueur
        if (Input.GetKeyDown(KeyCode.J))
        {
            HealPlayer(20);
        }
    }

    [HideInInspector]
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            //Respawn
            if (currentHealth <= 0)
            {
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

    public IEnumerator RespawnPlayer()
    {
        playerDeath = true;
        ResetPlayerHealth();

        playerMovement.RespawnPlayer();

        //Starting the postmortem dialog
        DialogManager_S.instance.StartDialog(dialog);

        //Bool used by DialogTrigger script in order to be able to advance in the dialog
        postmortemDialog = true;
        yield return new WaitForSeconds(1f);
        playerDeath = false;
    }

    //Apply a flash effect on the player (graphics only) when invincible - after taking damage
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

    [HideInInspector]
    public void ResetPlayerHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
}
