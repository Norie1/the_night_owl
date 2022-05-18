using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    private Transform target;

    [SerializeField] private int index;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, target.position) < 0.3f)
        {
            index = (index+1) % waypoints.Length;
            target = waypoints[index];
        }

    

    }
}