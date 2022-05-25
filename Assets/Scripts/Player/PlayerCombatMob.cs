	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	
	public class PlayerCombatMob : MonoBehaviour
	{
		
		public Animator animator;
		
		public Transform attackPoint;
		
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
			animator.SetTrigger("PlayerAttack");
			
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Enemies);
			
			foreach(Collider2D enemy in hitEnemies)
			{
				
				enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
				
			}
		}
		
		public void OnDrawGizmosSelected()
		{
			if (attackPoint == null) {
				return;
			}
				
			Gizmos.DrawWireSphere(attackPoint.position, attackRange);
		}
	    
	}
	
