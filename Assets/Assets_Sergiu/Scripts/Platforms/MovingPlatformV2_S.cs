using UnityEngine;

public class MovingPlatformV2_S : MonoBehaviour
{
    public int speed;

    public Transform destination;

    private bool isReached;
    private bool destinationReached;
    
    void Update()
    {
        //Moving the platform when the player steps on it and until the destination is reached
        if (isReached && !destinationReached)
        {
            Vector3 direction = destination.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            //Stopping the moving platform when destination is reached
            if (Vector2.Distance(transform.position, destination.position) < 0.03f)
            {
                destinationReached = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isReached = true;
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
