using UnityEngine;

public class MovingPlatform_S : MonoBehaviour
{
    public float speed;

    public int startingPoint;
    public Transform[] waypoints;

    private int index;

    private void Start()
    {
        index = startingPoint - 1;
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
}
