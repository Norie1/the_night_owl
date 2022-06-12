using UnityEngine;

public class BatPatrolV2_S : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    public int damageOnCollision;

    private Transform target;
    private int destPoint;

    public SpriteRenderer enemySprite;

    //Local bool used to establishe the direction of the enemy movement
    private bool movingForward;

    void Start()
    {
        //Initialization of the first target/destination
        destPoint = waypoints.Length - 1;
        target = waypoints[destPoint];
        
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;

        //Bat movement (normalization of the movement vector)
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);


        //When the enemy is close to the target
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            //If moving forward
            if (movingForward)
            {
                destPoint++;

                //If last waypoint is reached
                if (destPoint == waypoints.Length)
                {
                    //Starting backward movement
                    destPoint = waypoints.Length - 2;
                    enemySprite.flipX = true;
                    movingForward = false;
                }
            }

            //If moving backward
            else
            {
                destPoint--;

                //If first waypoint is reached
                if (destPoint == -1)
                {
                    //Starting forward movement
                    destPoint = 1;
                    enemySprite.flipX = false;
                    movingForward = true;
                }
            }
            target = waypoints[destPoint];
        }


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
