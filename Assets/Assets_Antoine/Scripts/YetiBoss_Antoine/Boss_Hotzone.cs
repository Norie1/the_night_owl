using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Hotzone : MonoBehaviour
{
    //Name attack animation "Enemy_Attack" so script can be used for multiple enemy

    private Boss_Behaviour enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake() {
        enemyParent = GetComponentInParent<Boss_Behaviour>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update(){
        if(inRange /* && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack")*/) 
        {
            enemyParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player")
        {
            inRange = false; //Player not in range
            enemyParent.isInRange = false;
            enemyParent.StopAttack();
            enemyParent.triggerArea.SetActive(true); //Activate trigger Area
            gameObject.SetActive(false); //Deactivate hotZone
            enemyParent.SelectTarget(); //Select next target

        }
    }
}
