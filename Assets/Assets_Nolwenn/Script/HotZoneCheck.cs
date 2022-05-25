using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    //Name attack animation "Enemy_Attack" so script can be used for multiple enemy

    private Enemy_behavior enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake() {
        enemyParent = GetComponentInParent<Enemy_behavior>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update(){
        if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack")) 
        {
            enemyParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.isInRange = false;
            enemyParent.SelectTarget();
        }
    }
}
