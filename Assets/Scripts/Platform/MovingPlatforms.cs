using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    protected int index;

    private void Start()
    {
        index = 0;
    }

    private void Update()
    {
        Vector3 direction = waypoints[index].position - transform.position;

        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, waypoints[index].position) < 0.03f)
        {
            index = (index + 1) % waypoints.Length;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
