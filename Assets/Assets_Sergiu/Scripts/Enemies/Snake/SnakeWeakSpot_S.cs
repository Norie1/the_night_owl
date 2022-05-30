using UnityEngine;

public class SnakeWeakSpot_S : MonoBehaviour
{
    public SpriteRenderer snakeSprite;
    public BoxCollider2D snakeCollider;

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
        if (PlayerHealth_S.instance.playerDeath)
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
