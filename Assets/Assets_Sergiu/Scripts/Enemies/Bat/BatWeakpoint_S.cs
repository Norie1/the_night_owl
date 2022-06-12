using UnityEngine;

public class BatWeakpoint_S : MonoBehaviour
{
    public SpriteRenderer batSprite;
    public BoxCollider2D batCollider;

    [SerializeField]
    private int checkpointID;

    private void Start()
    {
        batSprite.flipX = true;
    }

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
        if (collision.CompareTag("Projectile"))
        {
            RemoveEnemy();
        }
    }

    //Disables enemy collider and graphics
    public void RemoveEnemy()
    {
        batSprite.enabled = false;
        batCollider.enabled = false;
    }

    //Restores enemy collider and graphics
    public void RestoreEnemy()
    {
        batSprite.enabled = true;
        batCollider.enabled = true;
    }
}
