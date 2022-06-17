using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	
	public class Player_Combat_Boss_A : MonoBehaviour
	{
		
		public Animator animator;
		
		public Transform attackPointRight;
		public Transform attackPointLeft;

		public float attackRange = 1f;
		
		public int attackDamage = 40;
		
		public float attackRate = 2f;
		public float nextAttackTime = 0f;
		
		public LayerMask Boss;
		private Collider2D[] hitEnemies;
	
	    // Update is called once per frame
	    void Update()
	    {
			if (Time.time >= nextAttackTime)
			{
				if (Input.GetKeyDown(KeyCode.V))
				{
					PlayerAttack();
					nextAttackTime = Time.time + 1f / attackRate;
				}
			}
	    }
	    
	    public void PlayerAttack()
	    {
			animator.Play("Attack1");
			
			//GameObject player = GameObject.Find("Player");
            
            SpriteRenderer sprite = /*player.*/GetComponent<SpriteRenderer>();
			
			if(sprite.flipX == false){
				hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, Boss);
			}
			else {
				hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, Boss);
			}
			foreach(Collider2D enemy in hitEnemies)
			{
				Boss_Health bhj = enemy.GetComponent<Boss_Health>();
				
				if(bhj != null)
				{
					enemy.GetComponent<Boss_Health>().TakeDamage(attackDamage);

				}
			}
		}
		
	/*	public void OnDrawGizmosSelected()
		{
			if (attackPoint == null) {
				return;
			}
				
			Gizmos.DrawWireSphere(attackPoint.position, attackRange);
		}*/
	    
	}
