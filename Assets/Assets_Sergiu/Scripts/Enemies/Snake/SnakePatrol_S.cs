using UnityEngine;

public class SnakePatrol_S : MonoBehaviour
{

    public float speed;
    public Transform[] waypoints;

    public int damageOnCollision;

    private Transform target;
    private int destPoint;

    public SpriteRenderer enemySprite; 

    void Start()
    {
        //Initialization of the first target/destination
        target = waypoints[0];
        destPoint = 0;
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;

        //Snake movement (normalization of the movement vector)
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        //When the enemy is close to the target
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            //target = next target
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];

            //Snake flip
            enemySprite.flipX = !enemySprite.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth_S.instance.TakeDamage(damageOnCollision);
        }
    }
}
