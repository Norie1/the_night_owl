using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	
	public Animator animator;
	
	public Transform attackPoint;
	
	public float attackRange = 0.5f;
	
	public int attackDamage = 40;
	
	public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
			Attack();
        }
    }
    
    void Attack()
    {
		animator.SetTrigger("PlayerAttack");
		
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
		
		foreach(Collider2D enemy in hitEnemies)
		{
			enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
		}
	}
	
	void OnDrawGizmosSelected()
	{
		if (attackPoint == null) {
			return;
		}
			
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}
    
}
