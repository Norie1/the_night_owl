using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{
    public Transform attackArea;
    public float attackRadius;
    public LayerMask layerMask;
    public int damage;
    public static Boss_Attack instance;
    
	void Start()
	{
		instance = this;
	}

    
    public void CheckAttackPlayer()
    {
        if(Physics2D.OverlapCircle(attackArea.position, attackRadius,layerMask))
        {
            PlayerHealth.instance.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackArea.position, attackRadius);
    }

}
