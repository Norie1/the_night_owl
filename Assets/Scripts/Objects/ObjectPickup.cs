using UnityEngine;

public class ObjectPickUp : MonoBehaviour
{
    public static ObjectPickUp instance;

    public int checkpointID;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("ObjectPickup already initiliazed");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        // Verification of reached checkpoint
        bool activeRespawn = !RespawnManager.instance.checkpoints[checkpointID];

        // True if the player is dead
        bool playerDeath = PlayerHealth.instance.playerDeath;

        // Object restored on player death if already destroyed and checkpoint not yet reached
        if (playerDeath && activeRespawn)
        {
            RestoreObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }

    //Disables object collider and graphics
    [HideInInspector]
    public void RemoveObject()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    //Restores object collider and graphics
    [HideInInspector]
    public void RestoreObject()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
