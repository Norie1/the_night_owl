using UnityEngine;

public class Moonwalk_S : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    public SpriteRenderer playerSprite;
    public Animator animator;

    private Transform target;
    private int destPoint;
    private bool isDancing;


    void Start()
    {
        //Initialization of the first target/destination
        target = waypoints[0];
        destPoint = 0;
    }

    void Update()
    {
        if (isDancing)
        {
            Vector3 direction = target.position - transform.position;

            //Player movement (normalization of the movement vector)
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            //When the enemy is close to the target
            if (Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                //target = next target
                destPoint = (destPoint + 1) % waypoints.Length;
                target = waypoints[destPoint];

                //Player flip
                playerSprite.flipX = !playerSprite.flipX;
            }
        }
    }
}
