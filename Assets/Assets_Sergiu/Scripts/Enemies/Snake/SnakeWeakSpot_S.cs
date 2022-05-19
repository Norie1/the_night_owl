using UnityEngine;

public class SnakeWeakSpot_S : MonoBehaviour
{
    //Killed by the player on collision with the weakspot
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
