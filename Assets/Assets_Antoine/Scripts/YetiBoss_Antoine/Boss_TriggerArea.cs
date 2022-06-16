using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_TriggerArea : MonoBehaviour
{
    private Boss_Behaviour enemyParent;

    private void Awake() {
        enemyParent = GetComponentInParent<Boss_Behaviour>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            enemyParent.target = other.transform; //Target the player
            enemyParent.isInRange = true; //Player is in range
            enemyParent.hotZone.SetActive(true); //Activate hotZone
            gameObject.SetActive(false); //Deactivate trigger area object
        }
    }
}
