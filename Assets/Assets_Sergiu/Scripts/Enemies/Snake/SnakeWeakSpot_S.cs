using UnityEngine;

public class SnakeWeakSpot_S : MonoBehaviour
{
    public SpriteRenderer snakeSprite;
    public BoxCollider2D snakeCollider;
    public int checkpointID;

    //Killed by the player on collision with the weakspot
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RemoveEnemy();
        }
    }

    private void Update()
    {
        //Verification of reached checkpoint
        bool activeRespawn = !RespawnManager_S.instance.checkpoints[checkpointID];

        //Enemy restored on player death if already destroyed and checkpoint not yet reached
        if (PlayerHealth_S.instance.playerDeath && activeRespawn)
        {
            RestoreEnemy();
        }
    }

    //Disables object collider and graphics
    [HideInInspector]
    public void RemoveEnemy()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        snakeSprite.enabled = false;
        snakeCollider.enabled = false;
    }

    //Restores object collider and graphics
    [HideInInspector]
    public void RestoreEnemy()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        snakeSprite.enabled = true;
        snakeCollider.enabled = true;
    }
}
