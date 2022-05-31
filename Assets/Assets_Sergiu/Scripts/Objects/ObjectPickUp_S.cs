using UnityEngine;

public class ObjectPickup_S : MonoBehaviour
{
    public int checkpointID;

    private void Update()
    {
        //Verification of reached checkpoint
        bool activeRespawn = !RespawnManager_S.instance.checkpoints[checkpointID];

        //True if the player is dead
        bool playerDeath = PlayerHealth_S.instance.playerDeath;

        //Object restored on player death if already destroyed and checkpoint not yet reached
        if (playerDeath && activeRespawn)
        {
            RestoreObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Coin"))
            {
                Inventory_S.instance.AddCoins(1);
                RemoveObject();
            }   
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