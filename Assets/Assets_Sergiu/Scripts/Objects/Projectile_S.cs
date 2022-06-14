using UnityEngine;

public class Projectile_S : MonoBehaviour
{
    public float Speed = 4.5f;

    //Float used to determine when to autodestroy the projectile
    private float time = 2;

    //Bool that is set when projectile is created in PlayerMovement_S script
    [HideInInspector]
    public bool flipPlayer;

    private void Update()
    {
        if (!flipPlayer)
        {
            transform.position += Speed * Time.deltaTime * transform.right;
        }
        else
        {
            transform.position += Speed * Time.deltaTime * -transform.right;
        }

        time -= Time.deltaTime;

        //Projectile destroyed after 2 seconds
        if (time < 0)
        {
            Destroy(gameObject);
        }
    }

    //Projectile destroyed when hitting a bat or a wall
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bat") || collision.CompareTag("Foundation"))
        {
            Destroy(gameObject);
        }
    }
}

