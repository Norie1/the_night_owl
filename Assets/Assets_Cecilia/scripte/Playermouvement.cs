using UnityEngine;
using System.Collections;

public class PlayerMouvement : MonoBehaviour
{
    public float mouveSpeed;
    public float jumpforce;
    public bool isJumping = false;
    public bool isGrounded;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        isGrounded = Physics2d.OverlapArea(groundCheckLeft.position,groundCheckRight.position);
        float horizontalMovement = Input.GetAxis("Horizontal") * mouveSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        MovePlayer(horizontalMovement);
        float charactervelocity = Matf;
        animator.Setfloat("speed", rb.velocity.x);
    }

    void MovePlayer(float _horizontalMovement)
    {
        vector3 targetvelocity = new vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.Smoothdamp(rb.velocity, targetvelocity, ref velocity, .05f);


        if (isJumping == true)
        {
            rb.Addforce(new Vector2(0f, jumpForce));
            isJumping = false; 
        }
    }


}
