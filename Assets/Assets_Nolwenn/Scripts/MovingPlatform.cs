using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2;
    public Transform[] waypoints;
    private Transform target;
    [SerializeField] private bool activated;

    [SerializeField] private int index;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {

        //Vector2 dir = target.position - transform.position;
        //transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if(!activated)
        {
            return;
        }

        if (Vector2.Distance(transform.position, target.position) < 0.3f)
        {
            index = (index+1) % waypoints.Length;
            target = waypoints[index];
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[index].position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
        
    }

    public void activateMovement()
    {
        activated = true;
    }
}
