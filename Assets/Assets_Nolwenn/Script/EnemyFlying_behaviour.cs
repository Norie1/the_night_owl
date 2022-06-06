using UnityEngine;

public class EnemyFlying_behaviour : MonoBehaviour
{
    public float speed, checkRadius;
    //public int health = 20;
    public int damage = 5;

    private Rigidbody2D rb;
    private Animator anim;
    public Transform checkRoof, checkGround, checkSide;
    public LayerMask layerMask;
    //[SerializeField] 
    private bool facingLeft = true, touchRoof, touchGround, touchSide;

    private float dirX = -1, dirY = 0.25f;
    private Vector3 rotation = new Vector3(0,180,0);
    private bool dead;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    /*
    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(5);
        }
    } */

    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, dirY) * speed * Time.deltaTime;
        DetectCollision();
    }

    void DetectCollision()
    {
        touchRoof = Physics2D.OverlapCircle(checkRoof.position, checkRadius, layerMask);
        touchGround = Physics2D.OverlapCircle(checkGround.position, checkRadius, layerMask);
        touchSide = Physics2D.OverlapCircle(checkSide.position, checkRadius, layerMask);
        HitLogic();
    }

    void HitLogic()
    {
        if(touchSide)
        {
            Flip();
        }
        if(touchRoof)
        {
            dirY = -Mathf.Abs(dirY);
        }
        if(touchGround)
        {
            /*
            if(dead)
            {
                //DisableScript();
                Destroy(transform);
            } */
            dirY = Mathf.Abs(dirY);
        }
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        transform.Rotate(rotation);
        dirX = -dirX;
    }

    /*
    public void TakeDamage(int value)
    {
        if((health-=value) <= 0)
        {
            Debug.Log("Flying Enemy died.");
            anim.SetTrigger("Death");
            dead = true;
            dirX = 0;
            dirY = -1;
            //rb.velocity = Vector3.zero;
            
            //enabled = false;

        }
    } */

    /*
    private void DisableScript()
    {
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().isTrigger = true;
        enabled = false;
    } */

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(checkRoof.position, checkRadius);
        Gizmos.DrawWireSphere(checkGround.position, checkRadius);
        Gizmos.DrawWireSphere(checkSide.position, checkRadius);
    }
}
