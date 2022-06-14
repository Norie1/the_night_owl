using UnityEngine;

public class ActivableMovingPlatform : MovingPlatforms
{
    //public float speed = 2;
    //public Transform[] waypoints;
    [SerializeField] private bool activated;

    //[SerializeField] private int index;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
        index = 0;
    }

    // Update is called once per frame
    private void Update()
    {

        //Vector2 dir = target.position - transform.position;
        //transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if(!activated)
        { //Platform's not activated, do nothing
            return;
        }

        if (Vector2.Distance(transform.position, waypoints[index].position) < 0.3f)
        { //If target is reached, select next one to move to
            index = (index+1) % waypoints.Length;
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
