using UnityEngine;

public class deplacement : MonoBehaviour
{
    public float mouveSpeed

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
        
    void FixedUpdate()
    {
        float horizontaleMouvement = Input.GetAxis("Hozizontal") * mouveSpeed * Time.deltaTime;

        movePlayer(horizontaleMouvement);
    }

    void MovePlayer(float_horizontalement)
    {
        vector3 targetvelocity = new vector2(_horizontalementMouvement, rb.velocity.y);
        rb.velocity = Vector3.Smoothdamp(rb.velocity, targetvelocity, ref velocity, 05f);
    }


}
