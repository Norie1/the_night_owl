using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float moveSpeed;
  public float jumpForce; 
  public bool isGrounded;
  public bool isJumping;
  
  //yo les boys
 
 //var a link dans la scene (ici 2 point placer sur les pieds du joueurs)
  public Transform groundedCheckLeft;
  public Transform groundedCheckRight;
  
  public Rigidbody2D rb;
  public SpriteRenderer spriteRenderer;
  public Animator animator; 
 //initialise un vecteur3 a zero, unity même en 2D a besoin d'un vecteur 3
 //parfois on devras parfois crée un vecteur 2 et cast un vecteur 3 a celui ci, unity va mettre la position z a zero,
 //il n'y a pas vraient de 2d unique
  private Vector3 velocity = Vector3.zero;

	void FixedUpdate()
    {
		isGrounded = Physics2D.OverlapArea(groundedCheckLeft.position, groundedCheckRight.position);
  //calcul d'un déplacement set d'une velocity pour pouvoir avoir un mouvement continue 
		float horizontalMovoment = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
  		
  		if(Input.GetButtonDown("Jump") && isGrounded){
        //si Jump alors boolean jump = true
  			isJumping = true;
  		}
      //fonction d'envoie d'un mouvement
  		MovePlayer(horizontalMovoment);
      //flip x si besoin 
      flip(rb.velocity.x);

      //on prend la valeur absolu de x car si on ce déplace dans le sens inverse de l'axe (x ou y) ici il y aurras des valeurs négatives, 
      //donc pour cela nous devons prendre la valeur absolus  
  		float characterVelocity = Mathf.Abs(rb.velocity.x);
  		animator.SetFloat("Speed" , characterVelocity);
    }
    //calcule par rapport a la position de base du character si il doit flip (tourner) a gauche ou a droite
    void flip(float velocity){
      if(velocity > 0.1f){
        spriteRenderer.flipX = false;
      }
      else if( velocity < -0.1f){
        spriteRenderer.flipX = true;

      }
    }
    //crée un "SmoothDamp" qui est un déplacement "Smooth" pour permettre de faire avancer notre charcater
    void MovePlayer(float _horizontalMovement){
    	
    	Vector3 targetVelocity = new Vector2(_horizontalMovement,rb.velocity.y);
    	rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
    	if(isJumping == true){
    		rb.AddForce(new Vector2(0f , jumpForce));
    		isJumping = false;
        isGrounded = false;
    	}
    }
}
