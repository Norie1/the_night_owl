	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	
	public class PlayerCombatMob : MonoBehaviour
	{
		
		public Animator animator;
		
		public Transform attackPointLeft;
		public Transform attackPointRight;
		
		public float attackRange = 1f;
		
		public int attackDamage = 40;
		
		public float attackRate = 2f;
		public float nextAttackTime = 0f;
		
		public LayerMask Enemies;
	
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
			Collider2D[] hitEnemies;
			if(sprite.flipX == false){
				hitEnemies = Physics2D.OverlapCircleAll(attackPointRight.position, attackRange, Enemies);
			}
			else {
				hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, Enemies);
			}
			foreach(Collider2D enemy in hitEnemies)
			{
				
				EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
				EnemyHealth_J ehj = enemy.GetComponent<EnemyHealth_J>();
				EnemyHealth_N ehn = enemy.GetComponent<EnemyHealth_N>();
				
				if((eh = enemy.GetComponent<EnemyHealth>()) != null)
				{
					eh.TakeDamage(attackDamage);
				}
				if(ehj != null)
				{
				enemy.GetComponent<EnemyHealth_J>().TakeDamage(attackDamage);
				}
				if(ehn != null)
				{
					ehn.TakeDamage(attackDamage);
				}
			}
		}
		
		public void OnDrawGizmosSelected()
		{
			if (attackPointRight == null) {
				return;
			}
				
			Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
		}
	    
	}
	
