using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{
	public Transform player;
	private Transform boss;
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;
    public float attackRange = 1f;
    
    public Vector3 attackOffset;
    public LayerMask attackMask;
    
    void Start()
    {
		boss = GetComponent<Transform>();
	}
    
    void FixedUpdate()
    {
		
		if (Vector3.Distance(boss.position, player.position) < 0.3f)
		{
			Attack();
		}
	}
    
    public void Attack()
    {
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;
		
		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
		}
	}
	
	public void EnrageAttack()
    {
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;
		
		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerHealth>().TakeDamage(enragedAttackDamage);
		}
	}
}
