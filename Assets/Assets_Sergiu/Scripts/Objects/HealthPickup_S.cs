using UnityEngine;

public class HealthPickup_S : MonoBehaviour
{
    [SerializeField]
    private int checkpointID;

    private bool playerDeath;
    private bool activeRespawn;

    private PlayerHealth_S playerHealth;

    private void Start()
    {
        playerHealth = PlayerHealth_S.instance;
    }

    private void Update()
    {
        //Verification of reached checkpoint
        activeRespawn = !RespawnManager_S.instance.checkpoints[checkpointID];

        //True if the player is dead
        playerDeath = playerHealth.playerDeath;

        //Object restored on player death if already destroyed and checkpoint not yet reached
        if (playerDeath && activeRespawn)
        {
            RestoreObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !playerHealth.IsFullLife())
        {
            playerHealth.HealPlayer(50);
            RemoveObject();
        }
    }

    //Disables object collider and graphics
    public void RemoveObject()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    //Restores object collider and graphics
    public void RestoreObject()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}