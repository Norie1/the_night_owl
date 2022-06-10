using UnityEngine;

public class EnnemyPatrol_A : MonoBehaviour
{
 
  public float speed;
  public Transform[] waypoint;

  public int damageOnCollision = 5; // dmg par defaut

  public SpriteRenderer graphic;
  private Transform target;
  private int destPoint;
  

  void Start(){
    
    //set du first WayPoint
  	target = waypoint[0];
  }

    void Update()
    {
	Vector3 dir = target.position - transform.position;
	transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);        
    
    //si l'ennemie est casiment arriver a sa destination ~ 0.3f on flip x si besoin
 	if (Vector3.Distance(transform.position, target.position) < 0.3f) {
		destPoint = (destPoint + 1) % waypoint.Length;
		target = waypoint[destPoint];
	    graphic.flipX = !graphic.flipX; 
  	}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.transform.gameObject.tag == "Player")
      {
        PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(damageOnCollision);
      }
    }
}
