using UnityEngine;

public class SnakeWeakSpot_S : MonoBehaviour
{
    public GameObject snake;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(snake);
            //Destroy(transform.parent.parent.gameObject);
        }
    }
}
