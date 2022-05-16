using UnityEngine;

public class SnakePatrol_S : MonoBehaviour
{

    public float speed;
    public Transform[] waypoints;

    public int damageOnCollision;

    private Transform target;
    private int destPoint;

    public SpriteRenderer snake; 

    void Start()
    {
        //Initialization of the first target/destination
        target = waypoints[0];
        destPoint = 0;
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;

        //Snake movement (normalization of the movement vector)
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //When the enemy is close to the target
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            //target = next target
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];

            //Snake flip
            snake.flipX = !snake.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth_S playerHealth = collision.transform.GetComponent<PlayerHealth_S>();
            playerHealth.takeDamage(damageOnCollision);
        }
    }
}