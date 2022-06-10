using UnityEngine;

public class SnakeWeakSpot_S : MonoBehaviour
{
    public SpriteRenderer snakeSprite;
    public BoxCollider2D snakeCollider;

    [SerializeField]
    private int checkpointID;  

    private void Update()
    {
        //Verification of reached checkpoint
        bool activeRespawn = !RespawnManager_S.instance.checkpoints[checkpointID];

        //True if the player is dead
        bool playerDeath = PlayerHealth_S.instance.playerDeath;

        //Enemy restored on player death if already destroyed and checkpoint not yet reached
        if (playerDeath && activeRespawn)
        {
            RestoreEnemy();
        }
    }

    //Killed by the player on collision with the weakspot
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RemoveEnemy();
        }
    }

    //Disables object collider and graphics
    public void RemoveEnemy()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        snakeSprite.enabled = false;
        snakeCollider.enabled = false;
    }

    //Restores object collider and graphics
    public void RestoreEnemy()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        snakeSprite.enabled = true;
        snakeCollider.enabled = true;
    }
}
